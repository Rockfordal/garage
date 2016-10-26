using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageApp
{
    class MenuAction
    {
        public int id                 { get; set; }
        public Common.ActionType type { get; set; }
        public string data            { get; set; }
        public MenuItem fromMenuItem  { get; set; }
        //public string fromMenu        { get; set; }

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
    }

}
