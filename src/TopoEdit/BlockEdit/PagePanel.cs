using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using TopoEdit.Icon;
using System.Drawing;
using TopoEdit.EventHandler;
using TopoEdit.Command;

namespace TopoEdit
{
    public partial class PagePanel : IBaseDrawPanel
    {
        public event EventHandler<ChangePageEventArgs> ChangePageEvent;

        public PagePanel()
        {
            InitializeComponent();
        }

        public PagePanel(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        protected override void Init()
        {
            base.Init();

            RegHandler(new MoveSelectedIconHandler());
            RegHandler(new SelectRangeHandler());
            RegHandler(new DrawIconHandler());
            RegHandler(new CopyIconEventHandler());
            RegHandler(new ZoomSelectedBlockRectHandler());
            RegHandler(new DelectIconEventHandler());
            RegHandler(new SavePageEventHandler());
            RegHandler(new EditBlockRectPropertyEventHandler());
            RegHandler(new PaintBoundEventHandler(1920, 1080));
            RegHandler(new UnDoEventHandler());

            RangeData = null;
        }

        /// <summary>
        /// 通知拓扑图同步更新
        /// </summary>
        /// <param name="changeBlock"></param>
        internal void NotifyBookToUpdate(Page changePage)
        {
            if (ChangePageEvent != null)
            {
                Debug.Assert(null != changePage);
                ChangePageEvent(this, new ChangePageEventArgs(changePage));
            }
        }

        internal Page TopoData
        {
            get { return RangeData as Page; }
            set
            {
                RangeData = value;
            }
        }

        private void BlockTopoPanel_DragEnter(object sender, DragEventArgs e)
        {
            //处理拖拽
            e.Effect = DragDropEffects.Copy;
        }

        private void BlockTopoPanel_DragDrop(object sender, DragEventArgs e)
        {
            //处理拖拽
            Point pos = this.PointToClient(new Point(e.X, e.Y));
            Block block = e.Data.GetData((typeof(Block))) as Block;

            if (block != null)
            {
                BlockRect blockRect = new BlockRect(TopoData);
                CmdMgr.AddThenExec(new AddDrawCommand(this, TopoData, blockRect));//添加后则建立了父子关系
                Block blockTemplate = BlockContainer.Instance.GetBlockByName(block.Name);
                blockTemplate.ClearSelectIcon();
                blockRect.Init(block.Name + Book.Instance.GetNewNameIndex(block.Name), blockTemplate, pos);
                this.Refresh(Utility.AdjustRect(blockRect.BoundsRect, 10));
            }
        }
    }

    /// <summary>
    /// 修改Page事件
    /// </summary>
    public class ChangePageEventArgs : EventArgs
    {
        private Page m_page;

        internal Page PageItem
        {
            get { return m_page; }
        }

        internal ChangePageEventArgs(Page page)
        {
            m_page = page;
        }
    }
}
