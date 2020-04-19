using FlightSimulatorApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace FlightSimulatorApp.View
{
    public class DashboardVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private IFlightSimulatorModel model;
        // Properties.
        public string VMHeading { get { return model.Heading; } }
        public string VMVerticalSpeed { get { return model.VerticalSpeed; } }
        public string VMGroundSpeed { get { return model.GroundSpeed; } }
        public string VMAirspeed { get { return model.Airspeed; } }
        public string VMGpsAltitude { get { return model.GpsAltitude; } }
        public string VMRoll { get { return model.Roll; } }
        public string VMPitch { get { return model.Pitch; } }
        public string VMAltimeterAltitude { get { return model.AltimeterAltitude; } }
        public string VMDashBoardError { get { return model.DashBoardError; } }

        public DashboardVM(IFlightSimulatorModel m)
        {
            model = m;
            model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM" + e.PropertyName);
            };
        }

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
    }
}
