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
    public partial class PolygonPropertyControl : IDrawPropertyControl
    {
        public PolygonPropertyControl()
        {
            InitializeComponent();
        }

        private void PolygonPropertyControl_Load(object sender, EventArgs e)
        {

        }

        public override void InternalSaveData()
        {
            Debug.Assert(m_draw is IconPolygon);

            IconPolygon polygon = m_draw as IconPolygon;

            if (null != polygon)
            {
                float weight = polygon.Weight;
                if (float.TryParse(txtWeight.Text.Trim(), out weight))
                {
                    polygon.Weight = weight;
                }

                polygon.Fill = ckbFill.Checked;

                for (int i = 0; i < dgvPaths.Rows.Count; ++i)
                {
                    PolygonType newPathType = (PolygonType)Enum.Parse(typeof(PolygonType), dgvPaths.Rows[i].Cells["ColumnPathStyle"].Value.ToString());
                    polygon.Paths[i] = polygon.Paths[i].ConvertTo(newPathType);
                }
            }
        }

        public override void InternalLoadData()
        {
            Debug.Assert(m_draw is IconPolygon);

            IconPolygon polygon = m_draw as IconPolygon;

            if (null != polygon)
            {
                txtWeight.Text = Math.Round(polygon.Weight).ToString();
                ckbFill.Checked = polygon.Fill;
            }

            for (int i = 0; i < polygon.Paths.Count; ++i)
            {
                DataGridViewTextBoxCell columnIndex = new DataGridViewTextBoxCell();
                columnIndex.Value = i.ToString();

                DataGridViewRow row = new DataGridViewRow();
                row.Cells.Add(columnIndex);
                row.Cells.Add(new DataGridViewComboBoxCell());

                dgvPaths.Rows.Add(row);
            }

            for (int i = 0; i < polygon.Paths.Count; ++i)
            {
                DataGridViewComboBoxCell columnPathStyle = dgvPaths.Rows[i].Cells["ColumnPathStyle"] as DataGridViewComboBoxCell;
                columnPathStyle.Value = polygon.Paths[i].PathType.ToString();
            }
        }

        private void dgvPaths_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            DataGridViewComboBoxCell columnIndex = dgvPaths.Rows[e.RowIndex].Cells["ColumnPathStyle"] as DataGridViewComboBoxCell;
            columnIndex.Items.Clear();

            string[] pathTypes = Enum.GetNames(typeof(PolygonType));
            foreach (string pathType in pathTypes)
            {
                columnIndex.Items.Add(pathType);
            }
        }       
    }
}
