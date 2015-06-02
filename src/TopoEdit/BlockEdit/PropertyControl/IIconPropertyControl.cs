using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using TopoEdit.Icon;

namespace TopoEdit.PropertyControl
{
    public partial class IDrawPropertyControl : UserControl
    {
        /// <summary>
        /// 需要设置属性的ICON
        /// </summary>
        protected IDraw m_draw;
        /// <summary>
        /// 拦截器
        /// </summary>
        protected List<IPropertyControlInterceptor> m_interceptors = new List<IPropertyControlInterceptor>();

        public void Add(IPropertyControlInterceptor interceptor)
        {
            m_interceptors.Add(interceptor);
        }

        public void Remove(IPropertyControlInterceptor interceptor)
        {
            m_interceptors.Remove(interceptor);
        }

        public IDrawPropertyControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 保存属性设置到ICON
        /// </summary>
        public void SaveData(IDraw icon)
        {
            InternalSaveData();

            //数据保存后拦截
            PropertyControlInterceptorPara para = new PropertyControlInterceptorPara(this, m_draw);
            foreach (IPropertyControlInterceptor interceptor in m_interceptors)
            {
                interceptor.SaveDataHandle(para);
                if (para.IsOver)
                {
                    break;
                }
            }

            icon = para.Draw;
        }
        /// <summary>
        /// 从ICON加载属性
        /// </summary>
        /// <param name="icon"></param>
        public void LoadData(IDraw icon)
        {
            //加载数据前拦截
            PropertyControlInterceptorPara para = new PropertyControlInterceptorPara(this, icon);
            foreach (IPropertyControlInterceptor interceptor in m_interceptors)
            {
                interceptor.LoadDataHandle(para);
                if (para.IsOver)
                {
                    break;
                }
            }

            m_draw = para.Draw;
            InternalLoadData();
        }

        /// <summary>
        /// 保存属性设置到ICON
        /// </summary>
        public virtual void InternalSaveData()
        {
            //有意留空
        }
        /// <summary>
        /// 从ICON加载属性
        /// </summary>
        /// <param name="icon"></param>
        public virtual void InternalLoadData()
        {
            //有意留空
        }
    }
}
