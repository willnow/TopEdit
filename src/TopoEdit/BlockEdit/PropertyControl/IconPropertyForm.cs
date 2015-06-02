using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using TopoEdit.Icon;

namespace TopoEdit.PropertyControl
{
    public partial class IconPropertyForm : Form
    {
        /// <summary>
        /// 被编辑的ICON
        /// </summary>
        private IIcon m_icon;

        public IconPropertyForm(IIcon icon)
        {
            InitializeComponent();

            Debug.Assert(null != icon);
            m_icon = (IIcon)(icon.Clone());
        }

        public IIcon EditedIcon
        {
            get
            {
                return (IIcon)(m_icon.Clone());
            }
        }

        private void IconPropertyForm_Load(object sender, EventArgs e)
        {
            //根据ICON类型添加特殊属性控件
            TabPage sepcialPage = new TabPage("专有");

            IDrawPropertyControl control = null;
            
            switch(m_icon.IconType)
            {
                case TopoEdit.Icon.EmIconType.Circle:
                    {
                        control =new CirclePropertyControl();
                        Text = "Circle 属性";
                        break;
                    }
                case TopoEdit.Icon.EmIconType.Line:
                    {
                        control = new LinePropertyControl();
                        Text = "Line 属性";
                        break;
                    }
                case TopoEdit.Icon.EmIconType.Rectangle:
                    {
                        control = new RectanglePropertyControl();
                        Text = "Rectangle 属性";
                        break;
                    }
                case TopoEdit.Icon.EmIconType.Text:
                    {
                        control = new TextPropertyControl();
                        Text = "Text 属性";
                        break;
                    }
                case EmIconType.Polygon:
                    {
                        control = new PolygonPropertyControl();
                        Text = "Polygon 属性";
                        break;
                    }
            }

            if (null != control)
            {
                control.Dock = DockStyle.Fill;
                sepcialPage.Controls.Add(control);
                propertyTabControl.TabPages.Add(sepcialPage);
            }
           
            foreach (TabPage page in propertyTabControl.TabPages)
            {
                if(page.Controls.Count >= 1)
                {
                    (page.Controls[0] as IDrawPropertyControl).LoadData(m_icon);
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
                    (page.Controls[0] as IDrawPropertyControl).SaveData(m_icon);
                }
            }
        }
    }
}
