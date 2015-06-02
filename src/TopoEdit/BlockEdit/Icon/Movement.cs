//******************************************************************************
//文件名称 :     Movement.cs
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
using System;

using System.Drawing;
using TopoEdit.EventHandler;

namespace TopoEdit.Icon
{
    /// <summary>
    /// <para>封装对绘图元素点和矩形的位移操作。</para>
    /// </summary>
    public class Movement : ICloneable
    {
        public ScaleOpMode SubMode
        {
            get
            {
                return m_subMode;
            }
            set
            {
                m_subMode = value;
            }
        }

        /// <summary>
        /// <para>设置和返回延X轴偏移量</para>
        /// </summary>
        /// <remarks>
        /// <para> 蒋湘宁 2013-10-08  创建属性</para>
        /// </remarks>
        public float XMovement
        {
            get
            {
                return m_nXMovement;
            }
            set
            {
                m_nXMovement = value;
            }
        }
        /// <summary>
        /// <para>设置和返回延Y轴偏移量</para>
        /// </summary>
        /// <remarks>
        /// <para> 蒋湘宁 2013-10-08  创建属性</para>
        /// </remarks>
        public float YMovement
        {
            get
            {
                return m_nYMovement;
            }
            set
            {
                m_nYMovement = value;
            }
        }
        /// <summary>
        /// <para>初始化字段m_nXMovement和m_nYMovement为0</para>
        /// <para>前置条件：</para>
        /// <para>    无</para>
        /// <para>后置条件：</para>
        /// <para>    字段m_nXMovement和m_nYMovement被初始化为0</para>
        /// </summary>
        /// <returns>
        /// <para></para>
        /// </returns>
        /// <remarks>
        /// <para> 蒋湘宁 2013-10-08  创建函数</para>
        /// </remarks>
        public Movement()
        {
            m_nXMovement = 0;
            m_nYMovement = 0;
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
            point.X = point.X + (int)m_nXMovement;
            point.Y = point.Y + (int)m_nYMovement;
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
            point.X += m_nXMovement;
            point.Y += m_nYMovement;
        }
        /// <summary>
        /// <para>修改矩形框的位置。将lpRect右移nXMovement，下移nYMovement。</para>
        /// <para>前置条件：</para>
        /// <para>    lpRect 不为空</para>
        /// <para>后置条件：无</para>
        /// </summary>
        /// <param name="lpRect">需要修改位置的矩形框</param>
        /// <returns>
        /// <para>输入参数lpRect被修改</para>
        /// </returns>
        /// <remarks>
        /// <para> 蒋湘宁 2013-10-08  创建函数</para>
        /// </remarks>
        public void LPtoDP(ref Rectangle lpRect)
        {
            //LPRECT
            lpRect.X = lpRect.X + (int)m_nXMovement;
            lpRect.Y = lpRect.Y + (int)m_nYMovement;
        }
        /// <summary>
        /// <para>修改矩形框的位置。将lpRect右移nXMovement，下移nYMovement。</para>
        /// <para>前置条件：</para>
        /// <para>    lpRect 不为空</para>
        /// <para>后置条件：无</para>
        /// </summary>
        /// <param name="lpRect">需要修改位置的矩形框</param>
        /// <returns>
        /// <para>输入参数lpRect被修改</para>
        /// </returns>
        /// <remarks>
        /// <para> 蒋湘宁 2013-10-08  创建函数</para>
        /// </remarks>
        public void LPtoDP(ref RectangleF lpRect)
        {
            //LPRECT
            lpRect.X = lpRect.X + m_nXMovement;
            lpRect.Y = lpRect.Y + m_nYMovement;
        }

        /// <summary>
        /// <para>修改大小。将size的宽度增加nXMovement，高度增加nYMovement。</para>
        /// <para>前置条件：</para>
        /// <para>    size 不为空</para>
        /// <para>后置条件：无</para>
        /// </summary>
        /// <param name="size">需要修改的大小</param>
        /// <returns>
        /// <para>输入参数size被修改</para>
        /// </returns>
        /// <remarks>
        /// <para> 黎邓根 2014-09-18  创建函数</para>
        /// </remarks>
        public void LPtoDP(ref SizeF size)
        {
            size.Width += m_nXMovement;
            size.Height += m_nYMovement;
        }

        /// <summary>
        /// 创建一个将指定矩形平移到指定点的移动对象
        /// </summary>
        /// <param name="rect">被平移矩形</param>
        /// <param name="destRectCenter">目标矩形中心点</param>
        /// <returns></returns>
        public static Movement Create(RectangleF rect, PointF destRectCenter)
        {
            PointF rectCenter = new PointF(rect.Left + rect.Width / 2.0F, rect.Top + rect.Height / 2.0F);
            Movement move = new Movement();
            move.XMovement = destRectCenter.X - rectCenter.X;
            move.YMovement = destRectCenter.Y - rectCenter.Y;

            return move;
        }

        #region ICloneable 成员

        public object Clone()
        {
            Movement move = new Movement();
            move.m_nXMovement = m_nXMovement;
            move.m_nYMovement = m_nYMovement;
            move.m_subMode = m_subMode;

            return move;
        }

        #endregion

        public override string ToString()
        {
            return "move:(" + m_nXMovement + "," + m_nYMovement + ")";
        }

        /// <summary>
        /// <para>延X轴偏移量。正数为向右偏移，负数为向坐偏移。</para>
        /// </summary>
        private float m_nXMovement;
        /// <summary>
        /// <para>延Y轴偏移量。正数为向下偏移，负数为向上偏移。</para>
        /// </summary>
        private float m_nYMovement;
        /// <summary>
        /// <para>用户拉伸或收缩图元时的操作模式</para>
        /// </summary>
        private ScaleOpMode m_subMode = ScaleOpMode.None;
    }
}
