using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace GarageApp
{
    class GarageHandler
    {
        public GarageHandler()
        {
            StartOfWorld();
        }

        public Garage garage { get; set; }
        public Garage parkinglot { get; set; }

        public void StartOfWorld()
        {
            garage = new Garage{ name = "Grand Garage"};
            garage.vehicles.Add(new Car { name = "Audi R8", color = "black", weight = 850 });
            garage.vehicles.Add(new Motorcycle() { name = "Honda", color = "red", weight = 150 });
            garage.vehicles.Add(new OneWheeler() { name = "Wheel", color = "yellow", weight = 4 });
        }
    }
}
