using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace GarageApp
{
    class GarageHandler
    {
        public List<Garage<Vehicle>> garages  { get; set; }
        public int                   garageId { get; set; }

        public GarageHandler()
        {
            garages = new List<Garage<Vehicle>>();
            // FactorySample();
            // FactoryEmptyGarage();
            LoadFromDb();
        }

        internal object TryGetVehicle(int id)
        {
            Vehicle found = null;
            IEnumerable<Vehicle> vehiclesFound = GetCurrentGarage().Vehicles
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
                .Where(g => g.Id != id)
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
            var newVehicles = GetCurrentGarage().Vehicles
                .Where(v => v.id != id)
                .Select(b => b);

            foundG.Vehicles = newVehicles.ToList<Vehicle>();
            return vehicle;
        }


        internal void AddVehicle(Vehicle newVehicle)
        {
            var currentVehicles = GetCurrentGarage().Vehicles.ToList();
            currentVehicles.Add(newVehicle);
            GetCurrentGarage().Vehicles = currentVehicles;
        }


        internal void TrySetGarage(int id)
        {
            var found = garages.FirstOrDefault(g => g.Id == id);

            if (found != null)
                garageId = id;
            else
                ConsoleHelper.Announce("Kunde inte hitta garage #" + id);
        }


        public Garage<Vehicle> GetCurrentGarage()
        {
            return garages.FirstOrDefault(g => g.Id == garageId);
        }


        internal Vehicle FindVehicleByRegnr(string regnr)
        {
            var garage = GetCurrentGarage();
            var vehicles = garage.Vehicles;

            if (!vehicles.Any()) return null;

            //todo: fixa krash då garaget är tomt
            return vehicles
                    .FirstOrDefault(v => string.Equals(v.regnr,
                                                         regnr,
                                                         StringComparison.CurrentCultureIgnoreCase));
        }

        internal IEnumerable<Vehicle> FindVehicles(string searchString)
        {
            var garage    = GetCurrentGarage();
            var vehicles  = garage.Vehicles;
            var lowSearch = searchString.ToLower();

            if (!vehicles.Any()) return null;

            List<Vehicle> safeVehicles = new List<Vehicle>();

            foreach (var vehicle in vehicles)
            {
                var tempVehicle = vehicle;

                if (vehicle.regnr == null)
                    tempVehicle.regnr = "";

                safeVehicles.Add(tempVehicle);
            }

            return safeVehicles.Where(v =>
                (string.IsNullOrEmpty(searchString)
                || (v.name.ToLower().Contains(lowSearch)
                 || v.regnr.ToLower().Contains(lowSearch)
                 || v.weight.ToString().ToLower().Contains(lowSearch)
                 || v.color.ToLower().Contains(lowSearch)
                )
            ));
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
                    garageId = firstGarage.Id;
                else
                    error = true;
            }
            else
                error = true;

            if (error)
            {
                HandleDbMissing();
            }
            else
            {
                ResetGarageNextId();
                ResetVehicleNextId();
            }
        }

        private void HandleDbMissing()
        {
                const string msg = "Databasfilen kunde inte hittas.\nSkapar en ny åt dig (garages.json under din användarkatalog)..";
                ConsoleHelper.Announce(msg);
                FactoryEmptyGarage();
                FileHandler.SaveAllGarages(this.garages);
        }

        private void ResetGarageNextId()
        {
            var newNextId = 0;

            // Todo byt ut foreach till LINQ
            // int newNextId = garages.MaxBy(x => x.Height);

            foreach (var garage in garages)
            {
                if (garage.Id > newNextId)
                    newNextId = garage.Id + 1;
            }
            Garage<Vehicle>.NextId = newNextId + 1;
        }

        private void ResetVehicleNextId()
        {
            var newNextId = 0;

            // Todo byt ut foreach till LINQ
            // int newNextId = vehicles.MaxBy(x => x.Height);

            foreach (var garage in garages)
            {
                foreach (var vehicle in garage.Vehicles)
                {
                    if (vehicle.id > newNextId)
                        newNextId = vehicle.id + 1;
                }
            }
            Vehicle.NextId = newNextId;
        }


        public void LoadFromSample()
        {
            garages.Clear();

            var garage = new Garage<Vehicle>("Factory Grand Garage")
            {
                Id = 10,
                Vehicles = new List<Vehicle> {
                    new Car("Ford Escort", "vit", "YEO403", 825),
                    new Car("Audi R8", "black", "SUP775", 790),
                    new Motorcycle("Honda", "red", "AY16", 150),
                    new OneWheeler("Wheely", "yellow", "CY15", 4)
                }
            };
            garages.Add(garage);

            garage = new Garage<Vehicle>("Factory Deluxe Hangar")
            {
                Id = 11,
                Vehicles = new List<Vehicle> {
                    new Car("Ferarri Testarossa", "gul", "SKE001", 825),
                    new Car("Audi R8", "black", "KUP006", 790),
                    new Motorcycle("Honda", "red", "AY305", 150),
                    new Airplane("Flyer", "pink", "P3001", 1600),
                    new Buss("TaxiBuss", "svart", "BB0011", 3000)
                }
            };
            garages.Add(garage);
            garageId = garages.First().Id;
        }


        public void FactoryEmptyGarage()
        {
            garages.Clear();

            var g1 = new Garage<Vehicle>("Testgaraget");
            // g1.vehicles = new List<Vehicle> { new Car(999, "Skrothög", "brun", "ZZZ001", 100) };
            garages.Add(g1);
            garageId = g1.Id;
        }

    }

}
