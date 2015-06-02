//******************************************************************************
//文件名称 :     CommandManager.cs
//版权信息 :     北京南车时代信息技术有限公司 版权所有
//创建日期 :     2014-07-04
//文件描述 :     
//修改履历 :

//******************************************************************************
using System;
using System.Collections.Generic;
using System.Text;
using TopoEdit.Interceptor;

namespace TopoEdit.Command
{
    //命令管理类
    public class CommandManager
    {
        internal CommandInterceptorGroup CommandInterceptor
        {
            get { return m_commandInterceptor; }
        }

        public CommandManager()
        {
            m_command.AddFirst(m_firstCommand);
        }

        /// <summary>
        /// 增加一个命令，然后执行该命令
        /// </summary>
        /// <param name="cCommand"></param>
        public void AddThenExec(ICommand cCommand)
        {
            AddCommand(cCommand);
            cCommand.Exec();
        }
        /// <summary>
        /// 撤消操作，然后删除该命令
        /// </summary>
        /// <param name="cCommand"></param>
        public void UnExecThenDel(ICommand cCommand)
        {
            cCommand.UnExec();
            DelCommand(cCommand);
        }

        public void AddCommand(ICommand cCommand)
        {
            //清除当前命令之后的所有命令
            LinkedListNode<ICommand> cCommandNode = m_curCommandNode;
            if (null != cCommandNode)
            {
                while (null != m_curCommandNode.Next)
                {
                    //如果当前命令之后还有命令，则继续清除当前命令之后的第一个命令
                    cCommandNode = m_curCommandNode.Next;
                    m_command.Remove(cCommandNode);
                }
            }
            m_command.AddLast(cCommand);
            MoveCurCommandNodeToNext();
        }
        public void DelCommand(ICommand cCommand)
        {
            MoveCurCommandNodeToPrev(cCommand);
            m_command.Remove(cCommand);
        }
        public LinkedList<ICommand> Commands
        {
            get { return m_command; }
        }
        private void MoveCurCommandNodeToNext()
        {
            if (null == m_curCommandNode)
            {
                m_curCommandNode = m_command.Last;
            }
            else
            {
                m_curCommandNode = m_curCommandNode.Next;
            }
        }
        /// <summary>
        /// 将执行完的命令节点指针向前推移
        /// </summary>
        /// <param name="cCommand"></param>
        private void MoveCurCommandNodeToPrev(ICommand cCommand)
        {
            if (null != m_curCommandNode && m_curCommandNode.Value == cCommand)
            {
                m_curCommandNode = m_curCommandNode.Previous;
            }
        }
        
        public bool CanGoBack
        {
            get
            {
                if ((null != m_curCommandNode) && (null != m_curCommandNode.Value) && (m_firstCommand != m_curCommandNode.Value))
                {
                    return true;
                }
                else 
                {
                    return false;
                }
            }
        }
        public bool CanGoBefore
        {
            get
            {
                if (null != m_curCommandNode && null != m_curCommandNode.Next &&
                    null != m_curCommandNode.Next.Value)
                {
                    return true;
                }
                else 
                {
                    return false;
                }
            }
        }
        /// <summary>
        /// 手动撤消命令
        /// </summary>
        public void GoBack()
        {
            if (!CanGoBack)
            {
                return;
            }
            m_curCommandNode.Value.UnExec();
            m_curCommandNode = m_curCommandNode.Previous;
        }
        /// <summary>
        /// 手动恢复命令
        /// </summary>
        public void GoBefore()
        {
            if (!CanGoBefore)
            {
                return;
            }
            m_curCommandNode = m_curCommandNode.Next;
            if (null != m_curCommandNode)
            {
                m_curCommandNode.Value.Exec();
            }
        }

        /// <summary>
        /// 返回所有可回撤的命令
        /// </summary>
        /// <returns></returns>
        public List<ICommand> GetAllGoBackCommands()
        {
            List<ICommand> commands = new List<ICommand>();

            LinkedListNode<ICommand> cCommandNode = m_curCommandNode;
            while (null != cCommandNode)
            {
                if ((null != cCommandNode) && (null != cCommandNode.Value) && (cCommandNode.Value != m_firstCommand))
                {
                    commands.Add(cCommandNode.Value);
                }
                cCommandNode = cCommandNode.Previous;
            }

            return commands;
        }

        /// <summary>
        /// 返回所有可重做的命令
        /// </summary>
        /// <returns></returns>
        public List<ICommand> GetAllGoBeforeCommands()
        {
            List<ICommand> commands = new List<ICommand>();

            LinkedListNode<ICommand> cCommandNode = m_curCommandNode;
            while (null != cCommandNode)
            {
                cCommandNode = cCommandNode.Next;
                if ((null != cCommandNode) && (null != cCommandNode.Value))
                {
                    commands.Add(cCommandNode.Value);
                }
            }

            return commands;
        }

        /// <summary>
        /// 注册拦截器
        /// </summary>
        /// <param name="interceptor"></param>
        public void Reg(ICommandInterceptor interceptor)
        {
            m_commandInterceptor.Add(interceptor);
        }

        /// <summary>
        /// 注销拦截器
        /// </summary>
        /// <param name="interceptor"></param>
        public void UnReg(ICommandInterceptor interceptor)
        {
            m_commandInterceptor.Remove(interceptor);
        }

        #region 成员变量
        private LinkedList<ICommand> m_command = new LinkedList<ICommand>();
        private LinkedListNode<ICommand> m_curCommandNode;//当前执行完的命令节点（该节点前包括自身的命令都已执行，之后的命令都没有执行(被撤消)）
        private static readonly NullCommand m_firstCommand = new NullCommand(null);
        private CommandInterceptorGroup m_commandInterceptor = new CommandInterceptorGroup();//命令拦截器
        #endregion
    }
}
