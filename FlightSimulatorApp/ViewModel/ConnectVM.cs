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
    public class ConnectVM : INotifyPropertyChanged
    {
        private int port;
        private string ip;
        private bool isErrorAccured = false;
        public event PropertyChangedEventHandler PropertyChanged;
        private IFlightSimulatorModel model;
        // Properties.
        public string Ip { get { return ip; } set { ip = value; } }
        public int Port { get { return port; } set { port = value; } }
        public bool IsErrorAccured { get { return isErrorAccured; } set { isErrorAccured = value; } }

        public ConnectVM(IFlightSimulatorModel m)
        {
            Ip = ConfigurationManager.AppSettings.Get("ip");
            Port = int.Parse(ConfigurationManager.AppSettings.Get("port"));
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

        public void Connect()
        {
            model.Connect(Ip, Port);
        }

        public void Disconnect()
        {
            model.Disconnect();
        }
    }
}
