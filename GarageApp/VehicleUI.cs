using System;
using System.Collections.Generic;
using System.Linq;

namespace GarageApp
{
    class VehicleUI
    {

        internal static void DeleteVehicle(int id, GarageHandler gh, MenuHandler mh)
        {
            int beforeC = gh.GetCurrentGarage().vehicles.Count();
            Vehicle vehicle = (Vehicle) gh.TryDeleteVehicle(id);
            int afterC = gh.GetCurrentGarage().vehicles.Count();

            if (vehicle != null && afterC == beforeC - 1)
            {
                //ConsoleHelper.Announce("Resultat", "Fordonet har raderats" + beforeC  + " " + afterC);
                MenuBuilder.UpdateVehicleMenu(gh, mh);
                mh.GoBack();
            }
            else
            {
                ConsoleHelper.Announce("Tyvärr, Fordonet kunde inte hittas");
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
                                        new MenuItem("Namn: " + vehicle.name,   new MenuAction(MenuBuilder.ActionType.noop, "namn")),
                                        new MenuItem("Färg: " + vehicle.color,  new MenuAction(MenuBuilder.ActionType.noop, "color")),
                                        new MenuItem("Vikt: " + vehicle.weight, new MenuAction(MenuBuilder.ActionType.noop, "weight")) });
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

                } while (action.type != MenuBuilder.ActionType.back);
            }
        }


        internal static void ShowVehicleInfo(int id, GarageHandler gh)
        {
            var vehicle = (Vehicle) gh.TryGetVehicle(id);
            if (vehicle != null)
            {
                ConsoleHelper.PutLabel("Fordonsinfo");
                Console.WriteLine("{0,14} {1}", "Id:", vehicle.id);
                Console.WriteLine("{0,14} {1}", "Namn:", vehicle.name);
                Console.WriteLine("{0,14} {1} kg", "Vikt:", vehicle.weight);
                Console.WriteLine("{0,14} {1}", "Färg:", vehicle.color);
                Console.WriteLine("{0,14} {1}", "Regnr:", vehicle.regnr);
                Console.WriteLine("{0,14} {1}", "Fordonstyp:", vehicle.MyType);
                Console.ReadKey();
            }
        }

        internal static void AddVehicle(GarageHandler gh, MenuHandler mh)
        {
            Vehicle vehicle;
            ConsoleHelper.PutLabel("Lägg till fordon");
            var typ    = ConsoleHelper.AskQuestionText("Car/Motorcycle/OneWheeler/Airplane?").ToLower();
            var name   = ConsoleHelper.AskQuestionText("Namn");
            var color  = ConsoleHelper.AskQuestionText("Färg");
            var regnr  = ConsoleHelper.AskQuestionText("Regnr");
            var weight = ConsoleHelper.AskQuestionInt("Vikt");

            if (typ == "motorcycle")
                vehicle = new Motorcycle(100, name, color, regnr, weight);
            else if (typ == "onewheeler")
                vehicle = new OneWheeler(101, name, color, regnr, weight);
            else if (typ == "airplane")
                vehicle = new Airplane(101, name, color, regnr, weight);
            else
                vehicle = new Vehicle(102, name, color, regnr, weight);

            gh.AddVehicle(vehicle);
            MenuBuilder.UpdateVehicleMenu(gh, mh);
        }

        internal static void SearchByRegnr(GarageHandler gh, MenuHandler mh)
        {
            ConsoleHelper.PutLabel("Registernummersökning");
            var regnr = ConsoleHelper.AskQuestionText("Ange regnr");
            Vehicle found = gh.FindVehicleByRegnr(regnr);
            if (found != null)
                VehicleUI.ShowVehicleInfo(found.id, gh);
            else
                ConsoleHelper.Announce("Ursäkta, fordonet kunde tyvärr inte hittas.");
            //    SearchByRegnr(gh, mh);
        }
    }
}
