namespace TopoEdit
{
    partial class BlockEditControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BlockEditControl));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripComboBoxLevel = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonSaveZoomRate = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonRecover = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripComboBoxCustomZoom = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonZoomOut = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonZoomIn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonDisplayGrid = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonOnBound = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabelBackColor = new System.Windows.Forms.ToolStripLabel();
            this.toolStripComboBoxBackColor = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripButtonNotDisplayTransparentColor = new System.Windows.Forms.ToolStripButton();
            this.panelBlock = new TopoEdit.BlockPanel();
            this.toolStripButtonSel = new TopoEdit.Tool.IconTool(this.components);
            this.toolStripButtonCircle = new TopoEdit.Tool.IconTool(this.components);
            this.toolStripButtonPolygon = new TopoEdit.Tool.IconTool(this.components);
            this.toolStripButtonLine = new TopoEdit.Tool.IconTool(this.components);
            this.toolStripButtonRect = new TopoEdit.Tool.IconTool(this.components);
            this.toolStripButtonText = new TopoEdit.Tool.IconTool(this.components);
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonSel,
            this.toolStripButtonCircle,
            this.toolStripButtonPolygon,
            this.toolStripButtonLine,
            this.toolStripButtonRect,
            this.toolStripButtonText,
            this.toolStripSeparator1,
            this.toolStripLabel1,
            this.toolStripComboBoxLevel,
            this.toolStripSeparator2,
            this.toolStripButtonSaveZoomRate,
            this.toolStripButtonRecover,
            this.toolStripLabel2,
            this.toolStripComboBoxCustomZoom,
            this.toolStripSeparator3,
            this.toolStripButtonZoomOut,
            this.toolStripButtonZoomIn,
            this.toolStripSeparator4,
            this.toolStripButtonDisplayGrid,
            this.toolStripButtonOnBound,
            this.toolStripSeparator5,
            this.toolStripLabelBackColor,
            this.toolStripComboBoxBackColor,
            this.toolStripButtonNotDisplayTransparentColor});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1227, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(37, 22);
            this.toolStripLabel1.Text = "level:";
            // 
            // toolStripComboBoxLevel
            // 
            this.toolStripComboBoxLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBoxLevel.Name = "toolStripComboBoxLevel";
            this.toolStripComboBoxLevel.Size = new System.Drawing.Size(121, 25);
            this.toolStripComboBoxLevel.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBoxLevel_SelectedIndexChanged);
            this.toolStripComboBoxLevel.DropDown += new System.EventHandler(this.toolStripComboBoxLevel_DropDown);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
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
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
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
            // panelBlock
            // 
            this.panelBlock.AutoScroll = true;
            this.panelBlock.BackColor = System.Drawing.Color.LightGray;
            this.panelBlock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBlock.Location = new System.Drawing.Point(0, 25);
            this.panelBlock.Name = "panelBlock";
            this.panelBlock.Size = new System.Drawing.Size(1227, 402);
            this.panelBlock.TabIndex = 1;
            this.panelBlock.ZoomRate = 1F;
            this.panelBlock.ChangeBlockEvent += new System.EventHandler<TopoEdit.ChangeBlockEventArgs>(this.panelBlock_ChangeBlockEvent);
            // 
            // toolStripButtonSel
            // 
            this.toolStripButtonSel.Checked = true;
            this.toolStripButtonSel.CheckOnClick = true;
            this.toolStripButtonSel.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolStripButtonSel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonSel.DrawIcon = null;
            this.toolStripButtonSel.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonSel.Image")));
            this.toolStripButtonSel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSel.Name = "toolStripButtonSel";
            this.toolStripButtonSel.Size = new System.Drawing.Size(36, 22);
            this.toolStripButtonSel.Text = "选择";
            this.toolStripButtonSel.Click += new System.EventHandler(this.toolStripButtonSel_Click);
            // 
            // toolStripButtonCircle
            // 
            this.toolStripButtonCircle.CheckOnClick = true;
            this.toolStripButtonCircle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonCircle.DrawIcon = null;
            this.toolStripButtonCircle.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonCircle.Image")));
            this.toolStripButtonCircle.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonCircle.Name = "toolStripButtonCircle";
            this.toolStripButtonCircle.Size = new System.Drawing.Size(24, 22);
            this.toolStripButtonCircle.Text = "圆";
            // 
            // toolStripButtonPolygon
            // 
            this.toolStripButtonPolygon.CheckOnClick = true;
            this.toolStripButtonPolygon.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonPolygon.DrawIcon = null;
            this.toolStripButtonPolygon.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonPolygon.Image")));
            this.toolStripButtonPolygon.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonPolygon.Name = "toolStripButtonPolygon";
            this.toolStripButtonPolygon.Size = new System.Drawing.Size(48, 22);
            this.toolStripButtonPolygon.Text = "多边形";
            // 
            // toolStripButtonLine
            // 
            this.toolStripButtonLine.CheckOnClick = true;
            this.toolStripButtonLine.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonLine.DrawIcon = null;
            this.toolStripButtonLine.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonLine.Image")));
            this.toolStripButtonLine.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonLine.Name = "toolStripButtonLine";
            this.toolStripButtonLine.Size = new System.Drawing.Size(36, 22);
            this.toolStripButtonLine.Text = "线条";
            // 
            // toolStripButtonRect
            // 
            this.toolStripButtonRect.CheckOnClick = true;
            this.toolStripButtonRect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonRect.DrawIcon = null;
            this.toolStripButtonRect.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonRect.Image")));
            this.toolStripButtonRect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRect.Name = "toolStripButtonRect";
            this.toolStripButtonRect.Size = new System.Drawing.Size(36, 22);
            this.toolStripButtonRect.Text = "矩形";
            // 
            // toolStripButtonText
            // 
            this.toolStripButtonText.CheckOnClick = true;
            this.toolStripButtonText.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonText.DrawIcon = null;
            this.toolStripButtonText.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonText.Image")));
            this.toolStripButtonText.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonText.Name = "toolStripButtonText";
            this.toolStripButtonText.Size = new System.Drawing.Size(36, 22);
            this.toolStripButtonText.Text = "文字";
            // 
            // BlockEditControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelBlock);
            this.Controls.Add(this.toolStrip1);
            this.Name = "BlockEditControl";
            this.Size = new System.Drawing.Size(1227, 427);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private TopoEdit.Tool.IconTool toolStripButtonSel;
        private TopoEdit.Tool.IconTool toolStripButtonCircle;
        private TopoEdit.Tool.IconTool toolStripButtonPolygon;
        private TopoEdit.Tool.IconTool toolStripButtonLine;
        private TopoEdit.Tool.IconTool toolStripButtonRect;
        private TopoEdit.Tool.IconTool toolStripButtonText;
        private BlockPanel panelBlock;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxLevel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolStripButtonSaveZoomRate;
        private System.Windows.Forms.ToolStripButton toolStripButtonRecover;
        private System.Windows.Forms.ToolStripButton toolStripButtonZoomOut;
        private System.Windows.Forms.ToolStripButton toolStripButtonZoomIn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton toolStripButtonDisplayGrid;
        private System.Windows.Forms.ToolStripButton toolStripButtonOnBound;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxCustomZoom;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripLabel toolStripLabelBackColor;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxBackColor;
        private System.Windows.Forms.ToolStripButton toolStripButtonNotDisplayTransparentColor;
    }
}
