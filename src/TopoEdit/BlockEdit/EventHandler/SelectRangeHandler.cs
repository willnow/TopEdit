using System;
using System.Collections.Generic;
using System.Text;
using TopoEdit.Icon;
using System.Drawing;
using System.Diagnostics;

namespace TopoEdit.EventHandler
{
    internal class SelectRangeHandler : IRangeEventHandler
    {
        /// <summary>
        /// 正在选中的虚线矩形框
        /// </summary>
        private IconRectangle m_selectingRange = null;

        internal override void MouseDown(object sender, RangeMouseEventArgs e)
        {
            if (e.DownToopStripButton.Count  == 0)
            {
                IDraw selDraw = e.RangeData.Intersect(e.Location);
                if (selDraw == null)
                {
                    //点没有落在某个ICON上，清除对齐参考图元
                    if (e.RangeData.SelectedRange != null)
                    {
                        e.RangeData.SelectedRange.AlignRef = null;
                    }
                    //点没有落在某个ICON上，则可能进行区域选择
                    m_selectingRange = new IconRectangle(e.Location, 1F, 1F, false, System.Drawing.Drawing2D.DashStyle.Dot);
                }
                else
                {
                    //点落在某个ICON上，设置对齐参考图元
                    if (e.RangeData.SelectedRange != null)
                    {
                        if (selDraw is SelectedRange)
                        {
                            //落在已经选中的图元上
                            selDraw = selDraw.Intersect(e.Location);//在被选图元中进一步选择
                            e.RangeData.SelectedRange.AlignRef = selDraw as SelectedDraw;
                        }
                        else
                        {
                            //落在了没有被选中的图元上，不修改对齐参考图元
                        }
                    }

                    m_selectingRange = null;
                }
            }
        }

        internal override void MouseUp(object sender, RangeMouseEventArgs e)
        {
            if (m_selectingRange != null)
            {
                e.RangeView.Refresh(TopoEdit.Utility.AdjustRect(TopoEdit.Utility.ConvertRect(m_selectingRange.BoundsRect), 10));
                m_selectingRange = null;
            }
        }

        internal override void MouseMove(object sender, RangeMouseEventArgs e)
        {
            if (e.IsMouseDown)
            {
                if (m_selectingRange != null)
                {
                    RectangleF rect = m_selectingRange.BoundsRect;

                    //绘制选中虚线矩形框
                    CalcSelectionRangePos(e.Location, e.MouseDownPos);

                    m_selectingRange.Width = Math.Abs(e.Location.X - e.MouseDownPos.X);
                    m_selectingRange.Height = Math.Abs(e.Location.Y - e.MouseDownPos.Y);

                    //将所有完全在选择虚线矩形框范围内的ICON全部设置为选中
                    e.RangeData.SetIconInRectSelected(m_selectingRange.BoundsRect);

                    rect = Utility.Union(rect, m_selectingRange.BoundsRect);//将移动前和移动后矩形区域合并，即为刷新区域

                    e.RangeView.Refresh(TopoEdit.Utility.AdjustRect(TopoEdit.Utility.ConvertRect(rect), 10));
                }
            }
        }

        internal override void KeyDown(object sender, RangeKeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.A)
            {
                if (e.IsCtrlDown)
                {
                    //按下ctrl+A，表示全选
                    e.RangeData.SetIconInRectSelected(e.RangeData.BoundsRect);
                    e.RangeView.Refresh(TopoEdit.Utility.AdjustRect(TopoEdit.Utility.ConvertRect(e.RangeData.BoundsRect), 10));
                }
            }
        }

        internal override void Paint(object sender, RangePaintEventArgs e)
        {
            if (m_selectingRange != null)
            {
                m_selectingRange.Draw(e.Graphics, e.ClipRectangle);
            }
        }

        void CalcSelectionRangePos(Point mousePos, Point mouseDownPos)
        {
            if (mousePos.X < mouseDownPos.X)
            {
                if (mousePos.Y < mouseDownPos.Y)
                {
                    m_selectingRange.Position = mousePos;
                }
                else
                {
                    m_selectingRange.Position = new PointF(mousePos.X, mouseDownPos.Y);
                }
            }
            else
            {
                if (mousePos.Y < mouseDownPos.Y)
                {
                    m_selectingRange.Position = new PointF(mouseDownPos.X, mousePos.Y);
                }
                else
                {
                    m_selectingRange.Position = mouseDownPos;
                }
            }
        }
    }
}
