using BaseBusiness.Model;
using BaseBusiness.util;
using SystemApp.Services.Interfaces;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using SystemApp.DTOs;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Modules.SystemApp.Services
{
    public class AccountService : IAccountService
    {
        private readonly IConfiguration _configuration;

        public AccountService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Login(LoginDto loginDto)
        {
            string sql = @"SELECT id, name, email, password, status 
                           FROM users WHERE email = @Email";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Email", loginDto.Email)
            };

            UserModel user = DBUtils.GetItem<UserModel>(sql, parameters);
            if (user == null) return null;
            if (user.Status != 1) return null;

            bool isPasswordValid = PasswordHelper.VerifyPassword(loginDto.Password, user.Password);
            if (!isPasswordValid) return null;

            string sqlRole = @"SELECT r.name FROM roles r JOIN
                               role_user ru ON r.id = ru.role_id
                               WHERE ru.user_id = @UserId";

            SqlParameter[] roleParams = new SqlParameter[]
            {
                new SqlParameter("@UserId", user.Id)
            };

            return "";
        }
    }
}