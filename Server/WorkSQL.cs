using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class WorkSQL
    {
        static private int port = 4020;
        static private string IP = "127.0.0.1";
        static public string info;
        public string ExpertNum { get; set; }
        public string Login { get; set; }
        public string ID { get; set; }
        static public int GetPort()
        {
            return port;
        }
        static public string GetIP()
        {
            return IP;
        }
        public void Clean()
        {
            this.ExpertNum = "";
            this.Login = "";
            this.ID = "";
        }
    }
}
