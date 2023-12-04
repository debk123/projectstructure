using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin;
using Microsoft.Owin.Security.Jwt;
using Owin;
using System;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web.Http;

[assembly: OwinStartup(typeof(Pro_WebApiLayer.Startup))]

namespace Pro_WebApiLayer
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
            HttpConfiguration config = new HttpConfiguration();
            config.Formatters.Clear();
            config.Formatters.Add(new System.Net.Http.Formatting.JsonMediaTypeFormatter());

            WebApiConfig.Register(config); // bootstrap your existing WebApi config 

            // configure jwt token authentication

            app.UseJwtBearerAuthentication(new JwtBearerAuthenticationOptions
            {
                //AuthenticationMode = AuthenticationMode.

                TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true, // I guess you don't even have to sign the token
                    ValidIssuer = "http://localhost:50628/",
                    ValidAudience = "http://localhost:50628/",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ABCDEFGHIJKLMNOPQRSTUVWXYZ"))
                }
            });
            app.UseWebApi(config); // instruct OWIN to take over

        }
    }
}
