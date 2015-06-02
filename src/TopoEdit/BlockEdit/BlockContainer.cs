using System;
using System.Collections.Generic;
using System.Text;
using TopoEdit.Icon;
using TopoEdit.Model;
using BlockEdit.Model;

namespace TopoEdit
{
    public class BlockContainer : List<Block>
    {
        public static BlockContainer Instance = new BlockContainer();

        public Block GetBlockByName(string name)
        {
            for (int i = 0; i < Count; ++i)
            {
                if (this[i].Name == name)
                {
                    return this[i];
                }
            }

            return null;
        }

        public List<string> GetBlockNames()
        {
            List<string> names = new List<string>();

            for (int i = 0; i < Count; ++i)
            {
                names.Add(this[i].Name);
            }

            return names;
        }

        public ColorLib GetColorLib()
        {
            return ColorLib.Instance;
        }

        public MenuLib GetMenuLib()
        {
            return MenuLib.Instance;
        }
    }
}
