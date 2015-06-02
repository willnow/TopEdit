using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TopoEdit.Interceptor;

namespace TopoEdit.Visitor
{
    /// <summary>
    /// 命令执行后访问者
    /// </summary>
    class CommandAfterExecVisitor : ICommandVisitor
    {
        private CommandInterceptorGroup m_interceptor;

        public CommandAfterExecVisitor(CommandInterceptorGroup interceptor)
        {
            m_interceptor = interceptor;
        }

        #region ICommandVisitor 成员

        public void VisitAddDrawCommand(Command.AddDrawCommand command)
        {
            m_interceptor.ExecAddDrawAfter(command);
        }

        public void VisitDelDrawCommand(Command.DelDrawCommand command)
        {
            m_interceptor.ExecDelDrawAfter(command);
        }

        public void VisitUpdateDrawCommand(Command.UpdateDrawCommand command)
        {
            m_interceptor.ExecUpdateDrawAfter(command);
        }

        public void VisitUpdateSelectedDrawCommand(Command.UpdateSelectedDrawCommand command)
        {
            m_interceptor.ExecUpdateSelectedDrawAfter(command);
        }

        public void VisitMoveDrawCommand(Command.MoveDrawCommand command)
        {
            m_interceptor.ExecMoveDrawAfter(command);
        }

        #endregion
    }
}
