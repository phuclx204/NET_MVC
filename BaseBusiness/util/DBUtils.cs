using System.Collections.Generic;
using System.Data;
using System.Reflection;
using Microsoft.Data.SqlClient;

namespace BaseBusiness.util
{
    public class DBUtils
    {
        private static string ConnectionString = "Server=.;Database=SaleInventoryDB;User=sa;Password=123456;Trusted_Connection=True;Encrypt=False;";

        public static DataTable GetList(string sql)
        {

            // Tạo bảng để chứa dữ liệu
            DataTable dt = new DataTable();

            // Tạo đường nối đến cơ sở dữ liệu
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                // tạo query để gửi đi
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    // Adapter lấy dữ liệu từ database đổ vào bảng dt
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt); // đổ dữ liệu vào bảng dt
                    }
                }
            }
            return dt;
        }

        public static List<T> GetList<T>(string sql, SqlParameter[] parameters = null) where T : new()
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    // Nếu có tham số truyền vào thì nạp vào Command
                    if (parameters != null && parameters.Length > 0)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }
            }

            // 2. Chuyển đổi (Map) từ DataTable sang List<T> bằng Reflection
            List<T> list = new List<T>();

            // Lấy danh sách các thuộc tính (cột) của class Model
            PropertyInfo[] properties = typeof(T).GetProperties();

            foreach (DataRow row in dt.Rows)
            {
                T item = new T();
                foreach (PropertyInfo prop in properties)
                {
                    // Kiểm tra: Nếu trong bảng DB có cột trùng tên với Property VÀ giá trị không rỗng
                    if (dt.Columns.Contains(prop.Name) && row[prop.Name] != DBNull.Value)
                    {
                        try
                        {
                            // Chuyển đổi giá trị từ DB sang kiểu dữ liệu của Property (ví dụ: int, string, decimal)
                            object value = row[prop.Name];
                            prop.SetValue(item, Convert.ChangeType(value, prop.PropertyType));
                        }
                        catch
                        {
                            // Nếu lỗi chuyển đổi kiểu (ví dụ DB là null mà Model không cho null) thì bỏ qua
                            // Trong thực tế nên log lỗi này lại
                        }
                    }
                }
                list.Add(item);
            }

            return list;
        }
    }
}