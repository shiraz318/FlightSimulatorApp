using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp
{
    class PropertyChangedEventArgs
    {
        private string propertyName;
        private double value = 0;
        public PropertyChangedEventArgs(string propName)
        {
            this.propertyName = propName;
        }
        public string getName()
        {
            return propertyName;
        }
        public void setValue(double val)
        {
            value = val;
        }
        public double getValue()
        {
            return value;
        }
    }
}
