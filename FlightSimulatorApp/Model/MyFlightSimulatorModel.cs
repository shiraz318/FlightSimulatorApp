using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlightSimulatorApp.Model
{
    class MyFlightSimulatorModel : IFlightSimulatorModel
    {
        ITelnetClient telnetClient;
        volatile bool stop;
        private double rudder;
        private double elevator;
        private double aileron;
        private double throttle;

        public MyFlightSimulatorModel(ITelnetClient telnet)
        {
            this.telnetClient = telnet;
            this.stop = false;
        }
       
        public double Rudder { get { return rudder; } set { rudder = value; NotifyPropertyChanged("Rudder"); } }
        public double Elevator { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public double Aileron { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public double Throttle { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public event PropertyChangedEventHandler PropertyChanged;

        public void Connect(string ip, int port)
        {
            telnetClient.Connect(ip, port);
        }

        public void Disconnect()
        {
            telnetClient.Disconnect();
            stop = true; 
        }

        public void Start()
        {
            new Thread(delegate ()
            {
                while (!stop)
                {
                    Rudder = double.Parse(telnetClient.Read());
                    Aileron = double.Parse(telnetClient.Read());
                    Elevator = double.Parse(telnetClient.Read());
                    Throttle = double.Parse(telnetClient.Read());
                    //need more vars
                    Thread.Sleep(250);
                }
            }).Start();
        }
        public void NotifyPropertyChanged(string propName)
        {
            if(this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
                //"nili cohen"//
            }
        }
    }
}
