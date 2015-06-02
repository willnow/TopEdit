using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using TopoEdit.Icon;
using System.Xml;
using System.IO;
using System.Diagnostics;
using TopoEdit.InputControl;

namespace TopoEdit
{
    public partial class BlockControl : UserControl
    {
        public event EventHandler<BlockEventArgs> SelBlockEvent;
        /// <summary>
        /// 上一次的编辑的视图
        /// </summary>
        private IBaseDrawPanel m_lastEditType = null;
        /// <summary>
        /// 当前选中的Block
        /// </summary>
        private Button m_selBlockBtn = null;

        public BlockControl()
        {
            InitializeComponent();
        }

        private void BlockControl_Load(object sender, EventArgs e)
        {

        }

        public void LoadBlock(XmlNode blockNode)
        {
            //从根节点中读取所有block
            blockPanel.Controls.Clear();
            BlockContainer.Instance.Clear();
            foreach (XmlNode node in blockNode.ChildNodes)
            {
                Block block = new Block(node.SelectSingleNode("Name").InnerText);
                block.Load(node);
                AddBlock(block);
            }
        }

        private void AddBlock(Block block)
        {
            if (BlockContainer.Instance.GetBlockByName(block.Name) != null)
            {
                //存在同名Block，不允许添加
                return;
            }
            else
            {
                BlockContainer.Instance.Add(block);

                Button blockItem = new Button();
                blockItem.TextImageRelation = TextImageRelation.ImageAboveText;
                blockItem.ImageAlign = ContentAlignment.MiddleCenter;
                blockItem.TextAlign = ContentAlignment.BottomCenter;
                blockItem.FlatStyle = FlatStyle.Flat;
                blockItem.FlatAppearance.BorderSize = 0;
                blockItem.Size = new Size(75, 75);
                blockItem.Name = block.Name;
                blockItem.Text = block.Name;
                blockItem.Image = block.GetBitmap(Color.LightGray, 30, 30);
                blockItem.Tag = block;
                blockItem.MouseDown += new MouseEventHandler(blockItem_MouseDown);
                blockPanel.Controls.Add(blockItem);
            }
        }

        private void RemoveBlock(Button blockBtn)
        {
            BlockContainer.Instance.Remove(blockBtn.Tag as Block);
            blockPanel.Controls.Remove(blockBtn);
        }

        void blockItem_MouseDown(object sender, MouseEventArgs e)
        {
            DrawBlock(sender);
            m_selBlockBtn = sender as Button;
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStripBlock.Show(sender as Button, e.Location);
                DelBlockToolStripMenuItem.Enabled = true;
                AddBlockToolStripMenuItem.Enabled = false;
            }
            else
            {
                contextMenuStripBlock.Hide();

                //开始拖拽准备
                if (m_lastEditType is PagePanel)
                {
                    (sender as Button).DoDragDrop(((sender as Button).Tag as Block).Clone(), DragDropEffects.Copy);
                }
                else
                {
                    //未选中PagePanel，不准备拖拽
                    SelectBlock();

                }
                
            }
        }

        private void SelectBlock()
        {
            if (null != SelBlockEvent)
            {
                Block block = BlockContainer.Instance.GetBlockByName(m_selBlockBtn.Name);
                if (null != block)
                {
                    SelBlockEvent(this, new BlockEventArgs(block));
                }
            }
        }
        void DrawBlock(object sender)
        {
            Block block = BlockContainer.Instance.GetBlockByName(((Button)sender).Name);
            (sender as Button).Image = block.GetBitmap(Color.LightGray, 30, 30);
        }

        public void ChangeEditType(IBaseDrawPanel editView)
        {
            m_lastEditType = editView;
        }

        private void AddBlockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddBlockForm form = new AddBlockForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                Block block = new Block(form.BlockName);
                AddBlock(block);
                DBHelper.Instance.AddBlock(block);
            }
        }

        private void DelBlockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (m_selBlockBtn != null)
            {
                Button blockBtn = m_selBlockBtn;
                Block block = blockBtn.Tag as Block;

                if (block != null)
                {
                    if (MessageBox.Show("确定删除" + block.Name + "图块吗？", "删除提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        RemoveBlock(blockBtn);
                        //从XML中移除该Block
                        DBHelper.Instance.DelBlock(blockBtn.Tag as Block);
                    }
                }
                else
                {
                    MessageBox.Show("请选择欲删除的图块");
                }
            }
            else
            {
                MessageBox.Show("请选择欲删除的图块");
            }
        }

        private void blockPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStripBlock.Show(sender as Control, e.Location);
                DelBlockToolStripMenuItem.Enabled = false;
                AddBlockToolStripMenuItem.Enabled = true;
            }
            else
            {
                contextMenuStripBlock.Hide();
            }
        }

        private void SaveAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //将所有Block都保存到文件
            DBHelper.Instance.SaveBlocks(BlockContainer.Instance);
        }

        private void EditToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectBlock();
        }      
    }

    /// <summary>
    /// 选择BLOCK事件
    /// </summary>
    public class BlockEventArgs : EventArgs
    {
        private Block m_block;

        internal Block BlockItem
        {
            get { return m_block; }
        }
        internal BlockEventArgs(Block block)
        {
            m_block = block;
        }
    }
}
