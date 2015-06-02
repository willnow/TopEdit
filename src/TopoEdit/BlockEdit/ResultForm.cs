using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TopoEdit
{
    public partial class ResultForm : Form
    {
        public ResultForm()
        {
            InitializeComponent();
        }

        public void SetDisplayInfo(List<ErrorInfo> infoList)
        {
            System.Drawing.Icon cErrorImage = TopoEdit.Properties.Resources.messageboxerror;
            System.Drawing.Icon cAlarmImage = TopoEdit.Properties.Resources.messageboxalert;
            System.Drawing.Icon cInfoImage = TopoEdit.Properties.Resources.messageboxinfo;
            int index = 1;

            //首先添加错误
            foreach (ErrorInfo info in infoList)
            {
                if (info.ErrType == ErrorType.error)
                {
                    dgvResult.Rows.Add(cErrorImage, index, info.Content);
                    ++index;
                }
            }

            //然后添加警告
            foreach (ErrorInfo info in infoList)
            {
                if (info.ErrType == ErrorType.alarm)
                {
                    dgvResult.Rows.Add(cAlarmImage, index, info.Content);
                    ++index;
                }
            }

            //最后添加提示
            foreach (ErrorInfo info in infoList)
            {
                if (info.ErrType == ErrorType.info)
                {
                    dgvResult.Rows.Add(cInfoImage, index, info.Content);
                    ++index;
                }
            }
        }
    }

    public enum ErrorType
    {
        error,
        alarm,
        info,
    }

    public class ErrorInfo
    {
        private ErrorType m_type;
        private string m_content;

        public ErrorType ErrType
        {
            get
            {
                return m_type;
            }
            set
            {
                m_type = value;
            }
        }

        public string Content
        {
            get
            {
                return m_content;
            }
            set
            {
                m_content = value;
            }
        }

        public ErrorInfo(ErrorType errType, string content)
        {
            m_type = errType;
            m_content = content;
        }
    }
}
