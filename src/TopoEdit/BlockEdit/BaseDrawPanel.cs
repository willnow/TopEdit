using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using TopoEdit.EventHandler;
using TopoEdit.Icon;
using System.Drawing;
using System.Drawing.Drawing2D;
using TopoEdit.Command;
using TopoEdit.Interceptor;

namespace TopoEdit
{
    public partial class IBaseDrawPanel : Panel, IHelpHandle
    {
        /// <summary>
        /// 当前编辑的图元
        /// </summary>
        private IRange m_range = null;
        /// <summary>
        /// 事件处理器
        /// </summary>
        private List<IRangeEventHandler> m_eventHandler = new List<IRangeEventHandler>();
        /// <summary>
        /// 事件拦截器
        /// </summary>
        private DrawPanelEventInterceptorGroup m_eventInterceptor = new DrawPanelEventInterceptorGroup();
        /// <summary>
        /// 保存模型拦截器
        /// </summary>
        private ModelSaveInterceptorGroup m_modelSaveInterceptor = new ModelSaveInterceptorGroup();
        /// <summary>
        /// 内置事件拦截器，用于保存操作状态，作为事件处理器和事件拦截器的状态参数
        /// </summary>
        private RangeEditStatus m_rangeEditStatus = null;
        /// <summary>
        /// 需要显示的层,-1:显示所有层
        /// </summary>
        private int m_dispalyLevel = -1;
        /// <summary>
        /// 当前视图的缩放比
        /// </summary>
        private float m_zoomRate = 1;
        /// <summary>
        /// 帮助责任链对象
        /// </summary>
        HelpHandle m_cHelpHandle = new HelpHandle();
        /// <summary>
        /// 该视图的命令管理类
        /// </summary>
        private CommandManager m_commandManager = new CommandManager();

        public TopoEdit.Interceptor.ModelSaveInterceptorGroup ModelSaveInterceptor
        {
            get { return m_modelSaveInterceptor; }
        }

        public IBaseDrawPanel()
        {
            InitializeComponent();
            Init();
        }

        public IBaseDrawPanel(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
            Init();
        }

