using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
        class Client : WorkSQL
        {
            public Client()
            {
                Search = "";
                Name = "";
                Surname = "";
                Thirdname = "";
                Gender = "";
                Email = "";
                Age = "";
                Login = "";
                Password = "";
                
            }

            public string Search { get; set; }
            public string Password { get; set; }
            public string Name { get; set; }
            public string Surname { get; set; }
            public string Thirdname { get; set; }
            public string Gender { get; set; }
            public string Email { get; set; }
            public string Age { get; set; }
            public string Access { get; set; }
            public virtual void Clean()
            {
                this.Search = "";
                this.ID = "";
                this.Name = "";
                this.Surname = "";
                this.Thirdname = "";
                this.Gender = "";
                this.Email = "";
                this.Age = "";
                this.ID = "";
                this.Access = "";
                this.Password = "";
            }
        }
}
