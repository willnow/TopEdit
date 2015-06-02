using System;
using System.Collections.Generic;
using System.Text;
using TopoEdit.Icon;
using System.Drawing;

namespace TopoEdit.Stratege
{
    /// <summary>
    /// 对齐方式
    /// </summary>
    public enum EmAlign
    {
        Left,
        Center,
        Right,
        Top,
        Middle,
        Buttom,
    }

    public class AlignHelper
    {
        /// <summary>
        /// 对齐策略
        /// </summary>
        private Dictionary<EmAlign, IAlignStrategy> m_alignStrategy = new Dictionary<EmAlign, IAlignStrategy>();

        public AlignHelper()
        {
            m_alignStrategy.Add(EmAlign.Left, new AlignLeftStrategy());
            m_alignStrategy.Add(EmAlign.Right, new AlignRightStrategy());

            m_alignStrategy.Add(EmAlign.Top, new AlignTopStrategy());
            m_alignStrategy.Add(EmAlign.Buttom, new AlignBottomStrategy());

            m_alignStrategy.Add(EmAlign.Center, new AlignCenterStrategy());
            m_alignStrategy.Add(EmAlign.Middle, new AlignMiddleStrategy());
        }

        private void SetRef(EmAlign emAlign, RectangleF refDraw)
        {
            switch (emAlign)
            {
                case EmAlign.Left:
                    {
                        m_alignStrategy[emAlign].SetRef(refDraw.Left);
                        break;
                    }
                case EmAlign.Right:
                    {
                        m_alignStrategy[emAlign].SetRef(refDraw.Right);
                        break;
                    }
                case EmAlign.Center:
                    {
                        m_alignStrategy[emAlign].SetRef((refDraw.Left + refDraw.Right) / 2);
                        break;
                    }
                case EmAlign.Top:
                    {
                        m_alignStrategy[emAlign].SetRef(refDraw.Top);
                        break;
                    }
                case EmAlign.Buttom:
                    {
                        m_alignStrategy[emAlign].SetRef(refDraw.Bottom);
                        break;
                    }
                case EmAlign.Middle:
                    {
                        m_alignStrategy[emAlign].SetRef((refDraw.Top + refDraw.Bottom) / 2);
                        break;
                    }
            }
        }

        /// <summary>
        /// 使用指定对齐方式和对齐参考点对齐指定视图中的指定图元
        /// </summary>
        /// <param name="view">视图</param>
        /// <param name="range">被对齐图元</param>
        /// <param name="emAlign">对齐方式</param>
        /// <param name="refDraw">参考图元</param>
        public void Align(IBaseDrawPanel view, SelectedRange range, EmAlign emAlign, RectangleF refDraw)
        {
            if (!m_alignStrategy.ContainsKey(emAlign) || (m_alignStrategy[emAlign] == null))
            {
                return;
            }
            else
            {
                SetRef(emAlign, refDraw);
                m_alignStrategy[emAlign].Align(view, range);
            }
        }
    }
}
