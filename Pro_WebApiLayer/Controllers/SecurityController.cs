using Microsoft.IdentityModel.Tokens;
using Pro_DAL.Repo;
using Pro_EntityLayer;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Web.Http;

namespace Pro_WebApiLayer.Controllers
{
    public class SecurityController : ApiController
    {
        IUser UserService;
        public SecurityController()
        {
            UserService = new UserDataService();
        }

        [HttpPost]
        [Route("Login")]
        public IHttpActionResult Log(UserModel User)
        {
            string tok = "";
            try
            {
                var ExUser = UserService.GetUser(User);

                if (ExUser != null)
                {
                    tok = GenerateToken(ExUser.Role);
                    return Ok(tok);
                }
                else
                {
                    return BadRequest("record not found");
                }

            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("Register")]
        public IHttpActionResult Register(UserModel User)
        {
            try
            {
                UserService.AddUser(User);
                         
                return Ok(User);
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public string GenerateToken(string Role)
        {

            string Key = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var issuer = "http://localhost:50628/";

            var SecKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));

            var Credentials = new SigningCredentials(SecKey, SecurityAlgorithms.HmacSha256);

            var clms = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role,Role)

            };
            var token = new JwtSecurityToken(issuer, issuer,clms,
                expires: DateTime.Now.AddDays(2),
                signingCredentials: Credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
