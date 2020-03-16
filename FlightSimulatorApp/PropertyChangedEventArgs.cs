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
        public PropertyChangedEventArgs(string propName)
        {
            this.propertyName = propName;
        }
        string getName()
        {
            return propertyName;
        }
    }
}
