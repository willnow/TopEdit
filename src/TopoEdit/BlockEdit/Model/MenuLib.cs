using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Diagnostics;

namespace BlockEdit.Model
{
    public class MenuLib
    {
        internal static MenuLib Instance = new MenuLib();
        private XmlNode m_menuNode = null;

        public void Load(XmlNode menuNode)
        {
            m_menuNode = menuNode;
        }

        public string GetMenuXmlContent()
        {
            Debug.Assert(m_menuNode != null);

            return m_menuNode.InnerXml;
        }
    }
}
