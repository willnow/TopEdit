namespace TopoEdit.PropertyControl
{
    partial class BlockRectPropertyControl
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnEditBlock = new System.Windows.Forms.Button();
            this.txtName = new System.Windows.Forms.TextBox();
            this.cbTemplateName = new System.Windows.Forms.ComboBox();
            this.ckbAdjustBound = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtPosY = new System.Windows.Forms.TextBox();
            this.txtPosX = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(52, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(52, 152);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "Template Name";
            // 
            // btnEditBlock
            // 
            this.btnEditBlock.Location = new System.Drawing.Point(296, 194);
            this.btnEditBlock.Name = "btnEditBlock";
            this.btnEditBlock.Size = new System.Drawing.Size(75, 23);
            this.btnEditBlock.TabIndex = 2;
            this.btnEditBlock.Text = "编辑BLOCK";
            this.btnEditBlock.UseVisualStyleBackColor = true;
            this.btnEditBlock.Click += new System.EventHandler(this.btnEditBlock_Click);
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(178, 28);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(193, 21);
            this.txtName.TabIndex = 3;
            // 
            // cbTemplateName
            // 
            this.cbTemplateName.FormattingEnabled = true;
            this.cbTemplateName.Location = new System.Drawing.Point(178, 149);
            this.cbTemplateName.Name = "cbTemplateName";
            this.cbTemplateName.Size = new System.Drawing.Size(193, 20);
            this.cbTemplateName.TabIndex = 4;
            // 
            // ckbAdjustBound
            // 
            this.ckbAdjustBound.AutoSize = true;
            this.ckbAdjustBound.Location = new System.Drawing.Point(178, 198);
            this.ckbAdjustBound.Name = "ckbAdjustBound";
            this.ckbAdjustBound.Size = new System.Drawing.Size(84, 16);
            this.ckbAdjustBound.TabIndex = 6;
            this.ckbAdjustBound.Text = "自适应边框";
            this.ckbAdjustBound.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(52, 107);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 12);
            this.label8.TabIndex = 9;
            this.label8.Text = "Postion Y:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(52, 67);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 10;
            this.label7.Text = "Postion X:";
            // 
            // txtPosY
            // 
            this.txtPosY.Location = new System.Drawing.Point(178, 104);
            this.txtPosY.Name = "txtPosY";
            this.txtPosY.Size = new System.Drawing.Size(193, 21);
            this.txtPosY.TabIndex = 7;
            // 
            // txtPosX
            // 
            this.txtPosX.Location = new System.Drawing.Point(178, 64);
            this.txtPosX.Name = "txtPosX";
            this.txtPosX.Size = new System.Drawing.Size(193, 21);
            this.txtPosX.TabIndex = 8;
            // 
            // BlockRectPropertyControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtPosY);
            this.Controls.Add(this.txtPosX);
            this.Controls.Add(this.ckbAdjustBound);
            this.Controls.Add(this.cbTemplateName);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.btnEditBlock);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "BlockRectPropertyControl";
            this.Size = new System.Drawing.Size(438, 260);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnEditBlock;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.ComboBox cbTemplateName;
        private System.Windows.Forms.CheckBox ckbAdjustBound;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtPosY;
        private System.Windows.Forms.TextBox txtPosX;
    }
}
