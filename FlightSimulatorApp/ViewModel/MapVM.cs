using FlightSimulatorApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows;
using FlightSimulatorApp.View;
using Microsoft.Maps.MapControl.WPF;

namespace FlightSimulatorApp.View
{
    public class MapVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private IFlightSimulatorModel model;
        // Properties.
        public string VM_Latitude { get { return model.Latitude; } }
        public string VM_Longtude { get { return model.Longtude; } }
        public Location VM_Location { get { return new Location(double.Parse(model.Latitude), double.Parse(model.Longtude)); } }
        public string VM_ValidCoordinate { get { return model.ValidCoordinate; } }

        public MapVM(IFlightSimulatorModel m)
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
