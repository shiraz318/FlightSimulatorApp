using FlightSimulatorApp.Model;
using FlightSimulatorApp.View;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using System.ComponentModel;

namespace FlightSimulatorApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer dispatcherTimer;
        private ConnectVM connectVM;
        private bool isConnected;
        private MyFlightSimulatorModel mfsm;

        public MainWindow()
        {
            InitializeComponent();
            // Initialize the model.
            mfsm = new MyFlightSimulatorModel();
            // Initialize the components with a common model.
            wheel.Init(mfsm);
            dashboard.Init(mfsm);
            map.Init(mfsm);
            //
            connectVM = new ConnectVM(mfsm);
            DataContext = connectVM;
            isConnected = false;
            //
            ipLabel.Content = connectVM.Ip;
            PortLabel.Content = connectVM.Port;

            connectVM.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName.Equals("VM_Error") && (!connectVM.IsErrorAccured))
                {
                    isConnected = false;
                    connectVM.IsErrorAccured = true;
                    Application.Current.Dispatcher.Invoke((Action)delegate {
                        errorLAbel.Content = "Connection faulted Error";
                        Connect.IsEnabled = true;
                    });
                } 
            };
        }
        private void Connect_Click(object sender, RoutedEventArgs e)
        {
            if (!isConnected)
            {
                Connect.IsEnabled = false;
                isConnected = true;
                connectVM.IsErrorAccured = false;
                errorLAbel.Content = "";
                // Click animation.
                ClickAnimation();
                // Connect to the simulator.
                connectVM.Connect();
            }
        }
        private void ClickAnimation()
        {
            Thickness m = flyingAnimation.Margin;
            m.Top = 299;
            m.Right = 308;
            flyingAnimation.Margin = m;
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(DispatcherTimer_Tick);
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(12);
            m.Top -= 20;
            m.Right -= 40;
            flyingAnimation.Margin = m;
            dispatcherTimer.Start();
        }
        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            Thickness m = flyingAnimation.Margin;
            m.Top -= 10;
            m.Right -= 20;
            flyingAnimation.Margin = m;
            // End of the screen.
            if (flyingAnimation.Margin.Top < -2300 || flyingAnimation.Margin.Right < -2300)
            {
                dispatcherTimer.Stop();
            }

        }
        private void Setting_Click(object sender, RoutedEventArgs e)
        {
            SettingWindow setting = new SettingWindow();
            setting.ShowDialog();
            // If user enteres ip and port to change and pressed ok.
            if (setting.IsOk)
            {
                connectVM.Ip = setting.ipText.Text;
                connectVM.Port = int.Parse(setting.portText.Text);
                ipLabel.Content = connectVM.Ip;
                PortLabel.Content = connectVM.Port;
                setting.IsOk = false;
            }
        }
    }
}
