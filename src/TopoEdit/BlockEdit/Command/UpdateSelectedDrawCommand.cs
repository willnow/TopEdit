using System;
using System.Collections.Generic;
using System.Text;
using TopoEdit.Icon;
using System.Drawing;
using TopoEdit.Visitor;
using System.Diagnostics;

namespace TopoEdit.Command
{
    public class UpdateSelectedDrawCommand : UpdateDrawCommand
    {
        private SelectedDraw m_selDraw;

        public UpdateSelectedDrawCommand(IBaseDrawPanel view, SelectedDraw selDraw, IDraw newDraw, IDraw oldDraw)
            : base(view, selDraw.Icon, newDraw, oldDraw)
        {
            Debug.Assert(view != null);
            m_selDraw = selDraw;
        }

        public override void InnerExec()
        {
            RectangleF rect = new RectangleF();
            //将移动前区域加入刷新区域
            rect = TopoEdit.Utility.Union(rect, m_selDraw.BoundsRect);
            //操作
            base.InnerExec();
            m_selDraw.ResetAnchor();
            //将添加后区域加入刷新区域
            rect = TopoEdit.Utility.Union(rect, m_selDraw.BoundsRect);
            View.Refresh(Utility.AdjustRect(rect, 10));
        }

        public override void InnerUnExec()
        {
            RectangleF rect = new RectangleF();
            //将移动前区域加入刷新区域
            rect = TopoEdit.Utility.Union(rect, m_selDraw.BoundsRect);
            //操作
            base.InnerUnExec();
            m_selDraw.ResetAnchor();
            //将添加后区域加入刷新区域
            rect = TopoEdit.Utility.Union(rect, m_selDraw.BoundsRect);
            View.Refresh(Utility.AdjustRect(rect, 10));
        }

        public override void Accept(ICommandVisitor visitor)
        {
            visitor.VisitUpdateSelectedDrawCommand(this);
        }

        public override string ToString()
        {
            return "更新了一个图元";
        }
    }
}
