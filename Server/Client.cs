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
            AbonementId = "";
            TrainerId = "";
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
        public string AbonementId { get; set; }
        public string TrainerId { get; set; }
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
                this.Login = "";
                this.Password = "";
            this.AbonementId = "";
            this.TrainerId = "";
            }
        }
}
