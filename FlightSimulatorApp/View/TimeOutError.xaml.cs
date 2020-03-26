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

namespace FlightSimulatorApp.View
{
    /// <summary>
    /// Interaction logic for TimeOutError.xaml
    /// </summary>
    public partial class TimeOutError : Window
    {
        ConnectVM connectVM;
        public bool stay;
        public TimeOutError()
        {
            InitializeComponent();
        }

        private void Wait_Buttom_Click(object sender, RoutedEventArgs e)
        {
            stay = true;
            this.Close();
        }

        private void Disconnect_Buttom_Click(object sender, RoutedEventArgs e)
        {
            stay = false;
            connectVM.setStop(false);
            connectVM.disconnect();
            this.Close();
        }
        public void setVM(ConnectVM cvm)
        {
            connectVM = cvm;
        }
    }
}
