using System;
using System.Collections.Generic;
using System.Text;
using TopoEdit.Icon;
using System.Drawing;
using System.Diagnostics;
using TopoEdit.Visitor;

namespace TopoEdit.Icon
{
    public abstract class IRange : IDraw
    {
        /// <summary>
        /// range包含的IDraw集合
        /// </summary>
        protected List<IDraw> m_draws = new List<IDraw>();
        /// <summary>
        /// 被选中的区域
        /// </summary>
        private SelectedRange m_selRange;
        /// <summary>
        /// Range名称
        /// </summary>
        private string m_name = "";

        public IRange()
            : base()
        {
            //有意留空
        }

        public string Name
        {
            get { return m_name; }
            set { m_name = value; }
        }

        public int Count
        {
            get { return m_draws.Count; }
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
                //只要有一个ICON需要显示，则该区域被认为已显示
                foreach (IDraw icon in m_draws)
                {
                    if (icon.Visible)
                    {
                        return true;
                    }
                }
                return false;
            }
            set
            {
                //设置区域全部都显示或都不显示
                foreach (IDraw icon in m_draws)
                {
                    icon.Visible = value;
                }
            }
        }

        /// <summary>
        /// <para>停靠属性</para>
        /// </summary>
        public override Dictionary<DockType, bool> Dock
        {
            get
            {
                //分别计算四个方向上的停靠（DOCK）属性
                foreach (DockType dockType in Enum.GetValues(typeof(DockType)))
                {
                    base.Dock[dockType] = false;
                    //包含的图元中只要有一个该类型的DOCK属性，则区域就具有该类型的DOCK属性
                    foreach (IDraw draw in m_draws)
                    {
                        if (draw.Dock[dockType])
                        {
                            base.Dock[dockType] = true;
                            break;
                        }
                    }
                }

                return base.Dock;
            }
            set
            {
                //Range不允许设置Dock属性
                Debug.Assert(false);
            }
        }

        /// <summary>
        /// <para>停靠时是否固定</para>
        /// </summary>
        public override bool Fixed
        {
            get
            {
                //只要区域支持某个方向上停靠，则区域一定可以被拉伸
                Dictionary<DockType, bool>.Enumerator itor = Dock.GetEnumerator();
                while(itor.MoveNext())
                {
                    if (itor.Current.Value)
                    {
                        //存在某个方向上可以停靠，则一定可以拉伸
                        return false;
                    }
                }
                //没有任何一个方向支持停靠，则不可拉伸
                return true;
            }
            set
            {
                //Range不允许设置Fixed属性
                Debug.Assert(false);
            }
        }

        public override IntersectType IntersectIconType
        {
            get
            {
                if (m_draws.Count > 0)
                {
                    return m_draws[0].IntersectIconType;
                }
                else
                {
                    return IntersectType.InBound;
                }
            }
            set
            {
                foreach (IDraw draw in m_draws)
                {
                    draw.IntersectIconType = value;
                }
            }
        }

        public override bool VisibleTransparentColor
        {
            get
            {
                if (m_draws.Count > 0)
                {
                    return m_draws[0].VisibleTransparentColor;
                }
                else
                {
                    return false;
                }
            }
            set
            {
                foreach (IDraw draw in m_draws)
                {
                    draw.VisibleTransparentColor = value;
                }
            }
        }

        public IDraw GetIcon(int index)
        {
            return m_draws[index];
        }

        internal virtual void Add(IDraw icon)
        {
            Debug.Assert(icon != null);
            icon.Parent = this;
            m_draws.Add(icon);
        }

        internal virtual void AddRange(IEnumerable<IDraw> collection)
        {
            Debug.Assert(collection != null);
            foreach (IDraw draw in collection)
            {
                draw.Parent = this;
            }

            m_draws.AddRange(collection);
        }

        internal virtual void RemoveRange(IEnumerable<IDraw> collection)
        {
            Debug.Assert(collection != null);
            foreach (IDraw draw in collection)
            {
                draw.Parent = null;
                Remove(draw);
            }
        }

        internal virtual void Remove(IDraw icon)
        {
            Debug.Assert(icon != null);
            m_draws.Remove(icon);
            if (icon == m_selRange)
            {
                m_selRange = null;
            }
        }

        internal virtual void Clear()
        {
            m_draws.Clear();
            m_selRange = null;
        }

        public virtual List<IDraw> Icons
        {
            get { return m_draws; }
        }

