using System;
using System.Collections.Generic;

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

    }
}
