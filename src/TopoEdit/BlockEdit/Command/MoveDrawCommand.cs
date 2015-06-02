using System;
using System.Collections.Generic;
using System.Text;
using TopoEdit.Icon;
using System.Drawing;
using TopoEdit.Visitor;
using System.Diagnostics;

namespace TopoEdit.Command
{
    public class MoveDrawCommand : ICommand
    {
        /// <summary>
        /// 被平移元素
        /// </summary>
        IDraw m_draw;
        //平移向量
        Movement m_move;

        public MoveDrawCommand(IBaseDrawPanel view, IDraw draw, Movement move)
            : base(view)
        {
            Debug.Assert(view != null);
            m_draw = draw;
            m_move = move.Clone() as Movement;
        }

        public override void InnerExec()
        {
            RectangleF rect = new RectangleF();
            //将移动前区域加入刷新区域
            rect = TopoEdit.Utility.Union(rect, m_draw.BoundsRect);
            //操作
            m_draw.Move(m_move.Clone() as Movement);
            //将移动后区域加入刷新区域
            rect = TopoEdit.Utility.Union(rect, m_draw.BoundsRect);
            View.Refresh(Utility.AdjustRect(rect, 10));
        }

        public override void InnerUnExec()
        {
            RectangleF rect = new RectangleF();
            //将移动前区域加入刷新区域
            rect = TopoEdit.Utility.Union(rect, m_draw.BoundsRect);
            //操作
            Movement move = new Movement();
            move.SubMode = m_move.SubMode;
            move.XMovement = -m_move.XMovement;
            move.YMovement = -m_move.YMovement;

            m_draw.Move(move.Clone() as Movement);
            //将移动后区域加入刷新区域
            rect = TopoEdit.Utility.Union(rect, m_draw.BoundsRect);
            View.Refresh(Utility.AdjustRect(rect, 10));
        }

        public override void Accept(ICommandVisitor visitor)
        {
            visitor.VisitMoveDrawCommand(this);
        }

        public override string ToString()
        {
            return "平移 " + m_move.ToString();
        }
    }
}
