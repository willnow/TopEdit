using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using TopoEdit.Icon;
using TopoEdit.EventHandler;

namespace TopoEdit
{
    public partial class BlockPanel : IBaseDrawPanel
    {
        public event EventHandler<ChangeBlockEventArgs> ChangeBlockEvent;

        public BlockPanel()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // BlockPanel
            // 
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.BlockPanel_Paint);
            this.ResumeLayout(false);
        }

        internal Block BlockData
        {
            get { return RangeData as Block; }
            set
            {
                RangeData = value;
            }
        }

        /// <summary>
        /// 通知拓扑图同步更新
        /// </summary>
        /// <param name="changeBlock"></param>
        internal void NotifyPageToUpdate(Block changeBlock)
        {
            if (ChangeBlockEvent != null)
            {
                Debug.Assert(null != changeBlock);
                ChangeBlockEvent(this, new ChangeBlockEventArgs(changeBlock));
            }
        }

        protected override void Init()
        {
            base.Init();

            RegHandler(new MoveSelectedIconHandler());
            RegHandler(new SelectRangeHandler());
            RegHandler(new DrawIconHandler());
            RegHandler(new CopyIconEventHandler());
            RegHandler(new ZoomSelectedIconInBlockHandler());
            RegHandler(new DelectIconEventHandler());
            RegHandler(new EditIconPropertyEventHandler());
            RegHandler(new SaveBlockEventHandler());
            RegHandler(new DrawIconPolygonHandler());
            RegHandler(new UnDoEventHandler());
            //RegHandler(new ZoomPanelViewEventHandler());
        }

        void BlockPanel_Paint(object sender, PaintEventArgs e)
        {
            
        }
    }

    /// <summary>
    /// 修改BLOCK事件
    /// </summary>
    public class ChangeBlockEventArgs : EventArgs
    {
        private Block m_block;

        internal Block BlockItem
        {
            get { return m_block; }
        }

        internal ChangeBlockEventArgs(Block block)
        {
            m_block = block;
        }
    }
}
