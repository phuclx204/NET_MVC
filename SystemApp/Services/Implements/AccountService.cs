using BaseBusiness.Model;
using BaseBusiness.util;
using SystemApp.Services.Interfaces;
using Microsoft.Data.SqlClient;
using SystemApp.DTOs;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Modules.SystemApp.Services
{
    public class AccountService : IAccountService
    {
        private readonly IConfiguration _configuration;
        private readonly DBUtils _dBUtils;

        public AccountService(IConfiguration configuration, DBUtils dBUtils)
        {
            _configuration = configuration;
            _dBUtils = dBUtils;
        }
        public async Task<string> Login(LoginDto loginDto)
        {
            string sql = @"SELECT * FROM users WHERE email = @Email";

            SqlParameter[] parameters =
                {
                    new SqlParameter("@Email", loginDto.Email)
                };

            UserModel user = await _dBUtils.GetItemAsync<UserModel>(sql, parameters);

            if (user == null || user.Status != 1) return null;

            bool isPasswordValid = PasswordHelper.VerifyPassword(loginDto.Password, user.Password);
            if (!isPasswordValid) return null;

            string sqlRole = @"SELECT r.name FROM roles r JOIN
                               role_user ru ON r.id = ru.role_id
                               WHERE ru.user_id = @UserId";

            SqlParameter[] roleParams =
                {
                    new SqlParameter("@UserId", user.Id)
                };

            List<RoleDto> roleDtos = await _dBUtils.GetListAsync<RoleDto>(sqlRole, roleParams);
            List<string> roles = roleDtos.Select(r => r.Name).ToList();
            JwtUtils jwtUtils = new JwtUtils();
            return jwtUtils.GenerateJwtToken(user, roles, _configuration);
        }

        public async Task<bool> Register(RegisterDto dto)
        {
            string checkSql = "SELECT * FROM users WHERE email = @Email";
            SqlParameter[] checkParams = {
                new SqlParameter("@Email", dto.Email)
            };

            var existingUser = await _dBUtils.GetItemAsync<UserModel>(checkSql, checkParams);
            if (existingUser != null)
                return false;

            string passwordHash = PasswordHelper.HashPassword(dto.Password);

            string sql = @"INSERT INTO users (email, password, name)
                            VALUES (@Email, @Password, @Name) ";

            SqlParameter[] insertParams = {
                new SqlParameter("@Email", dto.Email),
                new SqlParameter("@Password", passwordHash),
                new SqlParameter("@Name", dto.Name)
            };

            await _dBUtils.ExecuteNonQueryAsync(sql, insertParams);
            return true;
        }

    }
}