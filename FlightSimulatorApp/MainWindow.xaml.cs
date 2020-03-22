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
     
        public MainWindow()
        {
            InitializeComponent();
            TcpClient tcpC = new TcpClient();
            MyFlightSimulatorModel mfsm = new MyFlightSimulatorModel(tcpC);
            dashboardVM = new DashboardVM(mfsm);
            wheelVM = new WheelVM(mfsm);
            mapVM = new MapVM(mfsm);
            connectVM = new ConnectVM(mfsm);
            DataContext = connectVM;
            wheel.DataContext = wheelVM;
            wheel.joystick.DataContext = wheelVM;
            dashboard.DataContext = dashboardVM;
            /*wheel.positionChanged += delegate (Object sender, PositionChangedEventArgs e)
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
            };*/
            /*dashboardVM.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                string name = e.getName();
                dashboard.name
            };*/
            //We should do Binding!! here but it does not working for some reason
            ipText.Text = connectVM.Ip;
            portText.Text = connectVM.Port.ToString();
        }

        private void Connect_Click(object sender, RoutedEventArgs e)
        {
            connectVM.connect();
        }

        private void Setting_Click(object sender, RoutedEventArgs e)
        {
            SettingWindow setting = new SettingWindow();
            setting.ShowDialog();
            if (setting.IsOk)
            {
                connectVM.Ip = setting.ipText.Text;
                connectVM.Port = int.Parse(setting.portText.Text);
                //Binding!!
                ipText.Text = connectVM.Ip;
                portText.Text = connectVM.Port.ToString();
                setting.IsOk = false;
            }
        }
    }
}
