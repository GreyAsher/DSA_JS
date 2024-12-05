using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSA1
{
    public class LoginValidator
    {
        public bool LoggedIn(string username, string password)
        {
            // Business logic for login
            return username == "Cati" && password == "Heskey";
        }
    }
}
