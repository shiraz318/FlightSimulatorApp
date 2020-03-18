using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp.Model
{
    interface ITelnetClient
    {
        void Connect(string ip, int port);
        void Write(string command);
        string Read(string command);
        void Disconnect();

    }
}
