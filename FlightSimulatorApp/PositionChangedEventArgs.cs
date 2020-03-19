using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp
{
    public class PositionChangedEventArgs
    {
        private string position;
        private double value;
        public PositionChangedEventArgs(string pos, double val)
        {
            this.position = pos;
            this.value = val;
        }
        public string getName()
        {
            return position;
        }
        public double getValue()
        {
            return value;
        }
    }
}
