using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace TopoEdit.EventHandler
{
    class PaintGridEventHandler : IRangeEventHandler
    {
        internal override void Paint(object sender, RangePaintEventArgs e)
        {
            //绘制网格
            
            RectangleF viewBound = e.RangeView.RealBound;
            //绘制横线
            for (int i = 0; i < viewBound.Height / 20; ++i)
            {
                e.Graphics.DrawLine(new Pen(Color.DarkGray, 1), new PointF(0, i * 20), new PointF(viewBound.Width, i * 20));
            }
            //绘制竖线
            for (int i = 0; i < viewBound.Width / 20; ++i)
            {
                e.Graphics.DrawLine(new Pen(Color.DarkGray, 1), new PointF(i * 20, 0), new PointF(i * 20, viewBound.Height));
            }
        }
    }
}
