using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp
{
    public class ConnectValuesChangedEventArgs
    {
        private string ip;
        private int port;
        public ConnectValuesChangedEventArgs(string ip, int port)
        {
            this.ip = ip;
            this.port = port;
        }
        public string getIp()
        {
            return ip;
        }
        public int getPort()
        {
            return port;
        }
    }
}

