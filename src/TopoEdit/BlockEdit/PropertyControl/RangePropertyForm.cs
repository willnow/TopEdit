using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using TopoEdit.Icon;
using System.Diagnostics;

namespace TopoEdit.PropertyControl
{
    public partial class RangePropertyForm : Form
    {
        /// <summary>
        /// 被编辑的ICON
        /// </summary>
        private IRange m_range;

        public RangePropertyForm(IRange range)
        {
            InitializeComponent();

            Debug.Assert(null != range);
            m_range = (IRange)(range.Clone());
        }

        public IRange EditedIcon
        {
            get
            {
                return (IRange)(m_range.Clone());
            }
        }

        private void RangePropertyForm_Load(object sender, EventArgs e)
        {
            foreach (TabPage page in propertyTabControl.TabPages)
            {
                if (page.Controls.Count >= 1)
                {
                    (page.Controls[0] as IDrawPropertyControl).LoadData(m_range);
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Apply();

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            Apply();
        }

        private void Apply()
        {
            foreach (TabPage page in propertyTabControl.TabPages)
            {
                if (page.Controls.Count >= 1)
                {
                    (page.Controls[0] as IDrawPropertyControl).SaveData(m_range);
                }
            }
        }
    }
}
