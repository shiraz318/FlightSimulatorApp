using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.ComponentModel;
using System.IO;
using System.Net.NetworkInformation;

namespace FlightSimulatorApp.Model
{
    public class MyFlightSimulatorModel : IFlightSimulatorModel
    {
        // Dashboard members.
        private string indicated_heading_deg;
        private string gps_indicated_vertical_speed;
        private string gps_indicated_ground_speed_kt;
        private string airspeed_indicator_indicated_speed_kt;
        private string gps_indicated_altitude_ft;
        private string attitude_indicator_internal_roll_deg;
        private string attitude_indicator_internal_pitch_deg;
        private string altimeter_indicated_altitude_ft;

        // Map members.
        public const int LatitudeUpBorder = 90;
        public const int LatitudeDownBorder = -90;
        public const int LongitudeDownBorder = -180;
        public const int LongitudeUpBorder = 180;
        private string latitude;
        private string longtude;

        // Error members.
        private string validation = "";
        private string error = "";
        private string timeOutError = "";
        private string dashBoardError = "";

        private Queue<string> messages = new Queue<string> { };
        private Dictionary<string, string> pathMap = new Dictionary<string, string> { };
        private Mutex mutex = new Mutex();
        private TcpClient tcpClient;
        private NetworkStream strm;
        volatile bool stop;

        // Properties.
        public string Error { get { return error; } set { error = value; NotifyPropertyChanged("Error"); if (!error.Equals("")) { Disconnect(); } } }
        public string TimeOutError { get { return timeOutError; } set { timeOutError = value; NotifyPropertyChanged("TimeOutError"); } }
        public string DashBoardError { get { return dashBoardError; } set { dashBoardError = value; NotifyPropertyChanged("DashBoardError"); } }
        public string Indicated_heading_deg { get { return indicated_heading_deg; } set { indicated_heading_deg = value; NotifyPropertyChanged("Indicated_heading_deg"); } }
        public string Gps_indicated_vertical_speed { get { return gps_indicated_vertical_speed; } set { gps_indicated_vertical_speed = value; NotifyPropertyChanged("Gps_indicated_vertical_speed"); } }
        public string Gps_indicated_ground_speed_kt { get { return gps_indicated_ground_speed_kt; } set { gps_indicated_ground_speed_kt = value; NotifyPropertyChanged("Gps_indicated_ground_speed_kt"); } }
        public string Airspeed_indicator_indicated_speed_kt { get { return airspeed_indicator_indicated_speed_kt; } set { airspeed_indicator_indicated_speed_kt = value; NotifyPropertyChanged("Airspeed_indicator_indicated_speed_kt"); } }
        public string Gps_indicated_altitude_ft { get { return gps_indicated_altitude_ft; } set { gps_indicated_altitude_ft = value; NotifyPropertyChanged("Gps_indicated_altitude_ft"); } }
        public string Attitude_indicator_internal_roll_deg { get { return attitude_indicator_internal_roll_deg; } set { attitude_indicator_internal_roll_deg = value; NotifyPropertyChanged("Attitude_indicator_internal_roll_deg"); } }
        public string Attitude_indicator_internal_pitch_deg { get { return attitude_indicator_internal_pitch_deg; } set { attitude_indicator_internal_pitch_deg = value; NotifyPropertyChanged("Attitude_indicator_internal_pitch_deg"); } }
        public string Altimeter_indicated_altitude_ft { get { return altimeter_indicated_altitude_ft; } set { altimeter_indicated_altitude_ft = value; NotifyPropertyChanged("Altimeter_indicated_altitude_ft"); } }
        public string Longtude { get { return longtude; } set { longtude = value; NotifyPropertyChanged("Longtude"); } }
        public string Latitude { get { return latitude; } set { latitude = value; NotifyPropertyChanged("Latitude"); } }
        public string ValidCoordinate { get { return validation; } set { validation = value; NotifyPropertyChanged("ValidCoordinate");} }

        public event PropertyChangedEventHandler PropertyChanged;

        public MyFlightSimulatorModel()
        {
            this.stop = false;
            pathMap.Add("aileron", "/controls/flight/aileron");
            pathMap.Add("throttle", "/controls/engines/current-engine/throttle");
            pathMap.Add("rudder", "/controls/flight/rudder");
            pathMap.Add("elevator", "/controls/flight/elevator");
            Reset();
        }

