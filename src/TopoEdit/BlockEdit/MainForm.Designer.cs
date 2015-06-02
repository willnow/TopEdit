using TopoEdit.Tool;
namespace TopoEdit
{
    partial class MainForm
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EditToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GoBackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GoBeforeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.视图ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonAlignLeft = new TopoEdit.Tool.AlignButton(this.components);
            this.toolStripButtonAlignCenter = new TopoEdit.Tool.AlignButton(this.components);
            this.toolStripButtonAlignRight = new TopoEdit.Tool.AlignButton(this.components);
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonAlignTop = new TopoEdit.Tool.AlignButton(this.components);
            this.toolStripButtonAlignMiddle = new TopoEdit.Tool.AlignButton(this.components);
            this.toolStripButtonAlignBottom = new TopoEdit.Tool.AlignButton(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.blockPreviewControl = new TopoEdit.BlockPreviewControl();
            this.blockControl = new TopoEdit.BlockControl();
            this.pageControl = new TopoEdit.PageControl();
            this.tabControl = new DevComponents.DotNetBar.TabControl();
            this.tabControlPanel1 = new DevComponents.DotNetBar.TabControlPanel();
            this.blockEditControl = new TopoEdit.BlockEditControl();
            this.tabItemBlock = new DevComponents.DotNetBar.TabItem(this.components);
            this.tabControlPanel2 = new DevComponents.DotNetBar.TabControlPanel();
            this.bookEditControl = new TopoEdit.BookEditControl();
            this.panelBook = new TopoEdit.BookPanel(this.components);
            this.tabItemBook = new DevComponents.DotNetBar.TabItem(this.components);
            this.mainMenuStrip.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabControl)).BeginInit();
            this.tabControl.SuspendLayout();
            this.tabControlPanel1.SuspendLayout();
            this.tabControlPanel2.SuspendLayout();
            this.bookEditControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件ToolStripMenuItem,
            this.EditToolStripMenuItem,
            this.视图ToolStripMenuItem});
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Size = new System.Drawing.Size(900, 25);
            this.mainMenuStrip.TabIndex = 1;
            this.mainMenuStrip.Text = "menuStrip1";
            // 
            // 文件ToolStripMenuItem
            // 
            this.文件ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openFileToolStripMenuItem});
            this.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            this.文件ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.文件ToolStripMenuItem.Text = "文件";
            // 
            // openFileToolStripMenuItem
            // 
            this.openFileToolStripMenuItem.Name = "openFileToolStripMenuItem";
            this.openFileToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.openFileToolStripMenuItem.Text = "打开";
            // 
            // EditToolStripMenuItem
            // 
            this.EditToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.GoBackToolStripMenuItem,
            this.GoBeforeToolStripMenuItem});
            this.EditToolStripMenuItem.Name = "EditToolStripMenuItem";
            this.EditToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.EditToolStripMenuItem.Text = "编辑";
            this.EditToolStripMenuItem.DropDownOpening += new System.EventHandler(this.EditToolStripMenuItem_DropDownOpening);
            // 
            // GoBackToolStripMenuItem
            // 
            this.GoBackToolStripMenuItem.Name = "GoBackToolStripMenuItem";
            this.GoBackToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.GoBackToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.GoBackToolStripMenuItem.Text = "撤销(U)";
            this.GoBackToolStripMenuItem.Click += new System.EventHandler(this.GoBackToolStripMenuItem_Click);
            // 
            // GoBeforeToolStripMenuItem
            // 
            this.GoBeforeToolStripMenuItem.Name = "GoBeforeToolStripMenuItem";
            this.GoBeforeToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.GoBeforeToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.GoBeforeToolStripMenuItem.Text = "恢复(R)";
            this.GoBeforeToolStripMenuItem.Click += new System.EventHandler(this.GoBeforeToolStripMenuItem_Click);
            // 
            // 视图ToolStripMenuItem
            // 
            this.视图ToolStripMenuItem.Name = "视图ToolStripMenuItem";
            this.视图ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.视图ToolStripMenuItem.Text = "视图";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 530);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(900, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(180, 17);
            this.toolStripStatusLabel.Text = "X=100, Y=200, Zoom = 100%";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonAlignLeft,
            this.toolStripButtonAlignCenter,
            this.toolStripButtonAlignRight,
            this.toolStripSeparator2,
            this.toolStripButtonAlignTop,
            this.toolStripButtonAlignMiddle,
            this.toolStripButtonAlignBottom});
            this.toolStrip1.Location = new System.Drawing.Point(0, 25);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(900, 25);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonAlignLeft
            // 
            this.toolStripButtonAlignLeft.AlignType = TopoEdit.Stratege.EmAlign.Left;
            this.toolStripButtonAlignLeft.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonAlignLeft.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonAlignLeft.Image")));
            this.toolStripButtonAlignLeft.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAlignLeft.Name = "toolStripButtonAlignLeft";
            this.toolStripButtonAlignLeft.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonAlignLeft.Text = "左对齐";
            this.toolStripButtonAlignLeft.Click += new System.EventHandler(this.toolStripButtonAlign_Click);
            // 
            // toolStripButtonAlignCenter
            // 
            this.toolStripButtonAlignCenter.AlignType = TopoEdit.Stratege.EmAlign.Left;
            this.toolStripButtonAlignCenter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonAlignCenter.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonAlignCenter.Image")));
            this.toolStripButtonAlignCenter.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAlignCenter.Name = "toolStripButtonAlignCenter";
            this.toolStripButtonAlignCenter.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonAlignCenter.Text = "居中对齐";
            this.toolStripButtonAlignCenter.Click += new System.EventHandler(this.toolStripButtonAlign_Click);
            // 
            // toolStripButtonAlignRight
            // 
            this.toolStripButtonAlignRight.AlignType = TopoEdit.Stratege.EmAlign.Left;
            this.toolStripButtonAlignRight.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonAlignRight.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonAlignRight.Image")));
            this.toolStripButtonAlignRight.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAlignRight.Name = "toolStripButtonAlignRight";
            this.toolStripButtonAlignRight.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonAlignRight.Text = "右对齐";
            this.toolStripButtonAlignRight.Click += new System.EventHandler(this.toolStripButtonAlign_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonAlignTop
            // 
            this.toolStripButtonAlignTop.AlignType = TopoEdit.Stratege.EmAlign.Left;
            this.toolStripButtonAlignTop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonAlignTop.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonAlignTop.Image")));
            this.toolStripButtonAlignTop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAlignTop.Name = "toolStripButtonAlignTop";
            this.toolStripButtonAlignTop.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonAlignTop.Text = "顶对齐";
            this.toolStripButtonAlignTop.Click += new System.EventHandler(this.toolStripButtonAlign_Click);
            // 
            // toolStripButtonAlignMiddle
            // 
            this.toolStripButtonAlignMiddle.AlignType = TopoEdit.Stratege.EmAlign.Left;
            this.toolStripButtonAlignMiddle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonAlignMiddle.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonAlignMiddle.Image")));
            this.toolStripButtonAlignMiddle.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAlignMiddle.Name = "toolStripButtonAlignMiddle";
            this.toolStripButtonAlignMiddle.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonAlignMiddle.Text = "中间对齐";
            this.toolStripButtonAlignMiddle.Click += new System.EventHandler(this.toolStripButtonAlign_Click);
            // 
            // toolStripButtonAlignBottom
            // 
            this.toolStripButtonAlignBottom.AlignType = TopoEdit.Stratege.EmAlign.Left;
            this.toolStripButtonAlignBottom.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonAlignBottom.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonAlignBottom.Image")));
            this.toolStripButtonAlignBottom.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAlignBottom.Name = "toolStripButtonAlignBottom";
            this.toolStripButtonAlignBottom.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonAlignBottom.Text = "底对齐";
            this.toolStripButtonAlignBottom.Click += new System.EventHandler(this.toolStripButtonAlign_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.splitContainer1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 50);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(900, 480);
            this.panel1.TabIndex = 4;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl);
            this.splitContainer1.Size = new System.Drawing.Size(900, 480);
            this.splitContainer1.SplitterDistance = 125;
            this.splitContainer1.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.OutsetDouble;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.blockPreviewControl, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.blockControl, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.pageControl, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 65.28497F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 34.71503F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 112F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(125, 480);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // blockPreviewControl
            // 
            this.blockPreviewControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.blockPreviewControl.Location = new System.Drawing.Point(6, 367);
            this.blockPreviewControl.Name = "blockPreviewControl";
            this.blockPreviewControl.Size = new System.Drawing.Size(113, 107);
            this.blockPreviewControl.TabIndex = 2;
            // 
            // blockControl
            // 
            this.blockControl.AutoScroll = true;
            this.blockControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.blockControl.Location = new System.Drawing.Point(6, 6);
            this.blockControl.Name = "blockControl";
            this.blockControl.Size = new System.Drawing.Size(113, 226);
            this.blockControl.TabIndex = 0;
            this.blockControl.SelBlockEvent += new System.EventHandler<TopoEdit.BlockEventArgs>(this.blockControl_SelBlockEvent);
            // 
            // pageControl
            // 
            this.pageControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pageControl.Location = new System.Drawing.Point(6, 241);
            this.pageControl.Name = "pageControl";
            this.pageControl.Size = new System.Drawing.Size(113, 117);
            this.pageControl.TabIndex = 3;
            this.pageControl.SelPageEvent += new System.EventHandler<TopoEdit.PageEventArgs>(this.pageControl_SelPageEvent);
            this.pageControl.AddPageEvent += new System.EventHandler<TopoEdit.PageEventArgs>(this.pageControl_AddPageEvent);
            this.pageControl.DelPageEvent += new System.EventHandler<TopoEdit.PageEventArgs>(this.pageControl_DelPageEvent);
            // 
            // tabControl
            // 
            this.tabControl.AllowDrop = true;
            this.tabControl.CanReorderTabs = true;
            this.tabControl.CloseButtonPosition = DevComponents.DotNetBar.eTabCloseButtonPosition.Right;
            this.tabControl.Controls.Add(this.tabControlPanel1);
            this.tabControl.Controls.Add(this.tabControlPanel2);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedTabFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.tabControl.SelectedTabIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(771, 480);
            this.tabControl.Style = DevComponents.DotNetBar.eTabStripStyle.VS2005Document;
            this.tabControl.TabAlignment = DevComponents.DotNetBar.eTabStripAlignment.Bottom;
            this.tabControl.TabIndex = 2;
            this.tabControl.TabLayoutType = DevComponents.DotNetBar.eTabLayoutType.MultilineWithNavigationBox;
            this.tabControl.Tabs.Add(this.tabItemBlock);
            this.tabControl.Tabs.Add(this.tabItemBook);
            this.tabControl.Text = "tabControl1";
            this.tabControl.SelectedTabChanged += new DevComponents.DotNetBar.TabStrip.SelectedTabChangedEventHandler(this.tabControl_SelectedTabChanged);
            // 
            // tabControlPanel1
            // 
            this.tabControlPanel1.Controls.Add(this.blockEditControl);
            this.tabControlPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPanel1.Location = new System.Drawing.Point(0, 0);
            this.tabControlPanel1.Name = "tabControlPanel1";
            this.tabControlPanel1.Padding = new System.Windows.Forms.Padding(1);
            this.tabControlPanel1.Size = new System.Drawing.Size(771, 454);
            this.tabControlPanel1.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(250)))), ((int)(((byte)(247)))));
            this.tabControlPanel1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.tabControlPanel1.Style.BorderColor.Color = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(157)))), ((int)(((byte)(185)))));
            this.tabControlPanel1.Style.BorderSide = ((DevComponents.DotNetBar.eBorderSide)(((DevComponents.DotNetBar.eBorderSide.Left | DevComponents.DotNetBar.eBorderSide.Right)
                        | DevComponents.DotNetBar.eBorderSide.Top)));
            this.tabControlPanel1.Style.GradientAngle = -90;
            this.tabControlPanel1.TabIndex = 1;
            this.tabControlPanel1.TabItem = this.tabItemBlock;
            // 
            // blockEditControl
            // 
            this.blockEditControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.blockEditControl.Location = new System.Drawing.Point(1, 1);
            this.blockEditControl.Name = "blockEditControl";
            this.blockEditControl.Size = new System.Drawing.Size(769, 452);
            this.blockEditControl.TabIndex = 0;
            this.blockEditControl.ChangeBlockEvent += new System.EventHandler<TopoEdit.ChangeBlockEventArgs>(this.blockEditControl_ChangeBlockEvent);
            // 
            // tabItemBlock
            // 
            this.tabItemBlock.AttachedControl = this.tabControlPanel1;
            this.tabItemBlock.Name = "tabItemBlock";
            this.tabItemBlock.Text = "Block View";
            // 
            // tabControlPanel2
            // 
            this.tabControlPanel2.Controls.Add(this.bookEditControl);
            this.tabControlPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPanel2.Location = new System.Drawing.Point(0, 0);
            this.tabControlPanel2.Name = "tabControlPanel2";
            this.tabControlPanel2.Padding = new System.Windows.Forms.Padding(1);
            this.tabControlPanel2.Size = new System.Drawing.Size(771, 454);
            this.tabControlPanel2.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(250)))), ((int)(((byte)(247)))));
            this.tabControlPanel2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.tabControlPanel2.Style.BorderColor.Color = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(157)))), ((int)(((byte)(185)))));
            this.tabControlPanel2.Style.BorderSide = ((DevComponents.DotNetBar.eBorderSide)(((DevComponents.DotNetBar.eBorderSide.Left | DevComponents.DotNetBar.eBorderSide.Right)
                        | DevComponents.DotNetBar.eBorderSide.Top)));
            this.tabControlPanel2.Style.GradientAngle = -90;
            this.tabControlPanel2.TabIndex = 2;
            this.tabControlPanel2.TabItem = this.tabItemBook;
            // 
            // bookEditControl
            // 
            this.bookEditControl.Controls.Add(this.panelBook);
            this.bookEditControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bookEditControl.Location = new System.Drawing.Point(1, 1);
            this.bookEditControl.Name = "bookEditControl";
            this.bookEditControl.Size = new System.Drawing.Size(769, 452);
            this.bookEditControl.TabIndex = 0;
            // 
            // panelBook
            // 
            this.panelBook.AllowDrop = true;
            this.panelBook.AutoScroll = true;
            this.panelBook.AutoScrollMinSize = new System.Drawing.Size(500, 500);
            this.panelBook.BackColor = System.Drawing.Color.LightGray;
            this.panelBook.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBook.Location = new System.Drawing.Point(0, 0);
            this.panelBook.Name = "panelBook";
            this.panelBook.Size = new System.Drawing.Size(769, 452);
            this.panelBook.TabIndex = 0;
            this.panelBook.ZoomRate = 1F;
            // 
            // tabItemBook
            // 
            this.tabItemBook.AttachedControl = this.tabControlPanel2;
            this.tabItemBook.Name = "tabItemBook";
            this.tabItemBook.Text = "Book View";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 552);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.mainMenuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Topo编辑器";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabControl)).EndInit();
            this.tabControl.ResumeLayout(false);
            this.tabControlPanel1.ResumeLayout(false);
            this.tabControlPanel2.ResumeLayout(false);
            this.bookEditControl.ResumeLayout(false);
            this.bookEditControl.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem 文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem EditToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 视图ToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.ToolStripMenuItem openFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem GoBackToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem GoBeforeToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private AlignButton toolStripButtonAlignLeft;
        private AlignButton toolStripButtonAlignCenter;
        private AlignButton toolStripButtonAlignRight;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private AlignButton toolStripButtonAlignTop;
        private AlignButton toolStripButtonAlignMiddle;
        private AlignButton toolStripButtonAlignBottom;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private BlockPreviewControl blockPreviewControl;
        private BlockControl blockControl;
        private PageControl pageControl;
        private DevComponents.DotNetBar.TabControl tabControl;
        private DevComponents.DotNetBar.TabControlPanel tabControlPanel1;
        private BlockEditControl blockEditControl;
        private DevComponents.DotNetBar.TabItem tabItemBlock;
        private DevComponents.DotNetBar.TabControlPanel tabControlPanel2;
        private BookEditControl bookEditControl;
        private BookPanel panelBook;
        private DevComponents.DotNetBar.TabItem tabItemBook;


    }
}

