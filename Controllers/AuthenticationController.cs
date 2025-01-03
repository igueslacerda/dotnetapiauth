using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace apiauth.Controllers
{
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        [HttpPost("/login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            if (request.Username == "igues" && request.Password == "12345") // Simples validação
            {
                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, request.Username)
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MinhaChaveSuperSecreta2024mais32"));
                //                                                         123456789*123456789*123456789*123456789*
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: "iguesauth.com",
                    audience: "iguesauth.com",
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(3),
                    signingCredentials: creds);

                var finalToken = new JwtSecurityTokenHandler().WriteToken(token);

                return Ok(new { token = finalToken });
            }
            return Unauthorized();
        }

        [Authorize]
        [HttpGet("/protected")]
        public IActionResult TestAccess()
        {
            return Ok("Este endpoint é protegido, se acessou é porque a autenticação foi bem sucedida!");
        }
    }

    public record LoginRequest(string Username, string Password);
}
