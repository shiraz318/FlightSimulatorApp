using FlightSimulatorApp.Model;
using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace FlightSimulatorApp.View
{
    public class MainVM : INotifyPropertyChanged
    {
        private int port;
        private string ip;
        public event PropertyChangedEventHandler PropertyChanged;
        private IFlightSimulatorModel model;
        // Properties.
        private WheelVM wheelViewModel;
        public WheelVM WheelViewModel
        {
            get { return wheelViewModel; }
            set { wheelViewModel = value; }
        }
        private DashboardVM dashboardViewModel;
        public DashboardVM DashboardViewModel
        {
            get { return dashboardViewModel; }
            set { dashboardViewModel = value; }
        }
        private MapVM mapViewModel;
        public MapVM MapViewModel
        {
            get { return mapViewModel; }
            set { mapViewModel = value; }
        }

        public string Ip { get { return ip; } set { ip = value; } }
        public int Port { get { return port; } set { port = value; } }
        public string VMTimeOutError { get { return model.TimeOutError; } }
        public string VMError { get { return model.Error; } }

        public MainVM(IFlightSimulatorModel m)
        {
            model = m;
            model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM" + e.PropertyName);
            };
            // Initialize other view models
            WheelViewModel = new WheelVM(model);
            DashboardViewModel = new DashboardVM(model);
            MapViewModel = new MapVM(model);

            Ip = ConfigurationManager.AppSettings.Get("ip");
            Port = int.Parse(ConfigurationManager.AppSettings.Get("port"));
        }

        // Notify property changed.
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        // Connect to the flight simulator.
        public void Connect()
        {
            model.Connect(Ip, Port);
        }

        // Disconnect from the flight simulator.
        public void Disconnect()
        {
            model.Disconnect();
        }
    }
}
