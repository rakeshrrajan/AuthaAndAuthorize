using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Employee;

namespace AuthenticationDemo.Models
{
    public class EmployeeBasicAuthentication : AuthorizationFilterAttribute
    {
        private readonly IIdentity<EmployeeModel> _employeIdentity;

        public EmployeeBasicAuthentication()
        {
            _employeIdentity = new EmployeeIdentity();
        }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var auth = actionContext.Request.Headers.Authorization;

            if(auth == null)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
            else
            {
                var token = actionContext.Request.Headers.Authorization.Parameter;
                var decodedToken = Encoding.UTF8.GetString(Convert.FromBase64String(token));
                var credentials = decodedToken.Split(':');
                var userName = credentials[0];
                var password = credentials[1];
                if (_employeIdentity.IsLogin(userName, password))
                {
                    var employee = _employeIdentity.GetIdentity(userName, password);
                    var principal = new GenericPrincipal(new GenericIdentity(employee.UserName), employee.Role.Split(','));
                    Thread.CurrentPrincipal = principal;
                    if (HttpContext.Current != null)
                    {
                        HttpContext.Current.User = principal;
                    }
                }
                else
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                }
            }
        }
    }
}