using FlightSimulatorApp.Model;
using FlightSimulatorApp.ViewModel;
using FlightSimulatorApp.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.Sockets;

namespace FlightSimulatorApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DashboardVM dashboardVM;
        private WheelVM wheelVM;
        private MapVM mapVM;
        private ConnectVM connectVM;
        private string ip = "127.0.0.1";
        private int port = 5402;

        public string Ip
        {
            get
            {
                return ip;
            }
            set
            {
                ip = value;
                ipText.Text = ip;
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
                portText.Text = port.ToString();
            }
        }
        public MainWindow()
        {
            InitializeComponent();
            TcpClient tcpC = new TcpClient();
            MyFlightSimulatorModel mfsm = new MyFlightSimulatorModel(tcpC);
            dashboardVM = new DashboardVM(mfsm);
            wheelVM = new WheelVM(mfsm);
            mapVM = new MapVM(mfsm);
            connectVM = new ConnectVM(mfsm);
            DataContext = dashboardVM;
            wheel.positionChanged += delegate (Object sender, PositionChangedEventArgs e)
            {
                //
                if (e.getName().Equals("Throttle"))
                {
                    wheelVM.VM_Throttle = e.getValue();
                }//
                else if (e.getName().Equals("Aileron"))
                {
                    wheelVM.VM_Aileron = e.getValue();
                }
                else if (e.getName().Equals("Rudder"))
                {
                    wheelVM.VM_Rudder = e.getValue();
                }
                else if (e.getName().Equals("Elevator"))
                {
                    wheelVM.VM_Elevator = e.getValue();
                }
            };

            ipText.Text = ip;
            portText.Text = port.ToString();
        }

        private void Connect_Click(object sender, RoutedEventArgs e)
        {
            connectVM.connect(ip, port);
        }

        private void Setting_Click(object sender, RoutedEventArgs e)
        {
            SettingWindow setting = new SettingWindow();
            setting.connectValuesChanged += delegate (Object sender1, ConnectValuesChangedEventArgs e1)
            {
                Ip = e1.getIp();
                Port = e1.getPort();
            };
            setting.ShowDialog();
        }
    }
}
