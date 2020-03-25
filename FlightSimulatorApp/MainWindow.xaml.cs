using FlightSimulatorApp.Model;
using FlightSimulatorApp.View;
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
using System.Threading;
using System.Windows.Threading;
using System.ComponentModel;

namespace FlightSimulatorApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer dispatcherTimer;// = new DispatcherTimer();
        private DashboardVM dashboardVM;
        private JoystickVM joystickVM;
        private MapVM mapVM;
        private ConnectVM connectVM;

        public MainWindow()
        {

            InitializeComponent();

            TcpClient tcpC = new TcpClient();
            MyFlightSimulatorModel mfsm = new MyFlightSimulatorModel(tcpC);
            dashboardVM = new DashboardVM(mfsm);
            joystickVM = new JoystickVM(mfsm);
            mapVM = new MapVM(mfsm);
            connectVM = new ConnectVM(mfsm);
            DataContext = mapVM;
            joystick.DataContext = joystickVM;

            dashboard.DataContext = dashboardVM;
            map.DataContext = mapVM;
            map.setVM(mapVM);


            //We should do Binding!! here but it does not working for some reason
            ipText.Text = connectVM.Ip;
            portText.Text = connectVM.Port.ToString();

            mapVM.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName.Equals("VM_Error") && (!mapVM.IsErrorAccured))
                {
                    mapVM.IsErrorAccured = true;
                    Application.Current.Dispatcher.Invoke((Action)delegate {
                        ConnectionError connectionError = new ConnectionError();
                        connectionError.ShowDialog();
                    });

                }
                /*else if(e.PropertyName.Equals("VM_Latitude"))
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        //map.pushPin.Location.Latitude = 0;
                    });
                
                }
                else if (e.PropertyName.Equals("VM_Longtude"))
                {
                    //LongitudeText.Text = mapVM.VM_Longtude.ToString();
                }*/
            };

        }

        private void Connect_Click(object sender, RoutedEventArgs e)
        {
            mapVM.IsErrorAccured = false;
            //animation
            //click animation
            Thickness m = flyingAnimation.Margin;
            m.Top = 299;
            m.Right = 308;
            flyingAnimation.Margin = m;
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(12);
            m.Top -= 20;
            m.Right -= 40;
            flyingAnimation.Margin = m;
            dispatcherTimer.Start();
            //connect to the simulator
            connectVM.connect();
        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            Thickness m = flyingAnimation.Margin;
            m.Top -= 10;
            m.Right -= 20;
            flyingAnimation.Margin = m;
            //end of the screen
            if (flyingAnimation.Margin.Top < -2300 || flyingAnimation.Margin.Right < -2300)
            {
                dispatcherTimer.Stop();
            }

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
        private void MapControl_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
