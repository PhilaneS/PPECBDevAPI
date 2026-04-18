using Infrastructure.configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("JwtSettings").Get<JwtSettings>();
            var secretKey = Encoding.UTF8.GetBytes(jwtSettings?.Key ?? string.Empty);
            var issuer = jwtSettings?.Issuer;
            var audience = jwtSettings?.Audience;
            var expiryMinutes = jwtSettings?.ExpiryMinutes;
            var subject = jwtSettings?.Subject; 
           
            services.AddAuthentication(options =>
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
                    IssuerSigningKey = new SymmetricSecurityKey(secretKey),

                    ClockSkew = TimeSpan.Zero

                };
                options.Events = new JwtBearerEvents 
                {
                    OnAuthenticationFailed = context => {
                        Console.WriteLine("JWT ERROR: " + context.Exception.Message);
                        return Task.CompletedTask;
                    }
                };
            });
            return services;
        }
        public static IServiceCollection AddCustomCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });
            return services;
        }
    }
}
