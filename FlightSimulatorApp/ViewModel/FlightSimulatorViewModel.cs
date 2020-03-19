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
        WheelViewModel wheelvm;
        DashboardViewModel dashboardvm;
        public event PropertyChangedEventHandler PropertyChanged;
        private IFlightSimulatorModel model;
        public FlightSimulatorViewModel(IFlightSimulatorModel mod)
        {
            this.model = mod;
            this.wheelvm = new WheelViewModel(model);
            this.dashboardvm = new DashboardViewModel(model);
            model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                // NotifyPropertyChanged("VM_" + e.getName());
                wheelvm.NotifyPropertyChanged(e.getName());
                dashboardvm.NotifyPropertyChanged(e.getName());
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
