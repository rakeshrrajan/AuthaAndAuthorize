using System;
using System.Collections.Generic;
using System.Text;

namespace Employee
{
    public interface IIdentity<T>
    {
        bool IsLogin(string userName, string password);

        IEnumerable<T> GetAllIdentities();        

        T GetIdentity(string userName, string password);
    }
}
