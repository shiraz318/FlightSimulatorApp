using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace FlightSimulatorApp.View
{

    //public delegate void PositionChanged(Object sender, PositionChangedEventArgs e);
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

        private double startX, startY, currentX, currentY, positionY = 0, positionX = 0;

        public double PositionX
        {
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
            }
        }



        public double RudderX
        {
            get { return (double)GetValue(RudderXProperty); }
            set { SetValue(RudderXProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RudderX.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RudderXProperty =
            DependencyProperty.Register("RudderX", typeof(double), typeof(Joystick));


        public Joystick()
        {
            InitializeComponent();
            //rudderLable.Content = PositionX;
            //elevatorLable.Content = PositionY;
        }

        private void CenterKnob_Completed(object sender, EventArgs e)
        {
        }

        private void Joystick_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!mousePressed)
            {
                mousePressed = true;
                Knob.CaptureMouse();
                startX = e.GetPosition(this).X;
                startY = e.GetPosition(this).Y;
            }
        }

        private void Joystick_MouseUp(object sender, MouseButtonEventArgs e)
        {
            mousePressed = false;
            UIElement element = (UIElement)Knob;
            element.ReleaseMouseCapture();
            knobPosition.X = 0;
            knobPosition.Y = 0;
            //if we want that when the user is in mouseup state - the text field will not be 0, which means it will not reset, so we just delete the following lines.
            PositionY = 0;
            PositionX = 0;
           // rudderLable.Content = PositionX;
           // elevatorLable.Content = PositionY;
        }

        private void Joystick_MouseMove(object sender, MouseEventArgs e)
        {
            if (mousePressed)
            {
                currentX = e.GetPosition(this).X;
                currentY = e.GetPosition(this).Y;
                PositionX = currentX - startX;
                PositionY = currentY - startY;
                //rudderLable.Content = PositionX / RANGE;
                //elevatorLable.Content = PositionY / -RANGE;
                RudderX = PositionX / RANGE;
                knobPosition.X = PositionX;
                knobPosition.Y = PositionY;
            }
        }
    }
}
