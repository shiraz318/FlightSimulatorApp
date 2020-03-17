using System;
using System.Windows.Controls;
using System.Windows.Input;

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
            if (!mousePressed)
            {
                mousePressed = true;
            }
        }

        private void Joystick_MouseUp(object sender, MouseButtonEventArgs e)
        {
            mousePressed = false;
            knobPosition.X = 0;
            knobPosition.Y = 0;
        }

        private void Joystick_MouseMove(object sender, MouseEventArgs e)
        {
            if (mousePressed)
            {
                knobPosition.X = -25;
                knobPosition.Y = -25;
            }
        }
    }
}
