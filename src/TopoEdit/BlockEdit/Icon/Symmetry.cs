using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace TopoEdit.Icon
{
    /// <summary>
    /// <para>封装对绘图元素点的直线对称操作。</para>
    /// </summary>
    public class Symmetry : ICloneable
    {
        private PointF m_point1;//对称直线的端点1
        private PointF m_point2;//对称直线的端点2

        private float m_A;//直线方程的参数A
        private float m_B;//直线方程的参数B
        private float m_C;//直线方程的参数C

        public PointF Point1
        {
            get
            {
                return m_point1;
            }
        }

        public PointF Point2
        {
            get
            {
                return m_point2;
            }
        }

        /// <summary>
        /// <para>初始化字段m_point1为和m_point2</para>
        /// <para>前置条件：</para>
        /// <para>    无</para>
        /// <para>后置条件：</para>
        /// <para>    字段m_point1和m_point2被初始化为指定值</para>
        /// </summary>
        /// <param name="point1">对称直线的端点1</param>
        /// <param name="point2">对称直线的端点2</param>
        /// <returns>
        /// <para></para>
        /// </returns>
        /// <remarks>
        /// <para> 蒋湘宁 2013-10-08  创建函数</para>
        /// </remarks>
        public Symmetry(PointF point1, PointF point2)
        {
            m_point1 = point1;
            m_point2 = point2;

            if (m_point1.X == m_point2.X)
            {
                m_A = 1;
                m_B = 0;
                m_C = -m_point1.X;
            }
            else
            {
                m_A = (point2.Y - point1.Y) / (point2.X - point1.X);
                m_B = -1;
                m_C = point1.Y - m_A * point1.X;
            }
        }
        /// <summary>
        /// <para>批量修改点的位置。</para>
        /// <para>前置条件：</para>
        /// <para>    rgPoints 不为空</para>
        /// <para>后置条件：无</para>
        /// </summary>
        /// <param name="rgPoints">需要修改位置的点的集合</param>
        /// <returns>
        /// <para>输入参数rgPoints被修改</para>
        /// </returns>
        /// <remarks>
        /// <para> 蒋湘宁 2013-10-08  创建函数</para>
        /// </remarks>
        public void LPtoDP(ref Point[] rgPoints)
        {
            for (int i = 0; i < rgPoints.Length; i++)
            {
                LPtoDP(ref rgPoints[i]);
            }
        }
        /// <summary>
        /// <para>批量修改点的位置。</para>
        /// <para>前置条件：</para>
        /// <para>    rgPoints 不为空</para>
        /// <para>后置条件：无</para>
        /// </summary>
        /// <param name="rgPoints">需要修改位置的点的集合</param>
        /// <returns>
        /// <para>输入参数rgPoints被修改</para>
        /// </returns>
        /// <remarks>
        /// <para> 蒋湘宁 2013-10-08  创建函数</para>
        /// </remarks>
        public void LPtoDP(ref PointF[] rgPoints)
        {
            for (int i = 0; i < rgPoints.Length; i++)
            {
                LPtoDP(ref rgPoints[i]);
            }
        }
        /// <summary>
        /// <para>修改单点的位置。将point点右移nXMovement，下移nYMovement。</para>
        /// <para>前置条件：</para>
        /// <para>    point 不为空</para>
        /// <para>后置条件：无</para>
        /// </summary>
        /// <param name="point">需要修改位置的点</param>
        /// <returns>
        /// <para>输入参数point被修改。</para>
        /// </returns>
        /// <remarks>
        /// <para> 蒋湘宁 2013-10-08  创建函数</para>
        /// </remarks>
        public void LPtoDP(ref Point point)//默认nCount=1
        {
            point.X = (int)Math.Round(point.X - (2 * m_A * (m_A * point.X + m_B * point.Y + m_C)) / (m_A * m_A + m_B * m_B));
            point.Y = (int)Math.Round(point.Y - (2 * m_B * (m_A * point.X + m_B * point.Y + m_C)) / (m_A * m_A + m_B * m_B));
        }
        /// <summary>
        /// <para>修改单点的位置。将point点右移nXMovement，下移nYMovement。</para>
        /// <para>前置条件：</para>
        /// <para>    point 不为空</para>
        /// <para>后置条件：无</para>
        /// </summary>
        /// <param name="point">需要修改位置的点</param>
        /// <returns>
        /// <para>输入参数point被修改。</para>
        /// </returns>
        /// <remarks>
        /// <para> 蒋湘宁 2013-10-08  创建函数</para>
        /// </remarks>
        public void LPtoDP(ref PointF point)
        {
            point.X = point.X - (2 * m_A * (m_A * point.X + m_B * point.Y + m_C)) / (m_A * m_A + m_B * m_B);
            point.Y = point.Y - (2 * m_B * (m_A * point.X + m_B * point.Y + m_C)) / (m_A * m_A + m_B * m_B);
        }

        #region ICloneable 成员

        public object Clone()
        {
            return new Symmetry(m_point1, m_point2);
        }

        #endregion
    }
}
