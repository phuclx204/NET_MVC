using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Data.SqlClient;

namespace BaseBusiness.util
{
    public class DBUtilsPro
    {
        private readonly string _connectionString;

        // Transaction Context
        private SqlConnection _txConnection;
        private SqlTransaction _transaction;

        public DBUtilsPro(string connectionString)
        {
            _connectionString = connectionString;
        }

        // ======================================================================
        //                           TRANSACTION SUPPORT
        // ======================================================================

        public async Task BeginTransactionAsync()
        {
            if (_txConnection != null)
                throw new Exception("Transaction has already started.");

            _txConnection = new SqlConnection(_connectionString);
            await _txConnection.OpenAsync();

            _transaction = _txConnection.BeginTransaction();
        }

        public void Commit()
        {
            if (_transaction == null)
                throw new Exception("No active transaction.");

            _transaction.Commit();
            _txConnection.Close();

            _transaction = null;
            _txConnection = null;
        }

        public void Rollback()
        {
            if (_transaction == null)
                throw new Exception("No active transaction.");

            _transaction.Rollback();
            _txConnection.Close();

            _transaction = null;
            _txConnection = null;
        }

        private SqlConnection GetConnection() =>
            _txConnection ?? new SqlConnection(_connectionString);

        private SqlTransaction GetTransaction() =>
            _transaction; // may be null → normal mode

        // ======================================================================
        //                            PROPERTY MAP CACHE
        // ======================================================================

        private static readonly Dictionary<Type, List<PropertyMap>> _mapCache
            = new Dictionary<Type, List<PropertyMap>>();

        private static List<PropertyMap> GetPropertyMaps(Type type)
        {
            if (_mapCache.TryGetValue(type, out var cached))
                return cached;

            var list = new List<PropertyMap>();

            foreach (var prop in type.GetProperties())
            {
                // 1. Ưu tiên ColumnAttribute
                string dbColumn = prop.Name;

                var colAttr = prop.GetCustomAttribute<ColumnAttribute>();
                if (colAttr != null)
                {
                    dbColumn = colAttr.Name;
                }
                else
                {
                    // 2. Nếu không có ColumnAttribute → auto snake_case
                    dbColumn = ToSnakeCase(prop.Name);
                }

                list.Add(new PropertyMap
                {
                    Property = prop,
                    ColumnName = dbColumn,
                    Type = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType
                });
            }

            _mapCache[type] = list;
            return list;
        }

        private class PropertyMap
        {
            public PropertyInfo Property { get; set; }
            public string ColumnName { get; set; }
            public Type Type { get; set; }
        }

        // ======================================================================
        //                   AUTO-CONVERT camelCase <-> snake_case
        // ======================================================================

        public static string ToSnakeCase(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return input;

            var result = "";
            foreach (var c in input)
            {
                if (char.IsUpper(c))
                {
                    if (result.Length > 0) result += "_";
                    result += char.ToLower(c);
                }
                else
                {
                    result += c;
                }
            }
            return result;
        }

        // ======================================================================
        //                          VALUE CONVERTER
        // ======================================================================

        private static object ConvertValue(object value, Type targetType)
        {
            if (value == null || value == DBNull.Value)
                return null;

            // Guid
            if (targetType == typeof(Guid))
                return Guid.Parse(value.ToString());

            // Enum
            if (targetType.IsEnum)
                return Enum.Parse(targetType, value.ToString());

            // byte[]
            if (targetType == typeof(byte[]))
                return (byte[])value;

            return Convert.ChangeType(value, targetType);
        }

        // ======================================================================
        //                        GET LIST (async + fast)
        // ======================================================================

        public async Task<List<T>> GetListAsync<T>(string sql, SqlParameter[] parameters = null) where T : new()
        {
            var result = new List<T>();
            var maps = GetPropertyMaps(typeof(T));

            using (var conn = GetConnection())
            {
                if (_txConnection == null)
                    await conn.OpenAsync(); // auto open when not in transaction

                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Transaction = GetTransaction();
                    if (parameters != null) cmd.Parameters.AddRange(parameters);

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var item = new T();

                            foreach (var map in maps)
                            {
                                if (!reader.HasColumn(map.ColumnName)) continue;

                                object dbValue = reader[map.ColumnName];
                                if (dbValue == DBNull.Value) continue;

                                var safeValue = ConvertValue(dbValue, map.Type);
                                map.Property.SetValue(item, safeValue);
                            }

                            result.Add(item);
                        }
                    }
                }
            }

            return result;
        }

        // ======================================================================
        //                        GET SINGLE ITEM
        // ======================================================================

        public async Task<T> GetItemAsync<T>(string sql, SqlParameter[] parameters = null) where T : new()
        {
            var list = await GetListAsync<T>(sql, parameters);
            return list.Count > 0 ? list[0] : default;
        }

        // ======================================================================
        //                       ExecuteNonQuery
        // ======================================================================

        public async Task<int> ExecuteNonQueryAsync(string sql, SqlParameter[] parameters = null)
        {
            using (var conn = GetConnection())
            {
                if (_txConnection == null)
                    await conn.OpenAsync();

                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Transaction = GetTransaction();
                    if (parameters != null) cmd.Parameters.AddRange(parameters);

                    return await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        //                       ExecuteScalar

        public async Task<object> ExecuteScalarAsync(string sql, SqlParameter[] parameters = null)
        {
            using (var conn = GetConnection())
            {
                if (_txConnection == null)
                    await conn.OpenAsync();

                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Transaction = GetTransaction();
                    if (parameters != null) cmd.Parameters.AddRange(parameters);

                    return await cmd.ExecuteScalarAsync();
                }
            }
        }
    }

    // Extension hỗ trợ kiểm tra xem DataReader có cột hay không
    public static class DataReaderExtensions
    {
        public static bool HasColumn(this IDataRecord reader, string columnName)
        {
            for (int i = 0; i < reader.FieldCount; i++)
                if (reader.GetName(i).Equals(columnName, StringComparison.OrdinalIgnoreCase))
                    return true;

            return false;
        }
    }
}
