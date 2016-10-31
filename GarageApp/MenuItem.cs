namespace GarageApp
{
    class MenuItem
    {
        public int id { get; set; }
        public string label { get; set; }
        public MenuAction action { get; set; }
        bool selectable = true;

        public MenuItem(string label) : this(label, new MenuAction(MenuBuilder.ActionType.Noop, ""))
        {
        }

        public MenuItem(string label, MenuAction action) : this(label, action, 0)
        {
        }

        public MenuItem(string label, MenuAction action, int id)
        {
            this.label = label;
            this.action = action;
            this.id = id;
        }

        public MenuItem(string label, bool selectable)
        {
            this.label = label;
            this.selectable = false;
        }

        public override string ToString()
        {
            return label;
        }
    }
}
