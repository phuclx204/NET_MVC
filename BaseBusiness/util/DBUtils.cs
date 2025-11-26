using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Data.SqlClient;

namespace BaseBusiness.util
{
    public class DBUtils
    {
        private readonly string _connectionString;

        // Transaction Context
        private SqlConnection _txConnection;
        private SqlTransaction _transaction;

        public DBUtils(string connectionString)
        {
            _connectionString = connectionString;
        }

        //                           TRANSACTION SUPPORT
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
            _transaction;

        //                            PROPERTY MAP CACHE
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

        //                   AUTO-CONVERT camelCase <-> snake_case
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

        //                          VALUE CONVERTER
        private static object ConvertValue(object value, Type targetType)
        {
            // Case: NULL from DB
            if (value == null || value == DBNull.Value)
            {
                // Nullable<T> → return null
                if (Nullable.GetUnderlyingType(targetType) != null)
                    return null;

                // Value type (int, long, DateTime, byte, bool, etc.)
                return Activator.CreateInstance(targetType);
            }

            // Guid
            if (targetType == typeof(Guid) || targetType == typeof(Guid?))
                return Guid.Parse(value.ToString());

            // Enum
            if (targetType.IsEnum)
                return Enum.Parse(targetType, value.ToString());

            // byte[]
            if (targetType == typeof(byte[]))
                return (byte[])value;

            // Nullable<T> → unwrap to underlying type
            var underlying = Nullable.GetUnderlyingType(targetType);
            if (underlying != null)
                return Convert.ChangeType(value, underlying);

            return Convert.ChangeType(value, targetType);
        }


        //               GET LIST (async)
        public async Task<List<T>> GetListAsync<T>(string sql, SqlParameter[] parameters = null)
        {
            var targetType = typeof(T);

            if (targetType.IsPrimitive || targetType == typeof(string) || targetType == typeof(decimal))
            {
                var primitiveList = new List<T>();
                using (var conn = GetConnection())
                {
                    if (_txConnection == null) await conn.OpenAsync();
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Transaction = GetTransaction();
                        if (parameters != null) cmd.Parameters.AddRange(parameters);
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                primitiveList.Add((T)ConvertValue(reader.GetValue(0), targetType));
                            }
                        }
                    }
                }
                return primitiveList;
            }

            // Xử lý trường hợp Class Model (Sử dụng Reflection Mapping)
            var result = new List<T>();
            var maps = GetPropertyMaps(targetType);

            using (var conn = GetConnection())
            {
                if (_txConnection == null) await conn.OpenAsync();

                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Transaction = GetTransaction();
                    if (parameters != null) cmd.Parameters.AddRange(parameters);

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            // Yêu cầu T phải có Constructor mặc định
                            var item = Activator.CreateInstance<T>();

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

        //               GET SINGLE ITEM (async)
        public async Task<T> GetItemAsync<T>(string sql, SqlParameter[] parameters = null)
        {
            // Tận dụng GetListAsync
            var list = await GetListAsync<T>(sql, parameters);
            return list.Count > 0 ? list[0] : default;
        }

        //               ExecuteNonQuery (async)
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
