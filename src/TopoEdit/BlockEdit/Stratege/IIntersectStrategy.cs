using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Diagnostics;

namespace TopoEdit.Stratege
{
    /// <summary>
    /// 判断指定点是否与图元相交
    /// </summary>
    interface IIntersectStrategy
    {
        bool IsVisible(PointF point);
    }

    /// <summary>
    /// 使用GraphicsPath判断是否与图元相交的算法
    /// </summary>
    abstract class IntersectByGraphicsPathStrategy : IIntersectStrategy
    {
        private GraphicsPath m_graphicsPath = null;

        protected GraphicsPath Path
        {
            get { return m_graphicsPath; }
        }

        public IntersectByGraphicsPathStrategy(GraphicsPath graphicsPath)
        {
            m_graphicsPath = graphicsPath;
            Debug.Assert(m_graphicsPath != null);
        }

        #region IIntersectStrategy 成员

        public virtual  bool IsVisible(PointF point)
        {
            return false;
        }

        #endregion
    }

    /// <summary>
    /// 点在图元区域内时便认为与图元相交
    /// </summary>
    class IntersectByGraphicsPathInBoundStrategy : IntersectByGraphicsPathStrategy
    {
        public IntersectByGraphicsPathInBoundStrategy(GraphicsPath graphicsPath) : base(graphicsPath)
        {
            Debug.Assert(Path != null);
        }

        public override bool IsVisible(PointF point)
        {
            return Path.IsVisible(point);
        }
    }

    /// <summary>
    /// 点在图元边界上时便认为与图元相交
    /// </summary>
    class IntersectByGraphicsPathOnBoundStrategy : IntersectByGraphicsPathStrategy
    {
        private Pen m_pen;

        public IntersectByGraphicsPathOnBoundStrategy(GraphicsPath graphicsPath, Pen pen)
            : base(graphicsPath)
        {
            m_pen = pen;
            Debug.Assert(m_pen != null);
            Debug.Assert(Path != null);
        }

        public override bool IsVisible(PointF point)
        {
            return Path.IsOutlineVisible(point, m_pen);
        }
    }

    /// <summary>
    /// 点在指定宽度的连线上时便认为与图元相交
    /// </summary>
    class IntersectByLineStrategy : IIntersectStrategy
    {
        private PointF m_point1;
        private PointF m_point2;
        private int m_width;

        public IntersectByLineStrategy(PointF point1, PointF point2, int width)
        {
            m_point1 = point1;
            m_point2 = point2;
            m_width = width;

            Debug.Assert(m_point1 != m_point2);
            Debug.Assert(m_width > 0);
        }

        public bool IsVisible(PointF point)
        {
            return CSR.CUIT.GlobalService.ShareLib.Utility.HitTestLine(Utility.ConvertPos(m_point1), Utility.ConvertPos(m_point2), Utility.ConvertPos(point), m_width);
        }
    }
}
