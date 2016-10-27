using System;
using System.Collections.Generic;
using System.Linq;

namespace GarageApp
{
    class ConsoleHelper
    {
        private static ConsoleKey Q             = ConsoleKey.Q;
        private static ConsoleKey Escape        = ConsoleKey.Escape;
        private static Common.ActionType back   = Common.ActionType.back;

        /// <summary>
        /// Clears the screen, Shows menulabel and generates underline
        /// </summary>
        /// <param name="label"></param>
        public static void PutLabel(string label)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(label);
            for (int i = 0; i < label.Length; i++)
            {
                Console.Write("-");
            }
            Console.WriteLine("\n");
        }

        /// <summary>
        /// Asks a question, and returns console input
        /// </summary>
        /// <param name="question"></param>
        /// <returns></returns>
        public static string AskQuestionText(string question)
        {
            Console.Write(question + ": ");
            return Console.ReadLine();
        }

        public static void AnyKey()
        {
            Console.WriteLine("Tryck valfri tangent för att fortsätta");
            Console.ReadKey(true);
        }

        public static void Pause()
        {
            Console.ReadKey(true);
        }

        public static MenuAction RenderMenu(Menu menu)
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

            WriteActive(menu, index); // Aktivera första valet

            do
            {
                ki = Console.ReadKey(true);
                key = ki.Key;

                if (key == ConsoleKey.UpArrow && index > 0)
                {
                    Console.SetCursorPosition(0, lowest + index);
                    WriteInactive(menu, index);
                    index -= 1;
                    Console.SetCursorPosition(0, lowest + index);
                    WriteActive(menu, index);
                }
                else if (key == ConsoleKey.DownArrow && index < indexMax)
                {
                    Console.SetCursorPosition(0, lowest + index);
                    WriteInactive(menu, index);
                    index += 1;
                    Console.SetCursorPosition(0, lowest + index);
                    WriteActive(menu, index);
                }
                else if (key == ConsoleKey.Enter)
                {
                    var item = menu.menuItems.ElementAt(index);
                    var action = item.action;
                    action.id = item.id;
                    return action;
                }

            } while (! new[]{Q, Escape}.Contains(key));

            return new MenuAction(back, "");
        }

        private static void WriteActive(Menu menu, int index)
        {
            Console.ForegroundColor = menu.settings.ActiveColor;
            Console.Write(menu.menuItems.ElementAt(index));
        }

        private static void WriteInactive(Menu menu, int index)
        {
            Console.ForegroundColor = menu.settings.PassiveColor;
            Console.Write(menu.menuItems.ElementAt(index));
        }

    }
}
