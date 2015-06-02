namespace TopoEdit.PropertyControl
{
    partial class GeneralPropertyControl
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
            this.cbType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbColor = new System.Windows.Forms.ComboBox();
            this.integerInputLevel = new DevComponents.Editors.IntegerInput();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.ckbFixed = new System.Windows.Forms.CheckBox();
            this.ckbDockLeft = new System.Windows.Forms.CheckBox();
            this.ckbDockRight = new System.Windows.Forms.CheckBox();
            this.ckbDockTop = new System.Windows.Forms.CheckBox();
            this.ckbDockBottom = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.integerInputLevel)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Level:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(34, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "Type:";
            // 
            // cbType
            // 
            this.cbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbType.FormattingEnabled = true;
            this.cbType.Location = new System.Drawing.Point(129, 81);
            this.cbType.Name = "cbType";
            this.cbType.Size = new System.Drawing.Size(318, 20);
            this.cbType.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(34, 134);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "ColorIndex:";
            // 
            // cbColor
            // 
            this.cbColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbColor.FormattingEnabled = true;
            this.cbColor.Location = new System.Drawing.Point(129, 131);
            this.cbColor.Name = "cbColor";
            this.cbColor.Size = new System.Drawing.Size(318, 20);
            this.cbColor.TabIndex = 5;
            // 
            // integerInputLevel
            // 
            // 
            // 
            // 
            this.integerInputLevel.BackgroundStyle.Class = "DateTimeInputBackground";
            this.integerInputLevel.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.integerInputLevel.Location = new System.Drawing.Point(129, 30);
            this.integerInputLevel.Name = "integerInputLevel";
            this.integerInputLevel.ShowUpDown = true;
            this.integerInputLevel.Size = new System.Drawing.Size(221, 21);
            this.integerInputLevel.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(34, 191);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "Dock:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(34, 239);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 2;
            this.label5.Text = "Fixed:";
            // 
            // ckbFixed
            // 
            this.ckbFixed.AutoSize = true;
            this.ckbFixed.Location = new System.Drawing.Point(129, 238);
            this.ckbFixed.Name = "ckbFixed";
            this.ckbFixed.Size = new System.Drawing.Size(15, 14);
            this.ckbFixed.TabIndex = 7;
            this.ckbFixed.UseVisualStyleBackColor = true;
            // 
            // ckbDockLeft
            // 
            this.ckbDockLeft.AutoSize = true;
            this.ckbDockLeft.Location = new System.Drawing.Point(129, 190);
            this.ckbDockLeft.Name = "ckbDockLeft";
            this.ckbDockLeft.Size = new System.Drawing.Size(48, 16);
            this.ckbDockLeft.TabIndex = 8;
            this.ckbDockLeft.Text = "Left";
            this.ckbDockLeft.UseVisualStyleBackColor = true;
            // 
            // ckbDockRight
            // 
            this.ckbDockRight.AutoSize = true;
            this.ckbDockRight.Location = new System.Drawing.Point(217, 190);
            this.ckbDockRight.Name = "ckbDockRight";
            this.ckbDockRight.Size = new System.Drawing.Size(54, 16);
            this.ckbDockRight.TabIndex = 8;
            this.ckbDockRight.Text = "Right";
            this.ckbDockRight.UseVisualStyleBackColor = true;
            // 
            // ckbDockTop
            // 
            this.ckbDockTop.AutoSize = true;
            this.ckbDockTop.Location = new System.Drawing.Point(308, 190);
            this.ckbDockTop.Name = "ckbDockTop";
            this.ckbDockTop.Size = new System.Drawing.Size(42, 16);
            this.ckbDockTop.TabIndex = 8;
            this.ckbDockTop.Text = "Top";
            this.ckbDockTop.UseVisualStyleBackColor = true;
            // 
            // ckbDockBottom
            // 
            this.ckbDockBottom.AutoSize = true;
            this.ckbDockBottom.Location = new System.Drawing.Point(399, 190);
            this.ckbDockBottom.Name = "ckbDockBottom";
            this.ckbDockBottom.Size = new System.Drawing.Size(60, 16);
            this.ckbDockBottom.TabIndex = 8;
            this.ckbDockBottom.Text = "Bottom";
            this.ckbDockBottom.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(356, 34);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 12);
            this.label6.TabIndex = 9;
            this.label6.Text = "值越大越上层";
            // 
            // GeneralPropertyControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label6);
            this.Controls.Add(this.ckbDockBottom);
            this.Controls.Add(this.ckbDockTop);
            this.Controls.Add(this.ckbDockRight);
            this.Controls.Add(this.ckbDockLeft);
            this.Controls.Add(this.ckbFixed);
            this.Controls.Add(this.integerInputLevel);
            this.Controls.Add(this.cbColor);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbType);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "GeneralPropertyControl";
            this.Size = new System.Drawing.Size(494, 307);
            this.Load += new System.EventHandler(this.GeneralPropertyControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.integerInputLevel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbColor;
        private DevComponents.Editors.IntegerInput integerInputLevel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox ckbFixed;
        private System.Windows.Forms.CheckBox ckbDockLeft;
        private System.Windows.Forms.CheckBox ckbDockRight;
        private System.Windows.Forms.CheckBox ckbDockTop;
        private System.Windows.Forms.CheckBox ckbDockBottom;
        private System.Windows.Forms.Label label6;
    }
}
