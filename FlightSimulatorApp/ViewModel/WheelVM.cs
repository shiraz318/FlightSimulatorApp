using FlightSimulatorApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace FlightSimulatorApp.View
{
    public class WheelVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private IFlightSimulatorModel model;
        // Properties.
        public double VM_Rudder { set { model.SetSimulator("rudder", value); } }
        public double VM_Elevator { set { model.SetSimulator("elevator", value); } }
        public double VM_Aileron { set { model.SetSimulator("aileron", value); } }
        public double VM_Throttle { set { model.SetSimulator("throttle", value); } }

        public WheelVM(IFlightSimulatorModel m)
        {
            model = m;
            model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM_" + e.PropertyName);
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
