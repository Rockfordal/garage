﻿using System;
using System.Linq;

namespace GarageApp
{
    class ConsoleHelper
    {
        private static ConsoleKey Q             = ConsoleKey.Q;
        private static ConsoleKey Escape        = ConsoleKey.Escape;
        private static MenuBuilder.ActionType back   = MenuBuilder.ActionType.back;

        /// <summary>
        /// Clears the screen, Shows menulabel and generates underline
        /// </summary>
        /// <param name="label"></param>
        public static void PutLabel(string label)
        {
            Console.Clear();
            var oldFg = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.White;
            PutLine(label.Length + 4);
            Console.WriteLine("| " + label + " |");
            PutLine(label.Length + 4);
            Console.ForegroundColor = oldFg;
            Console.WriteLine();
        }

        public static void PutLine(int n)
        {
            for (int i = 0; i < n; i++)
                Console.Write("-");
            Console.WriteLine();
        }

        /// <summary>
        /// Asks a question, and returns console input
        /// </summary>
        /// <param name="question"></param>
        /// <returns></returns>
        public static string AskQuestionText(string question)
        {
            ClearLine();
            Console.Write(question + ": ");
            return Console.ReadLine();
        }

        public static string AskQuestionText(string question, string untouched)
        {
            string newText = AskQuestionText(question);

            if (newText.Count() > 0)
                return newText;
            else
                return untouched;
        }

        public static int AskQuestionInt(string question)
        {
            ClearLine();
            int num;
            Console.Write(question + ": ");
            string s = Console.ReadLine();
            int.TryParse(s, out num);
            return num;
        }


        public static int AskQuestionInt(string question, int untouched)
        {
            ClearLine();
            int num;
            Console.Write(question + ": ");
            string s = Console.ReadLine();
            int.TryParse(s, out num);

            if (s.Count() > 0)
                return num;
            else
                return untouched;
        }

        private static void ClearLine()
        {
            int left = Console.CursorLeft;
            Console.Write("                             ");
            Console.CursorLeft = left;
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

            if (menu.menuItems.Count() > 0)
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

        internal static string GetTypeOf(Vehicle v)
        {
            if (v != null)
                return v.GetType().ToString().Split('.').LastOrDefault();
            else
                return null;
        }

        internal static void Announce(string text)
        {
            var oldFg = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(text);
            Console.ForegroundColor = oldFg;
            ConsoleHelper.Pause();
        }

        internal static void Announce(string label,string text)
        {
            var oldFg = Console.ForegroundColor;
            ConsoleHelper.PutLabel(label);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(text);
            Console.ForegroundColor = oldFg;
            ConsoleHelper.Pause();
        }

        internal static void Announce(int num)
        {
            var oldFg = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(num.ToString());
            Console.ForegroundColor = oldFg;
            ConsoleHelper.Pause();
        }


        internal static string SafeSub(string field, int num)
        {
            if (field != null)
            {
                int cap = field.Count();
                int cut = Math.Min(cap, num);
                return field.Substring(0, cut);
            }
            else
            {
                return "";
            }
        }


        internal static string SafeSub(int field, int num)
        {
            return SafeSub(field.ToString(), num);
        }

    }
}
