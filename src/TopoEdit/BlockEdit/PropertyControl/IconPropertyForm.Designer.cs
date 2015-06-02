namespace TopoEdit.PropertyControl
{
    partial class IconPropertyForm
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
            this.tabPageGeneral = new System.Windows.Forms.TabPage();
            this.generalPropertyControl1 = new TopoEdit.PropertyControl.GeneralPropertyControl();
            this.tabPageLogicTable = new System.Windows.Forms.TabPage();
            this.logicTablePropertyControl1 = new TopoEdit.PropertyControl.LogicTablePropertyControl();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.propertyTabControl.SuspendLayout();
            this.tabPageGeneral.SuspendLayout();
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(649, 408);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnCancel);
            this.flowLayoutPanel1.Controls.Add(this.btnOK);
            this.flowLayoutPanel1.Controls.Add(this.btnApply);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 374);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(643, 31);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(565, 3);
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
            this.btnOK.Location = new System.Drawing.Point(484, 3);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(403, 3);
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
            this.propertyTabControl.Controls.Add(this.tabPageGeneral);
            this.propertyTabControl.Controls.Add(this.tabPageLogicTable);
            this.propertyTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyTabControl.Location = new System.Drawing.Point(3, 3);
            this.propertyTabControl.Name = "propertyTabControl";
            this.propertyTabControl.SelectedIndex = 0;
            this.propertyTabControl.Size = new System.Drawing.Size(643, 365);
            this.propertyTabControl.TabIndex = 1;
            // 
            // tabPageGeneral
            // 
            this.tabPageGeneral.Controls.Add(this.generalPropertyControl1);
            this.tabPageGeneral.Location = new System.Drawing.Point(4, 22);
            this.tabPageGeneral.Name = "tabPageGeneral";
            this.tabPageGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageGeneral.Size = new System.Drawing.Size(635, 339);
            this.tabPageGeneral.TabIndex = 0;
            this.tabPageGeneral.Text = "一般";
            this.tabPageGeneral.UseVisualStyleBackColor = true;
            // 
            // generalPropertyControl1
            // 
            this.generalPropertyControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.generalPropertyControl1.Location = new System.Drawing.Point(3, 3);
            this.generalPropertyControl1.Name = "generalPropertyControl1";
            this.generalPropertyControl1.Size = new System.Drawing.Size(629, 333);
            this.generalPropertyControl1.TabIndex = 0;
            // 
            // tabPageLogicTable
            // 
            this.tabPageLogicTable.Controls.Add(this.logicTablePropertyControl1);
            this.tabPageLogicTable.Location = new System.Drawing.Point(4, 22);
            this.tabPageLogicTable.Name = "tabPageLogicTable";
            this.tabPageLogicTable.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageLogicTable.Size = new System.Drawing.Size(635, 299);
            this.tabPageLogicTable.TabIndex = 1;
            this.tabPageLogicTable.Text = "逻辑表";
            this.tabPageLogicTable.UseVisualStyleBackColor = true;
            // 
            // logicTablePropertyControl1
            // 
            this.logicTablePropertyControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logicTablePropertyControl1.Location = new System.Drawing.Point(3, 3);
            this.logicTablePropertyControl1.Name = "logicTablePropertyControl1";
            this.logicTablePropertyControl1.Size = new System.Drawing.Size(629, 293);
            this.logicTablePropertyControl1.TabIndex = 0;
            // 
            // IconPropertyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(649, 408);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "IconPropertyForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Icon属性";
            this.Load += new System.EventHandler(this.IconPropertyForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.propertyTabControl.ResumeLayout(false);
            this.tabPageGeneral.ResumeLayout(false);
            this.tabPageLogicTable.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TabControl propertyTabControl;
        private System.Windows.Forms.TabPage tabPageGeneral;
        private System.Windows.Forms.TabPage tabPageLogicTable;
        private GeneralPropertyControl generalPropertyControl1;
        private LogicTablePropertyControl logicTablePropertyControl1;
        private System.Windows.Forms.Button btnApply;
    }
}