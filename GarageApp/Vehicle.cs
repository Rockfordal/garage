using System;
using System.Linq;

namespace GarageApp
{
    class Vehicle
    {
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


        public Vehicle(int id, string name, string color, string regnr, int weight)
        {
            this.id     = id;
            this.name   = name;
            this.color  = color;
            this.regnr  = regnr;
            this.weight = weight;
        }


        public Vehicle()
        {
            this.name = "";
        }


        public override string ToString()
        {
            string colorF  = ConsoleHelper.SafeSub(color, 7);
            string nameF   = ConsoleHelper.SafeSub(name, 20);
            string weightF = ConsoleHelper.SafeSub(weight, 4);
            return String.Format("{0, 7} {1, 20} {2, 4}kg", colorF , nameF, weightF);
        }

    }
}
