using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using TopoEdit.Model;
using System.Xml;
using System.Diagnostics;
using TopoEdit.Icon;
using BlockEdit.Model;
using System.IO;
using TopoEdit.Command;
using TopoEdit.Stratege;
using TopoEdit.Tool;

namespace TopoEdit
{
    public partial class MainForm : Form, IHelpHandle
    {
        /// <summary>
        /// Page容器
        /// </summary>
        private List<PageEditControl> m_pageControlList = new List<PageEditControl>();
        /// <summary>
        /// 帮助提示辅助类
        /// </summary>
        private HelpHandle m_cHelpHandle = new HelpHandle();
        /// <summary>
        /// 对齐处理辅助类
        /// </summary>
        private AlignHelper m_alignHelper = new AlignHelper();

        /// <summary>
        /// Book视图控件
        /// </summary>
        public TopoEdit.BookEditControl BookEditControl
        {
            get { return bookEditControl; }
        }

        public MainForm()
        {
            InitializeComponent();

            blockEditControl.SetSuccessor(this);
            bookEditControl.SetSuccessor(this);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            tabItemBlock.ItemData = blockEditControl.View;
            tabItemBook.ItemData = bookEditControl.View;

            InitBlocks(Application.StartupPath + "\\" + Properties.Settings.Default.DataPath + "\\block.xml");
            InitPages(Application.StartupPath + "\\" + Properties.Settings.Default.DataPath + "\\Page");
            InitBook(Application.StartupPath + "\\" + Properties.Settings.Default.DataPath + "\\book.xml");
            InitAlignButton();

            if (BlockContainer.Instance.Count > 0)
            {
                blockEditControl.DrawBlock(BlockContainer.Instance[0]);
                blockPreviewControl.DrawBlock(BlockContainer.Instance[0]);
            }
           

            ComponentManager.Instance.LoadAll("component");//安装插件
        }

        private void InitAlignButton()
        {
            toolStripButtonAlignLeft.AlignType = EmAlign.Left;
            toolStripButtonAlignRight.AlignType = EmAlign.Right;
            toolStripButtonAlignTop.AlignType = EmAlign.Top;
            toolStripButtonAlignBottom.AlignType = EmAlign.Buttom;
            toolStripButtonAlignCenter.AlignType = EmAlign.Center;
            toolStripButtonAlignMiddle.AlignType = EmAlign.Middle;
        }

        private void blockControl_SelBlockEvent(object sender, BlockEventArgs e)
        {
            blockEditControl.DrawBlock(e.BlockItem);
            blockPreviewControl.DrawBlock(e.BlockItem);
            tabControl.SelectedTabIndex = 0;
        }

        public void InitBlocks(string fileName)
        {
            try
            {
                //打开并验证XML文件
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(fileName);
                xmlDoc.Schemas.Add(null, Application.StartupPath + "\\" + Properties.Settings.Default.DataPath + "\\block.xsd");
                xmlDoc.Validate(null);

                //初始化颜色库
                ColorLib.Instance.Load(xmlDoc.DocumentElement.SelectSingleNode("GeneralInfo/ColorLib"));
                //获取菜单信息
                MenuLib.Instance.Load(xmlDoc.DocumentElement.SelectSingleNode("Menus"));
                //从根节点中读取所有block
                blockControl.LoadBlock(xmlDoc.DocumentElement.SelectSingleNode("Blocks"));
            }
            catch (System.IO.FileNotFoundException ex)
            {
                MessageBox.Show("读取" + fileName + "时发生错误，错误原因：" + ex.Message);
            }
            catch (System.Xml.Schema.XmlSchemaValidationException ex)
            {
                MessageBox.Show("文件" + fileName + "格式不规范，原因" + ex.Message);
                return;
            }
            catch (System.Xml.XmlException ex)
            {
                MessageBox.Show("读取" + fileName + "时发生错误，错误原因：" + ex.Message);
            }
        }

        public void InitPages(string folderName)
        {
            //该目录下的所有XML文件为Page的XML文件
            DirectoryInfo parentdi = new DirectoryInfo(Application.StartupPath + "\\" + Properties.Settings.Default.DataPath + "\\Page");
            
            foreach (FileInfo di in parentdi.GetFiles())
            {
                if (di.Extension == ".xml")
                {
                    InitPage(di.FullName);
                    
                }
            }

            //将PAGE按照ID号排序，依次加入TabControl中
            PageContainer.Instance.Sort(ComparisonPage);

            int topoTabIndexStart = 3;
            foreach (Page page in PageContainer.Instance)
            {
                InsertPageToControl(topoTabIndexStart, page);
                ++topoTabIndexStart;
            }

            //初始化页控件
            pageControl.Init();
        }

