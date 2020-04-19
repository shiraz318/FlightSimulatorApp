using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace FlightSimulatorApp.Model
{
    public interface IFlightSimulatorModel : INotifyPropertyChanged
    {
        // Connection to the flight simulator. 
        void Connect(string ip, int port);
        void Disconnect();
        void Start();
        void SetSimulator(string var, double value);
        // Properties.
        string Heading { set; get; }
        string VerticalSpeed { set; get; }
        string GroundSpeed { set; get; }
        string Airspeed { set; get; }
        string GpsAltitude { set; get; }
        string Roll { set; get; }
        string Pitch { set; get; }
        string AltimeterAltitude { set; get; }
        string Latitude { get; set; }
        string Longtude { get; set; }
        string ValidCoordinate { get; set; }
        string DashBoardError { get; set; }
        string TimeOutError { get; set; }
        string Error { get; set; }

    }
}
