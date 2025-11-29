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
        private readonly DBUtils DBUtils;
        public BrandService(DBUtils dBUtils)
        {
            DBUtils = dBUtils;
        }
        public async Task<bool> Create(BrandModel model)
        {
            string sql = @"INSERT INTO brands (name,country, logo, status)
                       VALUES (@Name, 1)";
            SqlParameter[] parameters = {
                new SqlParameter("@Name",model.Name),
                new SqlParameter("@Country",model.Name),
                new SqlParameter("@Logo",model.Name),
                new SqlParameter("@Status",model.Name),
            };

            return await DBUtils.ExecuteNonQueryAsync(sql, parameters) > 0;
        }

        public async Task<bool> Delete(long id)
        {
            string sql = @"UPDATE brands SET status = 0
                           WHERE id = @Id";
            SqlParameter[] pa = {
                new SqlParameter("@Id",id)
            };
            return await DBUtils.ExecuteNonQueryAsync(sql, pa) > 0;
        }

        public async Task<List<BrandModel>> GetAll()
        {
            string sql = @"SELECT * FROM brands WHERE status = 1  ORDER BY created_at DESC";
            return await DBUtils.GetListAsync<BrandModel>(sql);
        }

        public async Task<BrandModel> GetById(long id)
        {
            string sql = "SELECT * FROM brands WHERE id = @Id";
            SqlParameter[] pa = {
                new SqlParameter("@Id",id)
            };
            return await DBUtils.GetItemAsync<BrandModel>(sql, pa);
        }

        public async Task<bool> Update(BrandModel model)
        {
            string sql = "UPDATE brands SET name = @Name WHERE id = @Id";

            SqlParameter[] pa = {
                new SqlParameter("@Name",model.Name),
                new SqlParameter("@Id",model.Id)
            };
            return await DBUtils.ExecuteNonQueryAsync(sql, pa) > 0;
        }
    }
}