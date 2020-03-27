using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.ComponentModel;
using System.IO;

namespace FlightSimulatorApp.Model
{
    public class MyFlightSimulatorModel : IFlightSimulatorModel
    {
        private Mutex mutex = new Mutex();
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
        private double latitude;
        private double longtude;
        private bool error = false;

        private Queue<string> messages = new Queue<string> { };
        private Dictionary<string, string> pathMap = new Dictionary<string, string> { };
        public MyFlightSimulatorModel()
        {

            this.stop = false;
            pathMap.Add("aileron", "/controls/flight/aileron");
            pathMap.Add("throttle", "/controls/engines/current-engine/throttle");
            pathMap.Add("rudder", "/controls/flight/rudder");
            pathMap.Add("elevator", "/controls/flight/elevator");
            Indicated_heading_deg = 0;
            Gps_indicated_vertical_speed = 0;
            Gps_indicated_ground_speed_kt = 0;
            Airspeed_indicator_indicated_speed_kt = 0;
            Gps_indicated_altitude_ft = 0;
            Attitude_indicator_internal_roll_deg = 0;
            Attitude_indicator_internal_pitch_deg = 0;
            Altimeter_indicated_altitude_ft = 0;
        }
        public double Latitude { get { return latitude; } set { latitude = value; NotifyPropertyChanged("Latitude"); } }
        public bool Error { get { return error; } set { Disconnect();  NotifyPropertyChanged("Error"); } }
        public double Indicated_heading_deg { get { return indicated_heading_deg; } set { indicated_heading_deg = value; NotifyPropertyChanged("Indicated_heading_deg"); } }
        public double Gps_indicated_vertical_speed { get { return gps_indicated_vertical_speed; } set { gps_indicated_vertical_speed = value; NotifyPropertyChanged("Gps_indicated_vertical_speed"); } }
        public double Gps_indicated_ground_speed_kt { get { return gps_indicated_ground_speed_kt; } set { gps_indicated_ground_speed_kt = value; NotifyPropertyChanged("Gps_indicated_ground_speed_kt"); } }
        public double Airspeed_indicator_indicated_speed_kt { get { return airspeed_indicator_indicated_speed_kt; } set { airspeed_indicator_indicated_speed_kt = value; NotifyPropertyChanged("Airspeed_indicator_indicated_speed_kt"); } }
        public double Gps_indicated_altitude_ft { get { return gps_indicated_altitude_ft; } set { gps_indicated_altitude_ft = value; NotifyPropertyChanged("Gps_indicated_altitude_ft"); } }
        public double Attitude_indicator_internal_roll_deg { get { return attitude_indicator_internal_roll_deg; } set { attitude_indicator_internal_roll_deg = value; NotifyPropertyChanged("Attitude_indicator_internal_roll_deg"); } }
        public double Attitude_indicator_internal_pitch_deg { get { return attitude_indicator_internal_pitch_deg; } set { attitude_indicator_internal_pitch_deg = value; NotifyPropertyChanged("Attitude_indicator_internal_pitch_deg"); } }
        public double Altimeter_indicated_altitude_ft { get { return altimeter_indicated_altitude_ft; } set { altimeter_indicated_altitude_ft = value; NotifyPropertyChanged("Altimeter_indicated_altitude_ft"); } }

        public double Longtude
        {
            get { return longtude; }
            set { longtude = value; NotifyPropertyChanged("Longtude"); }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void Connect(string ip, int port)
        {
            try
            {
                stop = false;
                messages = new Queue<string> { };
                tcpClient = new TcpClient();
                mutex = new Mutex();
                tcpClient.Connect(ip, port);
                strm = tcpClient.GetStream();
                //use the ipaddress as in thr server program
                Start();
            }
            catch (Exception e)
            {
                Error = true;
            }

        }

        public void Disconnect()
        {
            tcpClient.Close();
            tcpClient.Dispose();
            mutex.Dispose();
            stop = true;
        }

        public void Start()
        {

            new Thread(delegate () {

                while (!stop)
                {
                    if (messages.Count != 0)
                    {
                        //it throws exception of ה wait הסתיים עקב abandended  mutex. it happend when we had an error message (may be connected to the IsErrorAccured property and the condition in the mainwindow
                        string responseData;
                        mutex.WaitOne();
                        if (!Error)
                        {
                            Write(messages.Dequeue());
                        }
                       if (!Error)
                        {
                            responseData = Read();
                        }
                        mutex.ReleaseMutex();
                    }
                }

            }).Start();

            new Thread(delegate ()
            {
                while (!stop)
                {
                    string message = "get /instrumentation/gps/indicated-vertical-speed\nget /instrumentation/airspeed-indicator/indicated-speed-kt\nget /instrumentation/altimeter/indicated-altitude-ft\nget /instrumentation/attitude-indicator/internal-pitch-deg\nget /instrumentation/attitude-indicator/internal-roll-deg\nget /instrumentation/heading-indicator/indicated-heading-deg\nget /instrumentation/gps/indicated-altitude-ft\nget /instrumentation/gps/indicated-ground-speed-kt\n get /position/latitude-deg\nget /position/longitude-deg\n";
                    mutex.WaitOne();
                    Write(message);
                    //Separate the read message by \n
                    if (Error)
                    {
                        break;
                    }
                    var result = Read().Split('\n');
                    if (Error || result.Length < 10)
                    {
                        break;
                    }
                    mutex.ReleaseMutex();
                    if (double.TryParse(result[0], out double i1))
                    {
                        Gps_indicated_vertical_speed = i1;
                    }
                    else
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
                    if (double.TryParse(result[8], out double i9))
                    {
                        Latitude = i9;
                    }
                    else
                    {
                        //error
                    }
                    if (double.TryParse(result[9], out double i10))
                    {
                        Longtude = i10;
                    }
                    else
                    {
                        //error
                    }
                }
            }).Start();

        }
        private string Read()
        {
            try
            {
                strm.ReadTimeout = 10000;
                Byte[] data = new Byte[1024];
                String responseData = String.Empty;
                Int32 bytes = strm.Read(data, 0, data.Length);
                responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);

                return responseData;
            }
            catch (Exception e)
            {
                //announce error accured
                Error = true;
                stop = true;
                return "";
            }
        }
        private void Write(string message)
        {
            try
            {
                Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);
                strm.Write(data, 0, data.Length);
                Thread.Sleep(20);
            }
            catch (Exception e)
            {
                Error = true;
                stop = true;
            }
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
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
                //"nili cohen"//
            }
        }
    }
}
