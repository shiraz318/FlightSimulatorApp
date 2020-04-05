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
        private ConnectVM connectVM;
        private DispatcherTimer dispatcherTimer;
        private bool isConnected;
        private bool isDisConnected;
       
        public MainWindow()
        {
            InitializeComponent();
            connectVM = (Application.Current as App).ConnectviewModel;
            // Initialize the Data Context.
            DataContext = connectVM;
            wheel.DataContext = (Application.Current as App).WheelviewModel;
            dashboard.DataContext = (Application.Current as App).DashboardviewModel;
            map.DataContext = (Application.Current as App).MapviewModel;
            // Initialize connection.
            isConnected = false;
            isDisConnected = false;
            SetIsEnabled(true);
            // Initialize the ip and port.
            ipLabel.Content = connectVM.Ip;
            PortLabel.Content = connectVM.Port;
            connectVM.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName.Equals("VM_Error"))
                {
                    if (!isDisConnected)
                    {
                        try
                        {
                            Application.Current.Dispatcher.Invoke((Action)delegate
                            {
                                if (connectVM.VM_Error.Equals(""))
                                {
                                    errorLAbel.Content = "";

                                }
                                else
                                {
                                    errorLAbel.Content = "Connection faulted Error";
                                    SetIsEnabled(true);
                                    isConnected = false;
                                }
                            });
                        } catch(Exception e3)
                        {
                            string message = e3.Message;
                        }
                    }
                    
                }
                else if (e.PropertyName.Equals("VM_TimeOutError"))
                {
                    try
                    {
                        Application.Current.Dispatcher.Invoke((Action)delegate {

                            if (connectVM.VM_TimeOutError.Equals("") && connectVM.VM_Error.Equals(""))
                            {
                                errorLAbel.Content = "";
                            }
                            else
                            {
                                errorLAbel.Content = "Server is slow";
                            }
                        });
                    }
                    catch (Exception e4)
                    {
                        string message = e4.Message;
                    }
                }
            };
        }

        private void ClickAnimation()
        {
            Thickness m = flyingAnimation.Margin;
            //start - Margin="-7,458,979,7.6"
            m.Top = 458;
            m.Right = 979;
            flyingAnimation.Margin = m;
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(DispacherTimer_Tick);
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(12);
            dispatcherTimer.Start();
        }

        private void DispacherTimer_Tick(object sender, EventArgs e)
        {
            Thickness m = flyingAnimation.Margin;
            m.Top -= 10;
            m.Right -= 20;
            flyingAnimation.Margin = m;
            // End of screen.
            if (flyingAnimation.Margin.Right < -1600 || flyingAnimation.Margin.Top < -1600)
            {
                dispatcherTimer.Stop();
                m.Top = 458;
                m.Right = 979;
                flyingAnimation.Margin = m;
            }
        }

        private void Connect_Click(object sender, RoutedEventArgs e)
        {
            if (!isConnected)
            {
                SetIsEnabled(false);
                isConnected = true;
                isDisConnected = false;
                errorLAbel.Content = "";
                // Animation of a paper airplane.
                ClickAnimation();
                // Connect.
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
            SetIsEnabled(true);
            isDisConnected = true;
            errorLAbel.Content = "";
            // Disconnect.
            connectVM.Disconnect();
        }

        private void SetIsEnabled(bool value)
        {
            Connect.IsEnabled = value;
            Disconnect.IsEnabled = !value;
            Setting.IsEnabled = value;
        } 

    }
}
