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
        FlightSimulatorViewModel vm;
        public MainWindow()
        {
            InitializeComponent();
            vm = new FlightSimulatorViewModel(new MyFlightSimulatorModel(new MyTelnetClient()));
            //Binding binding = new Binding("VM_Rudder");
            //binding.Source = joystick.knobPosition.X;
            vm.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                e.getName
            };

            DataContext = vm;
        }

        private void throttle_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }
    }
}
