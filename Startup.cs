﻿using System;
using System.Threading.Tasks;
using System.Web.Http;
using AuthenticationDemo.Models;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;

[assembly: OwinStartup(typeof(AuthenticationDemo.Startup))]

namespace AuthenticationDemo
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {           
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
            //enable cors origin requests
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            var employeeTokenProvider = new EmployeeTokenBasedAuthentication();
            var options = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = employeeTokenProvider
            };
            app.UseOAuthAuthorizationServer(options);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());


            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);
        }
    }
}
