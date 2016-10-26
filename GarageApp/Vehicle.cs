using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageApp
{
    class Vehicle
    {
        public int id { get; set; }
        public string name { get; set; }
        public string color { get; set; }
        public int weight { get; set; }

        public Vehicle(int id, string name, string color, int weight)
        {
            this.id = id;
            this.name = name;
            this.color = color;
            this.weight = weight;
        }

        public override string ToString()
        {
            return String.Format("{0} {1}  ({2} kg)", color, name, weight);
        }
    }
}
