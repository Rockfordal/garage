using System;
using System.Collections.Generic;

namespace GarageApp
{
    class Program
    {
        private static Common.ActionType route  = Common.ActionType.route;
        private static Common.ActionType quit   = Common.ActionType.quit;
        private static Common.ActionType edit   = Common.ActionType.edit;
        private static Common.ActionType delete = Common.ActionType.delete;
        private static Common.ActionType info   = Common.ActionType.info;

        static void Main(string[] args)
        {
            var gh     = new GarageHandler();
            var mh     = new MenuHandler();
            var action = new MenuAction();
            var lastAction = action;
            int lastSelectedVehicle = 0;

            mh.AddMenu(new Menu("main", "Huvudmeny",       new List<MenuItem> {
                        new MenuItem("Lista garage",       new MenuAction(route, "garageIndex")),
                        new MenuItem("Lista alla grupper", new MenuAction(route, "allGroupIndex")),
                        new MenuItem("Lista fordon",       new MenuAction(route, "vehicleIndex")),
                        new MenuItem("Lägg till fordon",   new MenuAction(route, "vehicleAdd")),
                        new MenuItem("Sök på Regnr",       new MenuAction(route, "searchRegnr")),
                        new MenuItem("Spara"),
                        new MenuItem("Quit", new MenuAction(quit)) }));

            mh.AddMenu(new Menu("vehicleOptions", "Fordonsåtgärd", new List<MenuItem> {
                        new MenuItem("Information", new MenuAction(info,   "infoVehicle")),
                        new MenuItem("Redigera",    new MenuAction(edit,   "editVehicle")),
                        new MenuItem("Ta bort",     new MenuAction(delete, "deleteVehicle")) }));

            mh.AddMenu(new Menu("garageIndex",   "Välj garage", Common.GaragesToMenuItems(gh.garages)));
            mh.AddMenu(new Menu("allGroupIndex", "Välj grupp",  Common.VehiclesToMenuItemsByGroup(gh.GetCurrentGarage().vehicles)));
            mh.AddMenu(new Menu("vehicleIndex",  "Välj fordon", Common.VehiclesToMenuItems(gh.GetCurrentGarage().vehicles)));

            do
            {
                switch (action.type)
                {
                    case Common.ActionType.info:
                        VehicleUI.ShowVehicleInfo(lastSelectedVehicle, gh);
                        break;

                    case Common.ActionType.edit:
                        VehicleUI.EditVehicle(lastSelectedVehicle, gh, mh);
                        break;

                    case Common.ActionType.delete:
                        VehicleUI.DeleteVehicle(lastSelectedVehicle, gh, mh);
                        break;

                    case Common.ActionType.back:
                        if (mh.current.Count > 1)
                            mh.GoBack();
                        else
                            action = new MenuAction(quit, "");
                        break;

                    case Common.ActionType.route:
                        switch (action.data)
                        {
                            case "vehicleAdd":
                                VehicleUI.AddVehicle(gh, mh);
                                break;

                            case "searchRegnr":
                                VehicleUI.SearchByRegnr(gh, mh);
                                break;

                            default:
                                if (mh.currentMenu.name == "garageIndex")
                                {
                                    gh.TrySetGarage(action.id);
                                    Common.UpdateVehicleMenu(gh, mh);
                                    //ConsoleHelper.Announce(String.Format("\n Du har valt garage {0}", action.id));
                                    mh.current.Pop();
                                }
                                else if (mh.currentMenu.name == "vehicleIndex")
                                    lastSelectedVehicle = action.id;
                                break;
                        }
                        mh.TryGotoMenu(action.data);
                        break;
                }

                lastAction = action;

                if (action.type != quit)
                    action = ConsoleHelper.RenderMenu(mh.currentMenu);

            } while (action.type != quit);

            Console.SetCursorPosition(0, Console.WindowHeight - 2);  // We're done
        }
    }
}
