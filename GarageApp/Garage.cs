using System;
using System.Collections.Generic;

namespace GarageApp
{
    class Garage<T> where T : Vehicle
    {
        public int id                  { get; set; }
        public string name             { get; set; }
        public ICollection<T> vehicles { get; set; }

        private static int nextId = 0;

        public Garage(int id, string name)
        {
            // todo: implement id duplication check or remove this constructor
            this.id = id;
            this.name = name;
            this.vehicles = new List<T>();
        }

        public Garage(string name)
        {
            nextId += 1;
            this.id = nextId;
            this.name = name;
            this.vehicles = new List<T>();
        }

        public Garage() : this("")
        {
        }

        public override string ToString()
        {
            return String.Format("{1, 30} {2, 3}", id, name, vehicles.Count);
        }
    }
}
