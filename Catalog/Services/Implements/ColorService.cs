using BaseBusiness.Model;
using BaseBusiness.util;
using Catalog.Services.Interfaces;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace Modules.Catalog.Services
{
    public class ColorService : IColorService
    {
        public List<ColorModel> GetAll()
        {
            // Dùng @ trước chuỗi để viết xuống dòng
            string sql = @"SELECT * FROM colors 
                           WHERE status = 1 
                           ORDER BY created_at DESC";

            return DBUtils.GetList<ColorModel>(sql);
        }

        public ColorModel GetById(long id)
        {
            string sql = "SELECT * FROM colors WHERE id = @Id";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Id", id)
            };

            return DBUtils.GetItem<ColorModel>(sql, parameters);
        }

        public bool Create(ColorModel color)
        {
            // Dùng @Name(sql parameter) để tránh SQL Injection
            string sql = @"INSERT INTO colors (name, code, status, created_at) 
                           VALUES (@Name, @Code, 1, GETDATE())";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Name", color.Name),
                new SqlParameter("@Code", color.Code)
            };

            return DBUtils.ExecuteNonQuery(sql, parameters) > 0;
        }

        // 3. Cập nhật màu
        public bool Update(ColorModel color)
        {
            string sql = @"UPDATE colors 
                           SET name = @Name, 
                               code = @Code, 
                               updated_at = GETDATE() 
                           WHERE id = @Id";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Id", color.Id),
                new SqlParameter("@Name", color.Name),
                new SqlParameter("@Code", color.Code)
            };

            return DBUtils.ExecuteNonQuery(sql, parameters) > 0;
        }

        // 4. Xóa màu (Xóa mềm - chỉ đổi status)
        public bool Delete(long id)
        {
            string sql = @"UPDATE colors SET status = 0 WHERE id = @Id";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Id", id)
            };

            return DBUtils.ExecuteNonQuery(sql, parameters) > 0;
        }
    }
}