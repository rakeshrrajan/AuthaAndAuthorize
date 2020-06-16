using Employee;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace AuthenticationDemo.Models
{
    public class EmployeeTokenBasedAuthentication : OAuthAuthorizationServerProvider
    {
        private readonly IIdentity<EmployeeModel> _employeIdentity;

        public EmployeeTokenBasedAuthentication()
        {
            _employeIdentity = new EmployeeIdentity();
        }

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            await Task.Run(() => context.Validated());
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            await Task.Run(() =>
            {
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                var userName = context.UserName;
                var password = context.Password;
                if (_employeIdentity.IsLogin(userName, password))
                {
                    var employee = _employeIdentity.GetIdentity(userName, password);
                    identity.AddClaim(new Claim(ClaimTypes.Role, employee.Role));
                    identity.AddClaim(new Claim(ClaimTypes.Name, employee.UserName));
                    context.Validated(identity);
                }
                else
                {
                    context.SetError("invalid_user", "Provided username and password is incorrect");
                    return;
                }
            });
        }
    }
}