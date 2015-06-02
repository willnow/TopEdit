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
    public partial class CirclePropertyControl : IDrawPropertyControl
    {
        public CirclePropertyControl()
        {
            InitializeComponent();
        }

        public override void InternalSaveData()
        {
            Debug.Assert(m_draw is IconCircle);

            IconCircle circle = m_draw as IconCircle;

            if (null != circle)
            {
                PointF center = circle.GetCenter();
                float posx = center.X;
                float posy = center.Y;
                float diameter = circle.Diameter;

                if (float.TryParse(txtCenterX.Text.Trim(), out posx) && float.TryParse(txtCenterY.Text.Trim(), out posy) && float.TryParse(txtDiameter.Text.Trim(), out diameter))
                {
                    circle.SetCenter(new PointF(posx, posy), diameter);
                }

                float weight = circle.Weight;
                if (float.TryParse(txtWeight.Text.Trim(), out weight))
                {
                    circle.Weight = weight;
                }

                circle.Fill = checkBoxFill.Checked;
            }
        }

        public override void InternalLoadData()
        {
            Debug.Assert(m_draw is IconCircle);

            IconCircle circle = m_draw as IconCircle;

            if (null != circle)
            {
                PointF center = circle.GetCenter();
                txtCenterX.Text = Math.Round(center.X).ToString();
                txtCenterY.Text = Math.Round(center.Y).ToString();
                txtDiameter.Text = Math.Round(circle.Diameter).ToString();
                txtWeight.Text = Math.Round(circle.Weight).ToString();
                checkBoxFill.Checked = circle.Fill;
            }
        }
    }
}
