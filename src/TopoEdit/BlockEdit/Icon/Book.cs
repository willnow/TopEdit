using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Diagnostics;
using TopoEdit.Visitor;

namespace TopoEdit.Icon
{
    public class Book : ZoomableRange
    {
        public static Book Instance = new Book();

        private Book()
        {
            m_rangeZoom = 1;
        }

        public override bool Load(XmlNode bookNode)
        {
            m_draws.Clear();

            Name = bookNode.SelectSingleNode("Name").InnerText;

            XmlNodeList PageRectNodes = bookNode.SelectNodes("PageRect");
            foreach (XmlNode node in PageRectNodes)
            {
                PageRect pageRect = new PageRect(this);
                pageRect.Load(node);
                Add(pageRect);
            }

            m_draws.Sort();//按level从小到大排序,level越大越靠上层
            return true;
        }

        public override bool Save(XmlNode bookNode)
        {
            ClearSelectIcon();

            System.Xml.XmlNode nodeName = bookNode.OwnerDocument.CreateElement("Name");
            nodeName.InnerText = Name;
            bookNode.AppendChild(nodeName);

            foreach (IDraw draw in m_draws)
            {
                PageRect pageRect = draw as PageRect;
                pageRect.Save(bookNode);
            }

            return true;
        }

        /// <summary>
        /// 由于BLOCK发生变化，重置当前拓扑图中的所有关联BLOCK
        /// </summary>
        /// <param name="blockName">发生变化的BLOCK名称</param>
        public void ResetPages(string pageName)
        {
            //清除选中区域
            ClearSelectIcon();

            foreach (IDraw draw in m_draws)
            {
                PageRect pageRect = draw as PageRect;
                Debug.Assert(null != pageRect);

                if (pageRect.TemplateName == pageName)
                {
                    pageRect.ResetTemplate();
                }
            }
        }

        /// <summary>
        /// 根据名字前缀在全线查找所有名称，返回最大的名字索引+1
        /// </summary>
        /// <param name="namePrefix"></param>
        /// <returns></returns>
        public int GetNewNameIndex(string namePrefix)
        {
            GenIconNameIndexVisitor visitor = new GenIconNameIndexVisitor(namePrefix);

            foreach (IDraw pageRectDraw in m_draws)
            {
                PageRect pageRect = pageRectDraw as PageRect;
                Debug.Assert(null != pageRect);

                foreach (IDraw blockRectDraw in pageRect.Icons)
                {
                    blockRectDraw.Accept(visitor);
                }
            }

            //防止页没有加入Book，还需要搜索一下所有页
            foreach (Page page in PageContainer.Instance)
            {
                foreach (IDraw blockRectDraw in page.Icons)
                {
                    blockRectDraw.Accept(visitor);
                }
            }

            return visitor.MaxIndex + 1;
        }


        public override void Accept(TopoEdit.Visitor.IDrawVisitor visitor)
        {
            visitor.VisitorBook(this);
        }

        public override IDraw Clone()
        {
            throw new NotImplementedException();
        }

        public override void Copy(IDraw src)
        {
            throw new NotImplementedException();
        }
    }
}
