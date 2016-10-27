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
            int lastSelectedVehicle = 0;

            mh.AddMenu(new Menu("main", "Huvudmeny", new List<MenuItem> {
                        new MenuItem("Lista fordon", new MenuAction(route, "vehicleIndex")),
                        new MenuItem("Spara"),
                        new MenuItem("Quit", new MenuAction(quit)) }));

            mh.AddMenu(new Menu("vehicleOptions", "Fordonsåtgärd", new List<MenuItem> {
                        new MenuItem("Information", new MenuAction(info,   "infoVehicle")),
                        new MenuItem("Redigera",    new MenuAction(edit,   "editVehicle")),
                        new MenuItem("Ta bort",     new MenuAction(delete, "deleteVehicle")) }));

            mh.AddMenu(new Menu("vehicleIndex", "Välj fordon", Common.VehiclesToMenuItems(gh.garage.vehicles)));

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
                        if (mh.currentMenu.name == "vehicleIndex")
                            lastSelectedVehicle = action.id;
                        mh.trySetMenu(action.data);
                        break;
                }

                if (action.type != quit)
                    action = ConsoleHelper.RenderMenu(mh.currentMenu);

            } while (action.type != quit);

            Console.SetCursorPosition(0, Console.WindowHeight - 2);  // We're done
        }
    }
}
