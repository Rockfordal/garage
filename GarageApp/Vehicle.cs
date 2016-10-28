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


        public override string ToString()
        {
            var colorW = 10;
            var nameW = 20;
            var weightW = 4;
            string colorF  = ConsoleHelper.SafeSub(color, colorW);
            string nameF   = ConsoleHelper.SafeSub(name, nameW);
            string weightF = ConsoleHelper.SafeSub(weight, weightW);
            //     return String.Format("{0, 10} {1, 20} {2, 4}kg", colorF , nameF, weightF);
            return String.Format("{0, 2} {1, 10} {2, 20} {3, 4}kg", id, colorF , nameF, weightF);
        }

    }
}
