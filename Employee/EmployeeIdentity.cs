using System;
using System.Collections.Generic;
using System.Linq;

namespace Employee
{
    public class EmployeeIdentity : IIdentity<EmployeeModel>
    {
        private readonly IList<EmployeeModel> _employeeModel;

        public EmployeeIdentity()
        {
            _employeeModel = new List<EmployeeModel>()
            {
                new EmployeeModel("user1","123", "role1"),
                new EmployeeModel("user2","1234", "role2"),
                new EmployeeModel("user3","12345", "role3")
            };
        }

        public IEnumerable<EmployeeModel> GetAllIdentities()
        {
            return _employeeModel;
        }

        public bool IsLogin(string userName, string password)
        {
            Validate(userName, password);
            return _employeeModel.Where(e => e.UserName.Equals(userName) && e.Password.Equals(password)).Count() > 0;
        }

        public EmployeeModel GetIdentity(string userName,string password)
        {
            Validate(userName, password);
            return _employeeModel.Where(e => e.UserName.Equals(userName) && e.Password.Equals(password)).FirstOrDefault();
        }

        private void Validate(string userName, string password)
        {
            if (userName == null)
            {
                throw new ArgumentNullException(nameof(userName));
            }

            if (password == null)
            {
                throw new ArgumentNullException(nameof(password));
            }
        }
    }
}
