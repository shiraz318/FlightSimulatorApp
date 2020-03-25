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
                    this.Dispatcher.Invoke(() =>
                    {
                        pushPin.Location = new Microsoft.Maps.MapControl.WPF.Location(mapVM.VM_Latitude, pushPin.Location.Longitude);

                    });
                   
                }
                else if (e.PropertyName.Equals("VM_Longtude"))
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        pushPin.Location = new Microsoft.Maps.MapControl.WPF.Location(pushPin.Location.Latitude, mapVM.VM_Longtude);
                    });
                   
                }
            };
        }
        
    }
}
