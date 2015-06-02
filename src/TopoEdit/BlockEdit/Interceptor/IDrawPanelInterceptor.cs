using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using TopoEdit.EventHandler;

namespace TopoEdit.Interceptor
{
    /// <summary>
    /// 绘图板拦截器
    /// </summary>
    public interface IDrawPanelEventInterceptor
    {
        void PaintBefore(object sender, RangePaintEventArgs e);
        void PaintAfter(object sender, RangePaintEventArgs e);
        void ProcessMouseDownBeforeHandle(object sender, RangeMouseEventArgs e);
        void ProcessMouseDownAfterHandle(object sender, RangeMouseEventArgs e);
        void ProcessMouseUpBeforeHandle(object sender, RangeMouseEventArgs e);
        void ProcessMouseUpAfterHandle(object sender, RangeMouseEventArgs e);
        void ProcessMouseMoveBeforeHandle(object sender, RangeMouseEventArgs e);
        void ProcessMouseMoveAfterHandle(object sender, RangeMouseEventArgs e);
        void ProcessMouseWheelBeforeHandle(object sender, RangeMouseEventArgs e);
        void ProcessMouseWheelAfterHandle(object sender, RangeMouseEventArgs e);
        void ProcessKeyDownBeforeHandle(object sender, RangeKeyEventArgs e);
        void ProcessKeyDownAfterHandle(object sender, RangeKeyEventArgs e);
        void ProcessKeyUpBeforeHandle(object sender, RangeKeyEventArgs e);
        void ProcessKeyUpAfterHandle(object sender, RangeKeyEventArgs e);
        void ProcessMouseClickBeforeHandle(object sender, RangeMouseEventArgs e);
        void ProcessMouseClickAfterHandle(object sender, RangeMouseEventArgs e);
        void ProcessMouseDoubleClickBeforeHandle(object sender, RangeMouseEventArgs e);
        void ProcessMouseDoubleClickAfterHandle(object sender, RangeMouseEventArgs e);
    }

    public abstract class DrawPanelEventInterceptor : IDrawPanelEventInterceptor
    {

        #region IDrawPanelEventInterceptor 成员

        public virtual void PaintBefore(object sender, RangePaintEventArgs e)
        {
            //有意留空
        }

        public virtual void PaintAfter(object sender, RangePaintEventArgs e)
        {
            //有意留空
        }

        public virtual void ProcessMouseDownBeforeHandle(object sender, RangeMouseEventArgs e)
        {
            //有意留空
        }

        public virtual void ProcessMouseDownAfterHandle(object sender, RangeMouseEventArgs e)
        {
            //有意留空
        }

        public virtual void ProcessMouseUpBeforeHandle(object sender, RangeMouseEventArgs e)
        {
            //有意留空
        }

        public virtual void ProcessMouseUpAfterHandle(object sender, RangeMouseEventArgs e)
        {
            //有意留空
        }

        public virtual void ProcessMouseMoveBeforeHandle(object sender, RangeMouseEventArgs e)
        {
            //有意留空
        }

        public virtual void ProcessMouseMoveAfterHandle(object sender, RangeMouseEventArgs e)
        {
            //有意留空
        }

        public virtual void ProcessMouseWheelBeforeHandle(object sender, RangeMouseEventArgs e)
        {
            //有意留空
        }

        public virtual void ProcessMouseWheelAfterHandle(object sender, RangeMouseEventArgs e)
        {
            //有意留空
        }

        public virtual void ProcessKeyDownBeforeHandle(object sender, RangeKeyEventArgs e)
        {
            //有意留空
        }

        public virtual void ProcessKeyDownAfterHandle(object sender, RangeKeyEventArgs e)
        {
            //有意留空
        }

        public virtual void ProcessKeyUpBeforeHandle(object sender, RangeKeyEventArgs e)
        {
            //有意留空
        }

        public virtual void ProcessKeyUpAfterHandle(object sender, RangeKeyEventArgs e)
        {
            //有意留空
        }

        public virtual void ProcessMouseClickBeforeHandle(object sender, RangeMouseEventArgs e)
        {
            //有意留空
        }

        public virtual void ProcessMouseClickAfterHandle(object sender, RangeMouseEventArgs e)
        {
            //有意留空
        }

        public virtual void ProcessMouseDoubleClickBeforeHandle(object sender, RangeMouseEventArgs e)
        {
            //有意留空
        }

        public virtual void ProcessMouseDoubleClickAfterHandle(object sender, RangeMouseEventArgs e)
        {
            //有意留空
        }

        #endregion
    }

    /// <summary>
    /// 拦截器组合：外部拦截器应使用拦截器组
    /// </summary>
    public class DrawPanelEventInterceptorGroup :  List<IDrawPanelEventInterceptor>, IDrawPanelEventInterceptor
    {
        #region IDrawPanelEventInterceptor 成员

