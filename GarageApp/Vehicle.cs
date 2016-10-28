using System;

namespace GarageApp
{
    class Vehicle
    {
        public static int NextId = 0;
        public int    id         { get; set; }
        public string name       { get; set; }
        public string regnr      { get; set; }
        public int    wheelCount { get; set; }
        public string color      { get; set; }
        public int    weight     { get; set; }


        public string MyType
        {
            get { return ConsoleHelper.GetTypeOf(this); }
        }


        public Vehicle(string name, string color, string regnr, int weight)
        {
            this.id = NextId++;
            this.name   = name;
            this.color  = color;
            this.regnr  = regnr;
            this.weight = weight;
        }


        public Vehicle()
        {
            this.id = NextId++;
            this.name = "";
        }


        public static string ToHeader()
        {
            return String.Format("{0, 2} {1, 10} {2, 10} {3, 20} {4, 4}", "Id", "Typ", "Färg", "Namn", "Vikt");
        }

        public override string ToString()
        {
            const int colorW = 10;
            const int nameW = 20;
            const int weightW = 4;
            var typeW = 10;
            string colorF  = ConsoleHelper.SafeSub(color, colorW);
            string nameF   = ConsoleHelper.SafeSub(name, nameW);
            string weightF = ConsoleHelper.SafeSub(weight, weightW);
            string typeF   = ConsoleHelper.SafeSub(MyType, typeW);
            //            return String.Format("{0, 10} {1, 20} {2, 4}kg", colorF , nameF, weightF);
            //     return String.Format("{0, 2} {1, 10} {2, 20} {3, 4}kg", id, colorF , nameF, weightF);
            return String.Format("{0, 2} {1, 10} {2, 10} {3, 20} {4, 4}kg", id, typeF, colorF , nameF, weightF);
        }

    }
}
