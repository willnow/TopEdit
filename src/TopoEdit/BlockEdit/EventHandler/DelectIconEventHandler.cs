using System;
using System.Collections.Generic;
using System.Text;
using TopoEdit.EventHandler;
using System.Windows.Forms;
using TopoEdit.Command;

namespace TopoEdit.EventHandler
{
    class DelectIconEventHandler : IRangeEventHandler
    {
        internal override void KeyDown(object sender, RangeKeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Delete)
            {
                //删除选中的ICON
                if (null != e.RangeData.SelectedRange)
                {
                    if (MessageBox.Show("是否确定删除", "删除图元", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        //构建删除命令并执行
                        e.RangeView.CmdMgr.AddThenExec(new DelDrawCommand(e.RangeView, e.RangeData, e.RangeData.SelectedRange.Icons));

                        e.MouseSelectedIcon = null;//删除被选元素
                    }
                    else
                    {
                        //不删除图元
                    }
                }
                else
                {
                    //没有图元被选中
                }
            }
        }
    }
}
