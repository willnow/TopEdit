using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using TopoEdit.EventHandler;
using System.Diagnostics;
using TopoEdit.Icon;

namespace TopoEdit.Visitor
{

    /// <summary>
    /// 跟随锚点的移动缩放或平移页中的Block实例
    /// </summary>
    /// <param name="zoom">缩放向量</param>
    /// <param name="move">平移向量</param>
    /// <returns>需要刷新的区域</returns>
    class ZoomAndMoveSelBlockRectInPageByAnchorVisitor : IDrawVisitor
    {
        private Movement m_move;
        private Zoom m_zoom;
        private RectangleF m_range = new RectangleF();

        public ZoomAndMoveSelBlockRectInPageByAnchorVisitor(Zoom zoom, Movement move)
        {
            m_move = move.Clone() as Movement;
            m_zoom = zoom.Clone() as Zoom;

            Debug.Assert(m_move != null);
            Debug.Assert(m_zoom != null);
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
            VisitorIcon(icon);
        }

        public void VisitorLine(TopoEdit.Icon.IconLine icon)
        {
            VisitorIcon(icon);
        }

        public void VisitorPolygon(TopoEdit.Icon.IconPolygon icon)
        {
            VisitorIcon(icon);
        }

        public void VisitorRectangle(TopoEdit.Icon.IconRectangle icon)
        {
            //将缩放前区域加入刷新区域
            RectangleF rect = icon.BoundsRect;
            ScaleOpMode mode = m_zoom.SubMode;
            Movement newMove = m_move.Clone() as Movement;

            //根据停靠属性和固定属性，判断是否可以被缩放
            if ( icon.CanZoomPart(mode))
            {
                //可以缩放，则不能平移
                //缩放
                if (icon.Degree == 0)
                {
                    icon.Zoom(m_zoom);//如果是正常矩形，则直接缩放
                }
                else
                {
                    Movement move = m_move.Clone() as Movement;
                    icon.MovePolygon(move, m_zoom);
                }
            }
            else if (icon.CanMovePart(mode))
            {
                if (icon.Fixed)
                {
                    //固定大小元素不允许做拉伸平移
                    newMove.SubMode = TopoEdit.EventHandler.ScaleOpMode.None;
                }
                //不固定大小元素，做拉伸平移
                icon.Move(newMove);
            }
            else
            {
                //不能缩放也不能平移
            }

            //将缩放后区域加入刷新区域
            m_range = TopoEdit.Utility.Union(rect, icon.BoundsRect);
            
        }

        public void VisitorText(TopoEdit.Icon.IconText icon)
        {
            VisitorIcon(icon);
        }

        public void VisitorBlock(TopoEdit.Icon.Block block)
        {
            VisitorIRange(block);
        }

        public void VisitorBlockRect(TopoEdit.Icon.BlockRect blockRect)
        {
            RectangleF rect = blockRect.BoundsRect;

            if (blockRect.Template != null)
            {
                blockRect.Template.Accept(this);
            }

            PointF srcRectPos = blockRect.Rect.Position;
            blockRect.Rect.Zoom(m_zoom);

            //rect不能缩放的比block还要小，如果小了，则恢复成和block一样大
            if (blockRect.Template != null)
            {
                if ((blockRect.Rect.Position.X >= blockRect.Template.BoundsRect.Left) || (blockRect.Rect.Position.Y >= blockRect.Template.BoundsRect.Top))
                {
                    blockRect.Rect.Position = new PointF(blockRect.Template.BoundsRect.Left, blockRect.Template.BoundsRect.Top);
                    blockRect.Rect.Width = blockRect.Template.BoundsRect.Width;
                    blockRect.Rect.Height = blockRect.Template.BoundsRect.Height;
                }
            }

            PointF destRectPos = blockRect.Rect.Position;

            if (m_move.SubMode != ScaleOpMode.None)
            {
                blockRect.CalcCenter(srcRectPos, destRectPos);
            }

            m_range = Utility.Union(rect, blockRect.BoundsRect);
        }

