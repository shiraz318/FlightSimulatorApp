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
    class ConnectVM : INotifyPropertyChanged
    {
        private int port = int.Parse(ConfigurationSettings.AppSettings["port"]);
        //private int port = int.Parse(ConfigurationManager.AppSettings.Get("port"));
        private string ip = ConfigurationSettings.AppSettings["ip"];
        public string Ip
        {
            get
            {
                return ip;
            }
            set
            {
                ip = value;
            }
        }

        public int Port
        {
            get
            {
                return port;
            }
            set
            {
                port = value;
            }
        }
        private bool isErrorAccured = false;

        public bool IsErrorAccured
        {
            get { return isErrorAccured; }
            set { isErrorAccured = value; }
        }

        public static object ConfigurationManager { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private IFlightSimulatorModel model;
        public ConnectVM(IFlightSimulatorModel m)
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
        public void connect()
        {
            model.Connect(Ip, Port);
        }
    }
    
    
}
