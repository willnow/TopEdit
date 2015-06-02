using System;
using System.Collections.Generic;
using System.Text;
using TopoEdit.Visitor;
using System.Drawing;
using System.Diagnostics;

namespace TopoEdit.Icon
{
    public enum AnchorType
    {
        LeftTop,
        RightTop,
        LeftBottom,
        RightBottom,
        Left,
        Top,
        Right,
        Bottom,
        EndPoint,
        Control,
        None,
    }

    public class Anchor : IDraw
    {
        /// <summary>
        /// 锚点矩形
        /// </summary>
        private IconRectangle m_rect;
         /// <summary>
        /// 锚点类型
        /// </summary>
        private AnchorType m_anchorType;
        /// <summary>
        /// 锚点关联的图元
        /// </summary>
        private SelectedDraw m_selDraw;

        public IconRectangle Rect
        {
            get
            {
                return m_rect;
            }
        }

        public AnchorType GeneralAnchorType
        {
            get
            {
                return m_anchorType;
            }
        }

        public Anchor(SelectedDraw draw, AnchorType anchorType)
        {
            m_selDraw = draw;
            m_anchorType = anchorType;
            //初始化锚点为5*5大小正方形
            m_rect = new IconRectangle(new PointF(0.0F, 0.0F), 5.0f, 5.0f, true, System.Drawing.Drawing2D.DashStyle.Solid);
            m_rect.Level = int.MaxValue;
            m_rect.IntersectIconType = IntersectType.InBound;
            m_rect.Fixed = true;//锚点的大小时固定不变的
        }

        public override System.Drawing.RectangleF BoundsRect
        {
            get { return m_rect.BoundsRect; }
        }

        public override bool VisibleTransparentColor
        {
            get
            {
                return true;
            }
            set
            {
                //有意留空
            }
        }

        public PointF GetAnchorCenter()
        {
            AnchorType anchorType = m_anchorType;
            SizeF anchorSize = BoundsRect.Size;
            PointF pointCenter = new PointF();

            IDraw draw = m_selDraw.Icon;
            switch (anchorType)
            {
                case AnchorType.Bottom:
                    {
                        pointCenter.X = draw.BoundsRect.Location.X + draw.BoundsRect.Width / 2.0f;
                        pointCenter.Y = draw.BoundsRect.Location.Y + draw.BoundsRect.Height + anchorSize.Height / 2.0f;
                        break;
                    }
                case AnchorType.Left:
                    {
                        pointCenter.X = draw.BoundsRect.Location.X - anchorSize.Width / 2.0f;
                        pointCenter.Y = draw.BoundsRect.Location.Y + draw.BoundsRect.Height / 2.0f;
                        break;
                    }
                case AnchorType.LeftBottom:
                    {
                        pointCenter.X = draw.BoundsRect.Location.X - anchorSize.Width / 2.0f;
                        pointCenter.Y = draw.BoundsRect.Location.Y + draw.BoundsRect.Height + anchorSize.Height / 2.0f;
                        break;
                    }
                case AnchorType.LeftTop:
                    {
                        pointCenter.X = draw.BoundsRect.Location.X - anchorSize.Width / 2.0f;
                        pointCenter.Y = draw.BoundsRect.Location.Y - anchorSize.Height / 2.0f;
                        break;
                    }
                case AnchorType.Right:
                    {
                        pointCenter.X = draw.BoundsRect.Location.X + draw.BoundsRect.Width + anchorSize.Width / 2.0f;
                        pointCenter.Y = draw.BoundsRect.Location.Y + draw.BoundsRect.Height / 2.0f;
                        break;
                    }
                case AnchorType.RightBottom:
                    {
                        pointCenter.X = draw.BoundsRect.Location.X + draw.BoundsRect.Width + anchorSize.Width / 2.0f;
                        pointCenter.Y = draw.BoundsRect.Location.Y + draw.BoundsRect.Height + anchorSize.Height / 2.0f;
                        break;
                    }
                case AnchorType.RightTop:
                    {
                        pointCenter.X = draw.BoundsRect.Location.X + draw.BoundsRect.Width + anchorSize.Width / 2.0f;
                        pointCenter.Y = draw.BoundsRect.Location.Y - anchorSize.Height / 2.0f;
                        break;
                    }
                case AnchorType.Top:
                    {
                        pointCenter.X = draw.BoundsRect.Location.X + draw.BoundsRect.Width / 2.0f;
                        pointCenter.Y = draw.BoundsRect.Location.Y - anchorSize.Height / 2.0f;
                        break;
                    }
                default:
                    {
                        Debug.Assert(false);
                        break;
                    }
            }

            return pointCenter;
        }

        public override IDraw Intersect(System.Drawing.Point point)
        {
            return m_rect.Intersect(point);
        }

        public override void Draw(System.Drawing.Graphics cGraphics, System.Drawing.RectangleF cRect)
        {
            m_rect.Draw(cGraphics, cRect);
        }

        public override bool Load(System.Xml.XmlNode iconNode)
        {
            throw new NotImplementedException();
        }

        public override bool Save(System.Xml.XmlNode iconNode)
        {
            throw new NotImplementedException();
        }

        public override void Zoom(Zoom zoom)
        {
            m_rect.Zoom(zoom);
        }

        public override void Rotate(CSR.ShareLib.Rotate rotate)
        {
            m_rect.Rotate(rotate);
        }

        public override void Move(Movement move)
        {
            m_rect.Move(move);
        }

        public override void Symmetry(Symmetry synm)
        {
            m_rect.Symmetry(synm);
        }

        public override void Accept(IDrawVisitor visitor)
        {
            throw new NotImplementedException();
        }

        public override IDraw Clone()
        {
            throw new NotImplementedException();
        }

        public override void Copy(IDraw src)
        {
            throw new NotImplementedException();
        }

        public override void Round()
        {
            //有意留空
        }
    }
}
