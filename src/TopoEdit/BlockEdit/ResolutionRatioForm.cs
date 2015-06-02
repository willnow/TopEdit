using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TopoEdit
{
    public partial class ResolutionRatioForm : Form
    {
        public ResolutionRatioForm()
        {
            InitializeComponent();
        }

        public int RadioX
        {
            get
            {
                int radioX = 1920;
                int.TryParse(txtX.Text, out radioX);
                return radioX;
            }
        }

        public int RadioY
        {
            get
            {
                int radioY = 1080;
                int.TryParse(txtY.Text, out radioY);
                return radioY;
            }
        }

        private void ResolutionRatioForm_Load(object sender, EventArgs e)
        {

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
