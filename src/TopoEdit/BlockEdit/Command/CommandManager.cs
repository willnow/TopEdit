//******************************************************************************
//�ļ����� :     CommandManager.cs
//��Ȩ��Ϣ :     �����ϳ�ʱ����Ϣ�������޹�˾ ��Ȩ����
//�������� :     2014-07-04
//�ļ����� :     
//�޸����� :

//******************************************************************************
using System;
using System.Collections.Generic;
using System.Text;
using TopoEdit.Interceptor;

namespace TopoEdit.Command
{
    //���������
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
        /// ����һ�����Ȼ��ִ�и�����
        /// </summary>
        /// <param name="cCommand"></param>
        public void AddThenExec(ICommand cCommand)
        {
            AddCommand(cCommand);
            cCommand.Exec();
        }
        /// <summary>
        /// ����������Ȼ��ɾ��������
        /// </summary>
        /// <param name="cCommand"></param>
        public void UnExecThenDel(ICommand cCommand)
        {
            cCommand.UnExec();
            DelCommand(cCommand);
        }

        public void AddCommand(ICommand cCommand)
        {
            //�����ǰ����֮�����������
            LinkedListNode<ICommand> cCommandNode = m_curCommandNode;
            if (null != cCommandNode)
            {
                while (null != m_curCommandNode.Next)
                {
                    //�����ǰ����֮�����������������ǰ����֮��ĵ�һ������
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
        /// ��ִ���������ڵ�ָ����ǰ����
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
        /// �ֶ���������
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
        /// �ֶ��ָ�����
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
        /// �������пɻس�������
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
        /// �������п�����������
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
        /// ע��������
        /// </summary>
        /// <param name="interceptor"></param>
        public void Reg(ICommandInterceptor interceptor)
        {
            m_commandInterceptor.Add(interceptor);
        }

        /// <summary>
        /// ע��������
        /// </summary>
        /// <param name="interceptor"></param>
        public void UnReg(ICommandInterceptor interceptor)
        {
            m_commandInterceptor.Remove(interceptor);
        }

        #region ��Ա����
        private LinkedList<ICommand> m_command = new LinkedList<ICommand>();
        private LinkedListNode<ICommand> m_curCommandNode;//��ǰִ���������ڵ㣨�ýڵ�ǰ��������������ִ�У�֮������û��ִ��(������)��
        private static readonly NullCommand m_firstCommand = new NullCommand(null);
        private CommandInterceptorGroup m_commandInterceptor = new CommandInterceptorGroup();//����������
        #endregion
    }
}