        protected virtual void Init()
        {
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            m_rangeEditStatus = new RangeEditStatus(m_range, this);

            //重置处理事件类
            ResetHandler();
            //重置拦截器
            ResetEventInterceptor();

            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.BlockPanel_MouseMove);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BlockPanel_MouseDown);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.BlockPanel_MouseUp);
            this.MouseClick += new MouseEventHandler(BlockPanel_MouseClick);
            this.MouseDoubleClick += new MouseEventHandler(BlockPanel_MouseDoubleClick);
            this.MouseWheel += new MouseEventHandler(BlockPanel_MouseWheel);
            this.KeyUp += new KeyEventHandler(BlockPanel_KeyUp);
            this.KeyDown += new KeyEventHandler(BlockPanel_KeyDown);
        }

        internal IRange RangeData
        {
            get { return m_range; }
            set
            {
                m_range = value;
                m_rangeEditStatus = new RangeEditStatus(m_range, this);
            }
        }

        internal int DisplayLevel
        {
            get { return m_dispalyLevel; }
            set { m_dispalyLevel = value; }
        }

        public float ZoomRate
        {
            get { return m_zoomRate; }
            set { m_zoomRate = value; }
        }

        public RectangleF RealBound
        {
            get
            {
                return new RectangleF(0, 0, Math.Max(this.AutoScrollMinSize.Width, Bounds.Width), Math.Max(this.AutoScrollMinSize.Height, Bounds.Height));
            }
        }

        public CommandManager CmdMgr
        {
            get
            {
                return m_commandManager;
            }
        }

        /// <summary>
        /// 注册事件处理器
        /// </summary>
        /// <param name="handler"></param>
        internal void RegHandler(IRangeEventHandler handler)
        {
            m_eventHandler.Add(handler);
        }

        /// <summary>
        /// 注销事件处理器
        /// </summary>
        /// <param name="handler"></param>
        internal void UnRegHandler(IRangeEventHandler handler)
        {
            m_eventHandler.Remove(handler);
        }

        /// <summary>
        /// 重置事件处理器
        /// </summary>
        internal void ResetHandler()
        {
            m_eventHandler.Clear();
        }

        /// <summary>
        /// 注册事件拦截器
        /// </summary>
        /// <param name="interceptor"></param>
        public void RegEventInterceptor(IDrawPanelEventInterceptor interceptor)
        {
            m_eventInterceptor.Add(interceptor);
        }

        /// <summary>
        /// 注销事件拦截器
        /// </summary>
        /// <param name="interceptor"></param>
        public void UnRegEventInterceptor(IDrawPanelEventInterceptor interceptor)
        {
            m_eventInterceptor.Remove(interceptor);
        }

        /// <summary>
        /// 重置事件拦截器
        /// </summary>
        /// <param name="interceptor"></param>
        public void ResetEventInterceptor()
        {
            m_eventInterceptor.Clear();
        }

        /// <summary>
        /// 注册模型保存拦截器
        /// </summary>
        /// <param name="interceptor"></param>
        public void RegModelSaveInterceptor(IModelSaveInterceptor interceptor)
        {
            m_modelSaveInterceptor.Add(interceptor);
        }

        /// <summary>
        /// 注销模型保存拦截器
        /// </summary>
        /// <param name="interceptor"></param>
        public void UnRegModelSaveInterceptor(IModelSaveInterceptor interceptor)
        {
            m_modelSaveInterceptor.Remove(interceptor);
        }

        /// <summary>
        /// 重置模型保存拦截器
        /// </summary>
        /// <param name="interceptor"></param>
        public void ResetModelSaveInterceptor()
        {
            m_modelSaveInterceptor.Clear();
        }

        /// <summary>
        /// 重写IsInputKey方法以支持方向键
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool IsInputKey(Keys keyData)
        {
            if ((keyData == Keys.Left)
                || (keyData == Keys.Right)
                || (keyData == Keys.Up)
                || (keyData == Keys.Down))
            {
                return true;
            }
            else
            {
                return base.IsInputKey(keyData);
            }
        }
        internal void AddDownToolStripButton(ToolStripButton btn)
        {
            btn.Checked = true;
            m_rangeEditStatus.DownToopStripButton.Add(btn);
        }

        internal void RemoveDownToolStripButton(ToolStripButton btn)
        {
            btn.Checked = false;
            m_rangeEditStatus.DownToopStripButton.Remove(btn);
        }

        internal void ClearDownToolStripButton()
        {
            foreach (ToolStripButton btn in m_rangeEditStatus.DownToopStripButton)
            {
                btn.Checked = false;
            }
            m_rangeEditStatus.DownToopStripButton.Clear();
        }

        private void BlockPanel_MouseDown(object sender, MouseEventArgs eArg)
        {
            if (null == m_range)
            {
                return;
            }

            this.Focus();

            Point pos = DPtoLP(eArg.Location);

            MouseEventArgs e = new MouseEventArgs(eArg.Button, eArg.Clicks, pos.X, pos.Y, eArg.Delta);

            m_rangeEditStatus.ProcessMouseDownBeforeHandle(sender, e);

            RangeMouseEventArgs blockMouseEventArgs = new RangeMouseEventArgs(m_rangeEditStatus, e.Button, e.Clicks, e.X, e.Y, eArg.X, eArg.Y, e.Delta);

            //处理事件前拦截
            foreach (IDrawPanelEventInterceptor eventInterceptor in m_eventInterceptor)
            {
                eventInterceptor.ProcessMouseDownBeforeHandle(sender, blockMouseEventArgs);
            }
            
            //处理事件
            foreach (IRangeEventHandler eventHandler in m_eventHandler)
            {
                eventHandler.MouseDown(sender, blockMouseEventArgs);
            }

            //处理事件后拦截
            foreach (IDrawPanelEventInterceptor eventInterceptor in m_eventInterceptor)
            {
                eventInterceptor.ProcessMouseDownAfterHandle(sender, blockMouseEventArgs);
            }

            m_rangeEditStatus.ProcessMouseDownAfterHandle(sender, e);
        }

        private void BlockPanel_MouseUp(object sender, MouseEventArgs eArg)
        {
            if (null == m_range)
            {
                return;
            }

            Point pos = DPtoLP(eArg.Location);

            MouseEventArgs e = new MouseEventArgs(eArg.Button, eArg.Clicks, pos.X, pos.Y, eArg.Delta);

            m_rangeEditStatus.ProcessMouseUpBeforeHandle(sender, e);

            RangeMouseEventArgs blockMouseEventArgs = new RangeMouseEventArgs(m_rangeEditStatus, e.Button, e.Clicks, e.X, e.Y, eArg.X, eArg.Y, e.Delta);

            //处理事件前拦截
            foreach (IDrawPanelEventInterceptor eventInterceptor in m_eventInterceptor)
            {
                eventInterceptor.ProcessMouseUpBeforeHandle(sender, blockMouseEventArgs);
            }
            
            //处理事件
            foreach (IRangeEventHandler eventHandler in m_eventHandler)
            {
                eventHandler.MouseUp(sender, blockMouseEventArgs);
            }

            //处理事件后拦截
            foreach (IDrawPanelEventInterceptor eventInterceptor in m_eventInterceptor)
            {
                eventInterceptor.ProcessMouseUpAfterHandle(sender, blockMouseEventArgs);
            }

            m_rangeEditStatus.ProcessMouseUpAfterHandle(sender, e);
        }

        Point DPtoLP(Point posArg)
        {
            Movement move = new Movement();
            move.XMovement =  -AutoScrollPosition.X;
            move.YMovement =  -AutoScrollPosition.Y;
            Point pos = posArg;
            move.LPtoDP(ref pos);

            return pos;
        }

        private void BlockPanel_MouseMove(object sender, MouseEventArgs eArg)
        {
            Point pos = DPtoLP(eArg.Location);
            //显示鼠标当前位置
            m_cHelpHandle.HandleHelp("X: " + pos.X + ", Y:" + pos.Y);

            if (null == m_range)
            {
                return;
            }

            IDraw selDraw = m_range.Intersect(pos);
            if (selDraw != null)
            {
                m_cHelpHandle.HandleHelp("X: " + pos.X + ", Y:" + pos.Y + ", " + selDraw.ToString());
            }

            MouseEventArgs e = new MouseEventArgs(eArg.Button, eArg.Clicks, pos.X, pos.Y, eArg.Delta);

            m_rangeEditStatus.ProcessMouseMoveBeforeHandle(sender, e);

            RangeMouseEventArgs blockMouseEventArgs = new RangeMouseEventArgs(m_rangeEditStatus, e.Button, e.Clicks, e.X, e.Y, eArg.X, eArg.Y, e.Delta);

            //处理事件前拦截
            foreach (IDrawPanelEventInterceptor eventInterceptor in m_eventInterceptor)
            {
                eventInterceptor.ProcessMouseMoveBeforeHandle(sender, blockMouseEventArgs);
            }
            
            //处理事件
            foreach (IRangeEventHandler eventHandler in m_eventHandler)
            {
                eventHandler.MouseMove(sender, blockMouseEventArgs);
            }

            //处理事件后拦截
            foreach (IDrawPanelEventInterceptor eventInterceptor in m_eventInterceptor)
            {
                eventInterceptor.ProcessMouseMoveAfterHandle(sender, blockMouseEventArgs);
            }

            m_rangeEditStatus.ProcessMouseMoveAfterHandle(sender, e);
        }

        private void BlockPanel_KeyDown(object sender, KeyEventArgs e)
        {
            if (null == m_range)
            {
                return;
            }

            m_rangeEditStatus.ProcessKeyDownBeforeHandle(sender, e);

            RangeKeyEventArgs blockKeyEventArgs = new RangeKeyEventArgs(m_rangeEditStatus, e.KeyData);

            //处理事件前拦截
            foreach (IDrawPanelEventInterceptor eventInterceptor in m_eventInterceptor)
            {
                eventInterceptor.ProcessKeyDownBeforeHandle(sender, blockKeyEventArgs);
            }
            
            //处理事件
            foreach (IRangeEventHandler eventHandler in m_eventHandler)
            {
                eventHandler.KeyDown(sender, blockKeyEventArgs);
            }

            //处理事件后拦截
            foreach (IDrawPanelEventInterceptor eventInterceptor in m_eventInterceptor)
            {
                eventInterceptor.ProcessKeyDownAfterHandle(sender, blockKeyEventArgs);
            }

            m_rangeEditStatus.ProcessKeyDownAfterHandle(sender, e);
        }

        private void BlockPanel_KeyUp(object sender, KeyEventArgs e)
        {
            if (null == m_range)
            {
                return;
            }

            m_rangeEditStatus.ProcessKeyUpBeforeHandle(sender, e);

            RangeKeyEventArgs blockKeyEventArgs = new RangeKeyEventArgs(m_rangeEditStatus, e.KeyData);

            //处理事件前拦截
            foreach (IDrawPanelEventInterceptor eventInterceptor in m_eventInterceptor)
            {
                eventInterceptor.ProcessKeyUpBeforeHandle(sender, blockKeyEventArgs);
            }
            
            //处理事件
            foreach (IRangeEventHandler eventHandler in m_eventHandler)
            {
                eventHandler.KeyUp(sender, blockKeyEventArgs);
            }

            //处理事件后拦截
            foreach (IDrawPanelEventInterceptor eventInterceptor in m_eventInterceptor)
            {
                eventInterceptor.ProcessKeyUpAfterHandle(sender, blockKeyEventArgs);
            }

            m_rangeEditStatus.ProcessKeyUpAfterHandle(sender, e);
        }

        private void BlockPanel_MouseWheel(object sender, MouseEventArgs eArg)
        {
            if (null == m_range)
            {
                return;
            }

            Point pos = DPtoLP(eArg.Location);
            MouseEventArgs e = new MouseEventArgs(eArg.Button, eArg.Clicks, pos.X, pos.Y, eArg.Delta);

            m_rangeEditStatus.ProcessMouseWheelBeforeHandle(sender, e);

            RangeMouseEventArgs blockMouseEventArgs = new RangeMouseEventArgs(m_rangeEditStatus, e.Button, e.Clicks, e.X, e.Y, eArg.X, eArg.Y, e.Delta);

            //处理事件前拦截
            foreach (IDrawPanelEventInterceptor eventInterceptor in m_eventInterceptor)
            {
                eventInterceptor.ProcessMouseWheelBeforeHandle(sender, blockMouseEventArgs);
            }
            
            //处理事件
            foreach (IRangeEventHandler eventHandler in m_eventHandler)
            {
                eventHandler.MouseWheel(sender, blockMouseEventArgs);
            }

            //处理事件后拦截
            foreach (IDrawPanelEventInterceptor eventInterceptor in m_eventInterceptor)
            {
                eventInterceptor.ProcessMouseWheelAfterHandle(sender, blockMouseEventArgs);
            }

            m_rangeEditStatus.ProcessMouseWheelAfterHandle(sender, e);
        }

        void BlockPanel_MouseDoubleClick(object sender, MouseEventArgs eArg)
        {
            if (null == m_range)
            {
                return;
            }

            Point pos = DPtoLP(eArg.Location);
            MouseEventArgs e = new MouseEventArgs(eArg.Button, eArg.Clicks, pos.X, pos.Y, eArg.Delta);

            m_rangeEditStatus.ProcessMouseDoubleClickBeforeHandle(sender, e);

            RangeMouseEventArgs blockMouseEventArgs = new RangeMouseEventArgs(m_rangeEditStatus, e.Button, e.Clicks, e.X, e.Y, eArg.X, eArg.Y, e.Delta);

            //处理事件前拦截
            foreach (IDrawPanelEventInterceptor eventInterceptor in m_eventInterceptor)
            {
                eventInterceptor.ProcessMouseDoubleClickBeforeHandle(sender, blockMouseEventArgs);
            }
            
            //处理事件
            foreach (IRangeEventHandler eventHandler in m_eventHandler)
            {
                eventHandler.MouseDoubleClick(sender, blockMouseEventArgs);
            }

            //处理事件后拦截
            foreach (IDrawPanelEventInterceptor eventInterceptor in m_eventInterceptor)
            {
                eventInterceptor.ProcessMouseDoubleClickAfterHandle(sender, blockMouseEventArgs);
            }

            m_rangeEditStatus.ProcessMouseDoubleClickAfterHandle(sender, e);
        }

        void BlockPanel_MouseClick(object sender, MouseEventArgs eArg)
        {
            if (null == m_range)
            {
                return;
            }

            Point pos = DPtoLP(eArg.Location);
            MouseEventArgs e = new MouseEventArgs(eArg.Button, eArg.Clicks, pos.X, pos.Y, eArg.Delta);

            m_rangeEditStatus.ProcessMouseClickBeforeHandle(sender, e);

            RangeMouseEventArgs blockMouseEventArgs = new RangeMouseEventArgs(m_rangeEditStatus, e.Button, e.Clicks, e.X, e.Y, eArg.X, eArg.Y, e.Delta);

            //处理事件前拦截
            foreach (IDrawPanelEventInterceptor eventInterceptor in m_eventInterceptor)
            {
                eventInterceptor.ProcessMouseClickBeforeHandle(sender, blockMouseEventArgs);
            }
            
            //处理事件
            foreach (IRangeEventHandler eventHandler in m_eventHandler)
            {
                eventHandler.MouseClick(sender, blockMouseEventArgs);
            }

            //处理事件后拦截
            foreach (IDrawPanelEventInterceptor eventInterceptor in m_eventInterceptor)
            {
                eventInterceptor.ProcessMouseClickAfterHandle(sender, blockMouseEventArgs);
            }

            m_rangeEditStatus.ProcessMouseClickAfterHandle(sender, e);
        }

        public void Refresh(Rectangle rectRefresh)
        {
            Rectangle rect = rectRefresh;
            Movement move = new Movement();
            move.XMovement = AutoScrollPosition.X;
            move.YMovement = AutoScrollPosition.Y;
            move.LPtoDP(ref rect);

            Invalidate(rect);
        }

        private void BaseDrawPanel_Paint(object sender, PaintEventArgs e)
        {
            if (null == RangeData)
            {
                return;
            }

            using (BufferedGraphicsContext currentContext = BufferedGraphicsManager.Current)
            {
                using (BufferedGraphics myBuffer = currentContext.Allocate(e.Graphics, e.ClipRectangle))
                {
                    myBuffer.Graphics.SmoothingMode = SmoothingMode.HighQuality;
                    myBuffer.Graphics.Clear(BackColor);

                    //设置滚动条宽度
                    DoSetScrollMinSize();
                    
                    myBuffer.Graphics.TranslateTransform(AutoScrollPosition.X, AutoScrollPosition.Y);

                    Rectangle rect = e.ClipRectangle;
                    Movement move = new Movement();
                    move.XMovement = -AutoScrollPosition.X;
                    move.YMovement = -AutoScrollPosition.Y;
                    move.LPtoDP(ref rect);

                    RangePaintEventArgs blockEventHandlePaintEventArgs = new RangePaintEventArgs(m_rangeEditStatus, myBuffer.Graphics, rect);

                    //绘制前拦截
                    foreach (IDrawPanelEventInterceptor eventInterceptor in m_eventInterceptor)
                    {
                        eventInterceptor.PaintBefore(sender, blockEventHandlePaintEventArgs);
                    }
                    
                    //绘制
                    foreach (IRangeEventHandler eventHandler in m_eventHandler)
                    {
                        eventHandler.Paint(sender, blockEventHandlePaintEventArgs);
                    }
                    RangeData.Draw(myBuffer.Graphics, rect);

                    //绘制后拦截
                    foreach (IDrawPanelEventInterceptor eventInterceptor in m_eventInterceptor)
                    {
                        eventInterceptor.PaintAfter(sender, blockEventHandlePaintEventArgs);
                    }

                    myBuffer.Render(e.Graphics);  //呈现图像至关联的Graphics
                }
            }
        }

        protected virtual void DoSetScrollMinSize()
        {
            this.AutoScrollMinSize = new Size((int)(RangeData.BoundsRect.Size.Width + 500), (int)(RangeData.BoundsRect.Size.Height + 500));
        }

        #region IHelpHandle 成员

        public virtual void SetSuccessor(IHelpHandle handle)
        {
            m_cHelpHandle.SetSuccessor(handle);
        }

        public virtual void HandleHelp(string text)
        {
            m_cHelpHandle.HandleHelp(text);
        }

        #endregion
    }
}
