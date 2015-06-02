using System;
using System.Collections.Generic;
using System.Text;
using TopoEdit.Stratege;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Diagnostics;

namespace TopoEdit.Visitor
{
    class IntersectIconVisitor : IntersectVisitor
    {
        private GraphicsPath m_rgn;
        private Pen m_pen;

        public IntersectIconVisitor(IntersectType intersectType, PointF point, GraphicsPath rgn, Pen pen) : base(intersectType, point)
        {
            m_intersectType = intersectType;
            m_rgn = rgn;
            m_pen = pen;
            m_point = point;
            CreateIntersectStrategy();
        }

        protected void CreateIntersectStrategy()
        {
            switch (m_intersectType)
            {
                case IntersectType.OnBound:
                    {
                        m_intersectStrategy = new IntersectByGraphicsPathOnBoundStrategy(m_rgn, m_pen);
                    }
                    break;
                case IntersectType.InBound:
                    {
                        m_intersectStrategy = new IntersectByGraphicsPathInBoundStrategy(m_rgn);
                    }
                    break;
                default:
                    {
                        Debug.Assert(false);
                    }
                    break;
            }
            Debug.Assert(m_intersectStrategy != null);
        }

        #region IVisitor 成员

        public override void VisitorCircle(TopoEdit.Icon.IconCircle icon)
        {
            Debug.Assert(m_intersectStrategy != null);
            m_isVisible = m_intersectStrategy.IsVisible(m_point);
        }

        public override void VisitorLine(TopoEdit.Icon.IconLine icon)
        {
            throw new NotImplementedException();
        }

        public override void VisitorPolygon(TopoEdit.Icon.IconPolygon icon)
        {
            Debug.Assert(m_intersectStrategy != null);
            m_isVisible = m_intersectStrategy.IsVisible(m_point);
        }

        public override void VisitorRectangle(TopoEdit.Icon.IconRectangle icon)
        {
            Debug.Assert(m_intersectStrategy != null);
            m_isVisible = m_intersectStrategy.IsVisible(m_point);
        }

        public override void VisitorText(TopoEdit.Icon.IconText icon)
        {
            Debug.Assert(m_intersectStrategy != null);
            m_isVisible = m_intersectStrategy.IsVisible(m_point);
        }

        public override void VisitorBlock(TopoEdit.Icon.Block block)
        {
            throw new NotImplementedException();
        }

        public override void VisitorBlockRect(TopoEdit.Icon.BlockRect blockRect)
        {
            throw new NotImplementedException();
        }

        public override void VisitorPage(TopoEdit.Icon.Page topo)
        {
            throw new NotImplementedException();
        }

        public override void VisitorSelectedBlockRect(TopoEdit.Icon.SelectedBlockRect selBlockRect)
        {
            throw new NotImplementedException();
        }

        public override void VisitorSelectedItem(TopoEdit.Icon.SelectedItem selItem)
        {
            throw new NotImplementedException();
        }

        public override void VisitorSelectedRange(TopoEdit.Icon.SelectedRange selRange)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
