
using System.Text;
using DotNetEnv;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Socialix.Data;

namespace Socialix
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Load data from .env
            Env.Load();

            // Get jwt information
            var keyInAppsettings = builder.Configuration["JwtSettings:SecretKey"];
            var keyInEnv = Environment.GetEnvironmentVariable("SECRET_KEY");
            var secretKey = string.IsNullOrEmpty(keyInAppsettings) ? keyInEnv : keyInAppsettings;

            var issuerInAppsettings = builder.Configuration["JwtSettings:Issuer"];
            var issuerInEnv = Environment.GetEnvironmentVariable("ISSUER");
            var issuer = string.IsNullOrEmpty(issuerInAppsettings) ? issuerInEnv : issuerInAppsettings;

            var audienceInAppsettings = builder.Configuration["JwtSettings:Audience"];
            var audienceInEnv = Environment.GetEnvironmentVariable("AUDIENCE");
            var audience = string.IsNullOrEmpty(audienceInAppsettings) ? audienceInEnv : audienceInAppsettings;

            // Config jwt
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = issuer,
                        ValidAudience = audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                    };
                });

            // Get connection string 
            var applicationConnection = builder.Configuration.GetConnectionString("ApplicationDbConnection");
            var authConnection = builder.Configuration.GetConnectionString("AuthDbConnection");

            // Add services to the container.
            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(applicationConnection));
            builder.Services.AddDbContext<AuthDbContext>(options => options.UseSqlServer(authConnection));

            // Config Identity
            builder.Services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AuthDbContext>()
                .AddDefaultTokenProviders();


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
