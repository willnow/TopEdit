namespace TopoEdit
{
    partial class PageEditControl
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PageEditControl));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonSaveZoomRate = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonRecover = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonZoomOut = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonZoomIn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonDisplayGrid = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonOnBound = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabelBackColor = new System.Windows.Forms.ToolStripLabel();
            this.toolStripComboBoxBackColor = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripButtonNotDisplayTransparentColor = new System.Windows.Forms.ToolStripButton();
            this.panelPage = new TopoEdit.PagePanel(this.components);
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonSaveZoomRate,
            this.toolStripButtonRecover,
            this.toolStripSeparator3,
            this.toolStripButtonZoomOut,
            this.toolStripButtonZoomIn,
            this.toolStripSeparator4,
            this.toolStripButtonDisplayGrid,
            this.toolStripButtonOnBound,
            this.toolStripSeparator1,
            this.toolStripLabelBackColor,
            this.toolStripComboBoxBackColor,
            this.toolStripButtonNotDisplayTransparentColor});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(942, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonSaveZoomRate
            // 
            this.toolStripButtonSaveZoomRate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonSaveZoomRate.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonSaveZoomRate.Image")));
            this.toolStripButtonSaveZoomRate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSaveZoomRate.Name = "toolStripButtonSaveZoomRate";
            this.toolStripButtonSaveZoomRate.Size = new System.Drawing.Size(96, 22);
            this.toolStripButtonSaveZoomRate.Text = "保存为默认大小";
            this.toolStripButtonSaveZoomRate.Visible = false;
            this.toolStripButtonSaveZoomRate.Click += new System.EventHandler(this.toolStripButtonSaveZoomRate_Click);
            // 
            // toolStripButtonRecover
            // 
            this.toolStripButtonRecover.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonRecover.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonRecover.Image")));
            this.toolStripButtonRecover.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRecover.Name = "toolStripButtonRecover";
            this.toolStripButtonRecover.Size = new System.Drawing.Size(96, 22);
            this.toolStripButtonRecover.Text = "恢复为默认大小";
            this.toolStripButtonRecover.Click += new System.EventHandler(this.toolStripButtonRecover_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonZoomOut
            // 
            this.toolStripButtonZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonZoomOut.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonZoomOut.Image")));
            this.toolStripButtonZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonZoomOut.Name = "toolStripButtonZoomOut";
            this.toolStripButtonZoomOut.Size = new System.Drawing.Size(36, 22);
            this.toolStripButtonZoomOut.Text = "放大";
            this.toolStripButtonZoomOut.Click += new System.EventHandler(this.toolStripButtonZoomOut_Click);
            // 
            // toolStripButtonZoomIn
            // 
            this.toolStripButtonZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonZoomIn.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonZoomIn.Image")));
            this.toolStripButtonZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonZoomIn.Name = "toolStripButtonZoomIn";
            this.toolStripButtonZoomIn.Size = new System.Drawing.Size(36, 22);
            this.toolStripButtonZoomIn.Text = "缩小";
            this.toolStripButtonZoomIn.Click += new System.EventHandler(this.toolStripButtonZoomIn_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonDisplayGrid
            // 
            this.toolStripButtonDisplayGrid.CheckOnClick = true;
            this.toolStripButtonDisplayGrid.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonDisplayGrid.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonDisplayGrid.Image")));
            this.toolStripButtonDisplayGrid.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonDisplayGrid.Name = "toolStripButtonDisplayGrid";
            this.toolStripButtonDisplayGrid.Size = new System.Drawing.Size(36, 22);
            this.toolStripButtonDisplayGrid.Text = "网格";
            this.toolStripButtonDisplayGrid.Click += new System.EventHandler(this.toolStripButtonDisplayGrid_Click);
            // 
            // toolStripButtonOnBound
            // 
            this.toolStripButtonOnBound.CheckOnClick = true;
            this.toolStripButtonOnBound.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonOnBound.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonOnBound.Image")));
            this.toolStripButtonOnBound.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonOnBound.Name = "toolStripButtonOnBound";
            this.toolStripButtonOnBound.Size = new System.Drawing.Size(72, 22);
            this.toolStripButtonOnBound.Text = "边界上选中";
            this.toolStripButtonOnBound.Click += new System.EventHandler(this.toolStripButtonOnBound_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabelBackColor
            // 
            this.toolStripLabelBackColor.Name = "toolStripLabelBackColor";
            this.toolStripLabelBackColor.Size = new System.Drawing.Size(44, 22);
            this.toolStripLabelBackColor.Text = "背景色";
            // 
            // toolStripComboBoxBackColor
            // 
            this.toolStripComboBoxBackColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBoxBackColor.Name = "toolStripComboBoxBackColor";
            this.toolStripComboBoxBackColor.Size = new System.Drawing.Size(121, 25);
            this.toolStripComboBoxBackColor.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBoxBackColor_SelectedIndexChanged);
            // 
            // toolStripButtonNotDisplayTransparentColor
            // 
            this.toolStripButtonNotDisplayTransparentColor.CheckOnClick = true;
            this.toolStripButtonNotDisplayTransparentColor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonNotDisplayTransparentColor.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonNotDisplayTransparentColor.Image")));
            this.toolStripButtonNotDisplayTransparentColor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonNotDisplayTransparentColor.Name = "toolStripButtonNotDisplayTransparentColor";
            this.toolStripButtonNotDisplayTransparentColor.Size = new System.Drawing.Size(84, 22);
            this.toolStripButtonNotDisplayTransparentColor.Text = "不显示透明色";
            this.toolStripButtonNotDisplayTransparentColor.Click += new System.EventHandler(this.toolStripButtonNotDisplayTransparentColor_Click);
            // 
            // panelPage
            // 
            this.panelPage.AllowDrop = true;
            this.panelPage.AutoScroll = true;
            this.panelPage.AutoScrollMinSize = new System.Drawing.Size(500, 500);
            this.panelPage.BackColor = System.Drawing.Color.LightGray;
            this.panelPage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPage.Location = new System.Drawing.Point(0, 25);
            this.panelPage.Name = "panelPage";
            this.panelPage.Size = new System.Drawing.Size(942, 392);
            this.panelPage.TabIndex = 1;
            this.panelPage.ZoomRate = 1F;
            this.panelPage.ChangePageEvent += new System.EventHandler<TopoEdit.ChangePageEventArgs>(this.panelPage_ChangePageEvent);
            // 
            // PageEditControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelPage);
            this.Controls.Add(this.toolStrip1);
            this.Name = "PageEditControl";
            this.Size = new System.Drawing.Size(942, 417);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private PagePanel panelPage;
        private System.Windows.Forms.ToolStripButton toolStripButtonSaveZoomRate;
        private System.Windows.Forms.ToolStripButton toolStripButtonRecover;
        private System.Windows.Forms.ToolStripButton toolStripButtonZoomOut;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton toolStripButtonZoomIn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton toolStripButtonDisplayGrid;
        private System.Windows.Forms.ToolStripButton toolStripButtonOnBound;
        private System.Windows.Forms.ToolStripLabel toolStripLabelBackColor;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxBackColor;
        private System.Windows.Forms.ToolStripButton toolStripButtonNotDisplayTransparentColor;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;

    }
}
