using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Drawing2D;
using TopoEdit.Stratege;
using System.Drawing;
using System.Diagnostics;

namespace TopoEdit.Visitor
{
    class IntersectLineVisitor : IntersectVisitor
    {
        private PointF m_point1;
        private PointF m_point2;
        private int m_width;

        public IntersectLineVisitor(IntersectType intersectType, PointF point, PointF point1, PointF point2, int width)
            : base(intersectType, point)
        {
            m_point1 = point1;
            m_point2 = point2;
            m_width = width;

            CreateIntersectStrategy();

            Debug.Assert(m_point1 != m_point2);
            Debug.Assert(m_width > 0);
        }
        protected void CreateIntersectStrategy()
        {
            m_intersectStrategy = new IntersectByLineStrategy(m_point1, m_point2, m_width);
        }

        #region IVisitor 成员

        public override void VisitorLine(TopoEdit.Icon.IconLine icon)
        {
            m_isVisible = m_intersectStrategy.IsVisible(m_point);
        }
        #endregion
    }
}
