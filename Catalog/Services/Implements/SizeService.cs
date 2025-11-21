using BaseBusiness.Model;
using BaseBusiness.util;
using Catalog.Services.Interfaces;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace Modules.Catalog.Services
{
    public class SizeService : ISizeService
    {
        public List<SizeModel> GetAll()
        {
            // Dùng @ trước chuỗi để viết xuống dòng
            string sql = @"SELECT * FROM sizes 
                           WHERE status = 1 
                           ORDER BY created_at DESC";

            return DBUtils.GetList<SizeModel>(sql);
        }

        public SizeModel GetById(long id)
        {
            string sql = "SELECT * FROM sizes WHERE id = @Id";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Id", id)
            };

            return DBUtils.GetItem<SizeModel>(sql, parameters);
        }

        public bool Create(SizeModel size)
        {
            // Dùng @Name la sql parameter để tránh SQL Injection
            string sql = @"INSERT INTO sizes (name, status, created_at) 
                           VALUES (@Name, 1, GETDATE())";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Name", size.Name)
            };

            return DBUtils.ExecuteNonQuery(sql, parameters) > 0;
        }

        // 3. Cập nhật kích thước
        public bool Update(SizeModel size)
        {
            string sql = @"UPDATE sizes 
                           SET name = @Name, 
                               updated_at = GETDATE() 
                           WHERE id = @Id";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Id", size.Id),
                new SqlParameter("@Name", size.Name)
            };

            return DBUtils.ExecuteNonQuery(sql, parameters) > 0;
        }

        // 4. Xóa màu (Xóa mềm - chỉ đổi status)
        public bool Delete(long id)
        {
            string sql = @"UPDATE sizes SET status = 0 WHERE id = @Id";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Id", id)
            };

            return DBUtils.ExecuteNonQuery(sql, parameters) > 0;
        }
    }
}