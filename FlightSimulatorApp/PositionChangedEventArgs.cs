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
        public PositionChangedEventArgs(string pos)
        {
            this.position = pos;
        }
        public string getName()
        {
            return position;
        }
    }
}
