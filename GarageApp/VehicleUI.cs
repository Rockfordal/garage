using System;
using System.Collections.Generic;
using System.Linq;

namespace GarageApp
{
    class VehicleUI
    {

        internal static void DeleteVehicle(int id, GarageHandler gh, MenuHandler mh)
        {
            int beforeC = gh.garage.vehicles.Count();
            Vehicle vehicle = (Vehicle) gh.TryDeleteVehicle(id);
            int afterC = gh.garage.vehicles.Count();

            if (vehicle != null && afterC == beforeC - 1)
            {
                //ConsoleHelper.PutLabel("Resultat:");
                //Console.WriteLine("Fordonet har raderats" + beforeC  + " " + afterC);
                UpdateVehicleMenu(mh, gh);
                mh.GoBack();
            }
            else
            {
                Console.WriteLine("Tyvärr, Fordonet kunde inte hittas");
                Console.ReadKey();
            }
        }


        internal static void EditVehicle(int id, GarageHandler gh, MenuHandler mh)
        {
            var vehicle = (Vehicle) gh.TryGetVehicle(id);
            if (vehicle != null)
            {
                var action = new MenuAction();

                do
                {
                    Menu editMenu = new Menu("vehicleEdit", "Redigerar fordon", new List<MenuItem> {
                                        new MenuItem("Namn: " + vehicle.name,   new MenuAction(Common.ActionType.noop, "namn")),
                                        new MenuItem("Färg: " + vehicle.color,  new MenuAction(Common.ActionType.noop, "color")),
                                        new MenuItem("Vikt: " + vehicle.weight, new MenuAction(Common.ActionType.noop, "weight")) });
                    action = ConsoleHelper.RenderMenu(editMenu);

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    switch (action.data)
                    {
                        case "namn":
                            //Console.WriteLine("Ändra namn");
                            //Console.SetCursorPosition(0, 0);
                            break;

                        case "color":
                            Console.WriteLine("Ändra färg");
                            break;

                        case "weight":
                            Console.WriteLine("Ändra vikt");
                            break;
                    }
                //Console.ForegroundColor = mh.
                Console.ForegroundColor = ConsoleColor.White;

                //Console.WriteLine("type(" + action.type + ") (" + action.data + ") "+ action.id);
                ConsoleHelper.Pause();

                } while (action.type != Common.ActionType.back);
            }
        }


        internal static void ShowVehicleInfo(int id, GarageHandler gh)
        {
            var vehicle = (Vehicle) gh.TryGetVehicle(id);
            if (vehicle != null)
            {
                ConsoleHelper.PutLabel("Fordonsinfo");
                Console.WriteLine("Id:   " + vehicle.id);
                Console.WriteLine("Namn: " + vehicle.name);
                Console.WriteLine("Vikt: " + vehicle.weight);
                Console.WriteLine("Färg: " + vehicle.color);
                Console.ReadKey();
            }
        }

        private static void UpdateVehicleMenu(MenuHandler mh, GarageHandler gh)
        {
            mh.menus.Remove("vehicleIndex");
            mh.AddMenu(new Menu("vehicleIndex", "Välj fordon", Common.VehiclesToMenuItems(gh.garage.vehicles)));
        }

    }
}
