using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageApp
{
    class Vehicle
    {
        public string name { get; set; }
        public string color { get; set; }
        public int weight { get; set; }

        public override string ToString()
        {
            return String.Format("{0} {1}  ({2} kg)", color, name, weight);
        }
    }
}
