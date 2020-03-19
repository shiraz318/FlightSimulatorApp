using FlightSimulatorApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp.ViewModel
{
    class DashboardViewModel :INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private double vm_indicated_heading_deg;
        private double vm_gps_indicated_vertical_speed;
        private double vm_gps_indicated_ground_speed_kt;
        private double vm_airspeed_indicator_indicated_speed_kt;
        private double vm_gps_indicated_altitude_ft;
        private double vm_attitude_indicator_internal_roll_deg;
        private double vm_attitude_indicator_internal_pitch_deg;
        private double vm_altimeter_indicated_altitude_ft;
        private IFlightSimulatorModel model;
        public DashboardViewModel(IFlightSimulatorModel m)
        {
            model = m;
            model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM_" + e.getName());
            };

        }
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
                //"nili cohen"//
            }
        }

        public double VM_Indicated_heading_deg { get { return model.Indicated_heading_deg; } set { vm_indicated_heading_deg = value; /*I think or we do not need a setter or we need to add here NotifyPropertyChanged*/} }
        public double VM_Gps_indicated_vertical_speed { get { return model.Gps_indicated_vertical_speed; } set { vm_gps_indicated_vertical_speed = value; } }
        public double VM_Gps_indicated_ground_speed_kt { get { return model.Gps_indicated_ground_speed_kt; } set { vm_gps_indicated_ground_speed_kt = value; } }
        public double VM_Airspeed_indicator_indicated_speed_kt { get { return model.Airspeed_indicator_indicated_speed_kt; } set { vm_airspeed_indicator_indicated_speed_kt = value; } }
        public double VM_Gps_indicated_altitude_ft { get { return model.Gps_indicated_altitude_ft; } set { vm_gps_indicated_altitude_ft = value; } }
        public double VM_Attitude_indicator_internal_roll_deg { get { return model.Attitude_indicator_internal_roll_deg; } set { vm_attitude_indicator_internal_roll_deg = value; } }
        public double VM_Attitude_indicator_internal_pitch_deg { get { return model.Attitude_indicator_internal_pitch_deg; } set { vm_attitude_indicator_internal_pitch_deg = value; } }
        public double VM_Altimeter_indicated_altitude_ft { get { return model.Altimeter_indicated_altitude_ft; } set { vm_altimeter_indicated_altitude_ft = value; } }
    }
}
