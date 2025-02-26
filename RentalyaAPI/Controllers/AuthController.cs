using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using RentalyaAPI.Models;
using RentalyaAPI.Models.DTOs;
using RentalyaAPI.Configurations;
using RentalyaAPI.Data;
using MongoDB.Driver;
using BC = BCrypt.Net.BCrypt;
using RentalyaAPI.Services;

namespace RentalyaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly JwtConfig _jwtConfig;
        private readonly IAuthService _authService;

        public AuthController(
            ApplicationDbContext context,
            IOptions<JwtConfig> jwtConfig,
            IAuthService authService)
        {
            _context = context;
            _jwtConfig = jwtConfig.Value;
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            try
            {
                var user = await _context.Users
                    .Find(u => u.Email == model.Email)
                    .FirstOrDefaultAsync();

                if (user == null)
                {
                    return BadRequest(new AuthResponseDto
                    {
                        IsSuccess = false,
                        Message = "Kullanıcı bulunamadı"
                    });
                }

                if (model.IsAdmin && !user.IsAdmin)
                {
                    return BadRequest(new AuthResponseDto
                    {
                        IsSuccess = false,
                        Message = "Admin yetkisine sahip değilsiniz"
                    });
                }

                if (!BC.Verify(model.Password, user.PasswordHash))
                {
                    return BadRequest(new AuthResponseDto
                    {
                        IsSuccess = false,
                        Message = "Geçersiz şifre"
                    });
                }

                var token = GenerateJwtToken(user);

                return Ok(new AuthResponseDto
                {
                    IsSuccess = true,
                    Token = token,
                    Message = "Giriş başarılı",
                    Expiration = DateTime.UtcNow.AddMinutes(_jwtConfig.ExpiryInMinutes)
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new AuthResponseDto
                {
                    IsSuccess = false,
                    Message = "Bir hata oluştu: " + ex.Message
                });
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = await _authService.RegisterUser(model);
                return Ok(new { message = "Kullanıcı başarıyla kaydedildi" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        private string GenerateJwtToken(ApplicationUser user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", user.Id),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                    new Claim("IsAdmin", user.IsAdmin.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(_jwtConfig.ExpiryInMinutes),
                Issuer = _jwtConfig.Issuer,
                Audience = _jwtConfig.Audience,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            return jwtTokenHandler.WriteToken(token);
        }
    }
}