using System;
using System.Collections.Generic;
using System.Text;
using TopoEdit.Icon;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing;

namespace TopoEdit.EventHandler
{
    internal class RangeEditStatus
    {
        /// <summary>
        /// 被编辑的区域，目前可为BLOCK,TOPO
        /// </summary>
        private IRange m_range = null;
        /// <summary>
        /// 编辑该区域使用的画板
        /// </summary>
        private IBaseDrawPanel m_panel = null;
        /// <summary>
        /// 鼠标是否按下
        /// </summary>
        private bool m_mouseDown = false;
        /// <summary>
        /// 鼠标上一次位置
        /// </summary>
        private Point m_btnLastPos = new Point();
        /// <summary>
        /// 鼠标按下时的位置
        /// </summary>
        private Point m_btnDownPos = new Point();
        /// <summary>
        /// 鼠标所在位置的锚点
        /// </summary>
        private Anchor m_anchor = null;
        /// <summary>
        /// 鼠标按下时鼠标所在的Draw
        /// </summary>
        private SelectedRange m_selectedDraw = null;
        /// <summary>
        /// 工具栏中被按下的BUTTON
        /// </summary>
        private List<ToolStripButton> m_downToopStripButton = new List<ToolStripButton>();

        public RangeEditStatus(IRange range, IBaseDrawPanel panel)
        {
            m_range = range;
            m_panel = panel;
        }

        internal IRange RangeData
        {
            get { return m_range; }
        }

        internal IBaseDrawPanel RangeView
        {
            get { return m_panel; }
        }

        internal bool IsCtrlDown
        {
            get { return Utility.GetKeyState(Keys.ControlKey); }
        }

        internal bool IsMouseDown
        {
            get { return m_mouseDown; }
            set { m_mouseDown = value; }
        }

        internal Point MouseLastPos
        {
            get { return m_btnLastPos; }
            set { m_btnLastPos = value; }
        }

        internal Point MouseDownPos
        {
            get { return m_btnDownPos; }
            set { m_btnDownPos = value; }
        }

        internal Anchor MouseAnchor
        {
            get { return m_anchor; }
            set { m_anchor = value; }
        }

        internal SelectedRange MouseSelectedIcon
        {
            get { return m_selectedDraw; }
            set { m_selectedDraw = value; }
        }

        internal List<ToolStripButton> DownToopStripButton
        {
            get { return m_downToopStripButton; }
            set { m_downToopStripButton = value; }
        }

        internal void ProcessMouseDownBeforeHandle(object sender, MouseEventArgs e)
        {
            if (m_downToopStripButton.Count == 0)
            {
                //处理是否选中
                m_selectedDraw = m_range.SelectIcon(IsCtrlDown, new Point(e.X, e.Y));
                m_panel.Invalidate();//如果有有选中的需要刷新选中前的ICON和选中后的ICON，否则仅需要刷新选中后ICON，此次简化处理，全部刷新
            }
        }

        internal void ProcessMouseDownAfterHandle(object sender, MouseEventArgs e)
        {
            //记录鼠标按下位置
            m_mouseDown = true;
            m_btnLastPos = e.Location;
            m_btnDownPos = e.Location;
        }

        internal void ProcessMouseUpBeforeHandle(object sender, MouseEventArgs e)
        {
            //有意留空
        }

        internal void ProcessMouseUpAfterHandle(object sender, MouseEventArgs e)
        {
            m_mouseDown = false;
        }

        internal void ProcessMouseMoveBeforeHandle(object sender, MouseEventArgs e)
        {
            if (m_downToopStripButton.Count == 0)
            {
                if (!m_mouseDown)
                {
                    //判断鼠标是否移动到了锚点上
                    IDraw curIconOnMouse = (IDraw)(m_range.Intersect(e.Location));
                    if ((curIconOnMouse != null) && (curIconOnMouse is SelectedRange))
                    {
                        //鼠标移动到某个图元上
                        //判断单击在那个锚点上
                        Debug.Assert(curIconOnMouse is SelectedRange);
                        SelectedDraw selIcon = curIconOnMouse.Intersect(e.Location) as SelectedDraw;
                        if ((selIcon != null) && (selIcon is SelectedDraw))
                        {
                            m_anchor = selIcon.IntersectAnchor(e.Location);

                            if (m_anchor == null)
                            {
                                //鼠标没有移动到锚点上
                                m_panel.Cursor = System.Windows.Forms.Cursors.SizeAll;
                            }
                            else
                            {
                                SetCursorByAnchorType(m_anchor.GeneralAnchorType);
                            }
                            
                        }
                        else
                        {
                            //鼠标没有单击在锚点上
                            m_panel.Cursor = System.Windows.Forms.Cursors.Default;
                        }
                    }
                    else
                    {
                        //鼠标没有移动到某个图元上
                        m_panel.Cursor = System.Windows.Forms.Cursors.Default;
                        m_anchor = null;
                    }
                }
                else
                {
                    //不需要修改光标类型
                }
            }
            else
            {
                m_panel.Cursor = System.Windows.Forms.Cursors.Cross;
            }
        }

