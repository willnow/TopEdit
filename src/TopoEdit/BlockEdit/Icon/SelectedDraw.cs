using System;
using System.Collections.Generic;
using System.Text;
using TopoEdit.Icon;
using System.Diagnostics;
using System.Drawing;
using TopoEdit.EventHandler;
using TopoEdit.Visitor;

namespace TopoEdit.Icon
{
    

    public abstract class  SelectedDraw : IDraw
    {
        /// <summary>
        /// 被装饰的Draw
        /// </summary>
        private IDraw m_icon;
        /// <summary>
        /// 锚点
        /// </summary>
        private List<Anchor> m_anchorIcons = new List<Anchor>();

        public SelectedDraw(IDraw icon)
            : base()
        {
            Debug.Assert(null != icon);
            m_icon = icon;
        }

        public IDraw Icon
        {
            get { return m_icon; }
            set { m_icon = value; }
        }

        protected List<Anchor> AnchorIcons
        {
            get
            {
                return m_anchorIcons;
            }
            set
            {
                m_anchorIcons = value;
            }
        }

        /// <summary>
        /// <para>获得图元的显示属性。</para>
        /// </summary>
        /// <remarks>
        /// <para> 蒋湘宁 2013-10-08  创建属性</para>
        /// </remarks>
        public override bool Visible
        {
            get
            {
                return m_icon.Visible;
            }
            set
            {
                m_icon.Visible = value;
            }
        }

        /// <summary>
        /// <para>停靠属性</para>
        /// </summary>
        public override Dictionary<DockType, bool> Dock
        {
            get
            {
                return m_icon.Dock;
            }
            set
            {
                m_icon.Dock = value;
            }
        }

        /// <summary>
        /// <para>停靠时是否固定</para>
        /// </summary>
        public override bool Fixed
        {
            get
            {
                return m_icon.Fixed;
            }
            set
            {
                m_icon.Fixed = value;
            }
        }

        public override IntersectType IntersectIconType
        {
            get
            {
                return m_icon.IntersectIconType;
            }
            set
            {
                m_icon.IntersectIconType = value;
            }
        }

        /// <summary>
        /// 是否显示透明色
        /// </summary>
        public override bool VisibleTransparentColor
        {
            get
            {
                return m_icon.VisibleTransparentColor;
            }
            set
            {
                m_icon.VisibleTransparentColor = value;
            }
        }


        /// <summary>
        /// <para>获得图元的绘制的层级（先后顺序）。层级高的后绘制，层级低的先绘制。</para>
        /// </summary>
        /// <remarks>
        /// <para> 蒋湘宁 2013-10-08  创建属性</para>
        /// </remarks>
        public override Int32 Level
        {
            get
            {
                return m_icon.Level;
            }
            set
            {
                m_icon.Level = value;
            }
        }

        protected void AddAnchor(Anchor anchor)
        {
            //将锚点加入集合
            m_anchorIcons.Add(anchor);
        }

        /// <summary>
        /// 初始化锚点
        /// </summary>
        public abstract void InitAnchor();

        /// <summary>
        /// 根据ICON移动锚点位置
        /// </summary>
        public abstract void ResetAnchor();

        public override bool Load(System.Xml.XmlNode iconNode)
        {
            return m_icon.Load(iconNode);
        }

        public override bool Save(System.Xml.XmlNode iconNode)
        {
            return m_icon.Save(iconNode);
        }

        public override void Round()
        {
            m_icon.Round();
        }

        public override void Zoom(TopoEdit.Icon.Zoom zoom)
        {
            //缩放图标
            m_icon.Zoom(zoom);
            //重绘锚点
            ResetAnchor();
        }

        public override void Rotate(CSR.ShareLib.Rotate rotate)
        {
            //旋转图标
            m_icon.Rotate(rotate);
            //重绘锚点
            ResetAnchor();
        }

        public override void Move(TopoEdit.Icon.Movement move)
        {
            //平移图标
            m_icon.Move(move);
            //重绘锚点
            ResetAnchor();
        }

