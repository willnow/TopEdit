using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using TopoEdit.Icon;
using System.Diagnostics;
using TopoEdit.EventHandler;
using System.Xml;
using System.Windows.Forms;
using TopoEdit.Visitor;

namespace TopoEdit.Icon
{
    public class Page : ZoomableRange
    {
        /// <summary>
        /// page id号
        /// </summary>
        private int m_id = 1;

        public int Id
        {
            get { return m_id; }
        }

        public Page(string name, int id)
        {
            Name = name;
            m_id = id;
        }

        /// <summary>
        /// 根据Block实例名称查找Block实例
        /// </summary>
        /// <param name="name">Block实例名称</param>
        /// <returns>所有指定名称的Block实例集合</returns>
        public List<BlockRect> FindByName(string name)
        {
            ClearSelectIcon();//清除选中，否则无法保存所有图元都是BlockRect类型
            List<BlockRect> findBlockRect = new List<BlockRect>();
   
            foreach (IDraw draw in this.Icons)
            {
                BlockRect blockRect = draw as BlockRect;
                Debug.Assert(blockRect != null);
                if (blockRect.Name == name)
                {
                    findBlockRect.Add(blockRect);
                }
            }

            return findBlockRect;
        }

        /// <summary>
        /// 由于BLOCK发生变化，重置当前拓扑图中的所有关联BLOCK
        /// </summary>
        /// <param name="blockName">发生变化的BLOCK名称</param>
        public void ResetBlocks(string blockName)
        {
            //清除选中区域
            ClearSelectIcon();

            foreach (IDraw draw in m_draws)
            {
                BlockRect blockRect = draw as BlockRect;
                Debug.Assert(null != blockRect);

                if (blockRect.TemplateName == blockName)
                {
                    blockRect.ResetTemplate();
                }
            }
        }

        public override IDraw Clone()
        {
            Page page = new Page(this.Name, this.Id);
            page.Copy(this);
            return page;
        }

        public override void Copy(IDraw src)
        {
            if (src is Page)
            {
                Page srcItem = src as Page;

                Name = srcItem.Name;
                m_id = srcItem.Id;
                Clear();
                foreach (IDraw icon in srcItem.m_draws)
                {
                    Add(icon.Clone());
                }
            }
            else
            {
                throw new ArgumentException("被拷贝图元和目标图元类型不兼容", "src");
            }
        }

        public override bool Load(XmlNode topoNode)
        {
            m_draws.Clear();

            Name = topoNode.SelectSingleNode("Name").InnerText;
            m_id = int.Parse(topoNode.SelectSingleNode("Id").InnerText);
            
            XmlNodeList blockRectNodes = topoNode.SelectNodes("BlockRect");
            foreach (XmlNode node in blockRectNodes)
            {
                BlockRect blockRect = new BlockRect(this);
                blockRect.Load(node);
                Add(blockRect);
            }

            m_draws.Sort();//按level从小到大排序,level越大越靠上层
            return true;
        }

        public override bool Save(XmlNode topoNode)
        {
            ClearSelectIcon();

            System.Xml.XmlNode nodeName = topoNode.OwnerDocument.CreateElement("Name");
            nodeName.InnerText = Name;
            topoNode.AppendChild(nodeName);

            System.Xml.XmlNode nodeId = topoNode.OwnerDocument.CreateElement("Id");
            nodeId.InnerText = m_id.ToString();
            topoNode.AppendChild(nodeId);

            foreach (IDraw draw in m_draws)
            {
                BlockRect blockRect = draw as BlockRect;
                blockRect.Save(topoNode);
            }

            return true;
        }

        public override void Accept(TopoEdit.Visitor.IDrawVisitor visitor)
        {
            visitor.VisitorPage(this);
        }
    }
}
