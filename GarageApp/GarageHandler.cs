using System;
using System.Collections.Generic;
using System.Linq;

namespace GarageApp
{
    class GarageHandler
    {
        public GarageHandler()
        {
            garages = new List<Garage<Vehicle>>();
            // FactorySample();
            FactoryEmptyGarage();
            LoadFromDb();
        }

        public List<Garage<Vehicle>> garages  { get; set; }
        public int                   garageId { get; set; }


        internal object TryGetVehicle(int id)
        {
            Vehicle found = null;
            IEnumerable<Vehicle> vehiclesFound = GetCurrentGarage().vehicles
                .Where(v => v.id == id)
                .Select(b => b);
            if (vehiclesFound.Any())
                found = vehiclesFound.First();
            return found;
        }


        internal bool TryDeleteGarage(int id)
        {
            var currentGarage = GetCurrentGarage();
            var index = garages.IndexOf(currentGarage);
            if (index == -1) return false;

            var newGarages = garages
                .Where(g => g.id != id)
                .Select(b => b);

            this.garages = newGarages.ToList<Garage<Vehicle>>();
            return true;
        }


        internal Vehicle TryDeleteVehicle(int id)
        {
            var vehicle = (Vehicle) TryGetVehicle(id);
            if (vehicle == null) return null;

            var currentGarage = GetCurrentGarage();
            var index = garages.IndexOf(currentGarage);
            if (index == -1) return vehicle;

            var foundG = garages[index];
            var newVehicles = GetCurrentGarage().vehicles
                .Where(v => v.id != id)
                .Select(b => b);

            foundG.vehicles = newVehicles.ToList<Vehicle>();
            return vehicle;
        }


        internal void AddVehicle(Vehicle newVehicle)
        {
            var currentVehicles = GetCurrentGarage().vehicles.ToList();
            currentVehicles.Add(newVehicle);
            GetCurrentGarage().vehicles = currentVehicles;
        }


        internal void TrySetGarage(int id)
        {
            var found = garages.FirstOrDefault(g => g.id == id);

            if (found != null)
                garageId = id;
            else
                ConsoleHelper.Announce("Kunde inte hitta garage #" + id);
        }


        public Garage<Vehicle> GetCurrentGarage()
        {
            return garages.FirstOrDefault(g => g.id == garageId);
        }


        internal Vehicle FindVehicleByRegnr(string regnr)
        {
            var garage = GetCurrentGarage();
            var vehicles = garage.vehicles;

            if (!vehicles.Any()) return null;
            //ConsoleHelper.Announce(vehicles.Count);

            //todo: fixa krash då garaget är tomt
            return vehicles
                    .FirstOrDefault(v => string.Equals(v.regnr.ToLower(),
                                                         regnr.ToLower(),
                                                         StringComparison.Ordinal));
        }


        public void SaveToDb()
        {
            FileHandler.SaveAllGarages(this.garages);
        }


        public void LoadFromDb()
        {
            var error = false;
            garages.Clear();
            var garageCount = FileHandler.LoadAllGarages(this);

            if (garageCount > 0)
            {
                var firstGarage = garages.FirstOrDefault(g => true);
                if (firstGarage != null)
                    garageId = firstGarage.id;
                else
                    error = true;
            }
            else
                error = true;

            if (!error) return;

            const string msg = "Databasfilen kunde inte hittas.\nSkapar en ny åt dig (garages.json under din användarkatalog)..";
            ConsoleHelper.Announce(msg);
            FactoryEmptyGarage();
            FileHandler.SaveAllGarages(this.garages);
        }


        public void LoadFromSample()
        {
            garages.Clear();

            var garage = new Garage<Vehicle>("Factory Grand Garage")
            {
                id = 10,
                vehicles = new List<Vehicle> {
                    new Car("Ford Escort", "vit", "YEO403", 825),
                    new Car("Audi R8", "black", "SUP775", 790),
                    new Motorcycle("Honda", "red", "AY16", 150),
                    new OneWheeler("Wheely", "yellow", "CY15", 4)
                }
            };
            garages.Add(garage);

            garage = new Garage<Vehicle>("Factory Deluxe Hangar")
            {
                id = 11,
                vehicles = new List<Vehicle> {
                    new Car("Ferarri Testarossa", "gul", "SKE001", 825),
                    new Car("Audi R8", "black", "KUP006", 790),
                    new Motorcycle("Honda", "red", "AY305", 150),
                    new Airplane("Flyer", "pink", "P3001", 1600),
                    new Buss("TaxiBuss", "svart", "BB0011", 3000)
                }
            };
            garages.Add(garage);
            garageId = garages.First().id;
        }


        public void FactoryEmptyGarage()
        {
            garages.Clear();

            var g1 = new Garage<Vehicle>("Testgaraget");
            // g1.vehicles = new List<Vehicle> { new Car(999, "Skrothög", "brun", "ZZZ001", 100) };
            garages.Add(g1);
            garageId = g1.id;
        }

    }

}
