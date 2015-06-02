using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using TopoEdit.Icon;
using TopoEdit.EventHandler;
using System.Xml;
using System.Diagnostics;

namespace TopoEdit.EventHandler
{
    class SavePageEventHandler : TopoEdit.EventHandler.IRangeEventHandler
    {
        internal override void KeyDown(object sender, RangeKeyEventArgs e)
        {
            if (e.KeyCode == Keys.S)
            {
                if (e.IsCtrlDown)
                {
                    if (e.RangeData is Page)
                    {
                        if (MessageBox.Show("确定保存" + e.RangeData.Name + "吗?", "提示保存", MessageBoxButtons.OKCancel) == DialogResult.OK)
                        {
                            //ctrl + s
                            Page page = (e.RangeData as Page);//当前编辑的Page

                            //清楚page选中
                            e.RangeData.ClearSelectIcon();

                            //将Page恢复到默认大小
                            Zoom zoom = new Zoom();
                            zoom.ZoomMode = CSR.CUIT.GlobalService.ShareLib.EmZoomMode.UniformScale;
                            zoom.XRadio = 1.0F / e.RangeView.ZoomRate;
                            zoom.YRadio = zoom.XRadio;
                            page.Zoom(zoom);
                            page.Round();

                            e.RangeView.ModelSaveInterceptor.SavePageAfter(page);
                            DBHelper.Instance.SavePage(page);
                            e.RangeView.ModelSaveInterceptor.SavePageBefore(page);

                            //Page发生变化，需要重置拓扑图中的相关联对象
                            PagePanel pagePanel = e.RangeView as PagePanel;
                            Debug.Assert(null != pagePanel);
                            e.RangeData.Round();
                            pagePanel.NotifyBookToUpdate(e.RangeData as Page);

                            //保存后，将page恢复到当前大小
                            zoom.ZoomMode = CSR.CUIT.GlobalService.ShareLib.EmZoomMode.UniformScale;
                            zoom.XRadio = e.RangeView.ZoomRate;
                            zoom.YRadio = zoom.XRadio;
                            page.Zoom(zoom);
                            e.RangeData.Round();
                        }
                    }
                }
            }
        }
    }
}
