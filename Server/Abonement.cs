using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    internal class Abonement
    {
        public string CountOfAttendents { get; set; }
        public string TypeOfTraining { get; set; }
        public string Term { get; set; }
        public string Price { get; set; }
        public string ID { get; set; }
        public string Search { get; set; }
        public void Clean()
        {
            Term = "";
            Price = "";
            TypeOfTraining = "";
            ID = "";
            CountOfAttendents = "";
        }
    }
}
