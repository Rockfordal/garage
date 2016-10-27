using System;

namespace GarageApp
{
    class Program
    {
        private static MenuBuilder.ActionType quit   = MenuBuilder.ActionType.quit;

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
                    case MenuBuilder.ActionType.info:
                        VehicleUI.ShowVehicleInfo(lastSelectedVehicle, gh);
                        break;

                    case MenuBuilder.ActionType.edit:
                        VehicleUI.EditVehicle(lastSelectedVehicle, gh, mh);
                        break;

                    case MenuBuilder.ActionType.delete:
                        VehicleUI.DeleteVehicle(lastSelectedVehicle, gh, mh);
                        break;

                    case MenuBuilder.ActionType.route:
                        switch (action.data)
                        {
                            case "vehicleAdd":
                                VehicleUI.AddVehicle(gh, mh);
                                MenuBuilder.UpdateGroupMenu(gh, mh);
                                MenuBuilder.UpdateVehicleMenu(gh, mh);
                                break;

                            case "searchRegnr":
                                VehicleUI.SearchByRegnr(gh, mh);
                                break;

                            case "allaGarage":
                                MenuBuilder.UpdateAllMenus(gh, mh);
                                mh.current.Pop();
                                break;

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

                            default:
                                if (mh.currentMenu.name == "garageIndex")
                                {
                                    gh.TrySetGarage(action.id);
                                    MenuBuilder.UpdateVehicleMenu(gh, mh);
                                    // mh.current.Pop(); // Glöm garagemenyn (hoppa direkt till huvudmenyn på tillbakavägen)
                                    mh.current.Push("vehicleIndex");
                                }
                                else if (mh.currentMenu.name == "vehicleIndex")
                                    lastSelectedVehicle = action.id;
                                break;
                        }
                        mh.TryGotoMenu(action.data);
                        break;

                    case MenuBuilder.ActionType.back:
                        if (mh.current.Count > 1)
                            mh.GoBack();
                        else
                            action = new MenuAction(quit, "");
                        break;
                }

                //lastAction = action;

                if (action.type != quit)
                    action = ConsoleHelper.RenderMenu(mh.currentMenu);

            } while (action.type != quit);

            Console.SetCursorPosition(0, Console.WindowHeight - 2);  // We're done
        }
    }
}