        /// <summary>
        /// Page按照ID号从小到大排序
        /// </summary>
        /// <param name="x">Page x</param>
        /// <param name="y">Page y</param>
        /// <returns></returns>
        int ComparisonPage(Page x, Page y)
        {
            TopoEdit.Icon.Page pageX = x;
            TopoEdit.Icon.Page pageY = y;

            if (pageX.Id < pageY.Id)
            {
                return -1;
            }
            else if (pageX.Id == pageY.Id)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }

        public void InitPage(string fileName)
        {
            try
            {
                //打开并验证XML文件
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(fileName);
                xmlDoc.Schemas.Add(null, Application.StartupPath + "\\" + Properties.Settings.Default.DataPath + "\\Page\\page.xsd");
                xmlDoc.Validate(null);

                Page page = new Page(Path.GetFileNameWithoutExtension(fileName), 1);
                page.Load(xmlDoc.DocumentElement);
                PageContainer.Instance.Add(page);
            }
            catch (System.IO.FileNotFoundException ex)
            {
                MessageBox.Show("读取" + fileName + "时发生错误，错误原因：" + ex.Message);
            }
            catch (System.Xml.Schema.XmlSchemaValidationException ex)
            {
                MessageBox.Show("文件" + fileName + "格式不规范，原因" + ex.Message);
                return;
            }
            catch (System.Xml.XmlException ex)
            {
                MessageBox.Show("读取" + fileName + "时发生错误，错误原因：" + ex.Message);
            }
        }

        void InsertPageToControl(int topoTabIndexStart, Page page)
        {
            this.tabControl.SuspendLayout();

            DevComponents.DotNetBar.TabControlPanel pagePanel = new DevComponents.DotNetBar.TabControlPanel();
            PageEditControl pageControl = new PageEditControl();
            pageControl.ChangePageEvent += new EventHandler<ChangePageEventArgs>(pageControl_ChangePageEvent);
            DevComponents.DotNetBar.TabItem tabItem = new DevComponents.DotNetBar.TabItem();

            pagePanel.SuspendLayout();

            //初始化Panel
            this.tabControl.Controls.Add(pagePanel);
            pagePanel.Controls.Add(pageControl);
            pagePanel.Dock = System.Windows.Forms.DockStyle.Fill;

            pagePanel.TabIndex = topoTabIndexStart;
            pagePanel.TabItem = tabItem;
            tabItem.AttachedControl = pagePanel;

            //初始化TOPOCONTROL
            pageControl.LoadPage(page);
            pageControl.Dock = DockStyle.Fill;

            //初始化TABITEM
            this.tabControl.Tabs.Add(tabItem);
            tabItem.Text = "Page" + page.Id + " - " + page.Name;
            tabItem.ItemData = pageControl.View;

            pagePanel.ResumeLayout(false);

            m_pageControlList.Add(pageControl);

            pageControl.SetSuccessor(this);


            this.tabControl.ResumeLayout();
        }

        /// <summary>
        /// 根据Page对象从TabControl中找到对应的Panel
        /// </summary>
        /// <param name="page"></param>
        public DevComponents.DotNetBar.TabControlPanel FindPagePanel(Page page)
        {
            foreach (DevComponents.DotNetBar.TabControlPanel tabPanel in tabControl.Controls)
            {
                Debug.Assert(tabPanel != null);
                DevComponents.DotNetBar.TabItem tabItem = tabPanel.TabItem;
                IBaseDrawPanel view = tabItem.ItemData as IBaseDrawPanel;
                if (view != null)
                {
                    if (view.RangeData == page)
                    {
                        //找到了该Panel
                        return tabPanel;
                    }
                }
                else
                {
                    //该Tab页存在问题
                    Debug.Assert(false);
                }
            }

            return null;//未找到
        }

        void pageControl_ChangePageEvent(object sender, ChangePageEventArgs e)
        {
            Debug.Assert(null != e.PageItem);

            Book.Instance.ResetPages(e.PageItem.Name);
        }

        private void blockEditControl_ChangeBlockEvent(object sender, ChangeBlockEventArgs e)
        {
            Debug.Assert(null != e.BlockItem);

            foreach (PageEditControl pageControl in m_pageControlList)
            {
                pageControl.ResetBlocks(e.BlockItem.Name);
            }
        }

        public void InitBook(string fileName)
        {
            try
            {
                //打开并验证XML文件
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(fileName);
                xmlDoc.Schemas.Add(null, Application.StartupPath + "\\" + Properties.Settings.Default.DataPath + "\\book.xsd");
                xmlDoc.Validate(null);

                //从根节点中读取所有block
                 Book.Instance.Load(xmlDoc.DocumentElement);
            }
            catch (System.IO.FileNotFoundException ex)
            {
                MessageBox.Show("读取" + fileName + "时发生错误，错误原因：" + ex.Message);
            }
            catch (System.Xml.Schema.XmlSchemaValidationException ex)
            {
                MessageBox.Show("文件" + fileName + "格式不规范，原因" + ex.Message);
                return;
            }
            catch (System.Xml.XmlException ex)
            {
                MessageBox.Show("读取" + fileName + "时发生错误，错误原因：" + ex.Message);
            }
        }

