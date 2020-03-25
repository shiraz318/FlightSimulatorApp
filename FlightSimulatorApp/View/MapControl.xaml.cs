using FlightSimulatorApp.Model;
using FlightSimulatorApp.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        public const int LATITUDE_UP_BORDER = 90;
        public const int LATITUDE_DOWN_BORDER = -90;
        public const int LONGTUDE_DOWN_BORDER = -180;
        public const int LONGTUDE_UP_BORDER = 180;
        private MapVM mapVM;
         public MapControl()
        {
            InitializeComponent();
            pushPin.Location = new Microsoft.Maps.MapControl.WPF.Location(37.806029,-122.407007);
            
        }
        public void setVM(MapVM vm)
        {
            mapVM = vm;
            mapVM.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName.Equals("VM_Latitude"))
                {

                        //throw exception
                        this.Dispatcher.Invoke(() =>
                        {
                            double latitude = mapVM.VM_Latitude;
                            if (latitude <= LATITUDE_UP_BORDER && latitude >= LATITUDE_DOWN_BORDER)
                            {
                                pushPin.Location = new Microsoft.Maps.MapControl.WPF.Location(mapVM.VM_Latitude, pushPin.Location.Longitude);
                            } else
                            {
                                //latitudeLabel.Content = "Invalid Coordinate";
                            }

                        });
  
                   
                }
                else if (e.PropertyName.Equals("VM_Longtude"))
                {
                   
                        this.Dispatcher.Invoke(() => { 
                         double longtude = mapVM.VM_Longtude;
                            if (longtude <= LONGTUDE_UP_BORDER && longtude >= LONGTUDE_DOWN_BORDER)
                            {
                                pushPin.Location = new Microsoft.Maps.MapControl.WPF.Location(pushPin.Location.Latitude, mapVM.VM_Longtude);
                            }
                            else
                            {
                                //longtudeLabel.Content = "Invalid Coordinate";
                            }
                        });

                   
                }
            };
        }
        
    }
}
