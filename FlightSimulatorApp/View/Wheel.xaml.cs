using FlightSimulatorApp.Model;
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
    /// Interaction logic for Wheel.xaml
    /// </summary>
    public partial class Wheel : UserControl
    {
        WheelVM wheelViewModel;

        public Wheel()
        {   
            InitializeComponent();
        }

        // Set the view model and data context.
        public void SetViewModel(WheelVM wheelVM)
        {
            wheelViewModel = wheelVM;
            DataContext = wheelViewModel;
        }

        // Reset the sliders.
        public void ResetSliders()
        {
            throttle.Value = 0;
            aileron.Value = 0;
        }
    }
}
