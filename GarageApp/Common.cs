using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace GarageApp
{
    public class Common
    {
        public enum ActionType { noop, enter, back, quit, route, edit, delete, info }

        internal static IEnumerable<MenuItem> VehiclesToMenuItems(IEnumerable<Vehicle> vehicles)
        {
            List<MenuItem> menuItems = new List<MenuItem>();

            foreach (Vehicle vehicle in vehicles)
                menuItems.Add(VehicleToMenuItem(vehicle));

            return menuItems;
        }

        internal static MenuItem VehicleToMenuItem(Vehicle vehicle)
        {
            return new MenuItem(vehicle.ToString(), new MenuAction(Common.ActionType.route, "vehicleOptions"), vehicle.id);
        }

        internal static IEnumerable<MenuItem> GaragesToMenuItems(List<Garage<Vehicle>> garages)
        {
            List<MenuItem> menuItems = new List<MenuItem>();

            foreach (Garage<Vehicle> garage in garages)
                menuItems.Add(GarageToMenuItem(garage));

            return menuItems;
        }

        internal static MenuItem GarageToMenuItem(Garage<Vehicle> garage)
        {
            return new MenuItem(garage.ToString(), new MenuAction(Common.ActionType.route, "garageOptions"), garage.id);
        }

        internal static void UpdateGarageMenu(GarageHandler gh, MenuHandler mh)
        {
            mh.menus.Remove("garageIndex");
            mh.AddMenu(new Menu("garageIndex", "Välj garage", Common.GaragesToMenuItems(gh.garages)));
        }

        internal static void UpdateVehicleMenu(GarageHandler gh, MenuHandler mh)
        {
            mh.menus.Remove("vehicleIndex");
            mh.AddMenu(new Menu("vehicleIndex", "Välj fordon", Common.VehiclesToMenuItems(gh.GetCurrentGarage().vehicles)));
        }

        internal static IEnumerable<MenuItem> VehiclesToMenuItemsByGroup(IEnumerable<Vehicle> vehicles)
        {
            List<MenuItem> menuItems = new List<MenuItem>();
            Vehicle found = vehicles.FirstOrDefault();
            Type type = found.GetType();
            //if (type == typeof(Car)) ConsoleHelper.Announce("det är en bil");
            //ConsoleHelper.Announce(found.GetType().ToString());

            IEnumerable<IGrouping<Type, Vehicle>> query = vehicles.GroupBy(v => v.GetType());

            foreach (var entry in query)
            {
                //List<Vehicle> groupedVehicles = entry.ToList();
                //string name = entry.Key.ToString();
                //int vehicleCount = groupedVehicles.Count;
                //Console.WriteLine(name + " " + vehicleCount);
                menuItems.Add(GroupToMenuItem(entry));

                //foreach (var vehicle in groupedVehicles)
                //    Console.WriteLine(vehicle);
            }

            //ConsoleHelper.Pause();
            return menuItems;
        }

        private static MenuItem GroupToMenuItem(IGrouping<Type, Vehicle> entry)
        {
            string name = entry.Key.ToString();
            int count = entry.ToList().Count;
            string label = name + " (" + count + ")";
            return new MenuItem(label, new MenuAction(Common.ActionType.route, "groupOptions"));
        }
    }
}
