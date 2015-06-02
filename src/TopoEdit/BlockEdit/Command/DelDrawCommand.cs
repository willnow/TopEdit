using System;
using System.Collections.Generic;
using System.Text;
using TopoEdit.Icon;
using TopoEdit.Visitor;
using System.Diagnostics;

namespace TopoEdit.Command
{
    public class DelDrawCommand : ICommand
    {
        /// <summary>
        /// 将删除命令看做添加命令的反命令
        /// </summary>
        private AddDrawCommand m_addCommand = null;

        public DelDrawCommand(IBaseDrawPanel view, IRange delRange, List<IDraw> delDraw)
            : base(view)
        {
            Debug.Assert(view != null);
            m_addCommand = new AddDrawCommand(view, delRange, delDraw);
        }

        public DelDrawCommand(IBaseDrawPanel view, IRange delRange, IDraw delDraw)
            : base(view)
        {
            List<IDraw> delDraws = new List<IDraw>();
            delDraws.Add(delDraw);

            m_addCommand = new AddDrawCommand(view, delRange, delDraws);
        }

        #region ICommand 成员

        public override void InnerExec()
        {
            m_addCommand.InnerUnExec();
        }

        public override void InnerUnExec()
        {
            m_addCommand.InnerExec();
        }

        public override void Accept(ICommandVisitor visitor)
        {
            visitor.VisitDelDrawCommand(this);
        }

        public override string ToString()
        {
            return "删除了" + m_addCommand.AddDraw.Count + "个图元";
        }

        #endregion
    }
}
