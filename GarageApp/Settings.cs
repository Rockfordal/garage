using System;

namespace GarageApp
{
    class MenuSettings
    {
        public ConsoleColor BackgroundColor { get; set; }
        public ConsoleColor ActiveColor     { get; set; }
        public ConsoleColor PassiveColor    { get; set; }

        public MenuSettings()
        {
            BackgroundColor = ConsoleColor.Black;
            PassiveColor    = ConsoleColor.DarkGray;
            ActiveColor     = ConsoleColor.Yellow;
        }

    }
}
