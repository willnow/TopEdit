namespace TopoEdit
{
    partial class BlockControl
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
            this.buttonItem1 = new DevComponents.DotNetBar.ButtonItem();
            this.blockPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.contextMenuStripBlock = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.AddBlockToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DelBlockToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EditToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStripBlock.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonItem1
            // 
            this.buttonItem1.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItem1.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.buttonItem1.Name = "buttonItem1";
            this.buttonItem1.Text = "New Button";
            // 
            // blockPanel
            // 
            this.blockPanel.AutoScroll = true;
            this.blockPanel.AutoSize = true;
            this.blockPanel.BackColor = System.Drawing.Color.White;
            this.blockPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.blockPanel.Location = new System.Drawing.Point(0, 0);
            this.blockPanel.Name = "blockPanel";
            this.blockPanel.Size = new System.Drawing.Size(140, 396);
            this.blockPanel.TabIndex = 0;
            this.blockPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.blockPanel_MouseDown);
            // 
            // contextMenuStripBlock
            // 
            this.contextMenuStripBlock.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddBlockToolStripMenuItem,
            this.EditToolStripMenuItem,
            this.DelBlockToolStripMenuItem,
            this.SaveAllToolStripMenuItem});
            this.contextMenuStripBlock.Name = "contextMenuStrip1";
            this.contextMenuStripBlock.Size = new System.Drawing.Size(153, 114);
            // 
            // AddBlockToolStripMenuItem
            // 
            this.AddBlockToolStripMenuItem.Name = "AddBlockToolStripMenuItem";
            this.AddBlockToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.AddBlockToolStripMenuItem.Text = "添加";
            this.AddBlockToolStripMenuItem.Click += new System.EventHandler(this.AddBlockToolStripMenuItem_Click);
            // 
            // DelBlockToolStripMenuItem
            // 
            this.DelBlockToolStripMenuItem.Name = "DelBlockToolStripMenuItem";
            this.DelBlockToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.DelBlockToolStripMenuItem.Text = "删除";
            this.DelBlockToolStripMenuItem.Click += new System.EventHandler(this.DelBlockToolStripMenuItem_Click);
            // 
            // SaveAllToolStripMenuItem
            // 
            this.SaveAllToolStripMenuItem.Name = "SaveAllToolStripMenuItem";
            this.SaveAllToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.SaveAllToolStripMenuItem.Text = "保存全部";
            this.SaveAllToolStripMenuItem.Click += new System.EventHandler(this.SaveAllToolStripMenuItem_Click);
            // 
            // EditToolStripMenuItem
            // 
            this.EditToolStripMenuItem.Name = "EditToolStripMenuItem";
            this.EditToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.EditToolStripMenuItem.Text = "编辑";
            this.EditToolStripMenuItem.Click += new System.EventHandler(this.EditToolStripMenuItem_Click);
            // 
            // BlockControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.blockPanel);
            this.Name = "BlockControl";
            this.Size = new System.Drawing.Size(140, 396);
            this.Load += new System.EventHandler(this.BlockControl_Load);
            this.contextMenuStripBlock.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.ButtonItem buttonItem1;
        private System.Windows.Forms.FlowLayoutPanel blockPanel;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripBlock;
        private System.Windows.Forms.ToolStripMenuItem AddBlockToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DelBlockToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SaveAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem EditToolStripMenuItem;
    }
}
