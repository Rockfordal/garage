using System;
using System.Collections.Generic;

namespace GarageApp
{
    class Garage<T> where T : Vehicle
    {
        public static int NextId = 0;
        public int Id                  { get; set; }
        public string Name             { get; set; }
        public ICollection<T> Vehicles { get; set; }

        private static int nextId = 0;

        public Garage(string name)
        {
            this.Id = NextId++;
            this.Name = name;
            this.Vehicles = new List<T>();
        }

        public Garage() : this("")
        {
        }


        public static string ToHeader()
        {
            return string.Format("{0, 0} {1, 30} {2, 5}", "Id", "Namn    ", "Antal fordon");
        }


        public override string ToString()
        {
                  //return string.Format("{1, 30} {2, 3}", Id, Name, Vehicles.Count);
            return string.Format("{0, 0}{1, 30} {2, 5}", Id, Name, Vehicles.Count);
        }
    }
}
