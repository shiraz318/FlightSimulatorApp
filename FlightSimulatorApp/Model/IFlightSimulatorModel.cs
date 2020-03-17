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
        double Indicated_heading_deg { set; get; }
        double Gps_indicated_vertical_speed { set; get; }
        double Gps_indicated_ground_speed_kt { set; get; }
        double Airspeed_indicator_indicated_speed_kt { set; get; }
        double Gps_indicated_altitude_ft { set; get; }
        double Attitude_indicator_internal_roll_deg { set; get; }
        double Attitude_indicator_internal_pitch_deg { set; get; }
        double Altimeter_indicated_altitude_ft { set; get; }
    }
}
