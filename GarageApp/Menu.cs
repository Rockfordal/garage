using System.Collections.Generic;

namespace GarageApp
{
    class Menu
    {
        public string Name { get; set; }
        public string Label { get; set; }
        public IEnumerable<MenuItem> MenuItems { get; set; }
        public MenuSettings Settings { get; set; }

        public Menu(string name, string label, IEnumerable<MenuItem> newItems)
        {
            this.Name = name;
            this.Label = label;
            this.Settings = new MenuSettings();
            this.MenuItems = newItems;
        }

        public Menu(string name, string label) : this(name, label, new List<MenuItem>())
        {
        }

        public Menu(string str) : this(str, str)
        {
        }

        //public Menu(string name, string label, IEnumerable<Vehicle> vehicles)
        //{
        //    this.name = name;
        //    this.label = label;
        //    this.settings = new UserSettings();
        //    this.menuItems = new List<MenuItem>();
        //    foreach (var vehicle in vehicles)
        //        AddVehicle(vehicle);
        //}

        //public Menu(string name, string label) : this(name, label, new List<Vehicle>())
        //{
        //}

        //internal void AddVehicle(Vehicle vehicle)
        //{
        //    this.menuItems.Add(new MenuItem(vehicle.ToString()));
        //}

        //internal void AddLabelItem(string label)
        //{
        //    this.menuItems.Add(new MenuItem(label));
        //}
    }
}
