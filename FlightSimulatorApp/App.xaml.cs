using FlightSimulatorApp.Model;
using FlightSimulatorApp.View;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace FlightSimulatorApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        public void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = true;
            Environment.Exit(0);
        }
        public WheelVM WheelviewModel { get; internal set; }
        public DashboardVM DashboardviewModel { get; internal set; }
        public MapVM MapviewModel { get; internal set; }
        public ConnectVM ConnectviewModel { get; internal set; }
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            MyFlightSimulatorModel model = new MyFlightSimulatorModel();
            WheelviewModel = new WheelVM(model);
            DashboardviewModel = new DashboardVM(model);
            MapviewModel = new MapVM(model);
            ConnectviewModel = new ConnectVM(model);
        }
    }
}
