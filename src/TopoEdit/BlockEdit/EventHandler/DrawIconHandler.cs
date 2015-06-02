using System;
using System.Collections.Generic;
using System.Text;
using TopoEdit.Icon;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using TopoEdit.Command;

namespace TopoEdit.EventHandler
{
    internal class DrawIconHandler : IRangeEventHandler
    {
        private IDraw m_drawIcon;

        internal override void MouseDown(object sender, RangeMouseEventArgs e)
        {
            //仅能在该区域范围内绘制
            if (e.DownToopStripButton.Count == 1)
            {
                Tool.IconTool iconTool = e.DownToopStripButton[0] as Tool.IconTool;
                if ((iconTool != null) && (iconTool.DrawIcon != null))
                {
                    if (iconTool.DrawIcon is IconPolygon)
                    {
                        //不处理多边形
                        return;
                    }

                    //如果已经有图元被选中，则取消选中
                    if (e.MouseSelectedIcon != null)
                    {
                        RectangleF selectRect = e.MouseSelectedIcon.BoundsRect;

                        //此次不需要考虑撤销操作，因此不构建移除命令
                        e.RangeData.Remove(e.MouseSelectedIcon);
                        e.RangeView.Refresh(TopoEdit.Utility.AdjustRect(TopoEdit.Utility.ConvertRect(selectRect), 10));
                    }

                    //修改鼠标为十字
                    e.RangeView.Cursor = System.Windows.Forms.Cursors.Cross;
                    m_drawIcon = iconTool.DrawIcon.Clone();
                    Debug.Assert(null != m_drawIcon);
                    //将Icon平移到
                    TopoEdit.Icon.Movement move = new TopoEdit.Icon.Movement();
                    move.XMovement = e.Location.X - m_drawIcon.BoundsRect.Left;
                    move.YMovement = e.Location.Y - m_drawIcon.BoundsRect.Top;
                    m_drawIcon.Move(move);
                    //构建添加命令
                    e.RangeView.CmdMgr.AddThenExec(new AddDrawCommand(e.RangeView, e.RangeData, m_drawIcon));
                }
            }
        }

        internal override void MouseMove(object sender, RangeMouseEventArgs e)
        {
            if (e.IsMouseDown && (e.Location.X > e.MouseDownPos.X) && (e.Location.Y > e.MouseDownPos.Y))
            {
                //仅能在该区域范围内绘制
                if (e.DownToopStripButton.Count == 1)
                {
                    Tool.IconTool iconTool = e.DownToopStripButton[0] as Tool.IconTool;
                    if (iconTool != null)
                    {
                        if ((iconTool.DrawIcon == null) || (iconTool.DrawIcon is IconPolygon))
                        {
                            //不处理多边形
                            return;
                        }

                        //修改鼠标为十字
                        e.RangeView.Cursor = System.Windows.Forms.Cursors.Cross;

                        Debug.Assert(null != m_drawIcon);

                        //将Icon缩放到合适大小
                        TopoEdit.Icon.Zoom zoom = new TopoEdit.Icon.Zoom();
                        zoom.ZoomMode = CSR.CUIT.GlobalService.ShareLib.EmZoomMode.NonUniformScale;
                        zoom.SubMode = ScaleOpMode.RightDown;
                        zoom.XRadio = (e.Location.X - m_drawIcon.BoundsRect.Left) / m_drawIcon.BoundsRect.Width;
                        zoom.YRadio = (e.Location.Y - m_drawIcon.BoundsRect.Top) / m_drawIcon.BoundsRect.Height;

                        RectangleF rect = new RectangleF();
                        //将缩放前区域加入刷新区域
                        rect = TopoEdit.Utility.Union(rect, m_drawIcon.BoundsRect);
                        //缩放
                        m_drawIcon.Zoom(zoom);
                        //将缩放后区域加入刷新区域
                        rect = TopoEdit.Utility.Union(rect, m_drawIcon.BoundsRect);

                        e.RangeView.Refresh(TopoEdit.Utility.AdjustRect(TopoEdit.Utility.ConvertRect(rect), 10));
                    }
                }
               
            }
        }

        internal override void MouseUp(object sender, RangeMouseEventArgs e)
        {
            e.RangeView.Cursor = System.Windows.Forms.Cursors.Default;
        }
    }
}