        public void AddMenu(System.Windows.Forms.ToolStripMenuItem menu)
        {
            Debug.Assert(menu != null);

            this.mainMenuStrip.Items.Add(menu);
        }

        public void RemoveMenu(System.Windows.Forms.ToolStripMenuItem menu)
        {
            Debug.Assert(menu != null);

            this.mainMenuStrip.Items.Remove(menu);
        }

        private void tabControl_SelectedTabChanged(object sender, DevComponents.DotNetBar.TabStripTabChangedEventArgs e)
        {
            if (tabControl.SelectedTab.ItemData != null)
            {
                blockControl.ChangeEditType(tabControl.SelectedTab.ItemData as IBaseDrawPanel);
                pageControl.ChangeEditType(tabControl.SelectedTab.ItemData as IBaseDrawPanel);
            }
            else
            {
                //不需要处理
            }
        }

        #region IHelpHandle 成员

        public void SetSuccessor(IHelpHandle handle)
        {
            m_cHelpHandle.SetSuccessor(handle);
        }

        public void HandleHelp(string text)
        {
            DisplayStatusText(text);
        }

        /// <summary>
        /// 更新状态栏文字
        /// </summary>
        /// <param name="sText"></param>
        public void DisplayStatusText(string text)
        {
            toolStripStatusLabel.Text = text;
        }

        #endregion

        private void GoBackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (tabControl.SelectedTab.ItemData as IBaseDrawPanel).CmdMgr.GoBack();
            RefreshCommands();
        }

        private void GoBeforeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (tabControl.SelectedTab.ItemData as IBaseDrawPanel).CmdMgr.GoBefore();
            RefreshCommands();
        }

        private void EditToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            RefreshCommands();
        }

        private void RefreshCommands()
        {
            IBaseDrawPanel selView = (tabControl.SelectedTab.ItemData as IBaseDrawPanel);
            GoBackToolStripMenuItem.Enabled = selView.CmdMgr.CanGoBack;
            GoBeforeToolStripMenuItem.Enabled = selView.CmdMgr.CanGoBefore;

            //显示最近20条可回撤的步骤
            GoBackToolStripMenuItem.DropDownItems.Clear();
            List<ICommand> commands = selView.CmdMgr.GetAllGoBackCommands();
            int maxDisplayCommands = (commands.Count >= 20) ? 20 : commands.Count;

            for (int i = 0; i < maxDisplayCommands; ++i)
            {
                ToolStripMenuItem menuItem = new ToolStripMenuItem();
                menuItem.Text = commands[i].ToString();
                GoBackToolStripMenuItem.DropDownItems.Insert(0, menuItem);
            }

            //显示最近20条可重做的步骤
            GoBeforeToolStripMenuItem.DropDownItems.Clear();
            commands = selView.CmdMgr.GetAllGoBeforeCommands();
            maxDisplayCommands = (commands.Count >= 20) ? 20 : commands.Count;

            for (int i = 0; i < maxDisplayCommands; ++i)
            {
                ToolStripMenuItem menuItem = new ToolStripMenuItem();
                menuItem.Text = commands[i].ToString();
                GoBeforeToolStripMenuItem.DropDownItems.Insert(0, menuItem);
            }
        }

        private void toolStripButtonAlign_Click(object sender, EventArgs e)
        {
            //处理左对齐
            IBaseDrawPanel selView = tabControl.SelectedTab.ItemData as IBaseDrawPanel;
            AlignButton selBtn = sender as AlignButton;

            if ((selView != null) && (selView.RangeData != null) 
                && (selView.RangeData.SelectedRange != null) 
                && (selView.RangeData.SelectedRange.AlignRef != null))
            {
                m_alignHelper.Align(selView, selView.RangeData.SelectedRange, selBtn.AlignType
                    , selView.RangeData.SelectedRange.AlignRef.BoundsRect);
            }
        }

        private void pageControl_AddPageEvent(object sender, PageEventArgs e)
        {
            InsertPageToControl(tabControl.Tabs.Count, e.PageItem);
        }

        private void pageControl_DelPageEvent(object sender, PageEventArgs e)
        {
            this.tabControl.SuspendLayout();

            //从TabContrl中移除对应Panel
            DevComponents.DotNetBar.TabControlPanel pagePanel = FindPagePanel(e.PageItem);
            Debug.Assert(pagePanel.Controls.Count == 1);
            tabControl.Controls.Remove(pagePanel);
            //从页控件列表中移除
            m_pageControlList.Remove(pagePanel.Controls[0] as PageEditControl);
            this.tabControl.ResumeLayout();
        }

        private void pageControl_SelPageEvent(object sender, PageEventArgs e)
        {
            //根据选择的Page选中指定的Tab
            DevComponents.DotNetBar.TabControlPanel pagePanel = FindPagePanel(e.PageItem);
            this.tabControl.SelectedPanel = pagePanel;
        }
    }
}
