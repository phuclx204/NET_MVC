using BaseBusiness.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemApp.DTOs;
using SystemApp.Services.Interfaces;

namespace SystemApp.Controllers
{
    [AllowAnonymous]
    [Route("auth")]
    public class AuthController : Controller
    {
        private readonly IAccountService _accountService;

        public AuthController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        [HttpGet("login")]
        public IActionResult LoginForm()
        {
            return View("Login");
        }

        [HttpGet("register")]
        public IActionResult RegisterForm()
        {
            return View("Register");
        }



        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {

            var token = await _accountService.Login(loginDto);
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized(new { message = "Đăng nhập thất bại. Vui lòng kiểm tra lại email và mật khẩu." });
            }
            Response.Cookies.Append("jwtToken", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = false,
                SameSite = SameSiteMode.Lax,
                Expires = DateTimeOffset.UtcNow.AddMinutes(10)
            });
            return Ok(new { messsage = "Đăng nhập thành công" });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            bool success = await _accountService.Register(dto);

            if (!success)
                return BadRequest("Username already exists");

            return Ok("Register successful");
        }


        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Append("jwtToken", "", new CookieOptions
            {
                Expires = DateTimeOffset.UtcNow.AddDays(-1),
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict
            });

            return Ok(new { message = "Đăng xuất thành công." });
        }

    }
}
