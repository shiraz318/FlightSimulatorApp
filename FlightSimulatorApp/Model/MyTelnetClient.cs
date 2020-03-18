using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp.Model
{
    class MyTelnetClient : ITelnetClient
    {
        public void Connect(string ip, int port)
        {
            ServerSide.ExecuteServer();
        }

        public void Disconnect()
        {
            throw new NotImplementedException();
        }

        public string Read()
        {
            throw new NotImplementedException();
        }

        public void Write(string command)
        {
            throw new NotImplementedException();
        }
    }
}
