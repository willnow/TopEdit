//******************************************************************************
//文件名称 :     IDraw.cs
//版权信息 :     北京南车时代信息技术有限公司 版权所有
//创建日期 :     2013-10-08
//文件描述 :
//修改履历 :
// V1.0.0    蒋湘宁    2013-10-08    创建本文件
// V1.1.1    蒋湘宁    2013-10-21    完成第三轮迭代的功能
// V1.2.1    蒋湘宁    2013-10-22    重新编译
// V1.3.2    蒋湘宁    2013-10-25    根据《CUIT软件测试报告》V1.1.1的测试结果修改代码，修复BUG
// V1.4.4    蒋湘宁    2013-11-06    修复测试发现的BUG
// V1.5.5    蒋湘宁    2013-11-18    修复单元测试、系统测试发现的BUG
// V1.6.6    蒋湘宁    2013-11-22    修复系统测试发现的BUG
//******************************************************************************

using System.Drawing;
using System.Xml;
using CSR.ShareLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using TopoEdit.EventHandler;
using TopoEdit.Visitor;
using TopoEdit.Stratege;
using System.Drawing.Drawing2D;

namespace TopoEdit.Icon
{
    public enum DockType
    {
        Left,
        Right,
        Top,
        Bottom,
    }

    /// <summary>
    /// <para>定义绘制的方法。</para>
    /// </summary>
    public abstract class IDraw : IComparable<IDraw>
    {
        /// <summary>
        /// <para>指示图元在站场图上绘制的先后顺序，m_level值小的先绘，值大的后绘。初始值为0。</para>
        /// </summary>
        private int m_level = 0;
        /// <summary>
        /// <para>图元的当前显示状态。初始值为true。</para>
        /// </summary>
        private bool m_visible = true;
        /// <summary>
        /// <para>图元的停靠属性，四个方向均默认不停靠</para>
        /// </summary>
        private Dictionary<DockType, bool> m_dock = new Dictionary<DockType, bool>(Enum.GetValues(typeof(DockType)).Length);
        /// <summary>
        /// <para>停靠时是否固定大小，默认大小固定</para>
        /// </summary>
        private bool m_fixed = true;
        /// <summary>
        /// 图元的父图元（直接包含该图元的图元），根图元的父图元为空
        /// </summary>
        private IDraw m_parent = null;
        /// <summary>
        /// 判断相交的类型
        /// </summary>
        private IntersectType m_intersectType = IntersectType.InBound;

        public IDraw Parent
        {
            get { return m_parent; }
            set { m_parent = value; }
        }
        /// <summary>
        /// <para>获得图元的绘制的层级（先后顺序）。层级高的后绘制，层级低的先绘制。</para>
        /// </summary>
        /// <remarks>
        /// <para> 蒋湘宁 2013-10-08  创建属性</para>
        /// </remarks>
        public virtual Int32 Level
        {
            get
            {
                return m_level;
            }
            set
            {
                m_level = value;
            }
        }

        /// <summary>
        /// <para>获得图元的显示属性。</para>
        /// </summary>
        /// <remarks>
        /// <para> 蒋湘宁 2013-10-08  创建属性</para>
        /// </remarks>
        public virtual bool Visible
        {
            get
            {
                return m_visible;
            }
            set
            {
                m_visible = value;
            }
        }

        /// <summary>
        /// <para>停靠属性</para>
        /// </summary>
        public virtual Dictionary<DockType, bool> Dock
        {
            get 
            {
                return m_dock;
            }
            set
            {
                m_dock = value;
            }
        }

        /// <summary>
        /// <para>停靠时是否固定</para>
        /// </summary>
        public virtual bool Fixed
        {
            get
            {
                return m_fixed;
            }
            set
            {
                m_fixed = value;
            }
        }

        public abstract RectangleF BoundsRect
        {
            get;
        }


        public virtual IntersectType IntersectIconType
        {
            get
            {
                return m_intersectType;
            }
            set
            {
                m_intersectType = value;
            }
        }

        /// <summary>
        /// 是否显示透明色
        /// </summary>
        public abstract bool VisibleTransparentColor
        {
            get;
            set;
        }

        public IDraw()
        {
            m_dock.Add(DockType.Left, false);
            m_dock.Add(DockType.Right, false);
            m_dock.Add(DockType.Top, false);
            m_dock.Add(DockType.Bottom, false);
        }

        /// <summary>
        /// <para>根据被选中的图元类型创建被选中对象</para>
        /// </summary>
        public virtual SelectedDraw CreateSelectedDraw()
        {
            return new SelectedItem(this);
        }

        /// <summary>
        /// 跟随锚点移动时，是否可以进行部分缩放
        /// </summary>
        /// <returns></returns>
        public bool CanZoomPart(ScaleOpMode mode)
        {
            return mode == ScaleOpMode.None;
        }

        /// <summary>
        /// 跟随锚点移动时，如果不能缩放，判断是否可以平移
        /// </summary>
        /// <param name="zoom">缩放向量，通过缩放向量可以判断锚点类型</param>
        /// <returns></returns>
        public bool CanMovePart(ScaleOpMode mode)
        {
            Debug.Assert(!CanZoomPart(mode));

            //需要固定大小，且移动方向和停靠方向一致时，才能平移
            if (((mode == ScaleOpMode.Up) && Dock[DockType.Top])
                    || ((mode == ScaleOpMode.Down) && Dock[DockType.Bottom])
                    || ((mode == ScaleOpMode.Left) && Dock[DockType.Left])
                    || ((mode == ScaleOpMode.Right) && Dock[DockType.Right]))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// <para>判断输入点是否位于图元的所在区域内。该方法由每个IIcon子类具体实现。</para>
        /// </summary>
        /// <returns>
        /// <para></para>
        /// </returns>
        /// <remarks>
        /// <para> 蒋湘宁 2013-10-08  创建函数</para>
        /// </remarks>
        public abstract IDraw Intersect(Point point);
        public abstract void Draw(Graphics cGraphics, RectangleF cRect);
        public abstract bool Load(XmlNode iconNode);
        public abstract bool Save(XmlNode iconNode);
        public abstract void Zoom(Zoom zoom);
        public abstract void Rotate(Rotate rotate);
        public abstract void Move(Movement move);
        public abstract void Symmetry(Symmetry synm);
        public abstract void Accept(IDrawVisitor visitor);
        public abstract IDraw Clone();
        public abstract void Copy(IDraw src);//将源内容拷贝到目标内容
        public abstract void Round();//将所有影响坐标显示的浮点数都四舍五入整数
        public override string ToString()
        {
            return "";
        }

        #region IComparable<IDraw> 成员

        public int CompareTo(IDraw other)
        {
            if (Level < other.Level)
            {
                return -1;
            }
            else if (Level > other.Level)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        #endregion
    }
}
