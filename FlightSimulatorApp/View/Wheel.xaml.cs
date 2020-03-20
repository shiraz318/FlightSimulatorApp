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
       // public event PositionChanged positionChanged;
        public Wheel()
        {
            InitializeComponent();
            /*
            joystick.PositionChanged += delegate(Object sender, PositionChangedEventArgs e) {
                double relativePosition;
            if (e.getName().Equals("X"))
                {
                    //we want the text field to represent the relative position.
                    relativePosition = joystick.PositionX / Joystick.RANGE;
                    rudderText.Text = relativePosition.ToString();
                    positionChanged(this, new PositionChangedEventArgs("Rudder", relativePosition));

                    //here we need to sent notification to the view model (not notification exactly but viewmodel.setRudder for example)
                }
            else if (e.getName().Equals("Y"))
                {
                    //for some reason, the y position is the negative.
                    relativePosition = -joystick.PositionY / Joystick.RANGE;
                    elevatorText.Text = relativePosition.ToString();
                    positionChanged(this, new PositionChangedEventArgs("Elevator", relativePosition));
                    //here we need to sent notification to the view model (not notification exactly but viewmodel.setElevator for example)

                }
            };*/
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }
    }
}
