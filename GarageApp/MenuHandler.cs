using System.Collections.Generic;

namespace GarageApp
{
    class MenuHandler
    {
        public SortedDictionary<string, Menu> menus { get; set; }
        public Stack<string> current { get; set; }
        public string lastMenu { get; set; }
        public MenuSettings settings { get; set; }

        public Menu currentMenu
        {
            get
            {
                Menu found = null;
                if (current != null && current.Count > 0)
                    menus.TryGetValue(current.Peek(), out found);
                return found;
            }
            set
            { 
                var varde = value.name;
                if (current != null && current.Count != 0)
                    lastMenu = current.Peek();
                current.Push(varde);
            }
        }

        public MenuHandler()
        {
            current = new Stack<string>();
            menus = new SortedDictionary<string, Menu>();
            settings = new MenuSettings();
        }

        public void GoBack()
        {
            if (current.Count > 1 )
                current.Pop();
        }

        public void AddMenu(Menu menu)
        {
            menus.Add(menu.name, menu);
            if (currentMenu == null)
                currentMenu = menu;
        }

        public override string ToString()
        {
            return currentMenu.name;
        }

        internal void TryGotoMenu(string name)
        {
            bool exists = menus.ContainsKey(name);

            if (exists) 
                current.Push(name);
        }
    }
}

