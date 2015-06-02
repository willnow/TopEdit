using System;
using System.Collections.Generic;
using System.Text;
using TopoEdit.Icon;
using System.Windows.Forms;
using System.Xml;

namespace TopoEdit.EventHandler
{
    class SaveBookEventHandler : TopoEdit.EventHandler.IRangeEventHandler
    {
        internal override void KeyDown(object sender, RangeKeyEventArgs e)
        {
            if (e.KeyCode == Keys.S)
            {
                if (e.IsCtrlDown)
                {
                    if (e.RangeData is Book)
                    {
                        if (MessageBox.Show("确定保存" + e.RangeData.Name + "吗?", "提示保存", MessageBoxButtons.OKCancel) == DialogResult.OK)
                        {
                            //ctrl + s
                            //清楚topo选中
                            e.RangeData.ClearSelectIcon();
                            //当前编辑的Block
                            Book book = (e.RangeData as Book);
                            //将book恢复到默认大小
                            Zoom zoom = new Zoom();
                            zoom.ZoomMode = CSR.CUIT.GlobalService.ShareLib.EmZoomMode.UniformScale;
                            zoom.XRadio = 1.0F / e.RangeView.ZoomRate;
                            zoom.YRadio = zoom.XRadio;
                            book.Zoom(zoom);
                            book.Round();

                            e.RangeView.ModelSaveInterceptor.SaveBookBefore(book);
                            DBHelper.Instance.SaveBook(book);
                            e.RangeView.ModelSaveInterceptor.SaveBookAfter(book);

                            //保存后，将topo恢复到当前大小
                            zoom.ZoomMode = CSR.CUIT.GlobalService.ShareLib.EmZoomMode.UniformScale;
                            zoom.XRadio = e.RangeView.ZoomRate;
                            zoom.YRadio = zoom.XRadio;
                            book.Zoom(zoom);

                            e.RangeData.Round();
                        }
                    }
                }
            }
        }
    }
}
