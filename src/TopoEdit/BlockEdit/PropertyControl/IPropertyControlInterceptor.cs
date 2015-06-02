using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using TopoEdit.Icon;

namespace TopoEdit.PropertyControl
{
    /// <summary>
    /// 属性控件拦截器控制参数
    /// </summary>
    public class PropertyControlInterceptorPara
    {
        /// <summary>
        /// 是否处理完毕
        /// </summary>
        private bool m_isOver;
        /// <summary>
        /// 属性控件
        /// </summary>
        private IDrawPropertyControl m_propertyControl;
        /// <summary>
        /// 需要设置属性的ICON
        /// </summary>
        protected IDraw m_draw;

        public  bool IsOver
        {
            get { return m_isOver; }
        }

        public IDrawPropertyControl PropertyControl
        {
            get { return m_propertyControl; }
            set { m_propertyControl = value; }
        }

        public IDraw Draw
        {
            get { return m_draw; }
        }

        public PropertyControlInterceptorPara(IDrawPropertyControl propertyControl, IDraw draw)
        {
            Debug.Assert(propertyControl != null);
            Debug.Assert(draw != null);

            m_isOver = false;
            m_propertyControl = propertyControl;
            m_draw = draw;

            Debug.Assert(m_propertyControl != null);
            Debug.Assert(m_draw != null);
        }
    }
    /// <summary>
    /// 属性控件拦截器，在一般处理流程前可以对一些特殊情况进行拦截处理
    /// </summary>
    public abstract class IPropertyControlInterceptor
    {
        /// <summary>
        /// 在保存数据时进行拦截
        /// </summary>
        /// <param name="para"></param>
        public abstract void SaveDataHandle(PropertyControlInterceptorPara para);
        /// <summary>
        /// 在加载数据时进行拦截
        /// </summary>
        /// <param name="para"></param>
        public abstract void LoadDataHandle(PropertyControlInterceptorPara para);
    }
}
