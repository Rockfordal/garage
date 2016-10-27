using Microsoft.Win32.SafeHandles;

namespace GarageApp
{
    class MenuAction
    {
        public int id                 { get; set; }
        public MenuBuilder.ActionType type { get; set; }
        public string data            { get; set; }

        //public MenuAction(Common.ActionType type, string data, int id)
        //{
        //    this.type = type;
        //    this.data = data;
        //    this.id = id;
        //}

        public MenuAction(MenuBuilder.ActionType type, string data)
        {
            this.type = type;
            this.data = data;
        }

        public MenuAction(MenuBuilder.ActionType type) : this(type, "")
        {
        }

        public MenuAction() : this(MenuBuilder.ActionType.noop)
        {
        }

    }
}
