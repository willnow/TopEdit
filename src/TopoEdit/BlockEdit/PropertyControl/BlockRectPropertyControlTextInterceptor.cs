using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using TopoEdit.Icon;

namespace TopoEdit.PropertyControl
{
    class BlockRectPropertyControlTextInterceptor :  IPropertyControlInterceptor
    {
        /// <summary>
        /// 在保存数据时进行拦截
        /// </summary>
        /// <param name="para"></param>
        public override void SaveDataHandle(PropertyControlInterceptorPara para)
        {
            Debug.Assert(para.Draw is BlockRect);

            BlockRect blockRect = para.Draw as BlockRect;

            if (null != blockRect)
            {
                //判断是否符合拦截要求:仅包含一个文本图元
                Block block = blockRect.Template as Block;
                if ((block.Count == 1) && (block.GetIcon(0) is IconText))
                {
                    blockRect.Text = blockRect.Name;
                }
            }
        }

        /// <summary>
        /// 在加载数据时进行拦截
        /// </summary>
        /// <param name="para"></param>
        public override void LoadDataHandle(PropertyControlInterceptorPara para)
        {
            Debug.Assert(para.Draw is BlockRect);

            BlockRect blockRect = para.Draw as BlockRect;

            if (null != blockRect)
            {
                //判断是否符合拦截要求:仅包含一个文本图元
                Block block = blockRect.Template as Block;
                if ((block.Count == 1) && (block.GetIcon(0) is IconText))
                {
                    IconText text =  block.GetIcon(0) as IconText;
                    blockRect.Name = text.Text;

                    BlockRectPropertyControl control = para.PropertyControl as BlockRectPropertyControl;
                    control.NameEditEnable = text.Enable;
                }
            }
        }
    }
}
