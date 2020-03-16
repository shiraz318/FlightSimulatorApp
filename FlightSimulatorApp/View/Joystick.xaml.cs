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
    /// Interaction logic for Joystick.xaml
    /// </summary>
    public partial class Joystick : UserControl
    {
        bool mousePressed = false;
        public Joystick()
        {
            InitializeComponent();
        }

        private void CenterKnob_Completed(object sender, EventArgs e)
        {
            
        }

        private void Joystick_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(!mousePressed)
            {
                mousePressed = true;
            }
        }

        private void Joystick_MouseUp(object sender, MouseButtonEventArgs e)
        {
            mousePressed = false;
            knobPosition.X = 170;
            knobPosition.Y = 170;
        }

        private void Joystick_MouseMove(object sender, MouseEventArgs e)
        {
            if(mousePressed)
            {
                knobPosition.X = 200;
                knobPosition.Y = 200;
            }
        }
    }
}
