using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BaseBusiness.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BaseBusiness.util
{
    public class JwtUtils
    {
        public string GenerateJwtToken(UserModel user, List<string> roles, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");
            var key = Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]);

            // 1. Tạo danh sách các thông tin (Claims) chứa trong Token
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Name)
            };

            // 2. Lặp qua list roles lấy từ DB và add vào claims
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            // 3. Cấu hình chữ ký và thời gian hết hạn
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(60),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = jwtSettings["Issuer"],
                Audience = jwtSettings["Audience"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