        public void VisitorPage(TopoEdit.Icon.Page topo)
        {
            if (topo.SelectedRange != null)
            {
                //需要考虑包含元素的停靠属性和固定属性
                //将缩放前区域加入刷新区域
                RectangleF rect = topo.BoundsRect;

                //缩放或平移被选区域
                foreach (IDraw draw in topo.SelectedRange.SelIcons)
                {
                    //根据停靠属性和固定属性，判断是否可以被缩放
                    if (draw is SelectedBlockRect)
                    {
                        (draw as SelectedBlockRect).Accept(this);
                    }
                    else
                    {
                        Debug.Assert(false);
                    }
                }

                //将缩放后区域加入刷新区域
                m_range = TopoEdit.Utility.Union(rect, topo.BoundsRect); 
            }
            else
            {
                m_range = new RectangleF();
            }
        }

        public void VisitorPageRect(TopoEdit.Icon.PageRect blockRect)
        {
            throw new NotImplementedException();
        }

        public void VisitorSelectedBlockRect(TopoEdit.Icon.SelectedBlockRect selBlockRect)
        {
            Debug.Assert(selBlockRect.Icon is BlockRect);

            RectangleF rect = selBlockRect.BoundsRect;

            (selBlockRect.Icon as BlockRect).Accept(this);

            //平移或缩放后重置锚点
            selBlockRect.ResetAnchor();

            m_range = Utility.Union(rect, selBlockRect.BoundsRect);
        }

        public void VisitorSelectedItem(TopoEdit.Icon.SelectedItem selItem)
        {
            //将缩放前区域加入刷新区域
            RectangleF rect = selItem.BoundsRect;
            //旋转图标
            selItem.Icon.Accept(this);
            //重绘锚点
            selItem.ResetAnchor();
            //将缩放后区域加入刷新区域
            m_range = TopoEdit.Utility.Union(rect, selItem.BoundsRect);
        }

        public void VisitorSelectedRange(TopoEdit.Icon.SelectedRange selRange)
        {
            VisitorIRange(selRange);
        }

        private void VisitorIRange(IRange range)
        {
            if (range.SelectedRange != null)
            {
                range.SelectedRange.Accept(this);
                return;
            }
            else
            {
                //对于复杂元素，需要考虑包含元素的停靠属性和固定属性
                RectangleF rect = range.BoundsRect;

                //缩放或平移被选区域
                foreach (IDraw draw in range.Icons)
                {
                    ZoomAndMoveSelBlockRectInPageByAnchorVisitor visitor = new ZoomAndMoveSelBlockRectInPageByAnchorVisitor(m_zoom, m_move);
                    draw.Accept(visitor);
                }

                //将缩放后区域加入刷新区域
                m_range = TopoEdit.Utility.Union(rect, range.BoundsRect);
            }
        }

        private void VisitorIcon(IIcon icon)
        {
            //将缩放前区域加入刷新区域
            RectangleF rect = icon.BoundsRect;
            ScaleOpMode mode = m_zoom.SubMode;
            Movement newMove = m_move.Clone() as Movement;

            //根据停靠属性和固定属性，判断是否可以被缩放
            if (icon.CanZoomPart(mode))
            {
                //可以缩放，则不能平移
                icon.Zoom(m_zoom);
            }
            else if (icon.CanMovePart(mode))
            {
                if (icon.Fixed)
                {
                    //固定大小元素不允许做拉伸平移
                    newMove.SubMode = TopoEdit.EventHandler.ScaleOpMode.None;
                }
                //不固定大小元素，做拉伸平移
                icon.Move(newMove);
            }
            else
            {
                //不能缩放也不能平移
            }

            //将缩放后区域加入刷新区域
            m_range = TopoEdit.Utility.Union(rect, icon.BoundsRect);
        }

        public void VisitorSelectedPolygon(SelectedPolygon selPolygon)
        {
            VisitorSelectedItem(selPolygon);
        }

        public void VisitorBook(Book book)
        {
            throw new NotImplementedException();
        }

        public void VisitorSelectedPageRect(SelectedPageRect selPageRect)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