        public void Connect(string ip, int port)
        {
            Thread connectThread = new Thread(delegate ()
            {
                try
                {
                    stop = false;
                    Error = "";
                    TimeOutError = "";
                    messages = new Queue<string> { };
                    tcpClient = new TcpClient();
                    mutex = new Mutex();

                    // Connect.
                    tcpClient.Connect(ip, port);
                    strm = tcpClient.GetStream();
                    Start();
                }
                catch (Exception e)
                {
                    string message = e.Message;
                    Error = "Connection faulted Error";
                }
            });
            connectThread.IsBackground = true;
            connectThread.Start();
        }

        public void Disconnect()
        {
            tcpClient.Close();
            tcpClient.Dispose();
            mutex.Dispose();
            stop = true;
            // Reset values.
            Reset();
        }

        public void Start()
        {
            Thread setThread = new Thread(delegate () {

                while (!stop)
                {
                    // If there are messages in the queue.
                    if (messages.Count != 0)
                    {
                        try
                        {
                            mutex.WaitOne();
                            if (Error.Equals(""))
                            {
                                Write(messages.Dequeue());
                            }
                            if (Error.Equals(""))
                            {
                                Read();
                            }
                            mutex.ReleaseMutex();
                        } catch (Exception e)
                        {
                            string message = e.Message;
                            Error = "Connection faulted Error";
                        }
                    }
                }

            });
            setThread.IsBackground = true;
            setThread.Start();

            Thread getThread = new Thread(delegate ()
            {
                while (!stop)
                {
                    try
                    {
                        string message = "get /instrumentation/gps/indicated-vertical-speed\nget /instrumentation/airspeed-indicator/indicated-speed-kt\nget /instrumentation/altimeter/indicated-altitude-ft\nget /instrumentation/attitude-indicator/internal-pitch-deg\nget /instrumentation/attitude-indicator/internal-roll-deg\nget /instrumentation/heading-indicator/indicated-heading-deg\nget /instrumentation/gps/indicated-altitude-ft\nget /instrumentation/gps/indicated-ground-speed-kt\n get /position/latitude-deg\nget /position/longitude-deg\n";
                        mutex.WaitOne();
                        Write(message);
                        if (!Error.Equals(""))
                        {
                            mutex.ReleaseMutex();
                            break;
                        }
                        // Separate the read message by \n.
                        var result = Read().Split('\n');
                        if (!Error.Equals("") || result.Length < 10)
                        {
                            mutex.ReleaseMutex();
                            continue;
                        }
                        mutex.ReleaseMutex();

                        if (double.TryParse(result[0], out double i1))
                        {
                            Gps_indicated_vertical_speed = i1.ToString();
                            DashBoardError = "";
                        }
                        else
                        {
                            Gps_indicated_vertical_speed = "Error";
                            DashBoardError = "Error in the DashBoard";
                        }
                        if (double.TryParse(result[1], out double i2))
                        {
                            Airspeed_indicator_indicated_speed_kt = i2.ToString();
                        }
                        else
                        {
                            Airspeed_indicator_indicated_speed_kt = "Error";
                            DashBoardError = "Error in the DashBoard";
                        }
                        if (double.TryParse(result[2], out double i3))
                        {
                            Altimeter_indicated_altitude_ft = i3.ToString();
                        }
                        else
                        {
                            Altimeter_indicated_altitude_ft = "Error";
                            DashBoardError = "Error in the DashBoard";
                        }
                        if (double.TryParse(result[3], out double i4))
                        {
                            Attitude_indicator_internal_pitch_deg = i4.ToString();
                        }
                        else
                        {
                            Attitude_indicator_internal_pitch_deg = "Error";
                            DashBoardError = "Error in the DashBoard";
                        }
                        if (double.TryParse(result[4], out double i5))
                        {
                            Attitude_indicator_internal_roll_deg = i5.ToString();
                        }
                        else
                        {
                            Attitude_indicator_internal_roll_deg = "Error";
                            DashBoardError = "Error in the DashBoard";
                        }
                        if (double.TryParse(result[5], out double i6))
                        {
                            Indicated_heading_deg = i6.ToString();
                        }
                        else
                        {
                            Indicated_heading_deg = "Error";
                            DashBoardError = "Error in the DashBoard";
                        }
                        if (double.TryParse(result[6], out double i7))
                        {
                            Gps_indicated_altitude_ft = i7.ToString();
                        }
                        else
                        {
                            Gps_indicated_altitude_ft = "Error";
                            DashBoardError = "Error in the DashBoard";
                        }
                        if (double.TryParse(result[7], out double i8))
                        {
                            Gps_indicated_ground_speed_kt = i8.ToString();
                        }
                        else
                        {
                            Gps_indicated_ground_speed_kt = "Error";
                            DashBoardError = "Error in the DashBoard";
                        }
                        if (double.TryParse(result[8], out double i9))
                        {
                            // Checks borders of value.
                            if (i9 < LatitudeDownBorder)
                            {
                                ValidCoordinate = "Invalid Coordinate";
                                Latitude = LatitudeDownBorder.ToString();
                            }
                            if (i9 > LatitudeUpBorder)
                            {
                                ValidCoordinate = "Invalid Coordinate";
                                Latitude = LatitudeUpBorder.ToString();
                            }
                            if (i9 > LatitudeDownBorder && i9 < LatitudeUpBorder)
                            {
                                ValidCoordinate = "";
                                Latitude = i9.ToString();
                            }
                        }
                        else
                        {
                            ValidCoordinate = "Error getting Coordinate";
                        }
                        if (double.TryParse(result[9], out double i10))
                        {
                            // Checks borders of value.
                            if (i10 < LongitudeDownBorder)
                            {
                                ValidCoordinate = "Invalid Coordinate";
                                Longtude = LongitudeDownBorder.ToString();
                            }
                            if (i10 > LongitudeUpBorder)
                            {
                                ValidCoordinate = "Invalid Coordinate";
                                Longtude = LongitudeUpBorder.ToString();
                            }
                            if (i10 > LongitudeDownBorder && i10 < LongitudeUpBorder)
                            {
                                Longtude = i10.ToString();
                            }
                        }
                        else
                        {
                            ValidCoordinate = "Error getting Coordinate";
                        }
                        // Coordinates are valid.
                        if (ValidCoordinate.Equals(""))
                        {
                            NotifyPropertyChanged("Location");
                        }
                    } catch (Exception e)
                    {
                        string message = e.Message;
                        Error = "Connection faulted Error";
                    }
                    Thread.Sleep(100);
                }
            });
            getThread.IsBackground = true;
            getThread.Start();
        }

