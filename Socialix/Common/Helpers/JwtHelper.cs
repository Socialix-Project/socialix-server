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

            var keyInAppsettings = _configuration["JwtSettings:SecretKey"];
            var keyInEnv = Environment.GetEnvironmentVariable("SECRET_KEY");
            var secretKey = string.IsNullOrEmpty(keyInAppsettings) ? keyInEnv : keyInAppsettings;

            var issuerInAppsettings = _configuration["JwtSettings:Issuer"];
            var issuerInEnv = Environment.GetEnvironmentVariable("ISSUER");
            var issuer = string.IsNullOrEmpty(issuerInAppsettings) ? issuerInEnv : issuerInAppsettings;

            var audienceInAppsettings = _configuration["JwtSettings:Audience"];
            var audienceInEnv = Environment.GetEnvironmentVariable("AUDIENCE");
            var audience = string.IsNullOrEmpty(audienceInAppsettings) ? audienceInEnv : audienceInAppsettings;

            var expireMinutesInAppsettings = _configuration["JwtSettings:ExprireMinutes"];
            var expireMinutesEnv = Environment.GetEnvironmentVariable("EXPRIRE_MINUTES");
            var expire = string.IsNullOrEmpty(expireMinutesInAppsettings) ? double.Parse(expireMinutesEnv) : double.Parse(expireMinutesInAppsettings);

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
{
                new Claim("Name", userName),
                new Claim("Role", role ?? "No role"),
                new Claim("Expiration", expire.ToString())
            };

            var token = new JwtSecurityToken
            (
                issuer: issuer,
                claims: claims,
                expires: DateTime.Now.AddMinutes(expire),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
