using FlightSimulatorApp.Model;
using FlightSimulatorApp.ViewModel;
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

namespace FlightSimulatorApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DashboardVM dashboardVM;
        WheelVM wheelVM;
        public MainWindow()
        {
            InitializeComponent();
            MyTelnetClient mtc = new MyTelnetClient();
            MyFlightSimulatorModel mfsm = new MyFlightSimulatorModel(mtc);
            dashboardVM = new DashboardVM(mfsm);
            wheelVM = new WheelVM(mfsm);
            DataContext = dashboardVM;
        }

        private void throttle_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }
    }
}
