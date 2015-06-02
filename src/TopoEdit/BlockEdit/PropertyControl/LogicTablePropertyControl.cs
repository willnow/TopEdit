using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using TopoEdit.Model;
using System.Diagnostics;
using TopoEdit.Icon;
using System.Xml;
using System.IO;

namespace TopoEdit.PropertyControl
{
    public partial class LogicTablePropertyControl : IDrawPropertyControl
    {
        public LogicTablePropertyControl()
        {
            InitializeComponent();
        }

        public override void InternalSaveData()
        {
            Debug.Assert(m_draw is IIcon);
            IIcon icon = m_draw as IIcon;

           
            if (icon.LogicTableItem == null)
            {
                if (dgvLT.Rows.Count > 0)
                {
                    //原来没有逻辑表，但现在新增逻辑表
                    icon.LogicTableItem = new LogicTable(false);
                }
                else
                {
                    //原来没有逻辑表，现在也没有逻辑表
                    return;
                }
            }

            icon.LogicTableItem.ColorCollection.Clear();
            icon.LogicTableItem.ExpressionCollection.Clear();

            foreach (DataGridViewRow row in dgvLT.Rows)
            {
                if ((null != row.Cells["ColumnExpression"].Value) && ("" != row.Cells["ColumnExpression"].Value.ToString().Trim()))
                {
                    icon.LogicTableItem.ExpressionCollection.Add(row.Cells["ColumnExpression"].Value.ToString());
                    icon.LogicTableItem.ColorCollection.Add(ColorLib.Instance.GetColorIndexByName(row.Cells["ColumnColor"].Value.ToString()));
                }
            }

            LogicTableChecker checker = new LogicTableChecker(icon.LogicTableItem.ExpressionCollection);
            List<ErrorInfo> errorList;
            checker.Validate(out errorList);
            if (errorList.Count > 0)
            {
                ResultForm resultForm = new ResultForm();
                resultForm.Text = "逻辑表检查结果";
                resultForm.SetDisplayInfo(errorList);
                resultForm.ShowDialog();
            }
        }

        public override void InternalLoadData()
        {
            Debug.Assert(m_draw is IIcon);
            IIcon icon = m_draw as IIcon;

            dgvLT.Rows.Clear();

            if (icon.LogicTableItem == null)
            {
                return;
            }

            foreach (string exp in icon.LogicTableItem.ExpressionCollection)
            {
                DataGridViewTextBoxCell columnExpression = new DataGridViewTextBoxCell();
                columnExpression.Value = exp.Trim();

                DataGridViewRow row =new DataGridViewRow();
                row.Cells.Add(columnExpression);
                row.Cells.Add(new DataGridViewComboBoxCell());

                dgvLT.Rows.Add(row);
            }

            for (int i = 0; i < icon.LogicTableItem.ColorCollection.Count; ++i)
            {
                DataGridViewComboBoxCell columnColor = dgvLT.Rows[i].Cells["ColumnColor"] as DataGridViewComboBoxCell;
                columnColor.Value = ColorLib.Instance.GetColor(icon.LogicTableItem.ColorCollection[i]).ToString();
            }
        }

        private void dgvLT_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            DataGridViewComboBoxCell columnColor = dgvLT.Rows[e.RowIndex].Cells["ColumnColor"] as DataGridViewComboBoxCell;
            columnColor.Items.Clear();
            foreach (TdeColor color in ColorLib.Instance.GetAllColor())
            {
                columnColor.Items.Add(color.ToString());
            }
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            IIcon icon = m_draw as IIcon;

            if (icon.LogicTableItem != null)
            {
                XmlDocument xmlDoc = new XmlDocument();
                XmlNode ltNode = xmlDoc.CreateElement("logicTableCopyed");
                icon.LogicTableItem.Save(ltNode);
                Clipboard.SetDataObject("<logicTableCopyed>" + ltNode.InnerXml + "</logicTableCopyed>");
            }
            else
            {
                //没有数据，不需要复制
            }
        }

        private void btnPaste_Click(object sender, EventArgs e)
        {
            IDataObject iData = Clipboard.GetDataObject();
            if(iData.GetDataPresent(DataFormats.Text)) 
            {
                using(TextReader reader  = new StringReader((String)iData.GetData(DataFormats.Text)))
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(reader);
                    LogicTable logicTable = new LogicTable(false);
                    logicTable.Load(xmlDoc.DocumentElement);

                    Debug.Assert(m_draw is IIcon);
                    IIcon icon = m_draw as IIcon;

                    if (icon.LogicTableItem == null)
                    {
                        if (logicTable.ExpressionCollection.Count > 0)
                        {
                            //原来没有逻辑表，但现在新增逻辑表
                            icon.LogicTableItem = new LogicTable(false);
                        }
                        else
                        {
                            //原来没有逻辑表，现在也没有逻辑表
                            return;
                        }
                    }

                    for (int i = 0; i < logicTable.ColorCollection.Count; ++i)
                    {
                        string exp = logicTable.ExpressionCollection[i];

                        DataGridViewTextBoxCell columnExpression = new DataGridViewTextBoxCell();
                        columnExpression.Value = exp.Trim();

                        DataGridViewRow row = new DataGridViewRow();
                        row.Cells.Add(columnExpression);
                        row.Cells.Add(new DataGridViewComboBoxCell());

                        dgvLT.Rows.Add(row);
                        icon.LogicTableItem.ExpressionCollection.Add(exp);
                        icon.LogicTableItem.ColorCollection.Add(logicTable.ColorCollection[i]);

                        DataGridViewComboBoxCell columnColor = row.Cells["ColumnColor"] as DataGridViewComboBoxCell;
                        columnColor.Value = ColorLib.Instance.GetColor(logicTable.ColorCollection[i]).ToString();
                    }
                }
            }
        }
    }
}
