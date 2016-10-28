using System.Collections.Generic;

namespace GarageApp
{
    public class MenuBuilder
    {
        public enum ActionType { noop, enter, back, quit, route, edit, delete, info }
        private static ActionType route  = ActionType.route;
        private static ActionType quit   = ActionType.quit;
        private static ActionType edit   = ActionType.edit;
        private static ActionType delete = ActionType.delete;
        private static ActionType noop   = ActionType.noop;


        internal static void UpdateAllMenus(GarageHandler gh, MenuHandler mh)
        {
            if (!mainLoadedOnce)
            {
                MenuBuilder.UpdateMainMenu(gh, mh);
            }
            MenuBuilder.UpdateGarageMenu(gh, mh);
            MenuBuilder.UpdateGroupMenu(gh, mh);
            MenuBuilder.UpdateVehicleMenu(gh, mh);
        }

        public static bool mainLoadedOnce = false;
        internal static void UpdateMainMenu(GarageHandler gh, MenuHandler mh)
        {
            mh.AddMenu(new Menu("main", "Huvudmeny", new List<MenuItem> {
                        new MenuItem("Garage",             new MenuAction(route, "garageIndex")),
                        new MenuItem("Lista grupper",      new MenuAction(route, "allGroupIndex")),
                        //new MenuItem("Lista alla fordon (todo)",  new MenuAction(route, "vehicleIndex")),
                        new MenuItem("Lägg till fordon",   new MenuAction(route, "vehicleAdd")),
                        new MenuItem("Sök på Regnr",       new MenuAction(route, "searchRegnr")),
                        new MenuItem("Databas",            new MenuAction(route, "fileOptions")),
                        new MenuItem("Quit",               new MenuAction(quit)) }));

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
            var vehicles = gh.GetCurrentGarage().vehicles;
            mh.AddMenu(new Menu("allGroupIndex", "Välj grupp",
                            MenuConverter.VehiclesToMenuItemsByGroup(vehicles)));
        }


        internal static void UpdateGarageMenu(GarageHandler gh, MenuHandler mh)
        {
            mh.menus.Remove("garageIndex");
            mh.AddMenu(new Menu("garageIndex", "Välj garage",
                            MenuConverter.GaragesToMenuItems(gh.garages)));
        }


        internal static void UpdateVehicleMenu(GarageHandler gh, MenuHandler mh)
        {
            mh.menus.Remove("vehicleIndex");
            mh.AddMenu(new Menu("vehicleIndex", "Välj fordon",
                            MenuConverter.VehiclesToMenuItems(gh.GetCurrentGarage().vehicles)));
        }


        internal static void AddVehiclesToOptionsMenu(GarageHandler gh, MenuHandler mh, int id)
        {
            var items = new List<MenuItem>();
            var found = (Vehicle) gh.TryGetVehicle(id);

            items = (List<MenuItem>) MenuConverter.VehicleToDetailedMenuItems(found);

            //items.Add(new MenuItem("-----------", new MenuAction(noop,   "")));
            items.Add(new MenuItem("Redigera",    new MenuAction(edit,   "editVehicle")));
            items.Add(new MenuItem("Ta bort",     new MenuAction(delete, "deleteVehicle")));

            mh.menus.Remove("vehicleOptions");
            mh.AddMenu(new Menu("vehicleOptions", "Fordonsåtgärd", items));
        }

    }
}
