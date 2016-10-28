using System.Collections.Generic;
using System.Linq;
// using static GarageApp.MenuBuilder;  // Kräver C# 6

namespace GarageApp
{
    class MenuConverter
    {

        internal static IEnumerable<MenuItem> VehiclesToMenuItems(IEnumerable<Vehicle> vehicles)
        {
            List<MenuItem> menuItems = new List<MenuItem>();

            foreach (Vehicle vehicle in vehicles)
                menuItems.Add(VehicleToMenuItem(vehicle));

            return menuItems;
        }


        internal static MenuItem VehicleToMenuItem(Vehicle vehicle)
        {
            return new MenuItem(vehicle.ToString(),
                    //new MenuAction(MenuBuilder.ActionType.Route, "vehicleOptions"), vehicle.id);
                    new MenuAction(MenuBuilder.ActionType.Edit, "vehicle"), vehicle.id);
        }


        internal static IEnumerable<MenuItem> VehicleToDetailedMenuItems(Vehicle vehicle)
        {
            List<MenuItem> menuItems = new List<MenuItem>();

            menuItems.Add(FieldToMenuItem(string.Format("{0,14} {1}", "Id:", vehicle.id)));
            menuItems.Add(FieldToMenuItem(string.Format("{0,14} {1}", "Namn:", vehicle.name)));
            menuItems.Add(FieldToMenuItem(string.Format("{0,14} {1} kg", "Vikt:", vehicle.weight)));
            menuItems.Add(FieldToMenuItem(string.Format("{0,14} {1}", "Färg:", vehicle.color)));
            menuItems.Add(FieldToMenuItem(string.Format("{0,14} {1}", "Regnr:", vehicle.regnr)));
            menuItems.Add(FieldToMenuItem(string.Format("{0,14} {1}", "Fordonstyp:", vehicle.MyType)));

            menuItems.Add(new MenuItem(""));

            return menuItems;
        }


        internal static MenuItem FieldToMenuItem(string field)
        {
            return new MenuItem(field, new MenuAction(MenuBuilder.ActionType.Route, "fieldEdit"));
        }


        internal static IEnumerable<MenuItem> GaragesToMenuItems(List<Garage<Vehicle>> garages)
        {
            List<MenuItem> menuItems = new List<MenuItem>();
            // menuItems.Add(new MenuItem("(alla)", new MenuAction(Common.ActionType.route, "allaGarage")));

            foreach (Garage<Vehicle> garage in garages)
                menuItems.Add(GarageToMenuItem(garage));

            return menuItems;
        }


        internal static MenuItem GarageToMenuItem(Garage<Vehicle> garage)
        {
            return new MenuItem(garage.ToString(), new MenuAction(MenuBuilder.ActionType.Route, "garageOptions"), garage.Id);
        }


        internal static IEnumerable<MenuItem> VehiclesToMenuItemsByGroup(IEnumerable<Vehicle> vehicles)
        {
            List<MenuItem> menuItems = new List<MenuItem>();
            var enumerable = vehicles as IList<Vehicle> ?? vehicles.ToList();
            //Vehicle found = enumerable.FirstOrDefault();
            //string typ = ConsoleHelper.GetTypeOf(found);
            IEnumerable<IGrouping<string, Vehicle>> query = enumerable.GroupBy(ConsoleHelper.GetTypeOf);

            foreach (var entry in query)
            {
                menuItems.Add(GroupToMenuItem(entry));

                // Todo: Kombinerad Gruppvy
                // foreach (var vehicle in groupedVehicles) // Vi har åtkomst till fordon per grupp
                //    Console.WriteLine(vehicle);           // Ifall vi skulle vilja lista i annan vy
            }
            return menuItems;
        }


        private static MenuItem GroupToMenuItem(IGrouping<string, Vehicle> entry)
        {
            string name = entry.Key;
            int count = entry.ToList().Count;
            string label = string.Format("{0,10} ({1})", name, count);
            return new MenuItem(label, new MenuAction(MenuBuilder.ActionType.Route, "groupOptions"));
        }
    }
}
