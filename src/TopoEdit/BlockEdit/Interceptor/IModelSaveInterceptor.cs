using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TopoEdit.Icon;

namespace TopoEdit.Interceptor
{
    /// <summary>
    /// 保存模型拦截器
    /// </summary>
    public interface IModelSaveInterceptor
    {
        //Save Block
        void SaveBlockBefore(Block block);
        void SaveBlockAfter(Block block);

        //Save Page
        void SavePageBefore(Page page);
        void SavePageAfter(Page page);

        //Save Book
        void SaveBookBefore(Book book);
        void SaveBookAfter(Book book);
    }

    /// <summary>
    /// 拦截器组合：外部拦截器应使用拦截器组
    /// </summary>
    public class ModelSaveInterceptorGroup : List<IModelSaveInterceptor>, IModelSaveInterceptor
    {
        #region IModelSaveInterceptor 成员

        public void SaveBlockBefore(Block block)
        {
            foreach (IModelSaveInterceptor interceptor in this)
            {
                interceptor.SaveBlockBefore(block);
            }
        }

        public void SaveBlockAfter(Block block)
        {
            foreach (IModelSaveInterceptor interceptor in this)
            {
                interceptor.SaveBlockAfter(block);
            }
        }

        public void SavePageBefore(Page page)
        {
            foreach (IModelSaveInterceptor interceptor in this)
            {
                interceptor.SavePageBefore(page);
            }
        }

        public void SavePageAfter(Page page)
        {
            foreach (IModelSaveInterceptor interceptor in this)
            {
                interceptor.SavePageAfter(page);
            }
        }

        public void SaveBookBefore(Book book)
        {
            foreach (IModelSaveInterceptor interceptor in this)
            {
                interceptor.SaveBookBefore(book);
            }
        }

        public void SaveBookAfter(Book book)
        {
            foreach (IModelSaveInterceptor interceptor in this)
            {
                interceptor.SaveBookAfter(book);
            }
        }

        #endregion
    }
}
