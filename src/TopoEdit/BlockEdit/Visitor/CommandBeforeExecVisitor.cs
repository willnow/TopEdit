using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TopoEdit.Interceptor;

namespace TopoEdit.Visitor
{
    /// <summary>
    /// 命令执行前访问者
    /// </summary>
    class CommandBeforeExecVisitor : ICommandVisitor
    {
        private CommandInterceptorGroup m_interceptor;

        public CommandBeforeExecVisitor(CommandInterceptorGroup interceptor)
        {
            m_interceptor = interceptor;
        }

        #region ICommandVisitor 成员

        public void VisitAddDrawCommand(Command.AddDrawCommand command)
        {
            m_interceptor.ExecAddDrawBefore(command);
        }

        public void VisitDelDrawCommand(Command.DelDrawCommand command)
        {
            m_interceptor.ExecDelDrawBefore(command);
        }

        public void VisitUpdateDrawCommand(Command.UpdateDrawCommand command)
        {
            m_interceptor.ExecUpdateDrawBefore(command);
        }

        public void VisitUpdateSelectedDrawCommand(Command.UpdateSelectedDrawCommand command)
        {
            m_interceptor.ExecUpdateSelectedDrawBefore(command);
        }

        public void VisitMoveDrawCommand(Command.MoveDrawCommand command)
        {
            m_interceptor.ExecMoveDrawBefore(command);
        }

        #endregion
    }
}
