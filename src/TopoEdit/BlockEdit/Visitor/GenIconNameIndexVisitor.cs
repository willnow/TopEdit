using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TopoEdit.Visitor
{
    /// <summary>
    /// 生成图元名称索引，在复制一个图元时用于自动生成新图元名称
    /// </summary>
    class GenIconNameIndexVisitor : IDrawVisitor
    {
        /// <summary>
        /// 名称前缀
        /// </summary>
        private string m_namePrefix = "";
        /// <summary>
        /// 当前找到的最大索引
        /// </summary>
        private int m_maxIndex = 0;

        public int MaxIndex
        {
            get { return m_maxIndex; }
        }

        public GenIconNameIndexVisitor(string namePrefix)
        {
            m_namePrefix = namePrefix;
        }
        #region IDrawVisitor 成员

        public void VisitorCircle(Icon.IconCircle icon)
        {
            //不处理
        }

        public void VisitorLine(Icon.IconLine icon)
        {
            //不处理
        }

        public void VisitorPolygon(Icon.IconPolygon icon)
        {
            //不处理
        }

        public void VisitorRectangle(Icon.IconRectangle icon)
        {
            //不处理
        }

        public void VisitorText(Icon.IconText icon)
        {
            //不处理
        }

        public void VisitorBlock(Icon.Block block)
        {
            //不处理
        }

        public void VisitorBlockRect(Icon.BlockRect blockRect)
        {
            SetNameIndex(blockRect.Name);
        }

        public void VisitorPage(Icon.Page page)
        {
            //不处理
        }

        public void VisitorPageRect(Icon.PageRect pageRect)
        {
            SetNameIndex(pageRect.Name);
        }

        public void VisitorBook(Icon.Book book)
        {
            //不处理
        }

        public void VisitorSelectedBlockRect(Icon.SelectedBlockRect selBlockRect)
        {
            SetNameIndex((selBlockRect.Icon as Icon.BlockRect).Name);
        }

        public void VisitorSelectedPageRect(Icon.SelectedPageRect selPageRect)
        {
            SetNameIndex((selPageRect.Icon as Icon.PageRect).Name);
        }

        public void VisitorSelectedItem(Icon.SelectedItem selItem)
        {
            //不处理
        }

        public void VisitorSelectedRange(Icon.SelectedRange selRange)
        {
            //不处理
        }

        public void VisitorSelectedPolygon(Icon.SelectedPolygon selPolygon)
        {
            //不处理
        }

        #endregion

        private bool SetNameIndex(string name)
        {
            if (name.Length > m_namePrefix.Length)
            {
                if (name.Substring(0, m_namePrefix.Length) == m_namePrefix)
                {
                    string nameIndexString = name.Substring(m_namePrefix.Length, name.Length - m_namePrefix.Length);
                    int nameIndex = 0;
                    if (int.TryParse(nameIndexString, out nameIndex))
                    {
                        if (nameIndex > m_maxIndex)
                        {
                            m_maxIndex = nameIndex;
                        }
                        return true;
                    }
                    else
                    {
                        //名称字符串包含匹配前缀，但除后缀部分不是数字，不符合要求
                        return false;
                    }
                }
                else
                {
                    //名称字符串不包含匹配前缀，不符合要求
                    return false;
                }
            }
            else
            {
                //名称字符串小于匹配前缀，不符合要求
                return false;
            }
        }
    }
}
