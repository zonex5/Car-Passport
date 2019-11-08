using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarPassport
{
    class Item
    {
        public object id;
        public object name;
        public object tag;

        public Item(object id, object name)
        {
            this.id = id;
            this.name = name;
        }

        public override string ToString()
        {
            return name.ToString();
        }
    }
}
