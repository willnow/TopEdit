using System;
using System.Collections.Generic;
using System.Text;
using TopoEdit.EventHandler;
using TopoEdit.Icon;
using TopoEdit.PropertyControl;
using System.Drawing;
using TopoEdit.Command;

namespace TopoEdit.EventHandler
{
    class EditBlockRectPropertyEventHandler : IRangeEventHandler
    {
        internal override void MouseDoubleClick(object sender, RangeMouseEventArgs e)
        {
            if ((null != e.MouseSelectedIcon) && (e.MouseSelectedIcon is SelectedRange))
            {
                //仅单击了某个图元
                if ((e.MouseSelectedIcon as SelectedRange).Count == 1)
                {
                    SelectedDraw selIcon = (e.MouseSelectedIcon as SelectedRange).GetIcon(0) as SelectedDraw;
                    IDraw oldIcon = selIcon.Icon.Clone();
                    BlockRectPropertyForm form = new BlockRectPropertyForm(selIcon.Icon as BlockRect);
                    if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        //构建被选图元的更新命令
                        e.RangeView.CmdMgr.AddThenExec(new UpdateSelectedDrawCommand(e.RangeView, selIcon, form.EditedBlockRect, oldIcon));
                    }
                }
            }
        }
    }
}