        private string Read()
        {
            try
            {
                strm.ReadTimeout = 10000;
                Byte[] data = new Byte[1024];
                String responseData = String.Empty;
                Int32 bytes = strm.Read(data, 0, data.Length);
                TimeOutError = "";
                responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                return responseData;
            }
            catch (Exception e)
            {
                string timeoutMessage = "A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond.";
                // Timeout error.
                if (e.Message.Contains(timeoutMessage))
                {
                    TimeOutError = "Server is slow";
                  // Connection error.
                } else
                {
                    Error = "Connection faulted Error";
                    stop = true;
                }
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
                string messageerror = e.Message;
                Error = "Connection faulted Error";
                stop = true;
            }
        }

        public void SetSimulator(string var, double value)
        {
            string path = pathMap[var];
            string message = "set " + path + " " + value.ToString() + "\n";
            // Adding a new message to the messages queue.
            messages.Enqueue(message);
        }

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        public void Reset()
        {
            // Dashboard.
            Indicated_heading_deg = "0";
            Gps_indicated_vertical_speed = "0";
            Gps_indicated_ground_speed_kt = "0";
            Airspeed_indicator_indicated_speed_kt = "0";
            Gps_indicated_altitude_ft = "0";
            Attitude_indicator_internal_roll_deg = "0";
            Attitude_indicator_internal_pitch_deg = "0";
            Altimeter_indicated_altitude_ft = "0";

            // Map.
            Latitude = "0";
            Longtude = "-122.407007";
            NotifyPropertyChanged("Location");

            // Errors.
            ValidCoordinate = "";
            DashBoardError = "";
        }
    }
}
