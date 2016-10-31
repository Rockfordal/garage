using System;

namespace GarageApp
{
    class Program
    {
        private const MenuBuilder.ActionType Quit = MenuBuilder.ActionType.Quit;

        static void Main(string[] args)
        {
            var gh     = new GarageHandler();
            var mh     = new MenuHandler();
            var action = new MenuAction();
            var lastSelectedVehicle = 0;
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
                                VehicleUI.EditVehicle(action.id, gh, mh);
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
                        switch (action.data)
                        {
                            case "garage":
                                VehicleUI.CreateGarage(gh, mh);
                                break;

                            case "vehicle":
                                var newId = VehicleUI.AddVehicle(gh, mh);
                                VehicleUI.EditVehicle(newId, gh, mh);
                                break;
                        }
                        break;

                    case MenuBuilder.ActionType.Search:
                        if (action.data == "regnr")
                            VehicleUI.Search(gh, mh);
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
                                switch (mh.currentMenu.Name)
                                {
                                    case "garageIndex":
                                        gh.TrySetGarage(action.id);
                                        MenuBuilder.UpdateVehicleMenu(gh, mh);
                                        // mh.current.Pop(); // Glöm garagemenyn (hoppa direkt till huvudmenyn på tillbakavägen)
                                        mh.current.Push("vehicleIndex");
                                        break;
                                    //case "vehicleIndex":
                                    //    lastSelectedVehicle = action.id;
                                        // Vi hoppar direkt till redigera nu, så tror inte denna behövs:
                                        //MenuBuilder.AddVehiclesToOptionsMenu(gh, mh, lastSelectedVehicle);
                                        //break;
                                }
                            break;
                        }
                        mh.TryGotoMenu(action.data);
                        break;

                    case MenuBuilder.ActionType.Back:
                        if (mh.current.Count > 1)
                            mh.GoBack();
                        else
                            action = new MenuAction(Quit, "save");
                        break;
                }

                // lastAction = action;

                if (action.type != Quit)
                    action = ConsoleHelper.RenderMenu(mh.currentMenu);

            } while (action.type != Quit);

            if (action.data == "save")
                gh.SaveToDb();

            Console.SetCursorPosition(0, Console.WindowHeight - 2);  // We're done
        }
    }
}
