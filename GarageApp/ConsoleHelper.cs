using System;

namespace GarageApp
{
    class ConsoleHelper
    {
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
            Console.ReadKey();
        }

    }
}
