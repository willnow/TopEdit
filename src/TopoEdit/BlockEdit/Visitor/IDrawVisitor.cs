using System;
using System.Collections.Generic;
using System.Text;
using TopoEdit.Icon;

namespace TopoEdit.Visitor
{
    public interface IDrawVisitor
    {
        void VisitorCircle(IconCircle icon);
        void VisitorLine(IconLine icon);
        void VisitorPolygon(IconPolygon icon);
        void VisitorRectangle(IconRectangle icon);
        void VisitorText(IconText icon);
        void VisitorBlock(Block block);
        void VisitorBlockRect(BlockRect blockRect);
        void VisitorPage(Page page);
        void VisitorPageRect(PageRect blockRect);
        void VisitorBook(Book book);
        void VisitorSelectedBlockRect(SelectedBlockRect selBlockRect);
        void VisitorSelectedPageRect(SelectedPageRect selPageRect);
        void VisitorSelectedItem(SelectedItem selItem);
        void VisitorSelectedRange(SelectedRange selRange);
        void VisitorSelectedPolygon(SelectedPolygon selPolygon);
    }
}
