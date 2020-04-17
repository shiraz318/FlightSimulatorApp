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
        private MainVM mainVM;
        private DispatcherTimer dispatcherTimer;
        private bool isConnected;
        private bool isDisConnected;
       
        public MainWindow()
        {
            InitializeComponent();
            mainVM = (Application.Current as App).MainViewModel;

            // Initialize the Data Context.
            DataContext = mainVM;

            // Initialize ViewModels.
            wheel.SetViewModel(mainVM.WheelViewModel);
            dashboard.SetViewModel(mainVM.DashboardViewModel);
            map.SetViewModel(mainVM.MapViewModel);

            // Initialize connection.
            isConnected = false;
            isDisConnected = false;
            SetIsEnabled(true);

            // Initialize the ip and port.
            ipLabel.Content = mainVM.Ip;
            PortLabel.Content = mainVM.Port;

            mainVM.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName.Equals("VM_Error"))
                {
                    // If the user pressed disconnect button this is not an error.
                    if (!isDisConnected)
                    {
                        try
                        {
                            Application.Current.Dispatcher.Invoke((Action)delegate
                            {
                                if (mainVM.VM_Error.Equals(""))
                                {
                                    errorLAbel.Content = "";
                                }
                                else
                                {
                                    errorLAbel.Content = "Connection faulted Error";
                                    SetIsEnabled(true);
                                    isConnected = false;
                                    wheel.ResetSliders();
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

                            if (mainVM.VM_TimeOutError.Equals("") && mainVM.VM_Error.Equals(""))
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
                mainVM.Connect();
            }
        }

        private void Setting_Click(object sender, RoutedEventArgs e)
        {
            SettingWindow setting = new SettingWindow();
            setting.ShowDialog();

            // If user enteres ip and port to change and pressed ok.
            if (setting.IsOk)
            {
                mainVM.Ip = setting.ipText.Text;
                mainVM.Port = int.Parse(setting.portText.Text);
                ipLabel.Content = mainVM.Ip;
                PortLabel.Content = mainVM.Port;
                setting.IsOk = false;
            }
        }

        private void Disconnect_Click(object sender, RoutedEventArgs e)
        {
            isConnected = false;
            SetIsEnabled(true);
            isDisConnected = true;
            errorLAbel.Content = "";
            wheel.ResetSliders();

            // Disconnect.
            mainVM.Disconnect();
        }

        private void SetIsEnabled(bool value)
        {
            Connect.IsEnabled = value;
            Disconnect.IsEnabled = !value;
            Setting.IsEnabled = value;
        } 
    }
}
