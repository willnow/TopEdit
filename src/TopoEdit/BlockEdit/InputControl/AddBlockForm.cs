using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TopoEdit.InputControl
{
    public partial class AddBlockForm : Form
    {
        public string BlockName
        {
            get
            {
                return this.tbBlockName.Text;
            }
        }

        public AddBlockForm()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