        public void PaintBefore(object sender, RangePaintEventArgs e)
        {
            foreach (IDrawPanelEventInterceptor interceptor in this)
            {
                interceptor.PaintBefore(sender, e);
            }
        }

        public void PaintAfter(object sender, RangePaintEventArgs e)
        {
            foreach (IDrawPanelEventInterceptor interceptor in this)
            {
                interceptor.PaintAfter(sender, e);
            }
        }

        public void ProcessMouseDownBeforeHandle(object sender, RangeMouseEventArgs e)
        {
            foreach (IDrawPanelEventInterceptor interceptor in this)
            {
                interceptor.ProcessMouseDownBeforeHandle(sender, e);
            }
        }

        public void ProcessMouseDownAfterHandle(object sender, RangeMouseEventArgs e)
        {
            foreach (IDrawPanelEventInterceptor interceptor in this)
            {
                interceptor.ProcessMouseDownAfterHandle(sender, e);
            }
        }

        public void ProcessMouseUpBeforeHandle(object sender, RangeMouseEventArgs e)
        {
            foreach (IDrawPanelEventInterceptor interceptor in this)
            {
                interceptor.ProcessMouseUpBeforeHandle(sender, e);
            }
        }

        public void ProcessMouseUpAfterHandle(object sender, RangeMouseEventArgs e)
        {
            foreach (IDrawPanelEventInterceptor interceptor in this)
            {
                interceptor.ProcessMouseUpAfterHandle(sender, e);
            }
        }

        public void ProcessMouseMoveBeforeHandle(object sender, RangeMouseEventArgs e)
        {
            foreach (IDrawPanelEventInterceptor interceptor in this)
            {
                interceptor.ProcessMouseMoveBeforeHandle(sender, e);
            }
        }

        public void ProcessMouseMoveAfterHandle(object sender, RangeMouseEventArgs e)
        {
            foreach (IDrawPanelEventInterceptor interceptor in this)
            {
                interceptor.ProcessMouseMoveAfterHandle(sender, e);
            }
        }

        public void ProcessMouseWheelBeforeHandle(object sender, RangeMouseEventArgs e)
        {
            foreach (IDrawPanelEventInterceptor interceptor in this)
            {
                interceptor.ProcessMouseWheelBeforeHandle(sender, e);
            }
        }

        public void ProcessMouseWheelAfterHandle(object sender, RangeMouseEventArgs e)
        {
            foreach (IDrawPanelEventInterceptor interceptor in this)
            {
                interceptor.ProcessMouseWheelAfterHandle(sender, e);
            }
        }

        public void ProcessKeyDownBeforeHandle(object sender, RangeKeyEventArgs e)
        {
            foreach (IDrawPanelEventInterceptor interceptor in this)
            {
                interceptor.ProcessKeyDownBeforeHandle(sender, e);
            }
        }

        public void ProcessKeyDownAfterHandle(object sender, RangeKeyEventArgs e)
        {
            foreach (IDrawPanelEventInterceptor interceptor in this)
            {
                interceptor.ProcessKeyDownAfterHandle(sender, e);
            }
        }

        public void ProcessKeyUpBeforeHandle(object sender, RangeKeyEventArgs e)
        {
            foreach (IDrawPanelEventInterceptor interceptor in this)
            {
                interceptor.ProcessKeyUpBeforeHandle(sender, e);
            }
        }

        public void ProcessKeyUpAfterHandle(object sender, RangeKeyEventArgs e)
        {
            foreach (IDrawPanelEventInterceptor interceptor in this)
            {
                interceptor.ProcessKeyUpAfterHandle(sender, e);
            }
        }

        public void ProcessMouseClickBeforeHandle(object sender, RangeMouseEventArgs e)
        {
            foreach (IDrawPanelEventInterceptor interceptor in this)
            {
                interceptor.ProcessMouseClickBeforeHandle(sender, e);
            }
        }

        public void ProcessMouseClickAfterHandle(object sender, RangeMouseEventArgs e)
        {
            foreach (IDrawPanelEventInterceptor interceptor in this)
            {
                interceptor.ProcessMouseClickAfterHandle(sender, e);
            }
        }

        public void ProcessMouseDoubleClickBeforeHandle(object sender, RangeMouseEventArgs e)
        {
            foreach (IDrawPanelEventInterceptor interceptor in this)
            {
                interceptor.ProcessMouseDoubleClickBeforeHandle(sender, e);
            }
        }

        public void ProcessMouseDoubleClickAfterHandle(object sender, RangeMouseEventArgs e)
        {
            foreach (IDrawPanelEventInterceptor interceptor in this)
            {
                interceptor.ProcessMouseDoubleClickAfterHandle(sender, e);
            }
        }

        #endregion
    }
}
