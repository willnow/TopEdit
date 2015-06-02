using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace TopoEdit
{
    public interface IHelpHandle
    {
        void SetSuccessor(IHelpHandle handle);
        void HandleHelp(string text);
    }
    /// <summary>
    /// IHelpHandle的默认实现
    /// </summary>
    public class HelpHandle : IHelpHandle
    {
        #region IHelpHandle 成员

        public void SetSuccessor(IHelpHandle handle)
        {
            m_cSuccessor = handle;
        }

        public void HandleHelp(string text)
        {
            if (null != m_cSuccessor)
            {
                m_cSuccessor.HandleHelp(text);
            }
            else
            {
                MessageBox.Show(text);
            }
        }

        #endregion

        private IHelpHandle m_cSuccessor;//后继者
    }
}
