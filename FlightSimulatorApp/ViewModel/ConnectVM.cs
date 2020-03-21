using FlightSimulatorApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp.ViewModel
{
    class ConnectVM : INotifyPropertyChanged
    {
        private string port = "5402";
        private string ip = "127.0.0.1";
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

        public string Port
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
        public event PropertyChangedEventHandler PropertyChanged;
        private IFlightSimulatorModel model;
        public ConnectVM(IFlightSimulatorModel m)
        {
            model = m;
            model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM_" + e.getName());
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
            model.Connect(Ip, int.Parse(Port));
        }
    }
    
    
}
