using javax.crypto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Practice5_WebAPI.Authority;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Practice5_WebAPI.Controllers
{

    public class AuthorityController: ControllerBase
    {
        private readonly IConfiguration _configuration; 
        public AuthorityController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        [HttpPost("auth")]
        public IActionResult Authenticate([FromBody] WebApp credential)
        {
            if (Authenticator.Authenticate(credential.ClientId, credential.Secret))
            {
                var expirationdate = DateTime.UtcNow.AddDays(1);
                return Ok(new
                {
                    access_token = Authenticator.CreateJwtToken(credential, expirationdate,_configuration.GetValue<string>("SecretKey")),
                    expires_at = expirationdate
                });
            }
            else
                return BadRequest(new { message = "Username or password is incorrect" });
        }
    }
}
