using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TopoEdit.Interceptor;

namespace TopoEdit.Visitor
{
    /// <summary>
    /// 命令撤销后访问者
    /// </summary>
    class CommandAfterUnExecVisitor : ICommandVisitor
    {
        private CommandInterceptorGroup m_interceptor;

        public CommandAfterUnExecVisitor(CommandInterceptorGroup interceptor)
        {
            m_interceptor = interceptor;
        }

        #region ICommandVisitor 成员

        public void VisitAddDrawCommand(Command.AddDrawCommand command)
        {
            m_interceptor.UnExecAddDrawAfter(command);
        }

        public void VisitDelDrawCommand(Command.DelDrawCommand command)
        {
            m_interceptor.UnExecDelDrawAfter(command);
        }

        public void VisitUpdateDrawCommand(Command.UpdateDrawCommand command)
        {
            m_interceptor.UnExecUpdateDrawAfter(command);
        }

        public void VisitUpdateSelectedDrawCommand(Command.UpdateSelectedDrawCommand command)
        {
            m_interceptor.UnExecUpdateSelectedDrawAfter(command);
        }

        public void VisitMoveDrawCommand(Command.MoveDrawCommand command)
        {
            m_interceptor.UnExecMoveDrawAfter(command);
        }

        #endregion
    }
}
