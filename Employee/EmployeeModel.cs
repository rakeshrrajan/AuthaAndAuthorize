using System;
using System.Collections.Generic;
using System.Text;

namespace Employee
{
    public class EmployeeModel
    {
        public EmployeeModel(string userName, string password, string role)
        {
            UserName = userName ?? throw new ArgumentNullException(nameof(userName));
            Password = password ?? throw new ArgumentNullException(nameof(password));
            Role = role ?? throw new ArgumentNullException(nameof(role));
        }

        public String UserName { get; private set; }

        public String Password { get; private set; }

        public string Role { get; private set; }
    }
}
