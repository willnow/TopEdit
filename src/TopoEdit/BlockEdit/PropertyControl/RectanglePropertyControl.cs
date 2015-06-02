using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using TopoEdit.Icon;
using System.Diagnostics;

namespace TopoEdit.PropertyControl
{
    public partial class RectanglePropertyControl : IDrawPropertyControl
    {
        public RectanglePropertyControl()
        {
            InitializeComponent();
        }

        public override void InternalLoadData()
        {
            Debug.Assert(m_draw is IconRectangle);

            IconRectangle rect = m_draw as IconRectangle;

            if (null != rect)
            {
                txtPosX.Text = Math.Round(rect.Position.X).ToString();
                txtPosY.Text = Math.Round(rect.Position.Y).ToString();
                txtWidth.Text = Math.Round(rect.Width).ToString();
                txtHeight.Text = Math.Round(rect.Height).ToString();
                txtWeight.Text = Math.Round(rect.Weight).ToString();
                txtAngle.Text = rect.Degree.ToString();
                ckbFill.Checked = rect.Fill;
            }
        }

        public override void InternalSaveData()
        {
            Debug.Assert(m_draw is IconRectangle);

            IconRectangle rect = m_draw as IconRectangle;

            if (null != rect)
            {
                float posx = rect.Position.X;
                if (float.TryParse(txtPosX.Text.Trim(), out posx))
                {
                    rect.Position = new PointF(posx, rect.Position.Y);
                }

                float posy = rect.Position.Y;
                if (float.TryParse(txtPosY.Text.Trim(), out posy))
                {
                    rect.Position = new PointF(rect.Position.X, posy);
                }

                float weight = rect.Weight;
                if (float.TryParse(txtWeight.Text.Trim(), out weight))
                {
                    rect.Weight = weight;
                }

                float width = rect.Width;
                if (float.TryParse(txtWidth.Text.Trim(), out width))
                {
                    rect.Width = width;
                }

                float height = rect.Height;
                if (float.TryParse(txtHeight.Text.Trim(), out height))
                {
                    rect.Height = height;
                }

                int angle = rect.Degree;
                if (int.TryParse(txtAngle.Text.Trim(), out angle))
                {
                    rect.Degree = angle;
                }

                rect.Fill = ckbFill.Checked;
            }
        }
    }
}
