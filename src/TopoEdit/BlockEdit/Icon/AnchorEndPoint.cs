using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace TopoEdit.Icon
{
    /// <summary>
    /// 端点锚点（一般用于多边形，在多边形的各个顶点上设置锚点）
    /// </summary>
    public class AnchorEndPoint : Anchor
    {
        /// <summary>
        /// 端点在关联图元上的端点索引
        /// </summary>
        private int m_index = 0;
        /// <summary>
        /// 索引显示的文本框
        /// </summary>
        private IconText m_text = null;
        
        public int Index
        {
            get
            {
                return m_index;
            }
        }

        public AnchorEndPoint(SelectedDraw draw, AnchorType anchorType, int index)
            : base(draw, anchorType)
        {
            m_index = index;
            m_text = new IconText();
            m_text.Value = m_index.ToString();
            m_text.Width = 20;
            m_text.Height = 20;
            m_text.FontSize = 10;
           
            //将text放到锚点的上边
            PointF txtPos = new PointF();
            txtPos.X = Rect.BoundsRect.Location.X;
            txtPos.Y = Rect.BoundsRect.Location.Y;

            //Rect.DefaultColor.ColorInArgb = Color.Gray;
        }

        public override void Draw(System.Drawing.Graphics cGraphics, System.Drawing.RectangleF cRect)
        {
            base.Draw(cGraphics, cRect);
            m_text.Draw(cGraphics, cRect);
        }

        public override void Zoom(Zoom zoom)
        {
            base.Zoom(zoom);
            m_text.Zoom(zoom);
        }

        public override void Rotate(CSR.ShareLib.Rotate rotate)
        {
            base.Rotate(rotate);
            m_text.Rotate(rotate);
        }

        public override void Move(Movement move)
        {
            base.Move(move);
            m_text.Move(move);
        }
    }
}
