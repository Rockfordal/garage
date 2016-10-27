using System;
using System.Collections.Generic;

namespace GarageApp
{
    class Garage<T> where T : Vehicle
    {
        public int id                  { get; set; }
        public string name             { get; set; }
        public IEnumerable<T> vehicles { get; set; }

        public Garage()
        {
            name = "";
            vehicles = new List<T>();
        }

        public override string ToString()
        {
            return String.Format("{1}", id, name);
        }
    }
}
