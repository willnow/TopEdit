using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TopoEdit.InputControl
{
    public partial class AddPageForm : Form
    {
        private int m_pageId = 1;

        public string PageName
        {
            get
            {
                return tbPageName.Text;
            }
        }

        public int PageId
        {
            get
            {
                return m_pageId;
            }
        }
        public AddPageForm()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            int pageId = 1;
            if (!int.TryParse(tbPageId.Text, out pageId))
            {
                MessageBox.Show("请输入有效的页Id（数字）");
                return;
            }
            else
            {
                m_pageId = pageId;
            }

            if (tbPageName.Text.Trim() == "")
            {
                MessageBox.Show("页名称不能为空");
                return;
            }

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
