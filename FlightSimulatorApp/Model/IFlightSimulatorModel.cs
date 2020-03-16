using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp.Model
{
    interface IFlightSimulatorModel : INotifyPropertyChanged
    {
        //connection to the flight simulator 
        void Connect(string ip, int port);
        void Disconnect();
        void Start();
        //properties
        double Rudder { set; get; }
        double Elevator { set; get; }
        double Aileron { set; get; }
        double Throttle { set; get; }
    }
}
