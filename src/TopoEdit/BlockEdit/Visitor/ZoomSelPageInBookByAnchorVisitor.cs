using System;
using System.Collections.Generic;
using System.Text;
using TopoEdit.Icon;
using System.Drawing;

namespace TopoEdit.Visitor
{
    class ZoomSelPageInBookByAnchorVisitor : IDrawVisitor
    {
        private Zoom m_zoom;
        private RectangleF m_range = new RectangleF();

        public ZoomSelPageInBookByAnchorVisitor(Zoom zoom)
        {
            m_zoom = zoom;
        }

        public RectangleF Range
        {
            get
            {
                return m_range;
            }
        }

        #region IVisitor 成员

        public void VisitorCircle(TopoEdit.Icon.IconCircle icon)
        {
            throw new NotImplementedException();
        }

        public void VisitorLine(TopoEdit.Icon.IconLine icon)
        {
            throw new NotImplementedException();
        }

        public void VisitorPolygon(TopoEdit.Icon.IconPolygon icon)
        {
            throw new NotImplementedException();
        }

        public void VisitorRectangle(TopoEdit.Icon.IconRectangle icon)
        {
            throw new NotImplementedException();
        }

        public void VisitorText(TopoEdit.Icon.IconText icon)
        {
            throw new NotImplementedException();
        }

        public void VisitorBlock(TopoEdit.Icon.Block block)
        {
            throw new NotImplementedException();
        }

        public void VisitorBlockRect(TopoEdit.Icon.BlockRect blockRect)
        {
            throw new NotImplementedException();
        }

        public void VisitorPage(TopoEdit.Icon.Page page)
        {
            throw new NotImplementedException();
        }

        public void VisitorPageRect(TopoEdit.Icon.PageRect blockRect)
        {
            throw new NotImplementedException();
        }

        public void VisitorBook(TopoEdit.Icon.Book book)
        {
            throw new NotImplementedException();
        }

        public void VisitorSelectedBlockRect(TopoEdit.Icon.SelectedBlockRect selBlockRect)
        {
            throw new NotImplementedException();
        }

        public void VisitorSelectedPageRect(TopoEdit.Icon.SelectedPageRect selPageRect)
        {
            //将缩放前区域加入刷新区域
            RectangleF rect = selPageRect.BoundsRect;

            selPageRect.Zoom(m_zoom);

            //将缩放后区域加入刷新区域
            m_range = TopoEdit.Utility.Union(rect, selPageRect.BoundsRect);
        }

        public void VisitorSelectedItem(TopoEdit.Icon.SelectedItem selItem)
        {
            throw new NotImplementedException();
        }

        public void VisitorSelectedRange(TopoEdit.Icon.SelectedRange selRange)
        {
            throw new NotImplementedException();
        }

        public void VisitorSelectedPolygon(TopoEdit.Icon.SelectedPolygon selPolygon)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
