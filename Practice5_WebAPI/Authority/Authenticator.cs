using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Practice5_WebAPI.Authority
{
    public static class Authenticator
    {
        
        public static bool Authenticate(string clientId, string secret)
        {
            // find if any registered users have the credentials provided. 
            return AppRepository.applications.Any(x => x.ClientId == clientId && x.Secret == secret);
        }

        public static string CreateJwtToken(WebApp credential, DateTime expirationdate, string strSecretKey)
        {
            var claims = new List<Claim>
            {
                new Claim("Read", "true"),
                new Claim("Write", "true")
            };

            var secretKey = Encoding.ASCII.GetBytes(strSecretKey);
            var jwt = new JwtSecurityToken(
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(secretKey),
                    SecurityAlgorithms.HmacSha256Signature),
                    claims: claims,
                    expires: expirationdate,
                    notBefore: DateTime.UtcNow
                );

            return new JwtSecurityTokenHandler().WriteToken(jwt); 

        }
        public static bool VerifyToken(string token, string strSecretKey)
        {
            if (string.IsNullOrEmpty(token))
            {
                return false;
            }
            if (token.StartsWith("Bearer"))
            {
                token = token.Substring(6).Trim();
            }
            var secretKey = Encoding.ASCII.GetBytes(strSecretKey);
            SecurityToken securityToken;
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secretKey),
                    ValidateLifetime = true,
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ClockSkew = TimeSpan.Zero
                },
                out securityToken);
            }
            catch (SecurityTokenException)
            {
                return false;
            }
            catch
            {
                throw;
            }
            return securityToken != null;
        }
    }
}
