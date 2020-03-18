using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace FlightSimulatorApp.View
{
    /// <summary>
    /// Interaction logic for Joystick.xaml
    /// </summary>
    public partial class Joystick : UserControl
    {
        private bool mousePressed = false;
        private double upBorder = 35;
        private double downBorder = -35;
        private double leftBorder = -35;
        private double rightBorder = 35;
        private double startX, startY, currentX, currentY, positionX, positionY;
        public double PositionX { 
            get
            {
                return positionX;
            }
            set
            {
                //check borders
                if (value > upBorder)
                {
                    positionX = upBorder;
                }
                else if (value < downBorder)
                {
                    positionX = downBorder;
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
                if (value < leftBorder)
                {
                    positionY = leftBorder;
                }
                else if (value > rightBorder)
                {
                    positionY = rightBorder;
                }
                else
                {
                    positionY = value;
                }
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