        internal void ProcessMouseMoveAfterHandle(object sender, MouseEventArgs e)
        {
            m_btnLastPos = e.Location;
        }

        internal void ProcessMouseWheelBeforeHandle(object sender, MouseEventArgs e)
        {
            //有意留空
        }

        internal void ProcessMouseWheelAfterHandle(object sender, MouseEventArgs e)
        {
            //有意留空
        }

        internal void ProcessKeyDownBeforeHandle(object sender, KeyEventArgs e)
        {
            //有意留空
        }

        internal void ProcessKeyDownAfterHandle(object sender, KeyEventArgs e)
        {
            //有意留空
        }

        internal void ProcessKeyUpBeforeHandle(object sender, KeyEventArgs e)
        {
            //有意留空
        }

        internal void ProcessKeyUpAfterHandle(object sender, KeyEventArgs e)
        {
            //有意留空
        }

        internal void ProcessMouseClickBeforeHandle(object sender, MouseEventArgs e)
        {
            //有意留空
        }

        internal void ProcessMouseClickAfterHandle(object sender, MouseEventArgs e)
        {
            //有意留空
        }

        internal void ProcessMouseDoubleClickBeforeHandle(object sender, MouseEventArgs e)
        {
            //有意留空
        }

        internal void ProcessMouseDoubleClickAfterHandle(object sender, MouseEventArgs e)
        {
            //有意留空
        }

        /// <summary>
        /// 根据锚点类型设置光标图形
        /// </summary>
        void SetCursorByAnchorType(AnchorType anchorType)
        {
            switch (anchorType)
            {
                case AnchorType.Left:
                case AnchorType.Right:
                    {
                        m_panel.Cursor = System.Windows.Forms.Cursors.SizeWE;
                        break;
                    }
                case AnchorType.Top:
                case AnchorType.Bottom:
                    {
                        m_panel.Cursor = System.Windows.Forms.Cursors.SizeNS;
                        break;
                    }
                case AnchorType.LeftTop:
                case AnchorType.RightBottom:
                    {
                        m_panel.Cursor = System.Windows.Forms.Cursors.SizeNWSE;
                        break;
                    }
                case AnchorType.RightTop:
                case AnchorType.LeftBottom:
                    {
                        m_panel.Cursor = System.Windows.Forms.Cursors.SizeNESW;
                        break;
                    }
                case AnchorType.Control:
                case AnchorType.EndPoint:
                    {
                        m_panel.Cursor = System.Windows.Forms.Cursors.Cross;
                        break;
                    }
                case AnchorType.None:
                    {
                        break;
                    }
                default:
                    {
                        Debug.Assert(false);
                        break;
                    }
            }
        }
    }

    public interface IRangeEditStatusAccess
    {
        IRange RangeData
        {
            get;
        }

        IBaseDrawPanel RangeView
        {
            get;
        }

        bool IsCtrlDown
        {
            get;
        }

        bool IsMouseDown
        {
            get;
        }

        Point MouseLastPos
        {
            get;
        }

        Point MouseDownPos
        {
            get;
        }

        Anchor MouseAnchor
        {
            get;
        }

        SelectedRange MouseSelectedIcon
        {
            get;
            set;
        }

        List<ToolStripButton> DownToopStripButton
        {
            get;
        }
    }

    public class RangeKeyEventArgs : KeyEventArgs, IRangeEditStatusAccess
    {
        private RangeEditStatus m_rangeEditStatus;

        internal RangeKeyEventArgs(RangeEditStatus rangeEditStatus, Keys keyData) :
            base(keyData)
        {
            m_rangeEditStatus = rangeEditStatus;
        }

        public IRange RangeData
        {
            get { return m_rangeEditStatus.RangeData; }
        }

        public IBaseDrawPanel RangeView
        {
            get { return m_rangeEditStatus.RangeView; }
        }

        public bool IsCtrlDown
        {
            get { return m_rangeEditStatus.IsCtrlDown; }
        }

        public bool IsMouseDown
        {
            get { return m_rangeEditStatus.IsMouseDown; }
        }

        public Point MouseLastPos
        {
            get { return m_rangeEditStatus.MouseLastPos; }
        }

        public Point MouseDownPos
        {
            get { return m_rangeEditStatus.MouseDownPos; }
        }

