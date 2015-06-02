using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TopoEdit.Command;

namespace TopoEdit.Visitor
{
    /// <summary>
    /// 命令访问者
    /// </summary>
    public interface ICommandVisitor
    {
        void VisitAddDrawCommand(AddDrawCommand command);
        void VisitDelDrawCommand(DelDrawCommand command);
        void VisitUpdateDrawCommand(UpdateDrawCommand command);
        void VisitUpdateSelectedDrawCommand(UpdateSelectedDrawCommand command);
        void VisitMoveDrawCommand(MoveDrawCommand command);
    }
}
