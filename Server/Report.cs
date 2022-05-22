using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Report
    {
        public Report()
        {
            
            Cost = "";
            Date = "";
            ClientID = "";
            TransactionID = "";
        }
        public string TransactionID { get; set; }
        public string ClientID { get; set; }
        public string Cost { get; set; }
        public string Date { get; set; }

        public void Clear()
        {
            Cost = "";
            Date = "";
            ClientID = "";
            TransactionID = "";
        }
    }
}
