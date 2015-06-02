using System;
using System.Collections.Generic;
using System.Text;
using TopoEdit.Icon;
using TopoEdit.PropertyControl;
using System.Drawing;
using System.Diagnostics;
using TopoEdit.Command;

namespace TopoEdit.EventHandler
{
    class EditIconPropertyEventHandler : IRangeEventHandler
    {
        internal override void MouseDoubleClick(object sender, RangeMouseEventArgs e)
        {
            if ((null != e.MouseSelectedIcon) && (e.MouseSelectedIcon is SelectedRange))
            {
                if ((e.MouseSelectedIcon as SelectedRange).Count == 1)
                {
                    //仅双击了某个图元

                    SelectedDraw selIcon = (e.MouseSelectedIcon as SelectedRange).GetIcon(0) as SelectedDraw;
                    IDraw oldIcon = selIcon.Icon.Clone();
                    IconPropertyForm form = new IconPropertyForm(selIcon.Icon as IIcon);
                    if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        e.RangeView.CmdMgr.AddThenExec(new UpdateSelectedDrawCommand(e.RangeView, selIcon, form.EditedIcon, oldIcon));
                    }
                }
                else if ((e.MouseSelectedIcon as SelectedRange).Count > 1)
                {
                    //双击了多个图元
                    SelectedRange oldRange = e.MouseSelectedIcon as SelectedRange;
                    RangePropertyForm form = new RangePropertyForm(oldRange);
                    if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        //编辑前边界
                        RectangleF rect = oldRange.BoundsRect;
                        SelectedRange newRange = form.EditedIcon as SelectedRange;

                        Debug.Assert(oldRange.Count == newRange.Count);//批量设置属性不能修改图元个数

                        ComplexCommand command = new ComplexCommand();
                        command.Tip = "批量修改图元属性命令";
                        //逐个复制修改属性的图元
                        for (int i = 0; i < oldRange.Count; ++i)
                        {
                            SelectedDraw icon = oldRange.GetIcon(i) as SelectedDraw;
                            command.AddCommand(new UpdateSelectedDrawCommand(e.RangeView, icon, newRange.Icons[i], icon.Icon.Clone()));
                        }
                        e.RangeView.CmdMgr.AddThenExec(command);

                        rect = Utility.Union(rect, oldRange.BoundsRect);
                        e.RangeView.Refresh(Utility.AdjustRect(rect, 10));
                    }
                }
            }
        }
    }
}
