using System;
using System.Collections.Generic;
using System.Text;
using TopoEdit.Icon;
using System.Drawing;
using TopoEdit.Visitor;
using System.Diagnostics;

namespace TopoEdit.Command
{
    public class UpdateDrawCommand : ICommand
    {
        /// <summary>
        /// 被修改的图元
        /// </summary>
        protected IDraw m_updateDraw;
        /// <summary>
        /// 更新后的值
        /// </summary>
        protected IDraw m_newValue;
        /// <summary>
        /// 更新前的值
        /// </summary>
        protected IDraw m_oldValue;

        public IDraw UpdatedDraw
        {
            get
            {
                return m_updateDraw;
            }
        }

        public IDraw NewValue
        {
            get
            {
                return m_newValue;
            }
        }

        public IDraw OldValue
        {
            get
            {
                return m_oldValue;
            }
        }

        public UpdateDrawCommand(IBaseDrawPanel view, IDraw updateDraw, IDraw newDraw, IDraw oldDraw)
            : base(view)
        {
            Debug.Assert(view != null);
            m_newValue = newDraw.Clone();
            m_updateDraw = updateDraw;
            m_oldValue = oldDraw.Clone();
        }

        #region ICommand 成员

        public override void InnerExec()
        {
            RectangleF rect = new RectangleF();
            //将移动前区域加入刷新区域
            rect = TopoEdit.Utility.Union(rect, m_updateDraw.BoundsRect);
            //操作
            m_updateDraw.Copy(m_newValue);
            //将添加后区域加入刷新区域
            rect = TopoEdit.Utility.Union(rect, m_updateDraw.BoundsRect);
            View.Refresh(Utility.AdjustRect(rect, 10));
        }

        public override void InnerUnExec()
        {
            RectangleF rect = new RectangleF();
            //将移动前区域加入刷新区域
            rect = TopoEdit.Utility.Union(rect, m_updateDraw.BoundsRect);
            //操作
            m_updateDraw.Copy(m_oldValue);
            //将添加后区域加入刷新区域
            rect = TopoEdit.Utility.Union(rect, m_updateDraw.BoundsRect);
            View.Refresh(Utility.AdjustRect(rect, 10));
        }

        public override void Accept(ICommandVisitor visitor)
        {
            visitor.VisitUpdateDrawCommand(this);
        }

        public override string ToString()
        {
            return "更新了一个图元";
        }

        #endregion
    }
}
