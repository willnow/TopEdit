//******************************************************************************
//文件名称 :     ICommand.cs
//版权信息 :     北京南车时代信息技术有限公司 版权所有
//创建日期 :     2014-07-04
//文件描述 :     
//修改履历 :

//******************************************************************************
using System;
using System.Collections.Generic;
using System.Text;
using TopoEdit.Visitor;
using System.Diagnostics;

namespace TopoEdit.Command
{
    public abstract class ICommand
    {
        /// <summary>
        /// 操作该命令时会影响的View
        /// </summary>
        private IBaseDrawPanel m_view;

        /// <summary>
        /// 操作该命令时会影响的View
        /// </summary>
        protected IBaseDrawPanel View
        {
            get
            {
                return m_view;
            }

            set
            {
                m_view = value;
            }
        }

        public ICommand(IBaseDrawPanel view)
        {
            m_view = view;
        }

        public virtual void Exec()
        {
            CommandBeforeExecVisitor beforeVisitor = new CommandBeforeExecVisitor(m_view.CmdMgr.CommandInterceptor);
            this.Accept(beforeVisitor);

            InnerExec();

            CommandAfterExecVisitor afterVisitor = new CommandAfterExecVisitor(m_view.CmdMgr.CommandInterceptor);
            this.Accept(afterVisitor);
        }

        public virtual void UnExec()
        {
            CommandBeforeUnExecVisitor beforeVisitor = new CommandBeforeUnExecVisitor(m_view.CmdMgr.CommandInterceptor);
            this.Accept(beforeVisitor);

            InnerUnExec();

            CommandAfterUnExecVisitor afterVisitor = new CommandAfterUnExecVisitor(m_view.CmdMgr.CommandInterceptor);
            this.Accept(afterVisitor);
        }

        public abstract void Accept(ICommandVisitor visitor);
        public abstract void InnerExec();
        public abstract void InnerUnExec();

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
