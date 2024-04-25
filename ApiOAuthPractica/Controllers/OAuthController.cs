using ApiOAuthPractica.Helpers;
using ApiOAuthPractica.Models;
using ApiOAuthPractica.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ApiOAuthPractica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OAuthController : ControllerBase
    {
        private RepositoryDoctores repo;
        private HelperOAuth helper;

        public OAuthController(RepositoryDoctores repo, HelperOAuth helper)
        {
            this.repo = repo;
            this.helper = helper;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult> Login(LoginModel model)
        {
            Doctor doc = await this.repo.LogInDoctorAsync(model.Username, int.Parse(model.Password));
            if(doc == null)
            {
                return Unauthorized();
            }
            else
            {
                SigningCredentials credentials = new SigningCredentials
                    (this.helper.GetKeyToken(), SecurityAlgorithms.HmacSha256);
                string jsonDoc = JsonConvert.SerializeObject(doc);
                Claim[] informacion = new[]
                {
                    new Claim("UserData", jsonDoc)
                };
                JwtSecurityToken token = new JwtSecurityToken(
                        claims: informacion,
                        issuer: this.helper.Issuer,
                        audience: this.helper.Audience,
                        signingCredentials: credentials,
                        expires: DateTime.UtcNow.AddMinutes(30),
                        notBefore: DateTime.UtcNow
                    );
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token)
                });
            }
        }
    }
}
