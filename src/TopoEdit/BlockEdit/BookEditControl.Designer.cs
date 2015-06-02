namespace TopoEdit
{
    partial class BookEditControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BookEditControl));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonSaveZoomRate = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonRecover = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripComboBoxCustomZoom = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripButtonZoomOut = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonZoomIn = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonZoomSelOnly = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonDisplayGrid = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonNotDisplayTransparentColor = new System.Windows.Forms.ToolStripButton();
            this.panelBook = new TopoEdit.BookPanel(this.components);
            this.toolStripButtonResolutionRatio = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonSaveZoomRate,
            this.toolStripButtonRecover,
            this.toolStripLabel2,
            this.toolStripComboBoxCustomZoom,
            this.toolStripButtonZoomOut,
            this.toolStripButtonZoomIn,
            this.toolStripButtonZoomSelOnly,
            this.toolStripSeparator4,
            this.toolStripButtonDisplayGrid,
            this.toolStripButtonNotDisplayTransparentColor,
            this.toolStripButtonResolutionRatio});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(850, 25);
            this.toolStrip1.TabIndex = 1;
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
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(44, 22);
            this.toolStripLabel2.Text = "缩放比";
            // 
            // toolStripComboBoxCustomZoom
            // 
            this.toolStripComboBoxCustomZoom.Name = "toolStripComboBoxCustomZoom";
            this.toolStripComboBoxCustomZoom.Size = new System.Drawing.Size(121, 25);
            this.toolStripComboBoxCustomZoom.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBoxCustomZoom_SelectedIndexChanged);
            this.toolStripComboBoxCustomZoom.KeyDown += new System.Windows.Forms.KeyEventHandler(this.toolStripComboBoxCustomZoom_KeyDown);
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
            // toolStripButtonZoomSelOnly
            // 
            this.toolStripButtonZoomSelOnly.CheckOnClick = true;
            this.toolStripButtonZoomSelOnly.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonZoomSelOnly.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonZoomSelOnly.Image")));
            this.toolStripButtonZoomSelOnly.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonZoomSelOnly.Name = "toolStripButtonZoomSelOnly";
            this.toolStripButtonZoomSelOnly.Size = new System.Drawing.Size(96, 22);
            this.toolStripButtonZoomSelOnly.Text = "仅缩放选中元素";
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
            // panelBook
            // 
            this.panelBook.AllowDrop = true;
            this.panelBook.AutoScroll = true;
            this.panelBook.AutoScrollMinSize = new System.Drawing.Size(500, 500);
            this.panelBook.BackColor = System.Drawing.Color.LightGray;
            this.panelBook.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBook.Location = new System.Drawing.Point(0, 25);
            this.panelBook.Name = "panelBook";
            this.panelBook.Size = new System.Drawing.Size(850, 406);
            this.panelBook.TabIndex = 0;
            this.panelBook.ZoomRate = 1F;
            // 
            // toolStripButtonResolutionRatio
            // 
            this.toolStripButtonResolutionRatio.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonResolutionRatio.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonResolutionRatio.Image")));
            this.toolStripButtonResolutionRatio.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonResolutionRatio.Name = "toolStripButtonResolutionRatio";
            this.toolStripButtonResolutionRatio.Size = new System.Drawing.Size(48, 22);
            this.toolStripButtonResolutionRatio.Text = "分辨率";
            this.toolStripButtonResolutionRatio.Click += new System.EventHandler(this.toolStripButtonResolutionRatio_Click);
            // 
            // BookEditControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelBook);
            this.Controls.Add(this.toolStrip1);
            this.Name = "BookEditControl";
            this.Size = new System.Drawing.Size(850, 431);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private BookPanel panelBook;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButtonZoomOut;
        private System.Windows.Forms.ToolStripButton toolStripButtonZoomIn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton toolStripButtonDisplayGrid;
        private System.Windows.Forms.ToolStripButton toolStripButtonRecover;
        private System.Windows.Forms.ToolStripButton toolStripButtonZoomSelOnly;
        private System.Windows.Forms.ToolStripButton toolStripButtonSaveZoomRate;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxCustomZoom;
        private System.Windows.Forms.ToolStripButton toolStripButtonNotDisplayTransparentColor;
        private System.Windows.Forms.ToolStripButton toolStripButtonResolutionRatio;
    }
}
