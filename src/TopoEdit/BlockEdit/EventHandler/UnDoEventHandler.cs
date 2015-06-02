using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using TopoEdit.Icon;
using TopoEdit.Command;

namespace TopoEdit.EventHandler
{
    //ctrl+z撤销
    //ctrl+y恢复
    class UnDoEventHandler : IRangeEventHandler
    {
        internal override void KeyDown(object sender, RangeKeyEventArgs e)
        {
            if (e.DownToopStripButton.Count > 0)
            {
                return;
            }

            if (e.IsCtrlDown && e.KeyCode == Keys.Z)
            {
                //按下了ctrl+z
                e.RangeView.CmdMgr.GoBack();
            }
            
            if (e.IsCtrlDown && e.KeyCode == Keys.Y)
            {
                //按下了ctrl+y
                e.RangeView.CmdMgr.GoBefore();
            }
        }
    }
}
