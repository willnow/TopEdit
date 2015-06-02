using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using TopoEdit.Stratege;

namespace TopoEdit.Tool
{
    public partial class AlignButton : ToolStripButton
    {
        /// <summary>
        /// 该Button的类型
        /// </summary>
        private EmAlign m_emAlign = EmAlign.Left;

        public AlignButton()
        {
            InitializeComponent();
        }

        public AlignButton(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        public EmAlign AlignType
        {
            get
            {
                return m_emAlign;
            }
            set
            {
                m_emAlign = value;
            }
        }
    }
}
