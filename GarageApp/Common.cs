using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace GarageApp
{
    public class MenuBuilder
    {
        public enum ActionType { noop, enter, back, quit, route, edit, delete, info }
        private static ActionType route  = ActionType.route;
        private static ActionType info   = ActionType.info;
        private static ActionType quit   = ActionType.quit;
        private static ActionType edit   = ActionType.edit;
        private static ActionType delete = ActionType.delete;


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
                    new MenuAction(ActionType.route, "vehicleOptions"), vehicle.id);
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
            return new MenuItem(garage.ToString(), new MenuAction(MenuBuilder.ActionType.route, "garageOptions"), garage.id);
        }


        internal static void UpdateAllMenus(GarageHandler gh, MenuHandler mh)
        {
            if (!mainLoadedOnce)
                MenuBuilder.UpdateMainMenu(gh, mh);
            MenuBuilder.UpdateGarageMenu(gh, mh);
            MenuBuilder.UpdateGroupMenu(gh, mh);
            MenuBuilder.UpdateVehicleMenu(gh, mh);
        }


        public static bool mainLoadedOnce = false;
        internal static void UpdateMainMenu(GarageHandler gh, MenuHandler mh)
        {
            mh.AddMenu(new Menu("main", "Huvudmeny", new List<MenuItem> {
                        new MenuItem("Garage",             new MenuAction(route, "garageIndex")),
                        new MenuItem("Lista alla grupper", new MenuAction(route, "allGroupIndex")),
                        new MenuItem("Lista fordon",       new MenuAction(route, "vehicleIndex")),
                        new MenuItem("Lägg till fordon",   new MenuAction(route, "vehicleAdd")),
                        new MenuItem("Sök på Regnr",       new MenuAction(route, "searchRegnr")),
                        new MenuItem("Databas",            new MenuAction(route, "fileOptions")),
                        new MenuItem("Quit",               new MenuAction(quit)) }));

            mh.AddMenu(new Menu("vehicleOptions", "Fordonsåtgärd", new List<MenuItem> {
                        new MenuItem("Information",        new MenuAction(info,   "infoVehicle")),
                        new MenuItem("Redigera",           new MenuAction(edit,   "editVehicle")),
                        new MenuItem("Ta bort",            new MenuAction(delete, "deleteVehicle")) }));

            mh.AddMenu(new Menu("fileOptions", "Databasåtgärder", new List<MenuItem> {
                        new MenuItem("Spara till db",      new MenuAction(route, "save")),
                        new MenuItem("Töm",                new MenuAction(route, "clear")),
                        new MenuItem("Ladda testData",     new MenuAction(route, "reset")),
                        new MenuItem("Ladda från db",      new MenuAction(route, "load")) }));

            MenuBuilder.mainLoadedOnce = true;
        }


        internal static void UpdateGroupMenu(GarageHandler gh, MenuHandler mh)
        {
            mh.menus.Remove("allGroupIndex");
            mh.AddMenu(new Menu("allGroupIndex", "Välj grupp",  MenuBuilder.VehiclesToMenuItemsByGroup(gh.GetCurrentGarage().vehicles)));
        }


        internal static void UpdateGarageMenu(GarageHandler gh, MenuHandler mh)
        {
            mh.menus.Remove("garageIndex");
            mh.AddMenu(new Menu("garageIndex", "Välj garage", MenuBuilder.GaragesToMenuItems(gh.garages)));
        }


        internal static void UpdateVehicleMenu(GarageHandler gh, MenuHandler mh)
        {
            mh.menus.Remove("vehicleIndex");
            mh.AddMenu(new Menu("vehicleIndex", "Välj fordon", MenuBuilder.VehiclesToMenuItems(gh.GetCurrentGarage().vehicles)));
        }


        internal static IEnumerable<MenuItem> VehiclesToMenuItemsByGroup(IEnumerable<Vehicle> vehicles)
        {
            List<MenuItem> menuItems = new List<MenuItem>();
            Vehicle found = vehicles.FirstOrDefault();
            string typ = ConsoleHelper.GetTypeOf(found);
            IEnumerable<IGrouping<string, Vehicle>> query = vehicles.GroupBy(v => ConsoleHelper.GetTypeOf(v));

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
            return new MenuItem(label, new MenuAction(MenuBuilder.ActionType.route, "groupOptions"));
        }
    }
}
