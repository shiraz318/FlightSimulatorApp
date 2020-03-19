using FlightSimulatorApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp.ViewModel
{
    class FlightSimulatorViewModel : INotifyPropertyChanged
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
        public FlightSimulatorViewModel(IFlightSimulatorModel m)
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
        public double VM_Rudder { get { return model.Rudder; }} // I think we should bind it to the text field of rudder (of the view) and to add a set that will do model.setSimulator(rudder, value) and we can delete the get and the propert of rudder in the model.
        public double VM_Elevator { get { return model.Elevator; } }//kanal
        public double VM_Aileron { get { return model.Aileron; } }//kanal
        public double VM_Throttle { get { return model.Throttle; } }//kanal
        public double VM_Indicated_heading_deg { get { return model.Indicated_heading_deg; } set { vm_indicated_heading_deg = value; } }//I think we can delete the set, and in the view we need to add to the event propertyChanged a function that checks the property name and change the appropriate progress bar accordingly OR we can do it by data binding - the progress bar will be bind to this property.
        public double VM_Gps_indicated_vertical_speed { get { return model.Gps_indicated_vertical_speed; } set { vm_gps_indicated_vertical_speed = value; } }
        public double VM_Gps_indicated_ground_speed_kt { get { return model.Gps_indicated_ground_speed_kt; } set { vm_gps_indicated_ground_speed_kt = value; } }
        public double VM_Airspeed_indicator_indicated_speed_kt { get { return model.Airspeed_indicator_indicated_speed_kt; } set { vm_airspeed_indicator_indicated_speed_kt = value; } }
        public double VM_Gps_indicated_altitude_ft { get { return model.Gps_indicated_altitude_ft; } set { vm_gps_indicated_altitude_ft = value; } }
        public double VM_Attitude_indicator_internal_roll_deg { get { return model.Attitude_indicator_internal_roll_deg; } set { vm_attitude_indicator_internal_roll_deg = value; } }
        public double VM_Attitude_indicator_internal_pitch_deg { get { return model.Attitude_indicator_internal_pitch_deg; } set { vm_attitude_indicator_internal_pitch_deg = value; } }
        public double VM_Altimeter_indicated_altitude_ft { get { return model.Altimeter_indicated_altitude_ft; } set { vm_altimeter_indicated_altitude_ft = value; } }
    }
}
