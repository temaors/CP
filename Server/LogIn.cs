using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    internal class LogIn
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public void Clean()
        {
            Login = "";
            Password = "";
        }
    }
}
