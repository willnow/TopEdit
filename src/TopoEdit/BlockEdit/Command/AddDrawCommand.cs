using System;
using System.Collections.Generic;
using System.Text;
using TopoEdit.Icon;
using System.Drawing;
using TopoEdit.Interceptor;
using TopoEdit.Visitor;
using System.Diagnostics;

namespace TopoEdit.Command
{
    public class AddDrawCommand : ICommand
    {
        /// <summary>
        /// 被添加的图元
        /// </summary>
        private List<IDraw> m_addDraw;
        /// <summary>
        /// 图元被添加到的区域
        /// </summary>
        private IRange m_addRange;

        public IRange AddRange
        {
            get
            {
                return m_addRange;
            }
        }

        public List<IDraw> AddDraw
        {
            get
            {
                return m_addDraw;
            }
        }

        public AddDrawCommand(IBaseDrawPanel view, IRange addRange, List<IDraw> addDraw)
            : base(view)
        {
            Debug.Assert(view != null);
            m_addDraw = addDraw;
            m_addRange = addRange;
        }

        public AddDrawCommand(IBaseDrawPanel view, IRange addRange, IDraw addDraw)
            : base(view)
        {
            m_addDraw = new List<IDraw>();
            m_addDraw.Add(addDraw);
            m_addRange = addRange;
        }

        #region ICommand 成员

        public override void InnerExec()
        {
            RectangleF rect = new RectangleF();
            //将移动前区域加入刷新区域
            rect = TopoEdit.Utility.Union(rect, m_addRange.BoundsRect);
            //操作
            m_addRange.ClearSelectIcon();
            m_addRange.AddRange(m_addDraw);
            //将添加后区域加入刷新区域
            rect = TopoEdit.Utility.Union(rect, m_addRange.BoundsRect);
            View.Refresh(Utility.AdjustRect(rect, 10));
        }

        public override void InnerUnExec()
        {
            RectangleF rect = new RectangleF();
            //将移动前区域加入刷新区域
            rect = TopoEdit.Utility.Union(rect, m_addRange.BoundsRect);
            //操作
            m_addRange.ClearSelectIcon();
            m_addRange.RemoveRange(m_addDraw);
            //将操作后区域加入刷新区域
            rect = TopoEdit.Utility.Union(rect, m_addRange.BoundsRect);
            View.Refresh(Utility.AdjustRect(rect, 10));
        }

        public override void Accept(ICommandVisitor visitor)
        {
            visitor.VisitAddDrawCommand(this);
        }


        public override string ToString()
        {
            return "添加了" + m_addDraw.Count + "个图元";
        }

        #endregion
    }
}
