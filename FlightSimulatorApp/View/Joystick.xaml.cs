using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace FlightSimulatorApp.View
{
	/// <summary>
	/// Interaction logic for Joystick.xaml
	/// </summary>
	public partial class Joystick : UserControl
	{
		public const double Range = 80;
		private double radius = Range / 2;
		private bool mousePressed = false;
		private double startX, startY, currentX, currentY, positionY = 0, positionX = 0;
		private readonly Storyboard centerKnob;
		// Properties.
		public double PositionX { get { return positionX; } set { positionX = value; } }
		public double PositionY { get { return positionY; } set { positionY = value; } }
	    public double ValueX
		{
			get { return (double)GetValue(ValueXProperty); }
			set { SetValue(ValueXProperty, value); }
		}

		// Using a DependencyProperty as the backing store for RudderX.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty ValueXProperty =
			DependencyProperty.Register("ValueX", typeof(double), typeof(Joystick));
		public double ValueY
		{
			get { return (double)GetValue(ValueYProperty); }
			set { SetValue(ValueYProperty, value); }
		}

		// Using a DependencyProperty as the backing store for ElevatorY.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty ValueYProperty =
			DependencyProperty.Register("ValueY", typeof(double), typeof(Joystick));

		public Joystick()
		{
			InitializeComponent();
			centerKnob = Knob.Resources["CenterKnob"] as Storyboard;
		}

		private void CenterKnob_Completed(object sender, EventArgs e)
		{
		}

		// Define the action when the mouse is down.
		private void Joystick_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (!mousePressed)
			{
				mousePressed = true;
				Knob.CaptureMouse();
				// Initilaze the begining point.
				startX = e.GetPosition(this).X;
				startY = e.GetPosition(this).Y;
				centerKnob.Stop();
			}
		}

		// Define the action when the mouse is up.
		private void Joystick_MouseUp(object sender, MouseButtonEventArgs e)
		{
			mousePressed = false;
			UIElement element = (UIElement)Knob;
			element.ReleaseMouseCapture();

			// Reset the knob to the center (0,0).
			PositionY = 0;
			PositionX = 0;
			ValueX = 0;
			ValueY = 0;
			centerKnob.Begin();
		}

		// Define the action when the mouse moves.
		private void Joystick_MouseMove(object sender, MouseEventArgs e)
		{
			if (mousePressed)
			{
				// The place now.
				currentX = e.GetPosition(this).X;
				currentY = e.GetPosition(this).Y;
				double x = currentX - startX;
				double y = currentY - startY;
				Point p = UpdatePosition(x, y);
				PositionX = p.X;
				PositionY = p.Y;
				ValueX = PositionX / radius ;
				ValueY= -PositionY / radius;
				// Update the knob.
				knobPosition.X = PositionX;
				knobPosition.Y = PositionY;
			}
		}

		// Update the position of the joystick center.
		private Point UpdatePosition(double x, double y)
		{
			double pow = Math.Pow(x, 2) + Math.Pow(y, 2);
			double basePow = Math.Pow(radius, 2);
			if (pow <= basePow)
			{
				// Valid.
				return new Point(x, y);
			}
			else
			{
				// Outside the circle.
				return ClosestIntersection(new Point(-x, -y));
			}
		}
		
		// Find the closest intersection point.
		public Point ClosestIntersection(Point currentPoint)
		{
			Point inter1;
			Point inter2;
			// Calculate the intersection points.
			CalculateIntersections(currentPoint, out inter1, out inter2);
			double dist1 = Math.Sqrt((inter1.X * inter1.X) + (inter1.Y * inter1.Y));
			double dist2 = Math.Sqrt((inter2.X * inter2.X) + (inter2.Y * inter2.Y));
			// Returns the closest point.
			return (dist1 < dist2) ? inter1 : inter2;
		}
		
		// Calculate the intersection points.
		private void CalculateIntersections(Point currentPoint, out Point inter1, out Point inter2)
		{
			double dx, dy, A, C, delta, t;
			dx = currentPoint.X;
			dy = currentPoint.Y;
			// Calculate A,C for line equation.
			A = dx * dx + dy * dy;
			C = -radius * radius;
			delta = -4 * A * C;
			t = ((Math.Sqrt(delta)) / (2 * A));
			// Two options.
			inter1 = new Point(t * dx, t * dy);
			inter2 = new Point(-t * dx, -t * dy);
		}
	}
}
