using FlightSimulatorApp.Model;
using FlightSimulatorApp.ViewModel;
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

namespace FlightSimulatorApp.View
{
    /// <summary>
    /// Interaction logic for Map.xaml
    /// </summary>
    public partial class MapControl : UserControl
    {
        private MapVM mapVM;
        public MapControl()
        {
            InitializeComponent();
            MyTelnetClient mtc = new MyTelnetClient();
            MyFlightSimulatorModel mfsm = new MyFlightSimulatorModel(mtc);
            mapVM = new MapVM(mfsm);
        }

        private void Connect_Click(object sender, RoutedEventArgs e)
        {
            //default ip and port
            string ip = "";
            int port = 5402;
            mapVM.connect(ip, port);
        }

        private void Setting_Click(object sender, RoutedEventArgs e)
        {
            //open a window and collect data from user
            Setting s = new Setting();
        }
    }
}
