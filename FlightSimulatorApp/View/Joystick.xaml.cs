using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace FlightSimulatorApp.View
{
    
    public delegate void PositionChanged(Object sender, PositionChangedEventArgs e);
    /// <summary>
    /// Interaction logic for Joystick.xaml
    /// </summary>
    public partial class Joystick : UserControl
    {
        public const double UP_BOARDER = 35;
        public const double DOWN_BOARDER = -35;
        public const double LEFT_BOARDER = -35;
        public const double RIGHT_BOARDER = 35;
        public const double RANGE = 35;
        private bool mousePressed = false;
 
        private double startX, startY, currentX, currentY, positionX, positionY;
        public event PositionChanged positionChanged;
        public double PositionX { 
            get
            {
                return positionX;
            }
            set
            {
                //check borders
                if (value > UP_BOARDER)
                {
                    positionX = UP_BOARDER;
                }
                else if (value < DOWN_BOARDER)
                {
                    positionX = DOWN_BOARDER;
                }
                else
                {
                    positionX = value;
                }
                positionChanged(this, new PositionChangedEventArgs("X"));
            }
        }
        public double PositionY
        {
            get
            {
                return positionY;
            }
            set
            {
                //check borders
                if (value < LEFT_BOARDER)
                {
                    positionY = LEFT_BOARDER;
                }
                else if (value > RIGHT_BOARDER)
                {
                    positionY = RIGHT_BOARDER;
                }
                else
                {
                    positionY = value;
                }
                positionChanged(this, new PositionChangedEventArgs("Y"));
            }
        }
      
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
                startX = e.GetPosition(this).X;
                startY = e.GetPosition(this).Y;
            }
        }

        private void Joystick_MouseUp(object sender, MouseButtonEventArgs e)
        {
            mousePressed = false;
            knobPosition.X = 0;
            knobPosition.Y = 0;
            //if we want that when the user is in mouseup state - the text field will not be 0, which means it will not reset, so we just delete the following lines.
            PositionY = 0;
            PositionX = 0;
        }

        private void Joystick_MouseMove(object sender, MouseEventArgs e)
        {
            if (mousePressed)
            {
                currentX = e.GetPosition(this.Parent as UIElement).X;
                currentY = e.GetPosition(this.Parent as UIElement).Y;
                PositionX = currentX - startX;
                PositionY = currentY - startY;
                knobPosition.X = PositionX;
                knobPosition.Y = PositionY;
            }
        }
    }
}
