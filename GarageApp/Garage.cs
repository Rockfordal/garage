using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageApp
{
    class Garage
    {
        public Garage()
        {
            //name = "";
            vehicles = new List<Vehicle>();
        }

        public string name { get; set; }
        public List<Vehicle> vehicles { get; set; }

    }
}
