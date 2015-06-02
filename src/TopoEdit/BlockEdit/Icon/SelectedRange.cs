using System;
using System.Collections.Generic;
using System.Text;
using TopoEdit.Icon;
using System.Diagnostics;

namespace TopoEdit.Icon
{
    /// <summary>
    /// 选中区域
    /// </summary>
    public class SelectedRange : IRange
    {
        /// <summary>
        /// 作为对齐参考的图元
        /// </summary>
        private IDraw m_alignRef = null;

        public SelectedRange()
        {
            //有意留空
        }

        internal override void Add(IDraw icon)
        {
            m_draws.Add(icon.CreateSelectedDraw());
            m_alignRef = m_draws[0];
        }

        internal override void Remove(IDraw icon)
        {
            Debug.Assert(icon is SelectedItem);
            m_draws.Remove(icon);
            m_alignRef = m_draws[0];
        }

        public SelectedDraw AlignRef
        {
            get
            {
                return m_alignRef as SelectedDraw;
            }
            set
            {
                m_alignRef = value;
            }
        }

        /// <summary>
        /// 对于选中区域，当需要获取ICON时，返回选中区域中原始ICON（不包含锚点）
        /// </summary>
        public override List<IDraw> Icons
        {
            get 
            {
                Check();

                List<IDraw> icons = new List<IDraw>();
                foreach (IDraw icon in m_draws)
                {
                    icons.Add(((SelectedDraw)icon).Icon);
                }
                return icons; 
            }
        }

        /// <summary>
        /// 获取选中区域中所有被选图标（包含锚点）
        /// </summary>
        internal List<IDraw> SelIcons
        {
            get
            {
                return m_draws;
            }
        }

        private void Check()
        {
            foreach (IDraw icon in m_draws)
            {
                Debug.Assert(icon is SelectedDraw);
            }
        }

        public override IDraw Clone()
        {
            IRange range = new SelectedRange();
            range.Copy(this);
            return range;
        }

        public override void Copy(IDraw src)
        {
            if (src is SelectedRange)
            {
                SelectedRange srcItem = src as SelectedRange;

                Clear();
                foreach (IDraw icon in srcItem.m_draws)
                {
                    this.Add(icon.Clone());
                }
            }
            else
            {
                throw new ArgumentException("被拷贝图元和目标图元类型不兼容", "src");
            }
        }

        public override void Accept(TopoEdit.Visitor.IDrawVisitor visitor)
        {
            visitor.VisitorSelectedRange(this);
        }
    }
}
