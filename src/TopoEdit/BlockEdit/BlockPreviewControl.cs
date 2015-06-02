using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using TopoEdit.Icon;

namespace TopoEdit
{
    public partial class BlockPreviewControl : UserControl
    {
        private Block m_block = null;

        public BlockPreviewControl()
        {
            InitializeComponent();
        }

        internal void DrawBlock(Block srcblock)
        {
            m_block = (Block)(srcblock.Clone());
            //计算画板中心点
            Point posCenter = new Point(panelBlock.Size.Width / 2, panelBlock.Size.Height / 2);
            //计算BLOCK合适的显示大小
            int len = 100;
            if (panelBlock.Size.Width > panelBlock.Size.Height)
            {
                len = (int)(panelBlock.Size.Height * 0.75);
            }
            else
            {
                len = (int)(panelBlock.Size.Width * 0.75);
            }

            m_block.Reset(posCenter, len);

            panelBlock.Invalidate(true);
        }

        private void panelBlock_Paint(object sender, PaintEventArgs e)
        {
            if (null != m_block)
            {
                m_block.Draw(e.Graphics, e.ClipRectangle);
            }
        }
    }
}
