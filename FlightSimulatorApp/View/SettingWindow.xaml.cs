using System;
using System.Windows;

namespace FlightSimulatorApp.View
{
    /// <summary>
    /// Interaction logic for SettingWindow.xaml
    /// </summary>
    public partial class SettingWindow : Window
    {
        private bool isOk = false;

        public bool IsOk
        {
            get { return isOk; }
            set { isOk = value; }
        }

        public SettingWindow()
        {
            InitializeComponent();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            IsOk = false;
            this.Close();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            string ip = ipText.Text;
            int port = -1;
            if (Int32.TryParse(portText.Text, out int j))
            {
                port = j;
            }
            //and other validation checks if needed
           if ((port == -1) || (ip == "") || (portText.Text == ""))
            {
                validation.Content = "Invalid port or ip, please try again";
                //invalid
            } else
            {
                IsOk = true;
                this.Close();
            }           
        }
    }
}
