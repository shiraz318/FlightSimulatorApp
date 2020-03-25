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
    public class MapVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private IFlightSimulatorModel model;

        public double VM_Latitude
        {
            get { return model.Latitude; }
        }

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
        private bool isErrorAccured = false;

        public bool IsErrorAccured
        {
            get { return isErrorAccured; }
            set { isErrorAccured = value; }
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
