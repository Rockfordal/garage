using System.Collections.Generic;
using System.Linq;

namespace GarageApp
{
    class GarageHandler
    {
        public GarageHandler()
        {
            garages = new List<Garage<Vehicle>>();
            //FactorySample();
            FactoryEmptyGarage();
            LoadFromDb();
        }

        public List<Garage<Vehicle>> garages  { get; set; }
        public int                   garageId { get; set; }


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
                if (index != -1)
                {
                    Garage<Vehicle> foundG = garages[index];
                    IEnumerable<Vehicle> newVehicles = GetCurrentGarage().vehicles
                            .Where(v => v.id != id)
                            .Select(b => b);

                    foundG.vehicles = newVehicles.ToList<Vehicle>();
                }
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
                ConsoleHelper.Announce("Kunde inte hitta garage #" + id);
        }


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


        public void SaveToDb()
        {
            FileHandler.SaveAllGarages(this.garages);
        }


        public void LoadFromDb()
        {
            bool error = false;
            this.garages.Clear();
            int c = this.garages.Count();
            int garageCount = FileHandler.LoadAllGarages(this);

            if (garageCount > 0)
            {
                Garage<Vehicle> firstg = garages.FirstOrDefault(g => true);
                if (firstg != null)
                    garageId = firstg.id;
                else
                    error = true;
            }
            else
                error = true;

            if (error)
            {
                string msg = @"Ett problem uppstod när data skulle laddas.\n
                               Kontrollera att din datafil finns på rätt ställe,\n
                               eller välj Databas\n Spara för att skapa en ny.";
                ConsoleHelper.Announce(msg);
                FactoryEmptyGarage();
            }
        }


        public void LoadFromSample()
        {
            garages.Clear();

            Garage<Vehicle> garage;
            garage = new Garage<Vehicle>("Factory Grand Garage")
            {
                id = 10,
                vehicles = new List<Vehicle> {
                    new Car(201, "Ford Escort", "vit", "YEO403", 825),
                    new Car(202, "Audi R8", "black", "SUP775", 790),
                    new Motorcycle(203, "Honda", "red", "AY16", 150),
                    new OneWheeler(204, "Wheely", "yellow", "CY15", 4)
                }
            };
            garages.Add(garage);

            garage = new Garage<Vehicle>("Factory Deluxe Hangar")
            {
                id = 11,
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
