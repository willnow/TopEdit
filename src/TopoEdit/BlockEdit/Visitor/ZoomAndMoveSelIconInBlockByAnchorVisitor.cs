using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using TopoEdit.Icon;
using TopoEdit.EventHandler;
using System.Diagnostics;

namespace TopoEdit.Visitor
{
    class ZoomAndMoveSelIconInBlockByAnchorVisitor : IDrawVisitor
    {
        private Movement m_move;
        private Zoom m_zoom;
        private RectangleF m_range = new RectangleF();
        /// <summary>
        /// 移动的锚点
        /// </summary>
        private Anchor m_anchor = null;

        public ZoomAndMoveSelIconInBlockByAnchorVisitor(Anchor anchor, Zoom zoom, Movement move)
        {
            m_move = move.Clone() as Movement;
            m_zoom = zoom.Clone() as Zoom;

            m_anchor = anchor;

            Debug.Assert(m_move != null);
            Debug.Assert(m_zoom != null);
            Debug.Assert(m_anchor != null);
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
            VisitorDraw(icon);
        }

        public void VisitorLine(TopoEdit.Icon.IconLine icon)
        {
            VisitorDraw(icon);
        }

        public void VisitorPolygon(TopoEdit.Icon.IconPolygon icon)
        {
            //将缩放前区域加入刷新区域
            RectangleF rect = icon.BoundsRect;
            
            //移动多变形的一个顶点
            if (m_anchor is AnchorEndPoint)
            {
                icon.MoveEndPoint((m_anchor as AnchorEndPoint).Index, m_move);
            }
            else if (m_anchor is AnchorControlPoint)
            {
                icon.MoveControlPoint((m_anchor as AnchorControlPoint).PathIndex, (m_anchor as AnchorControlPoint).ControlIndex, m_move);
            }
            else
            {
                Debug.Assert(false);
            }

            //将缩放后区域加入刷新区域
            m_range = TopoEdit.Utility.Union(rect, icon.BoundsRect);
        }

        public void VisitorRectangle(TopoEdit.Icon.IconRectangle icon)
        {
            //将缩放前区域加入刷新区域
            RectangleF rect = icon.BoundsRect;

            if (icon.Degree == 0)
            {
                icon.Zoom(m_zoom);//如果是正常矩形，则直接缩放
            }
            else
            {
                Movement move = m_move.Clone() as Movement;
                icon.MovePolygon(move, m_zoom);
            }

            //将缩放后区域加入刷新区域
            m_range = TopoEdit.Utility.Union(rect, icon.BoundsRect);
        }

        public void VisitorText(TopoEdit.Icon.IconText icon)
        {
            VisitorDraw(icon);
        }

        public void VisitorBlock(TopoEdit.Icon.Block block)
        {
            Debug.Assert(false);
        }

        public void VisitorBlockRect(TopoEdit.Icon.BlockRect blockRect)
        {
            Debug.Assert(false);
        }

        public void VisitorPage(TopoEdit.Icon.Page topo)
        {
            Debug.Assert(false);
        }

        public void VisitorPageRect(TopoEdit.Icon.PageRect blockRect)
        {
            throw new NotImplementedException();
        }

        public void VisitorSelectedBlockRect(TopoEdit.Icon.SelectedBlockRect selBlockRect)
        {
            VisitorDraw(selBlockRect);
        }

        public void VisitorSelectedItem(TopoEdit.Icon.SelectedItem selItem)
        {
            //将缩放前区域加入刷新区域
            RectangleF rect = selItem.BoundsRect;
            //缩放图标
            selItem.Icon.Accept(this);
            //重绘锚点
            selItem.ResetAnchor();
            //将缩放后区域加入刷新区域
            m_range = TopoEdit.Utility.Union(rect, selItem.BoundsRect);
        }

        public void VisitorSelectedPolygon(SelectedPolygon selItem)
        {
            VisitorSelectedItem(selItem);
        }

        public void VisitorSelectedRange(TopoEdit.Icon.SelectedRange selRange)
        {
            Debug.Assert(false);
        }

        private void VisitorDraw(IDraw draw)
        {
            //将缩放前区域加入刷新区域
            RectangleF rect = draw.BoundsRect;
            draw.Zoom(m_zoom);
            //将缩放后区域加入刷新区域
            m_range = TopoEdit.Utility.Union(rect, draw.BoundsRect);
        }

        public void VisitorBook(Book book)
        {
            Debug.Assert(false);
        }

        public void VisitorSelectedPageRect(SelectedPageRect selPageRect)
        {
            Debug.Assert(false);
        }

        #endregion
    }
}
