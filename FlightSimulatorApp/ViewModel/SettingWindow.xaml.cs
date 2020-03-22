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
using System.Windows.Shapes;

namespace FlightSimulatorApp.ViewModel
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

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            IsOk = false;
            this.Close();
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
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
