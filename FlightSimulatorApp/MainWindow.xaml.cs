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
        MapVM mapVM;
        public MainWindow()
        {
            InitializeComponent();
            MyTelnetClient mtc = new MyTelnetClient();
            MyFlightSimulatorModel mfsm = new MyFlightSimulatorModel(mtc);
            dashboardVM = new DashboardVM(mfsm);
            wheelVM = new WheelVM(mfsm);
            mapVM = new MapVM(mfsm);
            DataContext = dashboardVM;
            wheel.positionChanged += delegate (Object sender, PositionChangedEventArgs e)
            {
                //
                if (e.getName().Equals("Throttle"))
                {
                    wheelVM.VM_Throttle = e.getValue();
                }//
                else if (e.getName().Equals("Aileron"))
                {
                    wheelVM.VM_Aileron = e.getValue();
                }
                else if (e.getName().Equals("Rudder"))
                {
                    wheelVM.VM_Rudder = e.getValue();
                }
                else if (e.getName().Equals("Elevator"))
                {
                    wheelVM.VM_Elevator = e.getValue();
                }
            };
           
        }
    }
}
