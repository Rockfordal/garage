namespace GarageApp
{
    class MenuAction
    {
        public int id { get; set; }
        public MenuBuilder.ActionType type { get; set; }
        public string data { get; set; }
        public string extra { get; set; }


        public MenuAction(MenuBuilder.ActionType type, string data, string extra)
        {
            this.type  = type;
            this.data  = data;
            this.extra = extra;
        }

        public MenuAction(MenuBuilder.ActionType type, string data) : this(type, data, null)
        {
        }

        public MenuAction(MenuBuilder.ActionType type) : this(type, "")
        {
        }

        public MenuAction() : this(MenuBuilder.ActionType.Noop)
        {
        }

    }
}
