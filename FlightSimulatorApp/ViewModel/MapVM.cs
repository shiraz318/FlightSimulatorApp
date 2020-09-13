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
        public string VMLatitude { get { return model.Latitude; } }
        public string VMLongtude { get { return model.Longtude; } }
        public Location VMLocation { get { return new Location(double.Parse(model.Latitude), double.Parse(model.Longtude)); } }
        public string VMValidCoordinate { get { return model.ValidCoordinate; } }

        public MapVM(IFlightSimulatorModel m)
        {
            model = m;
            model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM" + e.PropertyName);
            };
        }

        // Notify propery changed.
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
    }
}
