using System.Collections.Generic;
using System.Linq;

namespace GarageApp
{
    public class MenuBuilder
    {
        public enum ActionType { Noop, Enter, Back, Quit, Route, Edit, Delete, Create, Search }

        private const ActionType Route  = ActionType.Route;
        private const ActionType Quit   = ActionType.Quit;
        private const ActionType Edit   = ActionType.Edit;
        private const ActionType Delete = ActionType.Delete;
        private const ActionType Create = ActionType.Create;
        private const ActionType Search = ActionType.Search;



        internal static void UpdateAllMenus(GarageHandler gh, MenuHandler mh)
        {
            MenuBuilder.UpdateMainMenu(gh, mh);
            MenuBuilder.UpdateGarageMenu(gh, mh);
            MenuBuilder.UpdateGroupMenu(gh, mh);
            MenuBuilder.UpdateVehicleMenu(gh, mh);
        }

        internal static void UpdateMainMenu(GarageHandler gh, MenuHandler mh)
        {
            mh.menus.Remove("main");
            // var mainLabel = "Huvudmeny (" + Vehicle.NextId + ")";
            var mainLabel = "Huvudmeny";
            var garageLabel = "Garage (" + gh.garages.Count + "st)";
            mh.AddMenu(new Menu("main", mainLabel, new List<MenuItem> {
                        new MenuItem(garageLabel,              new MenuAction(Route, "garageIndex")),
                        new MenuItem("Grupper",                new MenuAction(Route, "allGroupIndex")),
                        // new MenuItem("Lista alla fordon (todo)",  new MenuAction(route, "vehicleIndex")),
                        new MenuItem("Sök fordon",             new MenuAction(Search, "regnr")),
                        new MenuItem("Databas",                new MenuAction(Route, "fileOptions")),
                        new MenuItem("Avsluta utan att spara", new MenuAction(Quit)) }));

            mh.menus.Remove("fileOptions");
            mh.AddMenu(new Menu("fileOptions", "Databasåtgärder", new List<MenuItem> {
                        new MenuItem("Ladda från db",          new MenuAction(Route, "load")),
                        new MenuItem("Ladda testData",         new MenuAction(Route, "reset")),
                        new MenuItem("Töm",                    new MenuAction(Route, "clear")),
                        new MenuItem("Spara till db",          new MenuAction(Route, "save")) }));
        }


        internal static void UpdateGroupMenu(GarageHandler gh, MenuHandler mh)
        {
            mh.menus.Remove("allGroupIndex");
            var vehicles = gh.GetCurrentGarage().Vehicles;
            mh.AddMenu(new Menu("allGroupIndex", "Välj grupp",
                            MenuConverter.VehiclesToMenuItemsByGroup(vehicles)));
        }


        internal static void UpdateGarageMenu(GarageHandler gh, MenuHandler mh)
        {
            mh.menus.Remove("garageIndex");
            var items = (List<MenuItem>) MenuConverter.GaragesToMenuItems(gh.garages);

            mh.AddMenu(new Menu("garageIndex", "Välj garage", items));

            var labelRow = Garage<Vehicle>.ToHeader();
            items.Insert(0, new MenuItem(labelRow));

            items.Add(new MenuItem(""));
            items.Add(new MenuItem("Nytt garage", new MenuAction(Create, "garage")));
        }


        internal static void UpdateVehicleMenu(GarageHandler gh, MenuHandler mh)
        {
            mh.menus.Remove("vehicleIndex");
            var items = new List<MenuItem>();

            var currentGarage = gh.GetCurrentGarage();
            string garageName;

            if (currentGarage != null)
            {
                items = (List<MenuItem>) MenuConverter.VehiclesToMenuItems(currentGarage.Vehicles);
                garageName = currentGarage.Name;
            }
            else
            {
                garageName = "";
            }

            var labelRow = Vehicle.ToHeader();
            items.Insert(0, new MenuItem(labelRow));

            items.Add(new MenuItem(""));
            items.Add(new MenuItem("Nytt fordon",        new MenuAction(Create, "vehicle")));
            items.Add(new MenuItem("Byt namn på garage", new MenuAction(Edit,   "garage")));
            items.Add(new MenuItem("Ta bort garage",     new MenuAction(Delete, "garage")));

            mh.AddMenu(new Menu("vehicleIndex", garageName + " -> Välj fordon", items));
        }


        internal static void UpdateGroupResult(GarageHandler gh, MenuHandler mh, string typ)
        {
            mh.menus.Remove("groupedVehicleIndex");

            var vehicles = gh.AllVehiclesByType(typ);

            var items = new List<MenuItem>();
            items = (List<MenuItem>) MenuConverter.VehiclesToMenuItems(vehicles);

            mh.AddMenu(new Menu("groupedVehicleIndex", "Fordon inom grupp", items));
        }
    }
}
