using FlightSimulatorApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows;
using FlightSimulatorApp.View;

namespace FlightSimulatorApp.View
{
    class MapVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private IFlightSimulatorModel model;
        private double vm_Latitude;

        public double VM_Latitude
        {
            get { return model.Latitude; }
        }
        private double vm_Longtude;

        public double VM_Longtude
        {
            get { return model.Longtude; }
        }
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
                //"nili cohen"//
            }
        }
        
    }

}
