using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TopoEdit.Interceptor;

namespace TopoEdit.Visitor
{
    /// <summary>
    /// 命令撤销前访问者
    /// </summary>
    class CommandBeforeUnExecVisitor : ICommandVisitor
    {
        private CommandInterceptorGroup m_interceptor;

        public CommandBeforeUnExecVisitor(CommandInterceptorGroup interceptor)
        {
            m_interceptor = interceptor;
        }

        #region ICommandVisitor 成员

        public void VisitAddDrawCommand(Command.AddDrawCommand command)
        {
            m_interceptor.UnExecAddDrawBefore(command);
        }

        public void VisitDelDrawCommand(Command.DelDrawCommand command)
        {
            m_interceptor.UnExecDelDrawBefore(command);
        }

        public void VisitUpdateDrawCommand(Command.UpdateDrawCommand command)
        {
            m_interceptor.UnExecUpdateDrawBefore(command);
        }

        public void VisitUpdateSelectedDrawCommand(Command.UpdateSelectedDrawCommand command)
        {
            m_interceptor.UnExecUpdateSelectedDrawBefore(command);
        }

        public void VisitMoveDrawCommand(Command.MoveDrawCommand command)
        {
            m_interceptor.UnExecMoveDrawBefore(command);
        }

        #endregion
    }
}
