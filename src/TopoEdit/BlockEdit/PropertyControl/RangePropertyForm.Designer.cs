namespace TopoEdit.PropertyControl
{
    partial class RangePropertyForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.propertyTabControl = new System.Windows.Forms.TabControl();
            this.tabPageLogicTable = new System.Windows.Forms.TabPage();
            this.rangeLogicTablePropertyControl1 = new TopoEdit.PropertyControl.RangeLogicTablePropertyControl();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.propertyTabControl.SuspendLayout();
            this.tabPageLogicTable.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.propertyTabControl, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90.9589F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.041096F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(642, 386);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnCancel);
            this.flowLayoutPanel1.Controls.Add(this.btnOK);
            this.flowLayoutPanel1.Controls.Add(this.btnApply);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 354);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(636, 29);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(558, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(477, 3);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(396, 3);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 23);
            this.btnApply.TabIndex = 2;
            this.btnApply.Text = "应用";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Visible = false;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // propertyTabControl
            // 
            this.propertyTabControl.Controls.Add(this.tabPageLogicTable);
            this.propertyTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyTabControl.Location = new System.Drawing.Point(3, 3);
            this.propertyTabControl.Name = "propertyTabControl";
            this.propertyTabControl.SelectedIndex = 0;
            this.propertyTabControl.Size = new System.Drawing.Size(636, 345);
            this.propertyTabControl.TabIndex = 1;
            // 
            // tabPageLogicTable
            // 
            this.tabPageLogicTable.Controls.Add(this.rangeLogicTablePropertyControl1);
            this.tabPageLogicTable.Location = new System.Drawing.Point(4, 22);
            this.tabPageLogicTable.Name = "tabPageLogicTable";
            this.tabPageLogicTable.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageLogicTable.Size = new System.Drawing.Size(628, 319);
            this.tabPageLogicTable.TabIndex = 1;
            this.tabPageLogicTable.Text = "逻辑表";
            this.tabPageLogicTable.UseVisualStyleBackColor = true;
            // 
            // rangeLogicTablePropertyControl1
            // 
            this.rangeLogicTablePropertyControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rangeLogicTablePropertyControl1.Location = new System.Drawing.Point(3, 3);
            this.rangeLogicTablePropertyControl1.Name = "rangeLogicTablePropertyControl1";
            this.rangeLogicTablePropertyControl1.Size = new System.Drawing.Size(622, 313);
            this.rangeLogicTablePropertyControl1.TabIndex = 0;
            // 
            // RangePropertyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(642, 386);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RangePropertyForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "批量设置图元属性";
            this.Load += new System.EventHandler(this.RangePropertyForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.propertyTabControl.ResumeLayout(false);
            this.tabPageLogicTable.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.TabControl propertyTabControl;
        private System.Windows.Forms.TabPage tabPageLogicTable;
        private RangeLogicTablePropertyControl rangeLogicTablePropertyControl1;

    }
}