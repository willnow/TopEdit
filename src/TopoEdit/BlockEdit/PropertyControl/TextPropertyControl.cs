using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using CSR.CUIT.GlobalService.ShareLib;
using System.Diagnostics;
using TopoEdit.Icon;
using TopoEdit.Model;

namespace TopoEdit.PropertyControl
{
    public partial class TextPropertyControl : IDrawPropertyControl
    {
        public TextPropertyControl()
        {
            InitializeComponent();
        }

        private void TextPropertyControl_Load(object sender, EventArgs e)
        {
            cbHAlign.DataSource = Enum.GetValues(typeof(EmHorizontalAlignment));
            cbVAlign.DataSource = Enum.GetValues(typeof(EmVerticalAlignment));
            cbFontName.DataSource = Utility.GetAllInstalledFont();
        }

        public override void InternalLoadData()
        {
            Debug.Assert(m_draw is IconText);

            IconText text = m_draw as IconText;

            if (null != text)
            {
                txtPosX.Text = (Math.Round(text.Pos.X)).ToString();
                txtPosY.Text = (Math.Round(text.Pos.Y)).ToString();
                txtWidth.Text = (Math.Round(text.Width)).ToString();
                txtHeight.Text = (Math.Round(text.Height)).ToString();
                txtValue.Text = text.Value;
                ckbEnable.Checked = text.Enable;
                cbHAlign.SelectedItem = text.HAlign;
                cbVAlign.SelectedItem = text.VAlign;
                cbFontName.SelectedItem = text.FontName;
                txtFontSize.Text = (Math.Round(text.FontSize)).ToString();
                ckbBold.Checked = ((int)text.FontStyle & (int)FontStyle.Bold) > 0;
                ckbItalic.Checked = ((int)text.FontStyle & (int)FontStyle.Italic) > 0;
                ckbStrokedOut.Checked = ((int)text.FontStyle & (int)FontStyle.Strikeout) > 0;
                ckbUnderlined.Checked = ((int)text.FontStyle & (int)FontStyle.Underline) > 0;
            }
        }

        public override void InternalSaveData()
        {
            Debug.Assert(m_draw is IconText);

            IconText text = m_draw as IconText;

            if (null != text)
            {
                //text.Pos.X
                float posx = text.Pos.X;
                if (float.TryParse(txtPosX.Text.Trim(), out posx))
                {
                    text.Pos = new PointF(posx, text.Pos.Y);
                }

                float posy = text.Pos.Y;
                if (float.TryParse(txtPosY.Text.Trim(), out posy))
                {
                    text.Pos = new PointF(text.Pos.X, posy);
                }

                float width = text.Width;
                if (float.TryParse(txtWidth.Text.Trim(), out width))
                {
                    text.Width = width;
                }

                float height = text.Height;
                if (float.TryParse(txtHeight.Text.Trim(), out height))
                {
                    text.Height = height;
                }

                text.Value = txtValue.Text;
                text.Enable = ckbEnable.Checked;
                text.HAlign = (EmHorizontalAlignment)cbHAlign.SelectedItem;
                text.VAlign = (EmVerticalAlignment)cbVAlign.SelectedItem;

                 FontStyle style = FontStyle.Regular;
                if (ckbBold.Checked)
                {
                    style |= FontStyle.Bold;
                }
                if (ckbItalic.Checked)
                {
                    style |= FontStyle.Italic;
                }
                if (ckbUnderlined.Checked)
                {
                    style |= FontStyle.Underline;
                }
                if (ckbStrokedOut.Checked)
                {
                    style |= FontStyle.Strikeout;
                }

                //检查字体与字体类型是否匹配
                try
                {
                    using (Font fnt = new Font((string)cbFontName.SelectedItem, 10, style, GraphicsUnit.Point))
                    {
                        //有意留空
                    }
                }
                catch (System.ArgumentException ex)
                {
                    MessageBox.Show("字体类型错误！" + ex.Message);
                    return;
                }

                //字体与字体类型匹配
                text.FontName = (string)cbFontName.SelectedItem;

                int fontSize = (int)Math.Round(text.FontSize);
                if (int.TryParse(txtFontSize.Text, out fontSize))
                {
                    text.FontSize = fontSize;
                }

                text.FontStyle = style;
            }
        }

    }
}
