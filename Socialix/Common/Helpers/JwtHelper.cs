using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DotNetEnv;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Socialix.Entities;

namespace Socialix.Common.Helpers
{
    public class JwtHelper
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<IdentityUser> _userManager;

        public JwtHelper(IConfiguration configuration, UserManager<IdentityUser> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }

        public async Task<string> GenerateJwtToken(string userName)
        {
            var identityUser = await _userManager.FindByNameAsync(userName);
            if (identityUser == null) return "";

            var role = (await _userManager.GetRolesAsync(identityUser))?.FirstOrDefault();

            Env.Load();

            var issuer = _configuration["JwtSettings:Issuer"] ?? Environment.GetEnvironmentVariable("ISSUER");
            var secretKey = _configuration["JwtSettings:SecretKey"] ?? Environment.GetEnvironmentVariable("SECRET_KEEY");
            double.TryParse(_configuration["JwtSettings:ExprireMinutes"] ?? Environment.GetEnvironmentVariable("EXPRIRE_MINUTES"), out var expireMinutes);

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
{
                new Claim("Name", userName),
                new Claim("Role", role ?? "No role"),
                new Claim("Expiration", expireMinutes.ToString())
            };

            var token = new JwtSecurityToken
            (
                issuer: issuer,
                claims: claims,
                expires: DateTime.Now.AddMinutes(expireMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
