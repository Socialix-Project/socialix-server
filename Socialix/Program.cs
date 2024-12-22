
using System.Text;
using DotNetEnv;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Socialix.Common.Repository;
using Socialix.Data;
using Socialix.Middlewares;
using Socialix.Repositories.Implementations;
using Socialix.Repositories.Interfaces;

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

            builder.Services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
            builder.Services.AddScoped<IAuthRepository, AuthRepository>();

            // Config Identity
            builder.Services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AuthDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyHeader();
                    policy.AllowAnyMethod();
                    policy.AllowAnyOrigin();
                });
            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Socialix API", Version = "v1" });

                // Config jwt in swagger docs
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter token and then your valid token."
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("AllowAll");

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseJwtMiddleware();  // Config custom jwt middleware
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
