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
        public Task<string> Login(LoginDto loginDto)
        {
            string sql = "SELECT * FROM users WHERE email = @Email";


        }
    }
}