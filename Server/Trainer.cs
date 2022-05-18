using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    internal class Trainer
    {
        public string ID { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string ThirdName { get; set; }
        public string Type { get; set; }
        public string Cost { get; set; }
        public string Search { get; set; }
        public void Clean()
        {
            ID = "";
            Surname = "";
            Name = "";
            ThirdName = "";
            Type = "";
            Cost = "";
            Search = "";
        }
    }
}
