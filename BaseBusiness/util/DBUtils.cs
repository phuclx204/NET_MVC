using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.ComponentModel.DataAnnotations.Schema; 
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace BaseBusiness.util
{
    public class DBUtils
    {
        // Property để lấy chuỗi kết nối (Đọc 1 lần duy nhất khi ứng dụng chạy)
        public static string ConnectionString { get; set; }
      
        /// <summary>
        /// Hàm dùng để Lấy danh sách (SELECT) và map vào List Model
        /// </summary>
        public static List<T> GetList<T>(string sql, SqlParameter[] parameters = null) where T : new()
        {
            List<T> list = new List<T>();

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    if (parameters != null) cmd.Parameters.AddRange(parameters);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        // Lấy thông tin Property một lần để tối ưu hiệu năng
                        PropertyInfo[] properties = typeof(T).GetProperties();

                        foreach (DataRow row in dt.Rows)
                        {
                            T item = new T();
                            foreach (PropertyInfo prop in properties)
                            {
                                // 1. Xác định tên cột trong DB dựa vào Attribute [Column("name")] hoặc tên Property
                                string dbColumnName = prop.Name;
                                var colAttr = prop.GetCustomAttribute<ColumnAttribute>();
                                if (colAttr != null) dbColumnName = colAttr.Name;

                                // 2. Map dữ liệu
                                if (dt.Columns.Contains(dbColumnName) && row[dbColumnName] != DBNull.Value)
                                {
                                    try
                                    {
                                        object dbValue = row[dbColumnName];

                                        Type t = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;

                                        object safeValue = Convert.ChangeType(dbValue, t);
                                        prop.SetValue(item, safeValue);
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine($"Lỗi map cột {dbColumnName}: {ex.Message}");
                                    }
                                }
                            }
                            list.Add(item);
                        }
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// Hàm lấy 1 đối tượng duy nhất (SELECT Single)
        /// </summary>
        public static T GetItem<T>(string sql, SqlParameter[] parameters = null) where T : new()
        {
            var list = GetList<T>(sql, parameters);
            return list.Count > 0 ? list[0] : default(T);
        }

        /// <summary>
        /// Hàm thực thi Insert, Update, Delete
        /// Trả về số dòng bị ảnh hưởng
        /// </summary>
        public static int ExecuteNonQuery(string sql, SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    if (parameters != null) cmd.Parameters.AddRange(parameters);
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Hàm thực thi lấy về 1 giá trị đơn (ví dụ: SELECT COUNT(*), hoặc SELECT SCOPE_IDENTITY())
        /// </summary>
        public static object ExecuteScalar(string sql, SqlParameter[] parameters = null)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    if (parameters != null) cmd.Parameters.AddRange(parameters);
                    return cmd.ExecuteScalar();
                }
            }
        }
    }
}