        public override void Zoom(TopoEdit.Icon.Zoom zoom)
        {
            foreach (IDraw icon in m_draws)
            {
                icon.Zoom(zoom);
            }
        }

        public override void Rotate(CSR.ShareLib.Rotate rotate)
        {
            foreach (IDraw icon in m_draws)
            {
                icon.Rotate(rotate);
            }
        }

        public override void Move(TopoEdit.Icon.Movement move)
        {
            foreach (IDraw icon in m_draws)
            {
                icon.Move(move);
            }
        }

        public override void Symmetry(Symmetry synm)
        {
            foreach (IDraw icon in m_draws)
            {
                icon.Symmetry(synm);
            }
        }

        public override System.Drawing.RectangleF BoundsRect
        {
            get
            {
                RectangleF rect = new RectangleF();
                foreach (IDraw icon in m_draws)
                {
                    rect = TopoEdit.Utility.Union(rect, icon.BoundsRect);
                }

                return rect;
            }
        }

        public override IDraw Intersect(System.Drawing.Point point)
        {
            if (!Visible)
            {
                return null;
            }

            foreach (IDraw icon in m_draws)
            {
                if (icon.Intersect(point) != null)
                {
                    return icon;
                }
            }

            return null;
        }

        public override void Draw(System.Drawing.Graphics cGraphics, RectangleF cRect)
        {
            //图元不可见，或图元不为空且没有和绘制区域有交集，此时不需要绘制当前图元
            if (!Visible || (!cRect.IsEmpty && !BoundsRect.IntersectsWith(cRect)))
            {
                return;
            }

            m_draws.Sort();
            foreach (IDraw icon in m_draws)
            {
                icon.Draw(cGraphics, cRect);
            }
        }

        public override bool Load(System.Xml.XmlNode iconNode)
        {
            bool isLoad = true;
            foreach (IDraw icon in m_draws)
            {
                if(!icon.Load(iconNode))
                {
                    isLoad = false;
                }
            }

            return isLoad;
        }

        public override bool Save(System.Xml.XmlNode iconParentNode)
        {
            bool isSave = true;

            foreach (IDraw icon in m_draws)
            {
                if (!icon.Save(iconParentNode))
                {
                    isSave = false;
                }
            }

            return isSave;
        }

        public override void Round()
        {
            foreach (IDraw icon in m_draws)
            {
                icon.Round();
            }
        }

        public virtual List<IDraw> GetIconByLevel(int level)
        {
            List<IDraw> icons = new List<IDraw>();
            foreach (IDraw icon in m_draws)
            {
                if ((level == -1) || (icon.Level == level))
                {
                    icons.Add(icon);
                }
            }

            return icons;
        }

        /// <summary>
        /// 设置显示指定等级的ICON
        /// </summary>
        /// <param name="level">指定显示等级，如果为-1，表示都显示</param>
        internal void SetVisible(int level)
        {
            foreach (IDraw icon in m_draws)
            {
                if ((level == -1) || (icon.Level == level))
                {
                    icon.Visible = true;
                }
                else
                {
                    icon.Visible = false;
                }
            }
        }

        public virtual List<int> GetLevels()
        {
            List<int> levels = new List<int>();
            foreach (IDraw icon in m_draws)
            {
                if (!levels.Contains(icon.Level))
                {
                    levels.Add(icon.Level);
                }
            }

            levels.Sort();

            return levels;
        }

        public SelectedRange SelectedRange
        {
            get { return m_selRange; }
        }

        internal void SetIconInRectSelected(RectangleF selRect)
        {
            ClearSelectIcon();
            //将ICON加入BLOCK
            List<IDraw> icons = new List<IDraw>();
            foreach (IDraw icon in m_draws)
            {
                if (selRect.Contains(icon.BoundsRect))
                {
                    //icon在指定矩形范围内，则加入选中区域
                    icons.Add(icon);
                }
            }

            foreach (IDraw icon in icons)
            {
                AddIconToSelectRange(icon);
            }
        }

