using System;
using System.Collections.Generic;
using System.Text;
using TopoEdit.Icon;
using System.Drawing;

namespace TopoEdit.EventHandler
{
    class ZoomPanelViewEventHandler : IRangeEventHandler
    {
        internal override void MouseWheel(object sender, RangeMouseEventArgs e)
        {
            if (e.IsCtrlDown)
            {
                Zoom zoom = new Zoom();
                zoom.ZoomMode = CSR.CUIT.GlobalService.ShareLib.EmZoomMode.UniformScale;

                if (e.Delta > 0)
                {
                    e.RangeView.ZoomRate *= 1.05F;
                    zoom.XRadio = e.Delta / 120.0F * 1.05F;
                }
                else
                {
                    e.RangeView.ZoomRate *= 0.95F;
                    zoom.XRadio = -e.Delta / 120.0F * 0.95F;
                }

                zoom.YRadio = zoom.XRadio;

                if (((e.RangeData.BoundsRect.Width < 20) ||
                    (e.RangeData.BoundsRect.Height < 20)) &&
                    (zoom.XRadio < 1.0))
                {
                    //BLOCK太小，不能在缩放
                    return;
                }
                else if (((e.RangeData.BoundsRect.Width > 1000) ||
                    (e.RangeData.BoundsRect.Height > 1000)) &&
                    (zoom.XRadio > 1.0))
                {
                    //BLOCK太大，不能在缩放
                    return;
                }
                else
                {
                    ////计算缩放前中心点
                    //PointF posBeforeZoom = TopoEdit.Utility.GetCenter(e.RangeData.BoundsRect);
                    ////缩放
                    //e.RangeData.Zoom(zoom);
                    ////计算缩放后中心点
                    //PointF posAfterZoom = TopoEdit.Utility.GetCenter(e.RangeData.BoundsRect);
                    ////将放大后BLOCK移动到放大前中心点所在位置
                    //Movement move = new Movement();
                    //move.XMovement = (int)(-posAfterZoom.X + posBeforeZoom.X);
                    //move.YMovement = (int)(-posAfterZoom.Y + posBeforeZoom.Y);
                    //e.RangeData.Move(move);

                    e.RangeData.Zoom(zoom);
                    //刷新绘制
                    e.RangeView.Invalidate(true);
                }
            }
        }
    }
}
