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
        private bool isDisConnected;

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
            isDisConnected = false;
            Disconnect.IsEnabled = false;
            Setting.IsEnabled = true;
            //
            ipLabel.Content = connectVM.Ip;
            PortLabel.Content = connectVM.Port;


            connectVM.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName.Equals("VM_Error") && (!connectVM.IsErrorAccured))
                {
                    if (!isDisConnected)
                    {
                        try
                        {
                            Application.Current.Dispatcher.Invoke((Action)delegate
                            {
                                errorLAbel.Content = "Connection faulted Error";
                                Connect.IsEnabled = true;
                                Disconnect.IsEnabled = false;
                                Setting.IsEnabled = true;
                            });
                        } catch(Exception e3)
                        {
                            string message = e3.Message;
                        }
                    }

                    isConnected = false;
                    connectVM.IsErrorAccured = true;
                }
                else if (e.PropertyName.Equals("VM_TimeOutError"))
                {
                    try
                    {
                        Application.Current.Dispatcher.Invoke((Action)delegate
                        {
                            errorLAbel.Content = "Server is slow";
                            Connect.IsEnabled = true;
                            Disconnect.IsEnabled = false;
                            Setting.IsEnabled = true;
                        });
                    }
                    catch (Exception e4)
                    {
                        string message = e4.Message;
                    }
                    isConnected = false;
                }
            };
        }
        private void Connect_Click(object sender, RoutedEventArgs e)
        {
            if (!isConnected)
            {
                Setting.IsEnabled = false;
                Connect.IsEnabled = false;
                Disconnect.IsEnabled = true;
                isConnected = true;
                isDisConnected = false;
                connectVM.IsErrorAccured = false;
                errorLAbel.Content = "";
                connectVM.Connect();
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

        private void Disconnect_Click(object sender, RoutedEventArgs e)
        {
            isConnected = false;
            Connect.IsEnabled = true;
            Disconnect.IsEnabled = false;
            Setting.IsEnabled = true;
            isDisConnected = true;
            connectVM.Disconnect();
        }

    }
}
