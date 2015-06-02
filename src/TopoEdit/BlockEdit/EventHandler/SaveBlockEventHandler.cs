using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using TopoEdit.Icon;
using System.Diagnostics;

namespace TopoEdit.EventHandler
{
    class SaveBlockEventHandler : IRangeEventHandler
    {
        internal override void KeyDown(object sender, RangeKeyEventArgs e)
        {
            if (e.KeyCode == Keys.S)
            {
                if (e.IsCtrlDown)
                {
                    if (e.RangeData is Block)
                    {

                        if (MessageBox.Show("确定保存" + e.RangeData.Name + "吗?", "提示保存", MessageBoxButtons.OKCancel) == DialogResult.OK)
                        {
                            //ctrl + s
                            Block block = (e.RangeData as Block);//当前编辑的Block

                            //清楚block选中
                            block.ClearSelectIcon();

                            //将Block恢复到默认大小
                            Zoom zoom = new Zoom();
                            zoom.ZoomMode = CSR.CUIT.GlobalService.ShareLib.EmZoomMode.UniformScale;
                            zoom.XRadio = 1.0F / e.RangeView.ZoomRate;
                            zoom.YRadio = zoom.XRadio;
                            block.Zoom(zoom);
                            //保存block
                            block.Round();

                            e.RangeView.ModelSaveInterceptor.SaveBlockBefore(block);
                            DBHelper.Instance.SaveBlock(block);
                            e.RangeView.ModelSaveInterceptor.SaveBlockAfter(block);

                            //BLOCK发生变化，需要重置拓扑图中的相关联对象
                            BlockPanel blockPanel = e.RangeView as BlockPanel;
                            Debug.Assert(null != blockPanel);
                            e.RangeData.Round();
                            blockPanel.NotifyPageToUpdate(e.RangeData as Block);

                            //保存后，将Block恢复到当前大小
                            zoom.ZoomMode = CSR.CUIT.GlobalService.ShareLib.EmZoomMode.UniformScale;
                            zoom.XRadio = e.RangeView.ZoomRate;
                            zoom.YRadio = zoom.XRadio;
                            block.Zoom(zoom);

                            e.RangeData.Round();
                        }
                    }
                }
            }
        }
    }
}
