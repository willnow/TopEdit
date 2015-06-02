using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using TopoEdit.Icon;

namespace TopoEdit.PropertyControl
{
    public partial class BlockRectPropertyControl : IDrawPropertyControl
    {
        public BlockRectPropertyControl()
        {
            InitializeComponent();

            Add(new BlockRectPropertyControlTextInterceptor());//添加拦截处理当Block实例仅仅包含一个文本图元的情况
        }

        public bool NameEditEnable
        {
            get { return txtName.Enabled; }
            set { txtName.Enabled = value; }
        }

        public override void InternalSaveData()
        {
            Debug.Assert(m_draw is BlockRect);

            BlockRect blockRect = m_draw as BlockRect;

            if (null != blockRect)
            {
                RectangleF blockRectBounds = blockRect.BoundsRect;
                float posx = blockRectBounds.X;
                float posy = blockRectBounds.Y;

                if (float.TryParse(txtPosX.Text.Trim(), out posx) && float.TryParse(txtPosY.Text.Trim(), out posy))
                {
                    Movement move = new Movement();
                    move.XMovement = posx - blockRectBounds.X;
                    move.YMovement = posy - blockRectBounds.Y;
                    blockRect.Move(move);
                }

                blockRect.TemplateName = cbTemplateName.Text;
                blockRect.Name = txtName.Text;

                if (ckbAdjustBound.Checked)
                {
                    blockRect.ResetRectange();
                }
            }
        }

        public override void InternalLoadData()
        {

            Debug.Assert(m_draw is BlockRect);

            BlockRect blockRect = m_draw as BlockRect;

            if (null != blockRect)
            {
                RectangleF blockRectBounds = blockRect.BoundsRect;
                txtPosX.Text = Math.Round(blockRectBounds.X).ToString();
                txtPosY.Text = Math.Round(blockRectBounds.Y).ToString();
                txtName.Text = blockRect.Name;
                cbTemplateName.DataSource = BlockContainer.Instance.GetBlockNames();
                cbTemplateName.Text = blockRect.TemplateName;
            }
        }

        private void btnEditBlock_Click(object sender, EventArgs e)
        {
            //切换到指定BLOCK视图
        }
    }
}
