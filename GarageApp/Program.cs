using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageApp
{
    class Program
    {
        private static ConsoleKey Q             = ConsoleKey.Q;
        private static ConsoleKey Escape        = ConsoleKey.Escape;
        private static Common.ActionType back   = Common.ActionType.back;
        private static Common.ActionType route  = Common.ActionType.route;
        private static Common.ActionType quit   = Common.ActionType.quit;
        private static Common.ActionType edit   = Common.ActionType.edit;
        private static Common.ActionType delete = Common.ActionType.delete;
        private static Common.ActionType info   = Common.ActionType.info;

        static void Main(string[] args)
        {
            MenuAction dummy = new MenuAction(Common.ActionType.noop, null);
            MenuAction action = dummy;
            MenuAction lastAction = dummy;

            var gh = new GarageHandler();
            var mh = new MenuHandler();

            mh.AddMenu(new Menu("main", "Huvudmeny", new List<MenuItem> {
                new MenuItem("Lista fordon", new MenuAction(route, "vehicleIndex")),
                new MenuItem("Test"),
                new MenuItem("Quit", new MenuAction(quit))
            }));

            mh.AddMenu(new Menu("vehicleOptions", "", new List<MenuItem>
            {
                new MenuItem("Information", new MenuAction(info,   "infoVehicle")),
                new MenuItem("Redigera",    new MenuAction(edit,   "editVehicle")),
                new MenuItem("Ta bort",     new MenuAction(delete, "deleteVehicle"))
            }));

            mh.AddMenu(new Menu("vehicleIndex", "Välj fordon", 
                VehiclesToMenuItems(gh.garage.vehicles)));

            do
            {
                if (action.type == info)
                {

                    Console.WriteLine("\nData: " + action.data + "\n");
                    //Console.WriteLine("\nId: " + action.id + "\n");
                    Console.ReadKey();
                }

                if (action.type == back)
                {
                    if (mh.current.Count > 1)
                        mh.GoBack();
                    else
                        action = new MenuAction(quit, "");
                }

                if (action.type == route)
                {
                    mh.trySetMenu(action.data);
                }

                if (action.type != quit)
                    lastAction = action;
                    //action = RenderMenu(mh.currentMenu);
                    action = RenderMenu(mh.currentMenu, lastAction);

            } while (action.type != quit);

            Console.SetCursorPosition(0, Console.WindowHeight - 2);
            // We're done
        }

        //static MenuAction RenderMenu(Menu menu)
        static MenuAction RenderMenu(Menu menu, MenuAction lastAction)
        {
            Console.Clear();
            ConsoleHelper.PutLabel(menu.label);
            Console.ForegroundColor = menu.settings.PassiveColor;
            int lowest = Console.CursorTop;

            foreach (var item in menu.menuItems)
                Console.WriteLine(item);

            ConsoleKeyInfo ki;
            ConsoleKey key;

            int index = 0;
            int indexMax = menu.menuItems.Count() - 1;
            Console.CursorTop = lowest;

            // Aktivera första valet
            Console.ForegroundColor = menu.settings.ActiveColor;
            Console.Write(menu.menuItems.ElementAt(index));

            do
            {
                ki = Console.ReadKey(true);
                key = ki.Key;

                if (key == ConsoleKey.UpArrow && index > 0)
                {
                    Console.ForegroundColor = menu.settings.PassiveColor;
                    Console.SetCursorPosition(0, lowest + index);
                    Console.Write(menu.menuItems.ElementAt(index));
                    index -= 1;
                    Console.SetCursorPosition(0, lowest + index);
                    Console.ForegroundColor = menu.settings.ActiveColor;
                    Console.Write(menu.menuItems.ElementAt(index));
                }
                else if (key == ConsoleKey.DownArrow && index < indexMax)
                {
                    Console.ForegroundColor = menu.settings.PassiveColor;
                    Console.SetCursorPosition(0, lowest + index);
                    Console.Write(menu.menuItems.ElementAt(index));
                    index += 1;
                    Console.SetCursorPosition(0, lowest + index);
                    Console.ForegroundColor = menu.settings.ActiveColor;
                    Console.Write(menu.menuItems.ElementAt(index));
                }
                else if (key == ConsoleKey.Enter)
                {
                    var action = menu.menuItems.ElementAt(index).action;
                    //if (action.fromMenuItem != null)
                    //    action.id = action.fromMenuItem.id;
                    return action;
                }

            } while (! new[]{Q, Escape}.Contains(key));

            return new MenuAction(Common.ActionType.back, "");
        }

        private static IEnumerable<MenuItem> VehiclesToMenuItems(IEnumerable<Vehicle> vehicles)
        {
            List<MenuItem> menuItems = new List<MenuItem>();
            foreach (Vehicle vehicle in vehicles)
            {
                menuItems.Add(VehicleToMenuItem(vehicle));
            }
            return menuItems;
        }

        static MenuItem VehicleToMenuItem(Vehicle vehicle)
        {
            return new MenuItem(vehicle.ToString(), new MenuAction(route, "vehicleOptions"), vehicle.id);
        }

    }
}
