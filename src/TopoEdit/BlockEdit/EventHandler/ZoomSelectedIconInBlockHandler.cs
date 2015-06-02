using System;
using System.Collections.Generic;
using System.Text;
using TopoEdit.EventHandler;
using TopoEdit.Icon;
using System.Diagnostics;
using System.Drawing;
using TopoEdit.Visitor;
using TopoEdit.Command;

namespace TopoEdit.EventHandler
{
    class ZoomSelectedIconInBlockHandler : IRangeEventHandler
    {
        private SelectedDraw m_selIcon = null;
        /// <summary>
        /// 保存变形前的值，以便可以撤销
        /// </summary>
        private IDraw m_oldIcon = null;
        /// <summary>
        /// 是否处在变形操作中
        /// </summary>
        private bool m_isMoving = false;

        internal override void MouseDown(object sender, RangeMouseEventArgs e)
        {
            if (e.MouseSelectedIcon != null)
            {
                m_selIcon = e.MouseSelectedIcon.Intersect(e.Location) as SelectedDraw;
                m_oldIcon = m_selIcon.Icon.Clone();
            }
        }

        internal override void MouseMove(object sender, RangeMouseEventArgs e)
        {
            if (e.IsMouseDown)
            {
                //鼠标已经按下，且单击在锚点上，则缩放被选图标
                if (e.MouseAnchor != null)
                {
                    if ((e.MouseSelectedIcon != null) && (e.MouseSelectedIcon is SelectedRange))
                    {
                        //判断单击在那个锚点上
                        Debug.Assert(e.MouseSelectedIcon is SelectedRange);
                        if ((m_selIcon != null) && (m_selIcon is SelectedDraw))
                        {
                            //根据锚点类型进行缩放和平移
                            Zoom zoom = null;
                            Movement move = null;
                            ScaleOpMode mode = ScaleOpMode.None;

                           (m_selIcon as SelectedDraw).CreateZoomAndMoveByAnchor(out zoom, out move, out mode, e.Location
                                , e.MouseLastPos, m_selIcon.BoundsRect, e.MouseAnchor);
                            

                            Debug.Assert(null != zoom);
                            Debug.Assert(null != move);

                            zoom.SubMode = mode;
                            move.SubMode = mode;

                            ZoomAndMoveSelIconInBlockByAnchorVisitor visitor = new ZoomAndMoveSelIconInBlockByAnchorVisitor(e.MouseAnchor, zoom, move);
                            m_selIcon.Accept(visitor);
                            RectangleF rect = visitor.Range;

                            //将缩放前区域加入刷新区域
                            //RectangleF rect = m_selIcon.BoundsRect;
                            //m_selIcon.Zoom(zoom);
                            ////将缩放后区域加入刷新区域
                            //rect = TopoEdit.Utility.Union(rect, m_selIcon.BoundsRect);

                            e.RangeView.Refresh(TopoEdit.Utility.AdjustRect(rect, 10));

                            m_isMoving = true;
                        }
                    }
                }
            }
        }

        internal override void MouseUp(object sender, RangeMouseEventArgs e)
        {
            if (m_isMoving)
            {
                e.RangeView.CmdMgr.AddCommand(new UpdateSelectedDrawCommand(e.RangeView, m_selIcon, m_selIcon.Icon, m_oldIcon));
                m_isMoving = false;
            }
        }
    }
}
