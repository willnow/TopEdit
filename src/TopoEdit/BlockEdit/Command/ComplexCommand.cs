using System;
using System.Collections.Generic;
using System.Text;
using TopoEdit.Visitor;

namespace TopoEdit.Command
{
    /// <summary>
    /// 组成而成的复杂命令
    /// </summary>
    class ComplexCommand : ICommand
    {
        private List<ICommand> m_commands = new List<ICommand>();
        /// <summary>
        /// 命令提示
        /// </summary>
        private string m_tip = "";

        public ComplexCommand()
            : base(null)
        {
            //有意留空
        }

        public string Tip
        {
            get
            {
                return m_tip;
            }
            set
            {
                m_tip = value;
            }
        }

        public void AddCommand(ICommand command)
        {
            m_commands.Add(command);
        }

        public override void Exec()
        {
            foreach (ICommand command in m_commands)
            {
                command.Exec();
            }
        }

        public override void UnExec()
        {
            foreach (ICommand command in m_commands)
            {
                command.UnExec();
            }
        }

        public override void InnerExec()
        {
            foreach (ICommand command in m_commands)
            {
                command.InnerExec();
            }
        }

        public override void InnerUnExec()
        {
            foreach (ICommand command in m_commands)
            {
                command.InnerUnExec();
            }
        }

        public override void Accept(ICommandVisitor visitor)
        {
            foreach (ICommand command in m_commands)
            {
                command.Accept(visitor);
            }
        }

        public override string ToString()
        {
            if (m_commands.Count == 1)
            {
                return m_commands[0].ToString();
            }
            else 
            {
                if (m_tip == "")
                {
                    return  "一个包含了" + m_commands.Count + "个命令的复合命令";
                }
                else
                {
                    return m_tip + ", 包含" + m_commands.Count + "个命令的复合命令";
                }
            }
        }
    }
}
