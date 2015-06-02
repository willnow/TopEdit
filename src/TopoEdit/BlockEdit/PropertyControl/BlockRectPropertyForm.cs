using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using TopoEdit.Icon;

namespace TopoEdit.PropertyControl
{
    internal partial class BlockRectPropertyForm : Form
    {
        /// <summary>
        /// 被编辑的Block实例
        /// </summary>
        private BlockRect m_blockRect;

        internal BlockRectPropertyForm(BlockRect blockRect)
        {
            InitializeComponent();
            m_blockRect = blockRect;
        }

        internal BlockRect EditedBlockRect
        {
            get
            {
                return (BlockRect)(m_blockRect.Clone());
            }
        }

        private void BlockRectPropertyForm_Load(object sender, EventArgs e)
        {
            blockRectPropertyControl.LoadData(m_blockRect);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            blockRectPropertyControl.SaveData(m_blockRect);
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
