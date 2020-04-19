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
        MapVM mapViewModel;

         public MapControl()
        { 
            InitializeComponent();
        }
        
        public void SetViewModel(MapVM mapVM)
        {
            mapViewModel = mapVM;
            DataContext = mapViewModel;
        }

        private void UserControl_ContextMenuClosing(object sender, ContextMenuEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
