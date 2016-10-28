using System;

namespace GarageApp
{
    class Program
    {
        private static MenuBuilder.ActionType quit   = MenuBuilder.ActionType.Quit;

        static void Main(string[] args)
        {
            var gh     = new GarageHandler();
            var mh     = new MenuHandler();
            var action = new MenuAction();
            int lastSelectedVehicle = 0;
            // var lastAction = action;

            MenuBuilder.UpdateAllMenus(gh, mh);

            do
            {
                switch (action.type)
                {
                        
                    case MenuBuilder.ActionType.Edit:
                        switch (action.data)
                        {
                            case "garage":
                                VehicleUI.RenameGarage(gh, mh);
                                break;

                            case "vehicle":
                                VehicleUI.EditVehicle(lastSelectedVehicle, gh, mh);
                                MenuBuilder.UpdateVehicleMenu(gh, mh);
                                break;
                        }
                        break;

                    case MenuBuilder.ActionType.Delete:
                        switch (action.data)
                        {
                            case "garage":
                                VehicleUI.DeleteGarage(gh, mh);
                                break;

                            case "vehicle":
                                VehicleUI.DeleteVehicle(lastSelectedVehicle, gh, mh);
                                break;
                        }
                        break;

                    case MenuBuilder.ActionType.Create:
                        if (action.data == "garage")
                            VehicleUI.CreateGarage(gh, mh);
                        else if (action.data == "vehicle")
                        {
                            VehicleUI.AddVehicle(gh, mh);
                            MenuBuilder.UpdateGroupMenu(gh, mh);
                            MenuBuilder.UpdateVehicleMenu(gh, mh);
                        }
                        break;

                    case MenuBuilder.ActionType.Search:
                        if (action.data == "regnr")
                            VehicleUI.SearchByRegnr(gh, mh);
                        break;

                    case MenuBuilder.ActionType.Route:
                        switch (action.data)
                        {
                            case "save":
                                gh.SaveToDb();
                                mh.current.Pop();
                                break;

                            case "load":
                                gh.LoadFromDb();
                                MenuBuilder.UpdateAllMenus(gh, mh);
                                mh.current.Pop();
                                break;

                            case "reset":
                                gh.LoadFromSample();
                                MenuBuilder.UpdateAllMenus(gh, mh);
                                mh.current.Pop();
                                break;

                            case "clear":
                                gh.FactoryEmptyGarage();
                                MenuBuilder.UpdateAllMenus(gh, mh);
                                mh.current.Pop();
                                break;

                            //case "allaGarage":
                            //    MenuBuilder.UpdateAllMenus(gh, mh);
                            //    mh.current.Pop();
                            //    break;


                            default:  // Om man valt att gå in i en meny?
                                if (mh.currentMenu.name == "garageIndex")
                                {
                                    gh.TrySetGarage(action.id);
                                    MenuBuilder.UpdateVehicleMenu(gh, mh);
                                    // mh.current.Pop(); // Glöm garagemenyn (hoppa direkt till huvudmenyn på tillbakavägen)
                                    mh.current.Push("vehicleIndex");
                                }
                                else if (mh.currentMenu.name == "vehicleIndex")
                                {
                                    lastSelectedVehicle = action.id;
                                    //ConsoleHelper.Announce("vInd");
                                    MenuBuilder.AddVehiclesToOptionsMenu(gh, mh, lastSelectedVehicle);
                                }
                                //else if (mh.currentMenu.name == "vehicleOptions")
                                //{
                                //    ConsoleHelper.Announce("vOpt");
                                //    //MenuBuilder.AddVehiclesToOptionsMenu(gh, mh);
                                //}
                                break;
                        }
                        mh.TryGotoMenu(action.data);
                        break;

                    case MenuBuilder.ActionType.Back:
                        if (mh.current.Count > 1)
                            mh.GoBack();
                        else
                            action = new MenuAction(quit, "save");
                        break;
                }

                // lastAction = action;

                if (action.type != quit)
                    action = ConsoleHelper.RenderMenu(mh.currentMenu);

            } while (action.type != quit);

            if (action.data == "save")
                gh.SaveToDb();

            Console.SetCursorPosition(0, Console.WindowHeight - 2);  // We're done
        }
    }
}
