using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace TopoEdit.EventHandler
{
    class PaintBoundEventHandler : IRangeEventHandler
    {
        private int m_resolutionRatioX = 1920;//水平分辨率
        private int m_resolutionRatioY = 1080;//水平分辨率

        public PaintBoundEventHandler(int resolutionRatioX, int resolutionRatioY)
        {
            m_resolutionRatioX = resolutionRatioX;
            m_resolutionRatioY = resolutionRatioY;
        }

        public int ResolutionRatioX
        {
            get
            {
                return m_resolutionRatioX;
            }
            set
            {
                m_resolutionRatioX = value;
            }
        }

        public int ResolutionRatioY
        {
            get
            {
                return m_resolutionRatioY;
            }
            set
            {
                m_resolutionRatioY = value;
            }
        }

        internal override void Paint(object sender, RangePaintEventArgs e)
        {
            //绘制分辨率框

            //绘制横线
            e.Graphics.DrawLine(new Pen(Color.DarkGray, 10), new PointF(0, m_resolutionRatioY), new PointF(m_resolutionRatioX, m_resolutionRatioY));
            //绘制竖线
            e.Graphics.DrawLine(new Pen(Color.DarkGray, 10), new PointF(m_resolutionRatioX, 0), new PointF(m_resolutionRatioX, m_resolutionRatioY));
        }
    }
}
