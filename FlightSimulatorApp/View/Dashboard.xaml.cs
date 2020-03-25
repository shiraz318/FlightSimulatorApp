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
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : UserControl
    {
        public Dashboard()
        {
            InitializeComponent();
           
        }
        public void reset()
        {
            this.Dispatcher.Invoke(() =>
            {
                airspeed_indicator_indicate_speed.Content = 0;
                gps_indicated_ground_speed.Content = 0;
                gps_indicated_vertical_speed.Content = 0;
                indicated_heading_deg.Content = 0;
                gps_indicated_altitude.Content = 0;
                attitude_indicator_internal_roll_deg.Content = 0;
                attitude_indicator_internal_pitch_deg.Content = 0;
                altimeter_indicated_altitude.Content = 0;
            });

           
        }
    }
}