        public override void Symmetry(TopoEdit.Icon.Symmetry symn)
        {
            //对称图标
            m_icon.Symmetry(symn);
            //重绘锚点
            ResetAnchor();
        }

        public override System.Drawing.RectangleF BoundsRect
        {
            get 
            {
                //被选中图标的边界就是锚点的边界
                RectangleF rect = m_icon.BoundsRect;
                foreach (Anchor anchor in m_anchorIcons)
                {
                    rect = TopoEdit.Utility.Union(rect, anchor.BoundsRect);
                }

                return rect;
            }
        }

        public override IDraw Intersect(System.Drawing.Point point)
        {
            if (m_icon.Intersect(point) != null)
            {
                return this;
            }
            else
            {
                //判断指定点是否在锚点内
                foreach (Anchor anchor in m_anchorIcons)
                {
                    if (anchor.Intersect(point) != null)
                    {
                        return this;
                    }
                }
                return null;
            }
        }

        internal Anchor IntersectAnchor(System.Drawing.Point point)
        {
            if (Intersect(point) != null)
            {
                foreach (Anchor anchor in m_anchorIcons)
                {
                    if (anchor.Intersect(point) != null)
                    {
                        return anchor;
                    }
                }
                return null;
            }
            return null;
        }

        internal virtual void CreateZoomAndMoveByAnchor(out Zoom zoom, out Movement move, out ScaleOpMode mode, PointF mousePos
            , PointF latsMousePos, RectangleF selIconRange, Anchor anchor)
        {
            AnchorType anchorType = anchor.GeneralAnchorType;
            PointF anchorPos = Utility.GetCenter(anchor.BoundsRect);

            zoom = new Zoom();
            zoom.XRadio = 1.0F;
            zoom.YRadio = 1.0F;
            zoom.ZoomMode = CSR.CUIT.GlobalService.ShareLib.EmZoomMode.NonUniformScale;

            move = new Movement();
            move.XMovement = 0.0F;
            move.YMovement = 0.0F;

            switch (anchorType)
            {
                case AnchorType.Bottom:
                    {
                        mode = ScaleOpMode.Down;
                        if (mousePos.Y > selIconRange.Top)
                        {
                            //可以缩放
                            zoom.YRadio = (mousePos.Y - selIconRange.Top) / (anchorPos.Y - selIconRange.Top);
                            move.YMovement = mousePos.Y - latsMousePos.Y;
                        }
                        else
                        {
                            //不能缩放
                        }
                        break;
                    }
                case AnchorType.Left:
                    {
                        mode = ScaleOpMode.Left;
                        if (mousePos.X < selIconRange.Right)
                        {
                            //可以缩放
                            zoom.XRadio = (mousePos.X - selIconRange.Right) / (anchorPos.X - selIconRange.Right);
                            move.XMovement = mousePos.X - latsMousePos.X;
                        }
                        else
                        {
                            //不能缩放
                        }
                        break;
                    }
                case AnchorType.LeftBottom:
                    {
                        mode = ScaleOpMode.LeftDown;
                        if ((mousePos.X < selIconRange.Right) && (mousePos.Y > selIconRange.Top))
                        {
                            //可以缩放
                            zoom.XRadio = (mousePos.X - selIconRange.Right) / (anchorPos.X - selIconRange.Right);
                            zoom.YRadio = (mousePos.Y - selIconRange.Top) / (anchorPos.Y - selIconRange.Top);

                            move.XMovement = mousePos.X - latsMousePos.X;
                            move.YMovement = mousePos.Y - latsMousePos.Y;
                        }
                        else
                        {
                            //不能缩放
                        }
                        break;
                    }
                case AnchorType.Top:
                    {
                        mode = ScaleOpMode.Up;
                        if (mousePos.Y < selIconRange.Bottom)
                        {
                            //可以缩放
                            zoom.YRadio = (mousePos.Y - selIconRange.Bottom) / (anchorPos.Y - selIconRange.Bottom);
                            move.YMovement = mousePos.Y - latsMousePos.Y;
                        }
                        else
                        {
                            //不能缩放
                        }
                        break;
                    }
                case AnchorType.LeftTop:
                    {
                        mode = ScaleOpMode.LeftUp;
                        if ((mousePos.X < selIconRange.Right) && (mousePos.Y < selIconRange.Bottom))
                        {
                            //可以缩放
                            zoom.XRadio = (mousePos.X - selIconRange.Right) / (anchorPos.X - selIconRange.Right);
                            zoom.YRadio = (mousePos.Y - selIconRange.Bottom) / (anchorPos.Y - selIconRange.Bottom);

                            move.XMovement = mousePos.X - latsMousePos.X;
                            move.YMovement = mousePos.Y - latsMousePos.Y;
                        }
                        else
                        {
                            //不能缩放
                        }
                        break;
                    }
                case AnchorType.Right:
                    {
                        mode = ScaleOpMode.Right;
                        if (mousePos.X > selIconRange.Left)
                        {
                            //可以缩放
                            zoom.XRadio = (mousePos.X - selIconRange.Left) / (anchorPos.X - selIconRange.Left);
                            move.XMovement = mousePos.X - latsMousePos.X;
                        }
                        else
                        {
                            //不能缩放
                        }
                        break;
                    }
                case AnchorType.RightBottom:
                    {
                        mode = ScaleOpMode.RightDown;
                        if ((mousePos.X > selIconRange.Left) && (mousePos.Y > selIconRange.Top))
                        {
                            //可以缩放
                            zoom.XRadio = (mousePos.X - selIconRange.Left) / (anchorPos.X - selIconRange.Left);
                            zoom.YRadio = (mousePos.Y - selIconRange.Top) / (anchorPos.Y - selIconRange.Top);

                            move.XMovement = mousePos.X - latsMousePos.X;
                            move.YMovement = mousePos.Y - latsMousePos.Y;
                        }
                        else
                        {
                            //不能缩放
                        }
                        break;
                    }
                case AnchorType.RightTop:
                    {
                        mode = ScaleOpMode.RightUp;
                        if ((mousePos.X > selIconRange.Left) && (mousePos.Y < selIconRange.Bottom))
                        {
                            //可以缩放
                            zoom.XRadio = (mousePos.X - selIconRange.Left) / (anchorPos.X - selIconRange.Left);
                            zoom.YRadio = (mousePos.Y - selIconRange.Bottom) / (anchorPos.Y - selIconRange.Bottom);

                            move.XMovement = mousePos.X - latsMousePos.X;
                            move.YMovement = mousePos.Y - latsMousePos.Y;
                        }
                        else
                        {
                            //不能缩放
                        }
                        break;
                    }
                case AnchorType.Control:
                case AnchorType.EndPoint:
                    {
                        mode = ScaleOpMode.Any;
                        zoom.XRadio = 1;
                        zoom.YRadio = 1;

                        move.XMovement = mousePos.X - latsMousePos.X;
                        move.YMovement = mousePos.Y - latsMousePos.Y;
                        break;
                    }
                default:
                    {
                        Debug.Assert(false);
                        mode = ScaleOpMode.None;
                        break;
                    }
            }
        }

        public override void Draw(System.Drawing.Graphics cGraphics, RectangleF rect)
        {
            //图元不可见，或图元不为空且没有和绘制区域有交集，此时不需要绘制当前图元
            if (!Visible || (!rect.IsEmpty && !BoundsRect.IntersectsWith(rect)))
            {
                return;
            }

            //绘制图标
            m_icon.Draw(cGraphics, rect);

            //绘制锚点
            foreach (Anchor anchor in m_anchorIcons)
            {
                anchor.Draw(cGraphics, rect);
            }
        }

        public override IDraw Clone()
        {
            return this.Icon.Clone();
        }

        public override void Copy(IDraw src)
        {
            if (src is SelectedDraw)
            {
                SelectedDraw srcItem = src as SelectedDraw;

                m_icon.Copy(srcItem.Icon);
                ResetAnchor();
            }
            else
            {
                throw new ArgumentException("被拷贝图元和目标图元类型不兼容", "src");
            }
        }

        public override string ToString()
        {
            return m_icon.ToString();
        }
    }
}
