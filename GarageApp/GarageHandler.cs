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

        public Garage<Vehicle> garage { get; set; }
        public Garage<Vehicle> parkinglot { get; set; }

        public void StartOfWorld()
        {
            garage = new Garage<Vehicle>
            {
                name = "Grand Garage",
                vehicles = new List<Vehicle>
                {
                    new Car(1, "Ford Escort", "vit", 825),
                    new Car(2, "Audi R8", "black", 790),
                    new Motorcycle(3, "Honda", "red", 150),
                    new OneWheeler(4, "Wheely", "yellow", 4)
                }
            };

        }

        internal object TryGetVehicle(int id)
        {
            Vehicle found = null;
            IEnumerable<Vehicle> vehicles = this.garage.vehicles
                .Where(v => v.id == id)
                .Select(b => b);
            if (vehicles.Count() > 0)
                found = vehicles.First();
            return found;
        }

        internal Vehicle TryDeleteVehicle(int id)
        {
            Vehicle vehicle = (Vehicle) TryGetVehicle(id);
            if (vehicle != null)
            {
                this.garage.vehicles =
                    this.garage.vehicles
                        .Where(v => v.id != id)
                        .Select(b => b);
            }
            return vehicle;
        }
    }
}
