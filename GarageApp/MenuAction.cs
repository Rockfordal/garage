using Microsoft.Win32.SafeHandles;

namespace GarageApp
{
    class MenuAction
    {
        public int id                 { get; set; }
        public Common.ActionType type { get; set; }
        public string data            { get; set; }

        public MenuAction(Common.ActionType type, string data, int id)
        {
            this.type = type;
            this.data = data;
            this.id = id;
        }

        public MenuAction(Common.ActionType type, string data)
        {
            this.type = type;
            this.data = data;
        }

        public MenuAction(Common.ActionType type) : this(type, "")
        {
        }

        public MenuAction() : this(Common.ActionType.noop)
        {
        }

    }
}
