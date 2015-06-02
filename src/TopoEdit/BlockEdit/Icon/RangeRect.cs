using System;
using System.Collections.Generic;
using System.Text;
using TopoEdit.Visitor;
using System.Drawing;
using System.Diagnostics;
using TopoEdit.EventHandler;

namespace TopoEdit.Icon
{
    public abstract class RangeRect : IRange
    {
        /// <summary>
        /// 模板实例关联的模板名称
        /// </summary>
        protected string m_templateName = null;
        /// <summary>
        /// 模板实例在父图元中的位置
        /// </summary>
        protected IconRectangle m_rect = new IconRectangle();
        /// <summary>
        /// 模板中心点相对rect左上角的坐标
        /// </summary>
        protected PointF m_templateCenter = new PointF();
        /// <summary>
        /// 模板缓存
        /// </summary>
        protected IRange m_templateCache = null;
        protected IntersectType m_intersectIconType = IntersectType.InBound;

        public string TemplateName
        {
            get { return m_templateName; }
            set
            {
                m_templateName = value;
                ResetTemplate();
            }
        }

        public IRange Template
        {
            get { return m_templateCache; }
        }

        public IconRectangle Rect
        {
            get { return m_rect; }
        }

        public override IntersectType IntersectIconType
        {
            get
            {
                return m_intersectIconType;
            }
            set
            {
                m_intersectIconType = value;
                m_rect.IntersectIconType = IntersectType.InBound;//BLOCK实例边界由于不可见，因此也不能使用边界
                m_templateCache.IntersectIconType = IntersectType.InBound;//BLOCK实例内的BLOCK在测试是否选中时一定使用边界内选中算法，否则操作会很不方便
            }
        }

        public RangeRect(IRange parent)
        {
            Debug.Assert(parent != null);
            Parent = parent;
            Debug.Assert(Parent != null);
        }

        /// <summary>
        /// 初始化Range实例
        /// </summary>
        /// <param name="name">Range实例名称</param>
        /// <param name="blockName">模板名称</param>
        /// <param name="pos">模板实例的左上角坐标</param>
        public virtual void Init(string name, IRange template, PointF pos)
        {
            Debug.Assert((Parent != null));

            Clear();

            Name = name;
            m_rect.Position = pos;
            m_rect.Width = 50;
            m_rect.Height = 50;

            if (template != null)
            {
                m_templateName = template.Name;
                m_templateCache = (IRange)template.Clone();
                m_templateCache.ClearSelectIcon();//清除选中状态

                //缩放矩形会同时缩放点和边长，因此需要首先缩放然后在平移
                //1.缩放
                Zoom zoom = new Zoom();
                zoom.ZoomMode = CSR.CUIT.GlobalService.ShareLib.EmZoomMode.UniformScale;
                zoom.SubMode = ScaleOpMode.None;
                zoom.XRadio = (Parent as ZoomableRange).RangeZoom;
                zoom.YRadio = (Parent as ZoomableRange).RangeZoom;
                m_templateCache.Zoom(zoom);

                //2.平移
                Movement move = new Movement();
                move.XMovement = m_rect.Position.X - m_templateCache.BoundsRect.X;
                move.YMovement = m_rect.Position.Y - m_templateCache.BoundsRect.Y;
                m_templateCache.Move(move);

                //调整矩形框大小与BLOCK相同
                m_rect.Position = m_templateCache.BoundsRect.Location;
                m_rect.Width = m_templateCache.BoundsRect.Width;
                m_rect.Height = m_templateCache.BoundsRect.Height;

                //计算中心点
                m_templateCenter = new PointF(m_rect.Width / 2.0F, m_rect.Height / 2.0F);

                Debug.Assert((m_templateCenter.X >= 0) && (m_templateCenter.Y >= 0));
            }
            else
            {
                m_templateCache = null;
            }

            Add(m_rect);

            if (m_templateCache != null)
            {
                Add(m_templateCache);
            }
        }

        /// <summary>
        /// 将模板缩放到和Rect一样大
        /// </summary>
        public abstract void ResetTemplate();

        /// <summary>
        /// 将矩形框缩放到和BLOCK一样大
        /// </summary>
        public void ResetRectange()
        {
            if (m_templateCache != null)
            {
                //保存原始RECT左上角坐标
                PointF srcRectPos = m_rect.Position;

                //将RECT缩放到和BLOCK边界一样大
                RectangleF blockBound = m_templateCache.BoundsRect;
                m_rect.Position = blockBound.Location;
                m_rect.Width = blockBound.Width;
                m_rect.Height = blockBound.Height;

                //重新计算BLOCK的中心点
                CalcCenter(srcRectPos, m_rect.Position);
            }
        }

        public override RectangleF BoundsRect
        {
            get
            {
                if (m_templateCache != null)
                {
                    return Utility.Union(m_rect.BoundsRect, m_templateCache.BoundsRect);
                }
                else
                {
                    return m_rect.BoundsRect;
                }
            }
        }

        public override void Draw(Graphics cGraphics, RectangleF cRect)
        {
            if (cRect.IsEmpty || BoundsRect.IntersectsWith(cRect))
            {
                if (m_templateCache != null)
                {
                    //绘制BLOCK
                    m_templateCache.Draw(cGraphics, cRect);
                }
                else
                {
                    //绘制矩形框
                    m_rect.Draw(cGraphics, cRect);
                }
            }
        }

        internal void CalcCenter(PointF srcRectPos, PointF destRectPos)
        {
            //只有当rect左上角坐标发生改变时，block中心点坐标才会改变
            Movement move = new Movement();
            move.XMovement = srcRectPos.X - destRectPos.X;
            move.YMovement = srcRectPos.Y - destRectPos.Y;

            move.LPtoDP(ref m_templateCenter);

            Debug.Assert((m_templateCenter.X >= 0) && (m_templateCenter.Y >= 0));
        }

        public override IDraw Intersect(Point point)
        {
            if (this.IntersectIconType == IntersectType.InBound)
            {
                if (null != m_rect.Intersect(point))
                {
                    return this;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                if ((null != m_templateCache) && (null != m_templateCache.Intersect(point)))
                {
                    return this;
                }
                else
                {
                    return null;
                }
            }
        }

        public override void Move(Movement move)
        {
            if (m_templateCache != null)
            {
                m_templateCache.Move(move);
            }

            PointF srcRectPos = m_rect.Position;
            m_rect.Move(move);
            PointF destRectPos = m_rect.Position;
            if (move.SubMode != ScaleOpMode.None)
            {
                CalcCenter(srcRectPos, destRectPos);
            }
        }

        public override string ToString()
        {
            return "RangleRect=> name:" + Name + ", template:" + m_templateName + " position:(" + Math.Round(m_rect.Position.X) + "," + Math.Round(m_rect.Position.Y) + "), width:"
                + Math.Round(m_rect.BoundsRect.Width) + ", height:" + Math.Round(m_rect.BoundsRect.Height);
        }

        public override bool VisibleTransparentColor
        {
            get
            {
                if (m_templateCache != null)
                {
                    return m_templateCache.VisibleTransparentColor;
                }
                else
                {
                    return m_rect.VisibleTransparentColor;
                }
            }
            set
            {
                if (m_templateCache != null)
                {
                    m_templateCache.VisibleTransparentColor = value;
                }
                else
                {
                    m_rect.VisibleTransparentColor = value;
                }
            }
        }
    }
}
