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
        public string Cost { get; set; }
        public string ID { get; set; }
        public void Clean()
        {
            Term = "";
            TypeOfTraining = "";
            ID = "";
            CountOfAttendents = "";
            Cost = "";
        }
    }
}
