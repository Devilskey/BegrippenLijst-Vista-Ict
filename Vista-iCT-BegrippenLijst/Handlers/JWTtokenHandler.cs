using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Handlers
{
    public static class JWTtokenHandler
    {
        public static string TokenSecret = Environment.GetEnvironmentVariable("JwtKey")! ?? "IAmAreallyGoodKeyArentIHopeSoAtLeast";

        public static String GenerateToken()
        {
            JwtSecurityTokenHandler jwtTokenHandler = new JwtSecurityTokenHandler();
            RandomNumberGenerator random = RandomNumberGenerator.Create();

            byte[] key = Encoding.UTF8.GetBytes(TokenSecret);

            var claims = new List<Claim>
            {
                new("TimeCreated", DateTime.Now.ToString())
            };

            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(key);
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddHours(8),
                Issuer = Environment.GetEnvironmentVariable("ValidIssuer")! ?? "http://localhost",
                Audience = Environment.GetEnvironmentVariable("ValidAudience")! ?? "http://localhost",
                SigningCredentials = credentials
            };

            SecurityToken token = jwtTokenHandler.CreateToken(tokenDescriptor);

            return jwtTokenHandler.WriteToken(token);
        }
    }
}
