using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var gh = new GarageHandler();
            var vehicles = gh.garage.vehicles;

            List<string> items = new List<string>();

            foreach (var vehicle in vehicles)
                items.Add(vehicle.ToString());

            Menu("Välj bil ur garaget", items);
        }

        static void Menu(string label, IEnumerable<string> items)
        {

            var active   = ConsoleColor.Yellow;
            var inactive = ConsoleColor.DarkGray;
            int lowest   = Console.CursorTop;

            foreach (var item in items)
                Console.WriteLine(item);

            ConsoleKeyInfo ki;
            ConsoleKey key;

            int index = 0;
            int indexMax = items.Count() - 1;
            Console.CursorTop = lowest;

            do
            {
                ki = Console.ReadKey();
                key = ki.Key;

                if (key == ConsoleKey.UpArrow && index > 0)
                {

                    Console.ForegroundColor = inactive;
                    Console.SetCursorPosition(0, lowest + index);
                    Console.Write(items.ElementAt(index));
                    index -= 1;
                    Console.SetCursorPosition(0, lowest + index);
                    Console.ForegroundColor = active;
                    Console.Write(items.ElementAt(index));
                }
                else if (key == ConsoleKey.DownArrow && index < indexMax)
                {
                    Console.ForegroundColor = inactive;
                    Console.SetCursorPosition(0, lowest + index);
                    Console.Write(items.ElementAt(index));
                    index += 1;
                    Console.SetCursorPosition(0, lowest + index);
                    Console.ForegroundColor = active;
                    Console.Write(items.ElementAt(index));
                }
                else if (key == ConsoleKey.Enter)
                {
                    
                }

            } while (key != ConsoleKey.Q);

        }

        //static void ShowVehicles(GarageHandler gh)
        //{
        //    ConsoleHelper.PutLabel("Fordon i garaget");

        //    var vehicles = gh.garage.vehicles;
        //    foreach (var vehicle in vehicles)
        //        Console.WriteLine(vehicle);
        //}
    }
}
