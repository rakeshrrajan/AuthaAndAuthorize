using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using AuthenticationDemo.Models;
using Employee;

namespace AuthenticationDemo.Controllers
{
    public class EmployeeController : ApiController
    {
        #region BasicAuthentication
        [EmployeeBasicAuthentication]
        [EmployeeBasicAutorizeAttribute(Roles = "role2")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        #endregion

        #region TokenBasedAuthentication
        [AllowAnonymous]
        [HttpGet]
        [Route("api/employee/time")]
        public IHttpActionResult GetServerTime()
        {
            return Ok("Now server time is: " + DateTime.Now.ToString());
        }

        [Authorize]
        [HttpGet]
        [Route("api/employee/authenticate")]
        public IHttpActionResult GetForAuthenticate()
        {
            var identity = (ClaimsIdentity)User.Identity;
            return Ok("Hello " + identity.Name);
        }

        [Authorize(Roles = "role1")]
        [HttpGet]
        [Route("api/employee/authorize")]
        public IHttpActionResult GetForAuthorize()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var roles = identity.Claims
                        .Where(c => c.Type == ClaimTypes.Role)
                        .Select(c => c.Value);
            return Ok("Hello " + identity.Name + " Role: " + string.Join(",", roles.ToList()));
        }
        #endregion
    }
}
