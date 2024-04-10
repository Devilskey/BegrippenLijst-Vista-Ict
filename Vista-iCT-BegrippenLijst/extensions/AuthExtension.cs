using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace extensions
{
    public static class AuthExtension
    {
        /// <summary>
        /// Extension that adds authentication token needed
        /// </summary>
        /// <param name="services"></param>
        public static void AddAuthTokenNeeded(this IServiceCollection services) 
        {
            //Setsup the token schemes and configures the token validationparameters. 
            //Uses envs or standaard parameters for the key, ValidIssuer and ValidAudience
            services.AddAuthentication(config =>
            {
                config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                config.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(config =>
            {
                config.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = Environment.GetEnvironmentVariable("ValidIssuer") ?? "http://localhost",
                    ValidAudience = Environment.GetEnvironmentVariable("ValidAudience") ?? "http://localhost",
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JwtKey") ?? "IAmAreallyGoodKeyArentIHopeSoAtLeast")),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true
                };
            });

        }
    }
}
