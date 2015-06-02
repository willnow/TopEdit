namespace TopoEdit.PropertyControl
{
    partial class PolygonPropertyControl
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.dgvPaths = new System.Windows.Forms.DataGridView();
            this.ColumnIndex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnPathStyle = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtWeight = new System.Windows.Forms.TextBox();
            this.ckbFill = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPaths)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvPaths
            // 
            this.dgvPaths.AllowUserToAddRows = false;
            this.dgvPaths.AllowUserToDeleteRows = false;
            this.dgvPaths.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPaths.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPaths.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnIndex,
            this.ColumnPathStyle});
            this.dgvPaths.Location = new System.Drawing.Point(46, 93);
            this.dgvPaths.Name = "dgvPaths";
            this.dgvPaths.RowTemplate.Height = 23;
            this.dgvPaths.Size = new System.Drawing.Size(513, 194);
            this.dgvPaths.TabIndex = 0;
            this.dgvPaths.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dgvPaths_RowsAdded);
            // 
            // ColumnIndex
            // 
            this.ColumnIndex.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColumnIndex.HeaderText = "索引";
            this.ColumnIndex.Name = "ColumnIndex";
            this.ColumnIndex.ReadOnly = true;
            // 
            // ColumnPathStyle
            // 
            this.ColumnPathStyle.HeaderText = "路径类型";
            this.ColumnPathStyle.Name = "ColumnPathStyle";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(44, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "weight:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(46, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "fill:";
            // 
            // txtWeight
            // 
            this.txtWeight.Location = new System.Drawing.Point(110, 19);
            this.txtWeight.Name = "txtWeight";
            this.txtWeight.Size = new System.Drawing.Size(449, 21);
            this.txtWeight.TabIndex = 3;
            // 
            // ckbFill
            // 
            this.ckbFill.AutoSize = true;
            this.ckbFill.Location = new System.Drawing.Point(110, 46);
            this.ckbFill.Name = "ckbFill";
            this.ckbFill.Size = new System.Drawing.Size(15, 14);
            this.ckbFill.TabIndex = 4;
            this.ckbFill.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(46, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "Path:";
            // 
            // PolygonPropertyControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ckbFill);
            this.Controls.Add(this.txtWeight);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvPaths);
            this.Name = "PolygonPropertyControl";
            this.Size = new System.Drawing.Size(624, 308);
            this.Load += new System.EventHandler(this.PolygonPropertyControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPaths)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvPaths;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtWeight;
        private System.Windows.Forms.CheckBox ckbFill;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnIndex;
        private System.Windows.Forms.DataGridViewComboBoxColumn ColumnPathStyle;
    }
}
