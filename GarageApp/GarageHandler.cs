using System.Collections.Generic;
using System.Linq;

namespace GarageApp
{
    class GarageHandler
    {
        public GarageHandler()
        {
            StartOfWorld();
        }

        public List<Garage<Vehicle>> garages    { get; set; }
        public int                   garageId   { get; set; }

        public void StartOfWorld()
        {
            garages = new List<Garage<Vehicle>>();
            Garage<Vehicle> garage;
            garage  = new Garage<Vehicle> {
                id = 10,
                name = "Grand Garage",
                vehicles = new List<Vehicle> {
                    new Car(201, "Ford Escort", "vit", "YEO403", 825),
                    new Car(202, "Audi R8", "black", "SUP775", 790),
                    new Motorcycle(203, "Honda", "red", "AY16", 150),
                    new OneWheeler(204, "Wheely", "yellow", "CY15", 4)
                }
            };
            garages.Add(garage);

            garage = new Garage<Vehicle> {
                id = 11,
                name = "Deluxe Hangar",
                vehicles = new List<Vehicle> {
                    new Car(101, "Ferarri Testarossa", "gul", "SKE001", 825),
                    new Car(102, "Audi R8", "black", "KUP006", 790),
                    new Motorcycle(103, "Honda", "red", "AY305", 150),
                    new Airplane(104, "Flyer", "pink", "P3001", 1600),
                    new Buss(205, "TaxiBuss", "svart", "BB0011", 3000)
                }
            };
            garages.Add(garage);
            garageId = garages.First().id;
        }

        internal object TryGetVehicle(int id)
        {
            Vehicle found = null;
            IEnumerable<Vehicle> vehicles = GetCurrentGarage().vehicles
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
                var currentGarage = GetCurrentGarage();
                var index = garages.IndexOf(currentGarage);
                garages[index].vehicles = 
                    GetCurrentGarage().vehicles
                        .Where(v => v.id != id)
                        .Select(b => b);
            }
            return vehicle;
        }

        internal void AddVehicle(Vehicle newVehicle)
        {
            List<Vehicle> list = new List<Vehicle>();

            foreach (Vehicle vehicle in GetCurrentGarage().vehicles)
                list.Add(vehicle);

            list.Add(newVehicle);
            GetCurrentGarage().vehicles = list;
        }

        internal void TrySetGarage(int id)
        {
            var found = garages.FirstOrDefault(g => g.id == id);

            if (found != null)
                garageId = id;
            else
                ConsoleHelper.Announce("Kunde inte hitta garage " + id);
        }
        //ConsoleHelper.Pause();

        public Garage<Vehicle> GetCurrentGarage()
        {
            return garages.FirstOrDefault(g => g.id == garageId);
        }

        internal Vehicle FindVehicleByRegnr(string regnr)
        {
            return GetCurrentGarage()
                    .vehicles
                    .FirstOrDefault(v => v.regnr.ToLower() == regnr.ToLower());
        }
    }
}
