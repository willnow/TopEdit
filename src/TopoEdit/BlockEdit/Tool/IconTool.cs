using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using TopoEdit.Icon;

namespace TopoEdit.Tool
{
    public partial class IconTool : ToolStripButton
    {
        /// <summary>
        /// 需要被绘制的ICON
        /// </summary>
        private IIcon m_icon = null;

        public IconTool()
        {
            InitializeComponent();
        }

        public IconTool(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        public IIcon DrawIcon
        {
            get { return m_icon; }
            set { m_icon = value; }
        }
    }
}