        internal SelectedRange SelectIcon(bool isCtrlDown, System.Drawing.Point point)
        {
            IDraw hitIcon = Intersect(point);
            if (null != m_selRange)
            {
                //当前已经有被选中的ICON
                if (m_selRange == hitIcon)
                {
                    //该点落在了被选中区域中，则不用处理，返回选中区域即可
                }
                else
                {
                    //点没有落在选中区域
                    if (null != hitIcon)
                    {
                        if (!isCtrlDown)
                        {
                            //未按下CTRL，清除选中区域
                            ClearSelectIcon();
                        }
                        AddIconToSelectRange(hitIcon);
                    }
                    else
                    {
                        //没有ICON被点中
                        ClearSelectIcon();
                    }
                }
            }
            else
            {
                //当前没有被选中的ICON
                if (null != hitIcon)
                {
                    AddIconToSelectRange(hitIcon);
                }
                else
                {
                    //没有ICON被点中，不处理
                }
            }

            return m_selRange;
        }

        private void AddIconToSelectRange(IDraw hitIcon)
        {
            //创建一个选中区域
            if (m_selRange == null)
            {
                m_selRange = new SelectedRange();
                //将选中区域加入BLOCK
                Add(m_selRange);
            }

            //将被选中的ICON加入选中区域
            m_selRange.Add(hitIcon);
            //将被选中ICON从BLOCK中移除
            Remove(hitIcon);
        }
        
        public void ClearSelectIcon()
        {
            if (null != m_selRange)
            {
                //取出选中区域中的ICON
                List<IDraw> icons = m_selRange.Icons;
                //将ICON加入BLOCK
                foreach (IDraw icon in icons)
                {
                    Add(icon);
                }
                //删除选中区域
                Remove(m_selRange);
                m_selRange = null;
            }
            else
            {
                //没有被选中区域，不用清除
            }
        }

        /// <summary>
        /// 移动被选图标，并计算刷新区域
        /// </summary>
        /// <param name="move"></param>
        /// <returns></returns>
        internal RectangleF MoveSelectIcon(TopoEdit.Icon.Movement move)
        {
            RectangleF rect = new RectangleF();
            if (null != m_selRange)
            {
                //将移动前区域加入刷新区域
                rect = TopoEdit.Utility.Union(rect, m_selRange.BoundsRect);
                //移动
                m_selRange.Move(move);
                //将移动后区域加入刷新区域
                rect = TopoEdit.Utility.Union(rect, m_selRange.BoundsRect);
            }
            return rect;
        }

        internal RectangleF ZoomSelection(TopoEdit.Icon.Zoom zoom)
        {
            RectangleF rect = new RectangleF();
            if (null != m_selRange)
            {
                //将缩放前区域加入刷新区域
                rect = TopoEdit.Utility.Union(rect, m_selRange.BoundsRect);
                //缩放
                m_selRange.Zoom(zoom);
                //将缩放后区域加入刷新区域
                rect = TopoEdit.Utility.Union(rect, m_selRange.BoundsRect);
            }
            return rect;
        }

        public override string ToString()
        {
            return "Range Cnt:" + m_draws.Count;
        }

        internal Bitmap GetBitmap(Color backcolor, int width, int height)
        {
            IRange range = (IRange)(this.Clone());

            range.ClearSelectIcon();//清除选中
            //创建缩略图
            Rectangle bitmapBound = new Rectangle();
            bitmapBound.Width = width;
            bitmapBound.Height = height;
            Bitmap bitmap = new Bitmap(bitmapBound.Width, bitmapBound.Height);
            Graphics cGraphics = Graphics.FromImage(bitmap);

            //绘制白色背景
            Brush brush = new SolidBrush(backcolor);
            cGraphics.FillRectangle(brush, bitmapBound);

            if (range.BoundsRect.Width <= 0 || range.BoundsRect.Height <= 0)
            {
                //无法绘制BLOCK
                cGraphics.Dispose();
                return bitmap;
            }
            else
            {
                //缩放后绘制BLOCK
                int defaultDestRectLen = (int)(bitmapBound.Width * 0.75);
                Point defaultDestRectCenter = new Point(bitmapBound.Width / 2, bitmapBound.Height / 2);
                //恢复Block到正常大小
                //将BLOCK缩放到合适大小
                TopoEdit.Icon.Zoom zoom = TopoEdit.Icon.Zoom.Create(range.BoundsRect, defaultDestRectLen);
                range.Zoom(zoom);
                //将BLOCK平移到画板中心
                TopoEdit.Icon.Movement move = TopoEdit.Icon.Movement.Create(range.BoundsRect, defaultDestRectCenter);
                range.Move(move);
                //绘制Block
                range.Draw(cGraphics, new Rectangle());

                cGraphics.Dispose();
                return bitmap;
            }
        }
    }
}
