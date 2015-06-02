using System;
using System.Collections.Generic;
using System.Text;
using TopoEdit.Visitor;

namespace TopoEdit.Command
{
    /// <summary>
    /// 空命令，作为命令管理器的哨兵
    /// </summary>
    class NullCommand : ICommand
    {
        public NullCommand(IBaseDrawPanel view)
            : base(view)
        {
            //有意留空
        }

        public override void InnerExec()
        {
            throw new NotImplementedException();
        }

        public override void InnerUnExec()
        {
            throw new NotImplementedException();
        }

        public override void Accept(ICommandVisitor visitor)
        {
            throw new NotImplementedException();
        }
    }
}
