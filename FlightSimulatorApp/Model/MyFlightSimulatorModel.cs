using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
        public MyFlightSimulatorModel(TcpClient telnet)
        {
           
            this.tcpClient = telnet;
            this.stop = false;
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
        }

        public void Disconnect()
        {
            tcpClient.Close();
            stop = true; 
        }

        public void Start()
        {
            new Thread(delegate ()
            {
                while (!stop)
                {
                 
                    /***/
                    Byte[] data = System.Text.Encoding.ASCII.GetBytes("get /instrumentation/gps/indicated-vertical-speed\n");
                    strm.Write(data, 0, data.Length);
                    data = new Byte[256];
                    String responseData = String.Empty;
                    Int32 bytes = strm.Read(data, 0, data.Length);
                    responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                    Gps_indicated_vertical_speed = double.Parse(responseData);
                    /***/
                    data = System.Text.Encoding.ASCII.GetBytes("get /instrumentation/airspeed-indicator/indicated-speed-kt\n");
                    strm.Write(data, 0, data.Length);
                    data = new Byte[256];
                    responseData = String.Empty;
                    bytes = strm.Read(data, 0, data.Length);
                    responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                    Airspeed_indicator_indicated_speed_kt = double.Parse(responseData);
                    /***/
                    data = System.Text.Encoding.ASCII.GetBytes("get /instrumentation/altimeter/indicated-altitude-ft\n");
                    strm.Write(data, 0, data.Length);
                    data = new Byte[256];
                    responseData = String.Empty;
                    bytes = strm.Read(data, 0, data.Length);
                    responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                    Altimeter_indicated_altitude_ft = double.Parse(responseData);
                    /***/
                    data = System.Text.Encoding.ASCII.GetBytes("get /instrumentation/attitude-indicator/internal-pitch-deg\n");
                    strm.Write(data, 0, data.Length);
                    data = new Byte[256];
                    responseData = String.Empty;
                    bytes = strm.Read(data, 0, data.Length);
                    responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                    Attitude_indicator_internal_pitch_deg = double.Parse(responseData);
                    /***/
                    data = System.Text.Encoding.ASCII.GetBytes("get /instrumentation/attitude-indicator/internal-roll-deg\n");
                    strm.Write(data, 0, data.Length);
                    data = new Byte[256];
                    responseData = String.Empty;
                    bytes = strm.Read(data, 0, data.Length);
                    responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                    Attitude_indicator_internal_roll_deg = double.Parse(responseData);
                    /***/
                    data = System.Text.Encoding.ASCII.GetBytes("get /instrumentation/heading-indicator/indicated-heading-deg\n");
                    strm.Write(data, 0, data.Length);
                    data = new Byte[256];
                    responseData = String.Empty;
                    bytes = strm.Read(data, 0, data.Length);
                    responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                    Indicated_heading_deg = double.Parse(responseData);
                    /***/
                    data = System.Text.Encoding.ASCII.GetBytes("get /instrumentation/gps/indicated-altitude-ft\n");
                    strm.Write(data, 0, data.Length);
                    data = new Byte[256];
                    responseData = String.Empty;
                    bytes = strm.Read(data, 0, data.Length);
                    responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                    Gps_indicated_altitude_ft = double.Parse(responseData);
                    /***/
                    data = System.Text.Encoding.ASCII.GetBytes("get /instrumentation/gps/indicated-ground-speed-kt\n");
                    strm.Write(data, 0, data.Length);
                    data = new Byte[256];
                    responseData = String.Empty;
                    bytes = strm.Read(data, 0, data.Length);
                    responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                    Gps_indicated_ground_speed_kt = double.Parse(responseData);


            //need more vars
            Thread.Sleep(100);
                }
            }).Start();
        }
        public void setSimulator(string var, double value)
        {
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
