using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using TopoEdit;

namespace TopoCom
{
    [ComVisible(true)]
    [Guid("5F8669E6-006E-400d-8FDB-F3FD4E90E3AC")]
    [ProgId("TopoCom.TopoProcess")]
    public class TopoProcess : IMenu, ITopoContainer
    {
        #region IMenu 成员

        public void Add(System.Windows.Forms.ToolStripMenuItem menu)
        {
            if (menu != null)
            {
                TopoHelper.TopoForm.AddMenu(menu);
            }
            else
            {
                //不处理
            }
        }

        public void Remvoe(System.Windows.Forms.ToolStripMenuItem menu)
        {
            if (menu != null)
            {
                TopoHelper.TopoForm.RemoveMenu(menu);
            }
            else
            {
                //不处理
            }
        }

        #endregion

        #region ITopoContainer 成员

        public TopoEdit.Icon.Book GetBook()
        {
            return TopoEdit.Icon.Book.Instance;
        }

        public BlockContainer GetAllBlocks()
        {
            return TopoEdit.BlockContainer.Instance;
        }

        public PagePanel GetPagePanel()
        {
            
        }

        #endregion
    }
}
