using System;
using System.Collections.Generic;
using System.Text;
using TopoEdit.Icon;
using System.Drawing;
using TopoEdit.Command;

namespace TopoEdit.EventHandler
{
    class DrawIconPolygonHandler : IRangeEventHandler
    {
        private IconPolygon m_drawIcon;
        /// <summary>
        /// 是否绘制结束
        /// </summary>
        private bool m_isOver = true;
        /// <summary>
        /// 鼠标单击的点的坐标集合（按单击顺序排列）
        /// </summary>
        private List<PointF> m_pos = new List<PointF>();
        /// <summary>
        /// 最后一条路径（结束端点是起点）
        /// </summary>
        private IPath m_lastPath = null;
        /// <summary>
        /// 倒数第二条路径
        /// </summary>
        private IPath m_lastSecondPath = null;

        private bool CanWork(RangeMouseEventArgs e)
        {
            //仅能在该区域范围内绘制
            if (e.DownToopStripButton.Count == 1)
            {
                Tool.IconTool iconTool = e.DownToopStripButton[0] as Tool.IconTool;
                if ((iconTool != null) && (iconTool.DrawIcon != null))
                {
                    if (iconTool.DrawIcon is IconPolygon)
                    {
                        //仅处理多边形
                        return true;
                    }
                }
            }

            return false;
        }

        internal override void MouseDown(object sender, RangeMouseEventArgs e)
        {
            if (CanWork(e))
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    if (m_isOver)
                    {
                        //重新开始一个新的绘制，绘制初始化
                        m_isOver = false;
                        m_drawIcon = (e.DownToopStripButton[0] as Tool.IconTool).DrawIcon.Clone() as IconPolygon;
                        //e.RangeData.Add(m_drawIcon);
                        e.RangeView.CmdMgr.AddThenExec(new AddDrawCommand(e.RangeView, e.RangeData, m_drawIcon));
                        m_pos.Clear();
                        m_pos.Add(e.Location);
                        m_lastPath = null;
                        m_lastSecondPath = null;
                    }
                    else
                    {
                        //正在绘制，不需要初始化
                        if (!m_pos.Contains(e.Location))
                        {
                            m_pos.Add(e.Location);
                            m_lastSecondPath = null;//倒数第二条在单击按钮后在移动鼠标时第一次不删除
                        }
                    }
                }
                else if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    //按下的是鼠标右键
                    //表示绘制结束
                    m_isOver = true;
                    if ((m_pos.Count < 3) && (m_drawIcon != null))
                    {
                        //e.RangeData.Remove(m_drawIcon);
                        e.RangeView.CmdMgr.AddThenExec(new DelDrawCommand(e.RangeView, e.RangeData, m_drawIcon));
                    }
                    m_lastPath = null;
                    m_pos.Clear();
                    m_drawIcon = null;
                    m_lastSecondPath = null;
                }
                else
                {
                    //其他鼠标按键按下，不处理
                }
            }
        }

        internal override void MouseMove(object sender, RangeMouseEventArgs e)
        {
            if (CanWork(e) && (m_drawIcon != null) && !m_isOver && (!m_drawIcon.IsEndPoint(e.Location)))
            {
                //计算刷新区域
                RectangleF refreshRect = new RectangleF();
                //已开始绘制，则添加路径到多边形中
                if (m_pos.Count == 0)
                {
                    //尚未初始化，不处理
                    return;
                }
                else if (m_pos.Count == 1)
                {
                    //当前路径中还没有路径
                    m_drawIcon.ClearPath();
                    IPath path = new StraightLineInPolygon(m_pos[0], e.Location);
                    m_drawIcon.AddPath(path);
                    refreshRect = m_drawIcon.BoundsRect;
                }
                else
                {
                    RectangleF editBefore = m_drawIcon.BoundsRect;

                    if (m_lastPath != null)
                    {
                        //移除最后一条路径
                        m_drawIcon.RemovePath(m_lastPath);
                    }
                    else
                    {
                        //继续处理
                    }

                    if (m_lastSecondPath != null)
                    {
                        //移除最后一条路径
                        m_drawIcon.RemovePath(m_lastSecondPath);
                    }
                    else
                    {
                        //继续处理
                    }

                    //添加两条路径，一条是当前点和上一次点组成的路径，另一条是当前点和起点组成的路径
                    IPath path1 = new StraightLineInPolygon(m_pos[m_pos.Count - 1], e.Location);
                    IPath path2 = new StraightLineInPolygon(e.Location, m_pos[0]);
                    m_lastPath = path2;
                    m_lastSecondPath = path1;
                    m_drawIcon.AddPath(path1);
                    m_drawIcon.AddPath(path2);

                    RectangleF editAfter = m_drawIcon.BoundsRect;

                    refreshRect = Utility.Union(editBefore, editAfter);
                }

                if (!refreshRect.IsEmpty)
                {
                    e.RangeView.Refresh(TopoEdit.Utility.AdjustRect(TopoEdit.Utility.ConvertRect(refreshRect), 10));
                }
            }
        }
    }
}
