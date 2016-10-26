using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageApp
{
    class Garage<T> where T : Vehicle
    {
        public string name { get; set; }
        public IEnumerable<T> vehicles { get; set; }

        public Garage()
        {
            name = "";
            vehicles = new List<T>();
        }

    }
}
