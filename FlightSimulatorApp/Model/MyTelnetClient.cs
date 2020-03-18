using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp.Model
{
    class MyTelnetClient : ITelnetClient
    {
        Socket sender;
        public void Connect(string ip, int port)
        {
            //ServerSide.ExecuteServer();
            sender = ClientSide.ExecuteClient(ip, port);
        }

        public void Disconnect()
        {
            ClientSide.CloseSocket(sender);
        }

        public string Read(string command)
        {
            command = "get /instrumentation/gps/indicated-vertical-speed";
            return ClientSide.Write(sender, command);
        }

        public void Write(string command)
        {
            ClientSide.Write(sender, command);
        }
    }
}
