using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Diagnostics;
using TopoEdit.Stratege;

namespace TopoEdit.Visitor
{
    /// <summary>
    /// 判断相交算法的类型
    /// </summary>
    public enum IntersectType
    {
        InBound,//边界里
        OnBound,//边界上
    }

    abstract class IntersectVisitor : IDrawVisitor
    {
        /// <summary>
        /// 判断相交的类型
        /// </summary>
        protected IntersectType m_intersectType = IntersectType.OnBound;
        protected bool m_isVisible = false;
        protected IIntersectStrategy m_intersectStrategy = null;
        protected PointF m_point;

        public IntersectVisitor(IntersectType intersectType, PointF point)
        {
            m_intersectType = intersectType;
            m_point = point;
        }

        public bool Visible
        {
            get
            {
                return m_isVisible;
            }
        }

        #region IVisitor 成员

        public virtual void VisitorCircle(TopoEdit.Icon.IconCircle icon)
        {
            throw new NotImplementedException();
        }

        public virtual void VisitorLine(TopoEdit.Icon.IconLine icon)
        {
            throw new NotImplementedException();
        }

        public virtual void VisitorPolygon(TopoEdit.Icon.IconPolygon icon)
        {
            throw new NotImplementedException();
        }

        public virtual void VisitorRectangle(TopoEdit.Icon.IconRectangle icon)
        {
            throw new NotImplementedException();
        }

        public virtual void VisitorText(TopoEdit.Icon.IconText icon)
        {
            throw new NotImplementedException();
        }

        public virtual void VisitorBlock(TopoEdit.Icon.Block block)
        {
            throw new NotImplementedException();
        }

        public virtual void VisitorBlockRect(TopoEdit.Icon.BlockRect blockRect)
        {
            throw new NotImplementedException();
        }

        public virtual void VisitorPage(TopoEdit.Icon.Page topo)
        {
            throw new NotImplementedException();
        }

        public void VisitorPageRect(TopoEdit.Icon.PageRect blockRect)
        {
            throw new NotImplementedException();
        }

        public virtual void VisitorSelectedBlockRect(TopoEdit.Icon.SelectedBlockRect selBlockRect)
        {
            throw new NotImplementedException();
        }

        public virtual void VisitorSelectedItem(TopoEdit.Icon.SelectedItem selItem)
        {
            throw new NotImplementedException();
        }

        public virtual void VisitorSelectedRange(TopoEdit.Icon.SelectedRange selRange)
        {
            throw new NotImplementedException();
        }

        public virtual void VisitorSelectedPolygon(TopoEdit.Icon.SelectedPolygon selPolygon)
        {
            throw new NotImplementedException();
        }

        public virtual void VisitorBook(TopoEdit.Icon.Book book)
        {
            throw new NotImplementedException();
        }

        public virtual void VisitorSelectedPageRect(TopoEdit.Icon.SelectedPageRect selPageRect)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
