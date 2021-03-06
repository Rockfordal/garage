﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace GarageApp
{
    class VehicleUI
    {
        private static MenuBuilder.ActionType Noop   = MenuBuilder.ActionType.Noop;

        // Todo: Ej klar! (det ska bli direkt-redigering i fordonsmenyn)
        internal static void EditVehicle(int id, GarageHandler gh, MenuHandler mh)
        {
            var vehicle = (Vehicle) gh.TryGetVehicle(id);

            if (vehicle == null)
            {
                ConsoleHelper.Announce("Kunde inte hitta fordonet med id " + id);
                return;
            };

            var action = new MenuAction(); // skrivs över
            int index = 0;

            do
            {
                Menu editMenu = new Menu("vehicleEdit", "Redigerar fordon", 
                    new List<MenuItem> {
                        new MenuItem("Namn:  " + vehicle.name,   new MenuAction(Noop, "namn")),
                        new MenuItem("Färg:  " + vehicle.color,  new MenuAction(Noop, "color")),
                        new MenuItem("Vikt:  " + vehicle.weight, new MenuAction(Noop, "weight")),
                        new MenuItem("Regnr: " + vehicle.regnr,  new MenuAction(Noop, "regnr")),
                        new MenuItem(""),
                        new MenuItem("Ta bort!",  new MenuAction(Noop, "deleteVehicle"))
                    });

                editMenu.lastIndex = index;

                action = ConsoleHelper.RenderMenu(editMenu);

                Console.ForegroundColor = ConsoleColor.Cyan;

                //index = Console.CursorTop;

                switch (action.data)
                {
                    case "namn":
                        Console.SetCursorPosition(0, 3);
                        Console.WriteLine("Ändra namn");
                        Console.ForegroundColor = mh.settings.ActiveColor;
                        vehicle.name = ConsoleHelper.EditQuestionText("Namn", vehicle.name);
                        index = 0;
                        break;

                    case "color":
                        Console.SetCursorPosition(0, 3);
                        Console.WriteLine("Ändra färg");
                        Console.SetCursorPosition(0, 5);
                        Console.ForegroundColor = mh.settings.ActiveColor;
                        vehicle.color = ConsoleHelper.EditQuestionText("Färg", vehicle.color);
                        index = 1;
                        break;

                    case "weight":
                        Console.SetCursorPosition(0, 3);
                        Console.WriteLine("Ändra vikt");
                        Console.SetCursorPosition(0, 6);
                        Console.ForegroundColor = mh.settings.ActiveColor;
                        vehicle.weight = ConsoleHelper.EditQuestionInt("Vikt", vehicle.weight);
                        index = 2;
                        break;

                    case "regnr":
                        Console.SetCursorPosition(0, 3);
                        Console.WriteLine("Ändra regnr");
                        Console.SetCursorPosition(0, 7);
                        Console.ForegroundColor = mh.settings.ActiveColor;
                        vehicle.regnr = ConsoleHelper.EditQuestionText("Regnr", vehicle.regnr);
                        index = 3;
                        break;

                    case "deleteVehicle":
                        gh.TryDeleteVehicle(vehicle.id);
                        MenuBuilder.UpdateAllMenus(gh, mh);
                        return;
                }
                Console.ForegroundColor = mh.settings.PassiveColor;
                // ConsoleHelper.Announce("type(" + action.type + ") (" + action.data + ") "+ action.id);

            } while (action.type != MenuBuilder.ActionType.Back);

            MenuBuilder.UpdateVehicleMenu(gh, mh);
        }


        internal static void DeleteVehicle(int id, GarageHandler gh, MenuHandler mh)
        {
            int beforeC = gh.GetCurrentGarage().Vehicles.Count();
            Vehicle vehicle = (Vehicle) gh.TryDeleteVehicle(id);
            int afterC = gh.GetCurrentGarage().Vehicles.Count();

            if (vehicle != null && afterC == beforeC - 1)
            {
                // ConsoleHelper.Announce("Resultat", "Fordonet har raderats" + beforeC  + " " + afterC);
                MenuBuilder.UpdateVehicleMenu(gh, mh);
                mh.GoBack();
            }
            else
            {
                ConsoleHelper.Announce("Tyvärr, Fordonet kunde inte hittas");
            }
        }


        internal static void DeleteGarage(GarageHandler gh, MenuHandler mh)
        {

            if (gh.garages.Count > 1)
            {
                var beforeC     = gh.garages.Count();
                var operationOk = gh.TryDeleteGarage(gh.garageId);
                var afterC      = gh.garages.Count();
                var minskatMedEtt = afterC == beforeC - 1;

                if (!operationOk || !minskatMedEtt)
                {
                    ConsoleHelper.Announce("Ett fel inträffade när garaget skulle raderas");
                    return;
                }

                MenuBuilder.UpdateGarageMenu(gh, mh);
                MenuBuilder.UpdateVehicleMenu(gh, mh);

                var firstGarage = gh.garages.FirstOrDefault();
                gh.TrySetGarage(firstGarage.Id);

                mh.GoBack();
            }
            else
            {
                ConsoleHelper.Announce("Minst ett garage måste existera");
            }
        }


        internal static int AddVehicle(GarageHandler gh, MenuHandler mh)
        {
            Vehicle vehicle;
            ConsoleHelper.PutLabel("Lägg till fordon");

            // Todo: Använd en genererad meny-ui istället
            var typ = ConsoleHelper.AskQuestionText("Car/Motorcycle/OneWheeler/Airplane?").ToLower();

            if (typ == "car")
                vehicle = new Car();
            else if (typ == "motorcycle")
                vehicle = new Motorcycle(); 
            else if (typ == "onewheeler")
                vehicle = new OneWheeler(); 
            else if (typ == "airplane")
                vehicle = new Airplane(); 
            else
                vehicle = new Vehicle(); 

            gh.AddVehicle(vehicle);

            // MenuBuilder.UpdateGarageMenu(gh, mh);
            // MenuBuilder.UpdateGroupMenu(gh, mh);
            MenuBuilder.UpdateAllMenus(gh, mh);

            return vehicle.id;
        }


        internal static void RenameGarage(GarageHandler gh, MenuHandler mh)
        {
            Console.WriteLine();
            ConsoleColor oldFg = Console.ForegroundColor;
            Console.ForegroundColor = mh.settings.ActiveColor;
            string name = ConsoleHelper.AskQuestionText("Nytt namn");
            gh.GetCurrentGarage().Name = name;
            MenuBuilder.UpdateGarageMenu(gh, mh);
            MenuBuilder.UpdateVehicleMenu(gh, mh);
            Console.ForegroundColor = oldFg;
        }


        internal static void CreateGarage(GarageHandler gh, MenuHandler mh)
        {
            Console.WriteLine();
            ConsoleColor oldFg = Console.ForegroundColor;
            Console.ForegroundColor = mh.settings.ActiveColor;
            string name = ConsoleHelper.AskQuestionText("Namn på nytt garage");
            Garage<Vehicle> newGarage = new Garage<Vehicle>(name);
            gh.garages.Add(newGarage);
            MenuBuilder.UpdateGarageMenu(gh, mh);
            Console.ForegroundColor = oldFg;
        }


        internal static void Search(GarageHandler gh, MenuHandler mh)
        {
            ConsoleHelper.PutLabel("Sökning");
            var text = ConsoleHelper.AskQuestionText("sökterm");
            IEnumerable<Vehicle> found = gh.FindVehicles(text);

            if (found.Any())
            {
                mh.menus.Remove("searchIndex");
                mh.AddMenu(new Menu("searchIndex", "Sökresultat",
                                    MenuConverter.VehiclesToMenuItems(found)));
                mh.TryGotoMenu("searchIndex");
            }
            else
                ConsoleHelper.Announce("Tyvärr, kunde inte hitta något fordon.");
        }


        //internal static void SearchByRegnr(GarageHandler gh, MenuHandler mh)
        //{
        //    ConsoleHelper.PutLabel("Registernummersökning");
        //    var regnr = ConsoleHelper.AskQuestionText("Ange regnr");
        //    Vehicle found = gh.FindVehicleByRegnr(regnr);

        //    if (found != null)
        //    {
        //        ConsoleHelper.Pause();
        //        //mh.TryGotoMenu("vehicle");
        //    }
        //    else
        //        ConsoleHelper.Announce("Ursäkta, fordonet kunde tyvärr inte hittas.");
        //}

    }
}
