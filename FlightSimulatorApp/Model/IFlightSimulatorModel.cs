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
        string Indicated_heading_deg { set; get; }
        string Gps_indicated_vertical_speed { set; get; }
        string Gps_indicated_ground_speed_kt { set; get; }
        string Airspeed_indicator_indicated_speed_kt { set; get; }
        string Gps_indicated_altitude_ft { set; get; }
        string Attitude_indicator_internal_roll_deg { set; get; }
        string Attitude_indicator_internal_pitch_deg { set; get; }
        string Altimeter_indicated_altitude_ft { set; get; }
        string Latitude { get; set; }
        string Longtude { get; set; }
        string ValidCoordinate { get; set; }
    }
}
