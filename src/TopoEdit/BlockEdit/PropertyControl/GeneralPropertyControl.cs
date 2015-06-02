using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using TopoEdit.Icon;
using TopoEdit.Model;
using System.Diagnostics;

namespace TopoEdit.PropertyControl
{
    public partial class GeneralPropertyControl : IDrawPropertyControl
    {
        public GeneralPropertyControl()
        {
            InitializeComponent();
        }

        public override void InternalSaveData()
        {
            Debug.Assert(m_draw is IIcon);
            IIcon icon = m_draw as IIcon;

            icon.Level = integerInputLevel.Value;
            icon.IconSubType = (string)cbType.SelectedItem;
            icon.DefaultColor = (TdeColor)cbColor.SelectedItem;
            icon.Dock[DockType.Left] = ckbDockLeft.Checked;
            icon.Dock[DockType.Right] = ckbDockRight.Checked;
            icon.Dock[DockType.Top] = ckbDockTop.Checked;
            icon.Dock[DockType.Bottom] = ckbDockBottom.Checked;
            icon.Fixed = ckbFixed.Checked;
        }

        public override void InternalLoadData()
        {
            Debug.Assert(m_draw is IIcon);
            IIcon icon = m_draw as IIcon;

            integerInputLevel.Value = icon.Level;
            cbType.SelectedItem = icon.IconSubType;
            cbColor.SelectedItem = icon.DefaultColor;
            ckbDockLeft.Checked = icon.Dock[DockType.Left];
            ckbDockRight.Checked = icon.Dock[DockType.Right];
            ckbDockTop.Checked = icon.Dock[DockType.Top];
            ckbDockBottom.Checked = icon.Dock[DockType.Bottom];
            ckbFixed.Checked = icon.Fixed;
        }

        private void GeneralPropertyControl_Load(object sender, EventArgs e)
        {
            cbType.DataSource = Enum.GetNames(typeof(TopoEdit.Icon.EmIconSubType));
            cbColor.DataSource = ColorLib.Instance.GetAllColor();
        }
    }
}
