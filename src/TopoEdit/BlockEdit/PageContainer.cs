using System;
using System.Collections.Generic;
using System.Text;
using TopoEdit.Icon;

namespace TopoEdit
{
    public class PageContainer : List<Page>
    {
        public static PageContainer Instance = new PageContainer();

        public Page GetPageByName(string name)
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

        public List<string> GetPageNames()
        {
            List<string> names = new List<string>();

            for (int i = 0; i < Count; ++i)
            {
                names.Add(this[i].Name);
            }

            return names;
        }
    }
}
