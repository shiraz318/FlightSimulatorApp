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
            portLabel.Content = mainVM.Port;

           setMainVM();
        }

        // Set the mainVM propertyChanged.
        private void setMainVM() {

         mainVM.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName.Equals("VMError"))
                {
                   setVMError();
                    
                }
                else if (e.PropertyName.Equals("VMTimeOutError"))
                {
                    try
                    {
                        Application.Current.Dispatcher.Invoke((Action)delegate {
                            errorLAbel.Content = (mainVM.VMTimeOutError.Equals("") && mainVM.VMError.Equals("")) ?
                            "" : "Server is slow";
                        });
                    }
                    catch {}
                }
            };
        }

        // Set the errorLable in case of VMError.
        private void setVMError() {
            
            // If the user pressed disconnect button this is not an error.
            if (!isDisConnected)
            {
                try
                {
                     Application.Current.Dispatcher.Invoke((Action)delegate
                     {
                        if (mainVM.VMError.Equals(""))
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
                } catch {}
            }
        }

        // Set an animation of a paper plan that flies when connect button is pressed.
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

        // One action after the interval is passed.
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

        // Define the action when the connect button is pressed.
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

        // Define the action when the setting button is pressed.
        private void Setting_Click(object sender, RoutedEventArgs e)
        {
            SettingWindow setting = new SettingWindow();
            setting.ShowDialog();

            // If user entered ip and port to change and pressed ok.
            if (setting.IsOk)
            {
                mainVM.Ip = setting.ipText.Text;
                mainVM.Port = int.Parse(setting.portText.Text);
                ipLabel.Content = mainVM.Ip;
                portLabel.Content = mainVM.Port;
                setting.IsOk = false;
            }
        }

        // Define the action when the disconnect button is pressed.
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

        // Set the isEnable propperty of the connect, setting and disconnect buttons.
        private void SetIsEnabled(bool value)
        {
            Connect.IsEnabled = value;
            Disconnect.IsEnabled = !value;
            Setting.IsEnabled = value;
        } 
    }
}
