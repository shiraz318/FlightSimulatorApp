using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.ComponentModel;

namespace FlightSimulatorApp.Model
{
    class MyFlightSimulatorModel : IFlightSimulatorModel
    {
        private TcpClient tcpClient;
        NetworkStream strm;
        volatile bool stop;
        private double indicated_heading_deg;
        private double gps_indicated_vertical_speed;
        private double gps_indicated_ground_speed_kt;
        private double airspeed_indicator_indicated_speed_kt;
        private double gps_indicated_altitude_ft;
        private double attitude_indicator_internal_roll_deg;
        private double attitude_indicator_internal_pitch_deg;
        private double altimeter_indicated_altitude_ft;
        private Queue<string> messages = new Queue<string> { };
        private Dictionary<string, string> pathMap = new Dictionary<string, string> { };
        public MyFlightSimulatorModel(TcpClient telnet)
        {
           
            this.tcpClient = telnet;
            this.stop = false;
            pathMap.Add("aileron", "/controls/flight/aileron");
            pathMap.Add("throttle", "/controls/engines/current-engine/throttle");
            pathMap.Add("rudder", "/controls/flight/rudder");
            pathMap.Add("elevator", "/controls/flight/elevator");
            Indicated_heading_deg = 50;
            Gps_indicated_vertical_speed = 60;
            Gps_indicated_ground_speed_kt = 70;
            Airspeed_indicator_indicated_speed_kt = 80;
            Gps_indicated_altitude_ft = 90;
            Attitude_indicator_internal_roll_deg = 100;
            Attitude_indicator_internal_pitch_deg = 102;
            Altimeter_indicated_altitude_ft = 30;
        }
        public double Indicated_heading_deg { get { return indicated_heading_deg; } set { indicated_heading_deg = value; NotifyPropertyChanged("Indicated_heading_deg"); } }
        public double Gps_indicated_vertical_speed { get { return gps_indicated_vertical_speed; } set { gps_indicated_vertical_speed = value; NotifyPropertyChanged("Gps_indicated_vertical_speed"); } }
        public double Gps_indicated_ground_speed_kt { get { return gps_indicated_ground_speed_kt; } set { gps_indicated_ground_speed_kt = value; NotifyPropertyChanged("Gps_indicated_ground_speed_kt"); } }
        public double Airspeed_indicator_indicated_speed_kt { get { return airspeed_indicator_indicated_speed_kt; } set { airspeed_indicator_indicated_speed_kt = value; NotifyPropertyChanged("Airspeed_indicator_indicated_speed_kt"); } }
        public double Gps_indicated_altitude_ft { get { return gps_indicated_altitude_ft; } set { gps_indicated_altitude_ft = value; NotifyPropertyChanged("Gps_indicated_altitude_ft"); } }
        public double Attitude_indicator_internal_roll_deg { get { return attitude_indicator_internal_roll_deg; } set { attitude_indicator_internal_roll_deg = value; NotifyPropertyChanged("Attitude_indicator_internal_roll_deg"); } }
        public double Attitude_indicator_internal_pitch_deg { get { return attitude_indicator_internal_pitch_deg; } set { attitude_indicator_internal_pitch_deg = value; NotifyPropertyChanged("Attitude_indicator_internal_pitch_deg"); } }
        public double Altimeter_indicated_altitude_ft { get { return altimeter_indicated_altitude_ft; } set { altimeter_indicated_altitude_ft = value; NotifyPropertyChanged("Altimeter_indicated_altitude_ft"); } }
        public event PropertyChangedEventHandler PropertyChanged;

        public void Connect(string ip, int port)
        {
            Gps_indicated_vertical_speed = 0;
            Airspeed_indicator_indicated_speed_kt = 0;
            /*
            try
            {
                Console.WriteLine("Connecting...");
                tcpClient.Connect(ip, port);
                strm = tcpClient.GetStream();
                //use the ipaddress as in thr server program
                Console.WriteLine("Connected");
                Start();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error:" + e.StackTrace);
            }
            */
        }

        public void Disconnect()
        {
            tcpClient.Close();
            stop = true; 
        }

        public void Start()
        {

           
            /*
            new Thread(delegate ()
               {
            while (!stop)
            {
               
               

               
                Byte[] data = System.Text.Encoding.ASCII.GetBytes("get /gps_indicated-vertical-speed\nget /airspeed-indicator_indicated-speed-kt\nget /altimeter_indicated-altitude-ft\nget /attitude-indicator_internal-pitch-deg\nget /attitude-indicator_internal-roll-deg\nget /indicated-heading-deg\nget /gps_indicated-altitude-ft\nget /gps_indicated-ground-speed-kt\n");
                //Byte[] data = System.Text.Encoding.ASCII.GetBytes("get /instrumentation/gps/indicated-vertical-speed\r\n); // get /airspeed-indicator_indicated-speed-kt\nget /altimeter_indicated-altitude-ft\nget /attitude-indicator_internal-pitch-deg\nget /attitude-indicator_internal-roll-deg\nget /indicated-heading-deg\nget /gps_indicated-altitude-ft\nget /gps_indicated-ground-speed-kt\n");
                strm.Write(data, 0, data.Length);
                data = new Byte[1024];
                String responseData = String.Empty;
                Int32 bytes = strm.Read(data, 0, data.Length);
                responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                var result = responseData.Split('\n');
                if (double.TryParse(result[0], out double i1))
                {
                    Gps_indicated_vertical_speed = i1;
                } else
                {
                    //error
                }
                if (double.TryParse(result[1], out double i2))
                {
                    Airspeed_indicator_indicated_speed_kt = i2;
                }
                else
                {
                    //error
                }
                if (double.TryParse(result[2], out double i3))
                {
                    Altimeter_indicated_altitude_ft = i3;
                }
                else
                {
                    //error
                }
                if (double.TryParse(result[3], out double i4))
                {
                    Attitude_indicator_internal_pitch_deg = i4;
                }
                else
                {
                    //error
                }
                if (double.TryParse(result[4], out double i5))
                {
                    Attitude_indicator_internal_roll_deg = i5;
                }
                else
                {
                    //error
                }
                if (double.TryParse(result[5], out double i6))
                {
                    Indicated_heading_deg = i6;
                }
                else
                {
                    //error
                }
                if (double.TryParse(result[6], out double i7))
                {
                    Gps_indicated_altitude_ft = i7;
                }
                else
                {
                    //error
                }
                if (double.TryParse(result[7], out double i8))
                {
                    Gps_indicated_ground_speed_kt = i8;
                }
                else
                {
                    //error
                }
 
                while (messages.Count != 0)
                {
                    data = System.Text.Encoding.ASCII.GetBytes("get /controls/engines/current-engine/throttle\n");
                    strm.Write(data, 0, data.Length);
                   // data = new Byte[256];
                    data = new Byte[256];
                    responseData = String.Empty;
                    bytes = strm.Read(data, 0, data.Length);
                    responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                    data = System.Text.Encoding.ASCII.GetBytes(messages.Dequeue());
                    strm.Write(data, 0, data.Length);
                    data = new Byte[256];
                    responseData = String.Empty;
                    bytes = strm.Read(data, 0, data.Length);
                    responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                    }
                
            //need more vars
            Thread.Sleep(100);
                }
            }).Start();
            */
        }
        public void setSimulator(string var, double value)
        {
            string path = pathMap[var];
            string message = "set " + path + " " + value.ToString() + "\n";
            messages.Enqueue(message);
            Console.WriteLine("");
            
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
