using FlightSimulatorApp.Model;
using FlightSimulatorApp.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

namespace FlightSimulatorApp.View
{
    /// <summary>
    /// Interaction logic for Map.xaml
    /// </summary>
    public partial class MapControl : UserControl
    {

        private MapVM viewModel;
         public MapControl()
        {
            InitializeComponent();
            // Initialize location and color of the push pin and the labels.
            //pushPin.Location = new Microsoft.Maps.MapControl.WPF.Location(37.806029,-122.407007);
            //latitudeLabel.Foreground = new SolidColorBrush(Color.FromRgb(166, 183, 243));
            //longtudeLabel.Foreground = new SolidColorBrush(Color.FromRgb(166, 183, 243));
        }

        public void Init(MyFlightSimulatorModel model)
        {
            viewModel = new MapVM(model);
            DataContext = viewModel;
           /* viewModel.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName.Equals("VM_Latitude"))
                {
                    try
                    {
                        this.Dispatcher.Invoke(() =>
                        {
                            double latitude = double.Parse(viewModel.VM_Latitude);
                            if (latitude <= LATITUDE_UP_BORDER && latitude >= LATITUDE_DOWN_BORDER)
                            {
                                pushPin.Location = new Microsoft.Maps.MapControl.WPF.Location(double.Parse(viewModel.VM_Latitude), pushPin.Location.Longitude);
                                latitudeLabel.Content = latitude;
                                latitudeLabel.Foreground = new SolidColorBrush(Color.FromRgb(166, 183, 243));
                            }
                            else
                            {
                                latitudeLabel.Content = "Invalid Coordinate";
                                latitudeLabel.Foreground = new SolidColorBrush(Color.FromRgb(205, 50, 50));
                            }

                        });
                    }
                    catch (TaskCanceledException e1)
                    {
                        Environment.Exit(0);
                    }
                }
                else if (e.PropertyName.Equals("VM_Longtude"))
                {
                    try
                    {
                        this.Dispatcher.Invoke(() =>
                        {
                            double longtude = double.Parse(viewModel.VM_Longtude);
                            if (longtude <= LONGTUDE_UP_BORDER && longtude >= LONGTUDE_DOWN_BORDER)
                            {
                                pushPin.Location = new Microsoft.Maps.MapControl.WPF.Location(pushPin.Location.Latitude, double.Parse(viewModel.VM_Longtude));
                                longtudeLabel.Content = longtude;
                                longtudeLabel.Foreground = new SolidColorBrush(Color.FromRgb(166, 183, 243));
                            }
                            else
                            {
                                longtudeLabel.Content = "Invalid Coordinate";
                                longtudeLabel.Foreground = new SolidColorBrush(Color.FromRgb(205, 50, 50));
                            }
                        });
                    }
                    catch (TaskCanceledException e2)
                    {
                        Environment.Exit(0);
                    }
                }
            };*/
        }
        private void UserControl_ContextMenuClosing(object sender, ContextMenuEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
