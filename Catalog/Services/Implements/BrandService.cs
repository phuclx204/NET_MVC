using BaseBusiness.Model;
using BaseBusiness.util;
using Catalog.Services.Interfaces;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace Modules.Catalog.Services
{
    public class BrandService : IBrandService
    {
        public bool Create(BrandModel model)
        {
            string sql = @"INSERT INTO brands (name, status)
                       VALUES (@Name, 1)";
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@Name",model.Name)
            };

            return DBUtils.ExecuteNonQuery(sql, parameters) > 0;
        }

        public bool Delete(long id)
        {
            string sql = @"UPDATE brands SET status = 0
                           WHERE id = @Id";
            SqlParameter[] pa = new SqlParameter[] {
                new SqlParameter("@Id",id)
            };
            return DBUtils.ExecuteNonQuery(sql, pa) > 0;
        }

        public List<BrandModel> GetAll()
        {
            string sql = @"SELECT * FROM brands WHERE status = 1";
            return DBUtils.GetList<BrandModel>(sql);
        }

        public BrandModel GetById(long id)
        {
            string sql = "SELECT * FROM brands WHERE id = @Id";
            SqlParameter[] pa = new SqlParameter[] {
                new SqlParameter("@Id",id)
            };
            return DBUtils.GetItem<BrandModel>(sql, pa);
        }

        public bool Update(BrandModel model)
        {
            string sql = "UPDATE brands SET name = @Name WHERE id = @Id";

            SqlParameter[] pa = new SqlParameter[] {
                new SqlParameter("@Name",model.Name),
                new SqlParameter("@Id",model.Id)
            };
            return DBUtils.ExecuteNonQuery(sql, pa) > 0;
        }
    }
}