        public Anchor MouseAnchor
        {
            get { return m_rangeEditStatus.MouseAnchor; }
        }

        public SelectedRange MouseSelectedIcon
        {
            get { return m_rangeEditStatus.MouseSelectedIcon; }
            set { m_rangeEditStatus.MouseSelectedIcon = value; }
        }

        public List<ToolStripButton> DownToopStripButton
        {
            get { return m_rangeEditStatus.DownToopStripButton; }
        }
    }

    public class RangeMouseEventArgs : MouseEventArgs, IRangeEditStatusAccess
    {
        private RangeEditStatus m_blockEditStatus;
        private Point m_dpLocation = new Point();

        internal RangeMouseEventArgs(RangeEditStatus blockEditStatus, MouseButtons button, int clicks, int xLP, int yLP, int xDP, int yDP, int delta) :
            base(button, clicks, xLP, yLP, delta)
        {
            m_blockEditStatus = blockEditStatus;
            m_dpLocation.X = xDP;
            m_dpLocation.Y = yDP;
        }

        public IRange RangeData
        {
            get { return m_blockEditStatus.RangeData; }
        }

        public IBaseDrawPanel RangeView
        {
            get { return m_blockEditStatus.RangeView; }
        }

        public bool IsCtrlDown
        {
            get { return m_blockEditStatus.IsCtrlDown; }
        }

        public bool IsMouseDown
        {
            get { return m_blockEditStatus.IsMouseDown; }
        }

        public Point MouseLastPos
        {
            get { return m_blockEditStatus.MouseLastPos; }
        }

        public Point MouseDownPos
        {
            get { return m_blockEditStatus.MouseDownPos; }
        }

        public Anchor MouseAnchor
        {
            get { return m_blockEditStatus.MouseAnchor; }
        }

        public SelectedRange MouseSelectedIcon
        {
            get { return m_blockEditStatus.MouseSelectedIcon; }
            set { m_blockEditStatus.MouseSelectedIcon = value; }
        }

        public List<ToolStripButton> DownToopStripButton
        {
            get { return m_blockEditStatus.DownToopStripButton; }
        }

        public Point DpLocation
        {
            get { return m_dpLocation; }
        }
    }

    public class RangePaintEventArgs : PaintEventArgs, IRangeEditStatusAccess
    {
        private RangeEditStatus m_blockEditStatus;

        internal RangePaintEventArgs(RangeEditStatus blockEditStatus, Graphics graphics, RectangleF rect):
            base(graphics, Utility.ConvertRect(rect))
        {
            m_blockEditStatus = blockEditStatus;
        }

        public IRange RangeData
        {
            get { return m_blockEditStatus.RangeData; }
        }

        public IBaseDrawPanel RangeView
        {
            get { return m_blockEditStatus.RangeView; }
        }

        public bool IsCtrlDown
        {
            get { return m_blockEditStatus.IsCtrlDown; }
        }

        public bool IsMouseDown
        {
            get { return m_blockEditStatus.IsMouseDown; }
        }

        public Point MouseLastPos
        {
            get { return m_blockEditStatus.MouseLastPos; }
        }

        public Point MouseDownPos
        {
            get { return m_blockEditStatus.MouseDownPos; }
        }

        public Anchor MouseAnchor
        {
            get { return m_blockEditStatus.MouseAnchor; }
        }

        public SelectedRange MouseSelectedIcon
        {
            get { return m_blockEditStatus.MouseSelectedIcon; }
            set { m_blockEditStatus.MouseSelectedIcon = value; }
        }

        public List<ToolStripButton> DownToopStripButton
        {
            get { return m_blockEditStatus.DownToopStripButton; }
        }
    }

    internal abstract class IRangeEventHandler
    {
        internal virtual void Paint(object sender, RangePaintEventArgs e)
        {
            //有意留空
        }

        internal virtual void MouseDown(object sender, RangeMouseEventArgs e)
        {
            //有意留空
        }

        internal virtual void MouseUp(object sender, RangeMouseEventArgs e)
        {
            //有意留空
        }

        internal virtual void MouseMove(object sender, RangeMouseEventArgs e)
        {
            //有意留空
        }

        internal virtual void MouseWheel(object sender, RangeMouseEventArgs e)
        {
            //有意留空
        }

        internal virtual void MouseClick(object sender, RangeMouseEventArgs e)
        {
            //有意留空
        }
        internal virtual void MouseDoubleClick(object sender, RangeMouseEventArgs e)
        {
            //有意留空
        }

        internal virtual void KeyDown(object sender, RangeKeyEventArgs e)
        {
            //有意留空
        }

        internal virtual void KeyUp(object sender, RangeKeyEventArgs e)
        {
            //有意留空
        }
    }
}
