using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using TopoEdit.Icon;
using System.Drawing.Drawing2D;
using System.Diagnostics;

namespace TopoEdit.PropertyControl
{
    public partial class LinePropertyControl : IDrawPropertyControl
    {
        public LinePropertyControl()
        {
            InitializeComponent();
        }

        public override void InternalSaveData()
        {
            Debug.Assert(m_draw is IconLine);

            IconLine line = m_draw as IconLine;

            if (null != line)
            {
                float posx = line.BeginPosition.X;
                if (float.TryParse(txtPosX.Text.Trim(), out posx))
                {
                    line.BeginPosition = new PointF(posx, line.BeginPosition.Y);
                }

                float posy = line.BeginPosition.Y;
                if (float.TryParse(txtPosY.Text.Trim(), out posy))
                {
                    line.BeginPosition = new PointF(line.BeginPosition.X, posy);
                }

                float len = line.Length;
                if (float.TryParse(txtLen.Text.Trim(), out len))
                {
                    line.Length = len;
                }

                float weight = line.Weight;
                if (float.TryParse(txtWeight.Text.Trim(), out weight))
                {
                    line.Weight = weight;
                }
                line.LineDashStyle = (DashStyle)cbDashStyle.SelectedValue;
            }
        }

        public override void InternalLoadData()
        {
            Debug.Assert(m_draw is IconLine);

            IconLine line = m_draw as IconLine;

            if (null != line)
            {
                txtPosX.Text = Math.Round(line.BeginPosition.X).ToString();
                txtPosY.Text = Math.Round(line.BeginPosition.Y).ToString();
                txtLen.Text = Math.Round(line.Length).ToString();
                txtWeight.Text = Math.Round(line.Weight).ToString();
                cbDashStyle.SelectedItem = line.LineDashStyle;
            }
        }

        private void LinePropertyControl_Load(object sender, EventArgs e)
        {
            cbDashStyle.DataSource = Enum.GetValues(typeof(DashStyle));
        }
    }
}
