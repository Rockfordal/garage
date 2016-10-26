using System;

namespace GarageApp
{
    class Settings
    {
        public ConsoleColor BackgroundColor { get; set; }
        public ConsoleColor ActiveColor     { get; set; }
        public ConsoleColor PassiveColor    { get; set; }

        public Settings()
        {
            BackgroundColor = ConsoleColor.Black;
            PassiveColor    = ConsoleColor.DarkGray;
            ActiveColor     = ConsoleColor.Yellow;
        }

    }
}
