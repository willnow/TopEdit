using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TopoEdit.Visitor
{
    /// <summary>
    /// 拷贝图元后，给图元赋新的名称
    /// </summary>
    class SetNewNameAfterCopyVisitor : IDrawVisitor
    {
        #region IDrawVisitor 成员

        public void VisitorCircle(Icon.IconCircle icon)
        {
            //不需要赋新名称
        }

        public void VisitorLine(Icon.IconLine icon)
        {
            //不需要赋新名称
        }

        public void VisitorPolygon(Icon.IconPolygon icon)
        {
            //不需要赋新名称
        }

        public void VisitorRectangle(Icon.IconRectangle icon)
        {
            //不需要赋新名称
        }

        public void VisitorText(Icon.IconText icon)
        {
            //不需要赋新名称
        }

        public void VisitorBlock(Icon.Block block)
        {
            //不需要赋新名称
        }

        public void VisitorBlockRect(Icon.BlockRect blockRect)
        {
            blockRect.Name = blockRect.TemplateName + Icon.Book.Instance.GetNewNameIndex(blockRect.TemplateName);
        }

        public void VisitorPage(Icon.Page page)
        {
            //不需要赋新名称
        }

        public void VisitorPageRect(Icon.PageRect pageRect)
        {
            pageRect.Name = pageRect.TemplateName + Icon.Book.Instance.GetNewNameIndex(pageRect.TemplateName);
        }

        public void VisitorBook(Icon.Book book)
        {
            //不需要赋新名称
        }

        public void VisitorSelectedBlockRect(Icon.SelectedBlockRect selBlockRect)
        {
            VisitorBlockRect(selBlockRect.Icon as Icon.BlockRect);
        }

        public void VisitorSelectedPageRect(Icon.SelectedPageRect selPageRect)
        {
            VisitorPageRect(selPageRect.Icon as Icon.PageRect);
        }

        public void VisitorSelectedItem(Icon.SelectedItem selItem)
        {
            //不需要赋新名称
        }

        public void VisitorSelectedRange(Icon.SelectedRange selRange)
        {
            foreach (Icon.IDraw draw in selRange.Icons)
            {
                draw.Accept(this);
            }
        }

        public void VisitorSelectedPolygon(Icon.SelectedPolygon selPolygon)
        {
            //不需要赋新名称
        }

        #endregion
    }
}
