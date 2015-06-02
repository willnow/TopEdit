using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using TopoEdit.Icon;
using System.Diagnostics;
using TopoEdit.InputControl;

namespace TopoEdit
{
    public partial class PageControl : UserControl
    {
        public event EventHandler<PageEventArgs> SelPageEvent;
        public event EventHandler<PageEventArgs> AddPageEvent;
        public event EventHandler<PageEventArgs> DelPageEvent;
        /// <summary>
        /// 上一次的编辑视图
        /// </summary>
        private IBaseDrawPanel m_lastEditType = null;
        /// <summary>
        /// 当前选中的Page
        /// </summary>
        private Button m_selPageBtn = null;

        public PageControl()
        {
            InitializeComponent();
        }

        public void Init()
        {
            //从根节点中读取所有block
            pagePanel.Controls.Clear();
            foreach (Page page in PageContainer.Instance)
            {
                AddPageButton(page);
            }
        }

        private void AddPageButton(Page page)
        {
            Button pageItem = new Button();
            pageItem.TextImageRelation = TextImageRelation.ImageAboveText;
            pageItem.ImageAlign = ContentAlignment.MiddleCenter;
            pageItem.TextAlign = ContentAlignment.BottomCenter;
            pageItem.FlatStyle = FlatStyle.Flat;
            pageItem.FlatAppearance.BorderSize = 0;
            pageItem.Size = new Size(180, 75);
            pageItem.Name = page.Name;
            pageItem.Text = page.Name;
            pageItem.Image = page.GetBitmap(Color.LightGray, pageItem.Width, pageItem.Height);
            pageItem.Tag = page;
            pageItem.MouseDown += new MouseEventHandler(pageItem_MouseDown);
            pagePanel.Controls.Add(pageItem);
        }

        void pageItem_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        void pageItem_MouseDown(object sender, MouseEventArgs e)
        {
            DrawPage(sender as Button);
            m_selPageBtn = sender as Button;
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStripPage.Show(sender as Button, e.Location);
                DelToolStripMenuItem.Enabled = true;
                AddToolStripMenuItem.Enabled = false;
            }
            else
            {
                contextMenuStripPage.Hide();
                //如果当前是BookPanel，则开始拖拽准备
                if (m_lastEditType is BookPanel)
                {
                    (sender as Button).DoDragDrop(((sender as Button).Tag as Page).Clone(), DragDropEffects.Copy);
                }
                else
                {
                    //不需要准备拖拽
                    if (null != SelPageEvent)
                    {
                        Page page = PageContainer.Instance.GetPageByName(((Button)sender).Name);
                        if (null != page)
                        {
                            SelPageEvent(sender, new PageEventArgs(page));
                        }
                    }
                }
            }

            
        }

        void DrawPage(Button pageBtn)
        {
            Page page = PageContainer.Instance.GetPageByName(pageBtn.Name);
            pageBtn.Image = page.GetBitmap(Color.LightGray, pageBtn.Width, pageBtn.Height);
        }

        public void ChangeEditType(IBaseDrawPanel editView)
        {
            m_lastEditType = editView;
        }

        private void AddPage(Page page)
        {
            if (PageContainer.Instance.GetPageByName(page.Name) != null)
            {
                //存在同名Page，不允许添加
                return;
            }
            else
            {
                PageContainer.Instance.Add(page);

                AddPageButton(page);

                //通知主界面创建一个Page的Tab页
                if (null != AddPageEvent)
                {
                    AddPageEvent(this, new PageEventArgs(page));
                }
            }
        }

        private void AddToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddPageForm form = new AddPageForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                if (PageContainer.Instance.GetPageByName(form.PageName) == null)
                {
                    Page page = new Page(form.PageName, form.PageId);
                    AddPage(page);
                    DBHelper.Instance.AddPage(page);
                }
                else
                {
                    //有重名Page
                    MessageBox.Show("页" + form.PageName + "已存在");
                }
            }
        }

        private void pagePanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStripPage.Show(sender as Control, e.Location);
                DelToolStripMenuItem.Enabled = false;
                AddToolStripMenuItem.Enabled = true;
            }
            else
            {
                contextMenuStripPage.Hide();
            }
        }

        private void DelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (m_selPageBtn != null)
            {
                Button pageBtn = m_selPageBtn;
                Page page = pageBtn.Tag as Page;

                if (page != null)
                {
                    if (MessageBox.Show("确定删除" + page.Name + "页吗？", "删除提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        RemovePage(pageBtn);
                        //从XML中移除该Block
                        DBHelper.Instance.DelPage(page);

                        //通知主界面删除Page
                        if (null != DelPageEvent)
                        {
                            DelPageEvent(this, new PageEventArgs(page));
                        }
                    }
                }
                else
                {
                    MessageBox.Show("请选择欲删除的页");
                }
            }
            else
            {
                MessageBox.Show("请选择欲删除的页");
            }
        }

        private void RemovePage(Button pageBtn)
        {
            PageContainer.Instance.Remove(pageBtn.Tag as Page);
            pagePanel.Controls.Remove(pageBtn);
        }
    }

    /// <summary>
    /// 选择BLOCK事件
    /// </summary>
    public class PageEventArgs : EventArgs
    {
        private Page m_page;

        internal Page PageItem
        {
            get { return m_page; }
        }
        internal PageEventArgs(Page page)
        {
            m_page = page;
        }
    }
}
