using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TopoEdit.Command;

namespace TopoEdit.Interceptor
{
    public interface ICommandInterceptor
    {
        //AddDrawCommand拦截器
        void ExecAddDrawBefore(AddDrawCommand command);
        void ExecAddDrawAfter(AddDrawCommand command);
        void UnExecAddDrawBefore(AddDrawCommand command);
        void UnExecAddDrawAfter(AddDrawCommand command);

        //DelDrawCommand拦截器
        void ExecDelDrawBefore(DelDrawCommand command);
        void ExecDelDrawAfter(DelDrawCommand command);
        void UnExecDelDrawBefore(DelDrawCommand command);
        void UnExecDelDrawAfter(DelDrawCommand command);

        //MoveDrawCommand拦截器
        void ExecMoveDrawBefore(MoveDrawCommand command);
        void ExecMoveDrawAfter(MoveDrawCommand command);
        void UnExecMoveDrawBefore(MoveDrawCommand command);
        void UnExecMoveDrawAfter(MoveDrawCommand command);

        //UpdateDrawCommand拦截器
        void ExecUpdateDrawBefore(UpdateDrawCommand command);
        void ExecUpdateDrawAfter(UpdateDrawCommand command);
        void UnExecUpdateDrawBefore(UpdateDrawCommand command);
        void UnExecUpdateDrawAfter(UpdateDrawCommand command);

        //UpdateSelectedDrawCommand拦截器
        void ExecUpdateSelectedDrawBefore(UpdateSelectedDrawCommand command);
        void ExecUpdateSelectedDrawAfter(UpdateSelectedDrawCommand command);
        void UnExecUpdateSelectedDrawBefore(UpdateSelectedDrawCommand command);
        void UnExecUpdateSelectedDrawAfter(UpdateSelectedDrawCommand command);
    }

    /// <summary>
    /// 拦截器组合：外部拦截器应使用拦截器组
    /// </summary>
    class CommandInterceptorGroup : List<ICommandInterceptor>, ICommandInterceptor
    {

        #region ICommandInterceptor 成员

        public void ExecAddDrawBefore(AddDrawCommand command)
        {
           foreach (ICommandInterceptor interceptor in this)
           {
               interceptor.ExecAddDrawBefore(command);
           }
        }

        public void ExecAddDrawAfter(AddDrawCommand command)
        {
            foreach (ICommandInterceptor interceptor in this)
            {
                interceptor.ExecAddDrawAfter(command);
            }
        }

        public void UnExecAddDrawBefore(AddDrawCommand command)
        {
            foreach (ICommandInterceptor interceptor in this)
            {
                interceptor.UnExecAddDrawBefore(command);
            }
        }

        public void UnExecAddDrawAfter(AddDrawCommand command)
        {
            foreach (ICommandInterceptor interceptor in this)
            {
                interceptor.UnExecAddDrawAfter(command);
            }
        }

        public void ExecDelDrawBefore(DelDrawCommand command)
        {
            foreach (ICommandInterceptor interceptor in this)
            {
                interceptor.ExecDelDrawBefore(command);
            }
        }

        public void ExecDelDrawAfter(DelDrawCommand command)
        {
            foreach (ICommandInterceptor interceptor in this)
            {
                interceptor.ExecDelDrawAfter(command);
            }
        }

        public void UnExecDelDrawBefore(DelDrawCommand command)
        {
            foreach (ICommandInterceptor interceptor in this)
            {
                interceptor.UnExecDelDrawBefore(command);
            }
        }

        public void UnExecDelDrawAfter(DelDrawCommand command)
        {
            foreach (ICommandInterceptor interceptor in this)
            {
                interceptor.UnExecDelDrawAfter(command);
            }
        }

        public void ExecMoveDrawBefore(MoveDrawCommand command)
        {
            foreach (ICommandInterceptor interceptor in this)
            {
                interceptor.ExecMoveDrawBefore(command);
            }
        }

        public void ExecMoveDrawAfter(MoveDrawCommand command)
        {
            foreach (ICommandInterceptor interceptor in this)
            {
                interceptor.ExecMoveDrawAfter(command);
            }
        }

        public void UnExecMoveDrawBefore(MoveDrawCommand command)
        {
            foreach (ICommandInterceptor interceptor in this)
            {
                interceptor.UnExecMoveDrawBefore(command);
            }
        }

        public void UnExecMoveDrawAfter(MoveDrawCommand command)
        {
            foreach (ICommandInterceptor interceptor in this)
            {
                interceptor.UnExecMoveDrawAfter(command);
            }
        }

        public void ExecUpdateDrawBefore(UpdateDrawCommand command)
        {
            foreach (ICommandInterceptor interceptor in this)
            {
                interceptor.ExecUpdateDrawBefore(command);
            }
        }

        public void ExecUpdateDrawAfter(UpdateDrawCommand command)
        {
            foreach (ICommandInterceptor interceptor in this)
            {
                interceptor.ExecUpdateDrawAfter(command);
            }
        }

        public void UnExecUpdateDrawBefore(UpdateDrawCommand command)
        {
            foreach (ICommandInterceptor interceptor in this)
            {
                interceptor.UnExecUpdateDrawBefore(command);
            }
        }

        public void UnExecUpdateDrawAfter(UpdateDrawCommand command)
        {
            foreach (ICommandInterceptor interceptor in this)
            {
                interceptor.UnExecUpdateDrawAfter(command);
            }
        }

        public void ExecUpdateSelectedDrawBefore(UpdateSelectedDrawCommand command)
        {
            foreach (ICommandInterceptor interceptor in this)
            {
                interceptor.ExecUpdateSelectedDrawBefore(command);
            }
        }

        public void ExecUpdateSelectedDrawAfter(UpdateSelectedDrawCommand command)
        {
            foreach (ICommandInterceptor interceptor in this)
            {
                interceptor.ExecUpdateSelectedDrawAfter(command);
            }
        }

        public void UnExecUpdateSelectedDrawBefore(UpdateSelectedDrawCommand command)
        {
            foreach (ICommandInterceptor interceptor in this)
            {
                interceptor.UnExecUpdateSelectedDrawBefore(command);
            }
        }

        public void UnExecUpdateSelectedDrawAfter(UpdateSelectedDrawCommand command)
        {
            foreach (ICommandInterceptor interceptor in this)
            {
                interceptor.UnExecUpdateSelectedDrawAfter(command);
            }
        }

        #endregion
    }
}
