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
/*
        // Dependency Property
        public static readonly DependencyProperty DependencyVM_Latitued =
             DependencyProperty.Register("VM_Latitude", typeof(double),
             typeof(MapControl), new PropertyMetadata(double.NaN));

        // .NET Property wrapper
        public DateTime CurrentTime
        {
            get { return (DateTime)GetValue(CurrentTimeProperty); }
            set { SetValue(CurrentTimeProperty, value); }
        }

        public double VM_Latitude
        {
            get { return (double)GetValue(DependencyVM_Latitued); }
            set {SetValue(DependencyVM_Latitued, value); }
        }

        private double vm_Longtude;

        public double VM_Longtude
        {
            get { return vm_Longtude; }
            set { vm_Longtude = value; }
        }*/
        
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
