//******************************************************************************
//文件名称 :     Zoom.cs
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
// V1.18.23    JXN    2013-01-07    修复测试发现的BUG
//******************************************************************************
using System;

using CSR.CUIT.GlobalService.ShareLib;
using System.Drawing;
using System.Diagnostics;
using TopoEdit.EventHandler;

namespace TopoEdit.Icon
{
    /// <summary>
    /// <para>封装对绘图元素点和矩形的缩放操作。</para>
    /// </summary>
    public class Zoom : ICloneable
    {
        public EmZoomMode ZoomMode
        {
            get
            {
                return m_ZoomMode;
            }
            set
            {
                m_ZoomMode = value;
            }
        }

        public ScaleOpMode SubMode
        {
            get
            {
                return m_ZoomSubMode;
            }
            set
            {
                m_ZoomSubMode = value;
            }
        }

        public Double YRadio
        {
            get
            {
                return m_dYRadio;
            }
            set
            {
                Debug.Assert(value > 0);
                m_dYRadio = value;
            }
        }

        public Double XRadio
        {
            get
            {
                return m_dXRadio;
            }
            set
            {
                Debug.Assert(value > 0);
                m_dXRadio = value;
            }
        }

        /// <summary>
        /// <para>初始化字段，初始化显示模式为正常显示。</para>
        /// <para>前置条件：</para>
        /// <para>    无</para>
        /// <para>后置条件：无</para>
        /// </summary>
        /// <returns>
        /// <para>m_dXRadio和m_dYRadio被初始化为1</para>
        /// </returns>
        /// <remarks>
        /// <para> 蒋湘宁 2013-10-08  创建函数</para>
        /// </remarks>
        public Zoom()
        {
            m_ZoomMode = EmZoomMode.None;
            m_dXRadio = 1;
            m_dYRadio = 1;
        }
        /// <summary>
        /// <para>用于等比缩放显示模式，比较X轴缩放比例与Y轴缩放比例，返回其中一个值。</para>
        /// <para>当X轴缩放比例与Y轴缩放比例都小于1时，返回两者中大的值，否则返回两者中小的值。</para>
        /// <para>前置条件：</para>
        /// <para>    无</para>
        /// <para>后置条件：</para>
        /// <para>    等比缩放比例</para>
        /// </summary>
        /// <returns>
        /// <para></para>
        /// </returns>
        /// <remarks>
        /// <para> 蒋湘宁 2013-10-08  创建函数</para>
        /// </remarks>
        public Double CalcRadio()
        {
            Double radio = m_dXRadio < m_dYRadio ? m_dXRadio : m_dYRadio;
            if (m_dYRadio < 1 && m_dXRadio < 1)
            {
                radio = m_dXRadio > m_dYRadio ? m_dXRadio : m_dYRadio;
            }
            return radio;
        }
        /// <summary>
        /// <para>批量缩放点。</para>
        /// <para>前置条件：</para>
        /// <para>    rgPoints 不为空</para>
        /// <para>后置条件：无</para>
        /// </summary>
        /// <param name="rgPoints">需要缩放的点的集合</param>
        /// <returns>
        /// <para>rgPoints值被改变</para>
        /// </returns>
        /// <remarks>
        /// <para> 蒋湘宁 2013-10-08  创建函数</para>
        /// </remarks>
        public void LPtoDP(ref Point[] lpPoints)
        {
            for (int i = 0; i < lpPoints.Length; i++)
            {
                LPtoDP(ref lpPoints[i]);
            }
        }
        public void LPtoDP(ref PointF[] lpPoints)
        {
            for (int i = 0; i < lpPoints.Length; i++)
            {
                LPtoDP(ref lpPoints[i]);
            }
        }
        /// <summary>
        /// <para>缩放单点。</para>
        /// <para>前置条件：</para>
        /// <para>    point 不为空</para>
        /// <para>后置条件：无</para>
        /// </summary>
        /// <param name="point">需要缩放的点</param>
        /// <returns>
        /// <para>point值被改变</para>
        /// </returns>
        /// <remarks>
        /// <para> 蒋湘宁 2013-10-08  创建函数</para>
        /// </remarks>
        public void LPtoDP(ref Point point)
        {
            switch (m_ZoomMode)
            {
                case EmZoomMode.NonUniformScale:
                    {
                        point.X = (int)Math.Round(point.X * m_dXRadio);
                        point.Y = (int)Math.Round(point.Y * m_dYRadio);
                        break;
                    }
                case EmZoomMode.UniformScale:
                    {
                        double radio = CalcRadio();
                        point.X = (int)Math.Round(point.X * radio);
                        point.Y = (int)Math.Round(point.Y * radio);
                        break;
                    }
                case EmZoomMode.ActualSize:
                case EmZoomMode.None:
                default:
                    break;
            }
        }
        public void LPtoDP(ref PointF point)
        {
            switch (m_ZoomMode)
            {
                case EmZoomMode.NonUniformScale:
                    {
                        point.X = (float)((point.X) * m_dXRadio);
                        point.Y = (float)((point.Y) * m_dYRadio);
                        break;
                    }
                case EmZoomMode.UniformScale:
                    {
                        double radio = CalcRadio();
                        point.X = (float)((point.X) * radio);
                        point.Y = (float)((point.Y) * radio);
                        break;
                    }
                default:
                    break;
            }
        }
        /// <summary>
        /// <para>缩放单个矩形框。</para>
        /// <para>前置条件：</para>
        /// <para>    lpRect 不为空</para>
        /// <para>后置条件：无</para>
        /// </summary>
        /// <param name="lpRect">需要缩放的矩形</param>
        /// <returns>
        /// <para>lpRect值被改变</para>
        /// </returns>
        /// <remarks>
        /// <para> 蒋湘宁 2013-10-08  创建函数</para>
        /// </remarks>
        public void LPtoDP(ref Rectangle lpRect)
        {
            switch (m_ZoomMode)
            {
                case EmZoomMode.NonUniformScale:
                    lpRect.X = (int)Math.Round(lpRect.X * m_dXRadio);
                    lpRect.Y = (int)Math.Round(lpRect.Y * m_dYRadio);
                    lpRect.Width = (int)Math.Round(lpRect.Width * m_dXRadio);
                    lpRect.Height = (int)Math.Round(lpRect.Height * m_dYRadio);
                    break;
                case EmZoomMode.UniformScale:
                    double radio = CalcRadio();
                    lpRect.X = (int)Math.Round(lpRect.X * radio);
                    lpRect.Y = (int)Math.Round(lpRect.Y * radio);
                    lpRect.Width = (int)Math.Round(lpRect.Width * radio);
                    lpRect.Height = (int)Math.Round(lpRect.Height * radio);
                    break;
                default:
                    break;
            }
            return;
        }
        /// <summary>
        /// <para>缩放单个Size结构。</para>
        /// <para>前置条件：</para>
        /// <para>    lpSize 不为空</para>
        /// <para>后置条件：无</para>
        /// </summary>
        /// <param name="lpSize">需要缩放的Size</param>
        /// <returns>
        /// <para>lpSize值被改变</para>
        /// </returns>
        /// <remarks>
        /// <para> 蒋湘宁 2013-10-08  创建函数</para>
        /// </remarks>
        public void LPtoDP(ref Size lpSize)
        {
            switch (m_ZoomMode)
            {
                case EmZoomMode.NonUniformScale:
                    lpSize.Width = (int)Math.Round(lpSize.Width * m_dXRadio);
                    lpSize.Height = (int)Math.Round(lpSize.Height * m_dYRadio);
                    break;
                case EmZoomMode.UniformScale:
                    double radio = CalcRadio();
                    lpSize.Width = (int)Math.Round(lpSize.Width * radio);
                    lpSize.Height = (int)Math.Round(lpSize.Height * radio);
                    break;
                default:
                    break;
            }
            return;
        }
        /// <summary>
        /// <para>缩放单个SizeF结构。</para>
        /// <para>前置条件：</para>
        /// <para>    lpSize 不为空</para>
        /// <para>后置条件：无</para>
        /// </summary>
        /// <param name="lpSize">需要缩放的SizeF</param>
        /// <returns>
        /// <para>lpSize值被改变</para>
        /// </returns>
        /// <remarks>
        /// <para> 蒋湘宁 2013-10-08  创建函数</para>
        /// </remarks>
        public void LPtoDP(ref SizeF lpSize)
        {
            switch (m_ZoomMode)
            {
                case EmZoomMode.NonUniformScale:
                    lpSize.Width = (float)(lpSize.Width * m_dXRadio);
                    lpSize.Height = (float)(lpSize.Height * m_dYRadio);
                    break;
                case EmZoomMode.UniformScale:
                    double radio = CalcRadio();
                    lpSize.Width = (float)(lpSize.Width * radio);
                    lpSize.Height = (float)(lpSize.Height * radio);
                    break;
                default:
                    break;
            }
        }
        public void LPtoDP(ref float fontSize)
        {
            switch (m_ZoomMode)
            {
                case EmZoomMode.NonUniformScale:
                    {
                        fontSize = (float)(fontSize * m_dXRadio * m_dYRadio);
                        break;
                    }
                case EmZoomMode.UniformScale:
                    {
                        double radio = CalcRadio();
                        fontSize = (float)(fontSize * radio);
                        break;
                    }
                case EmZoomMode.ActualSize:
                case EmZoomMode.None:
                default:
                    {
                        break;
                    }
            }
        }

        /// <summary>
        /// 创建一个将指定矩形缩放到指定边长的正方形内所需的缩放对象
        /// </summary>
        /// <param name="rect">被缩放矩形</param>
        /// <param name="len">正方形边长</param>
        public static Zoom Create(RectangleF rect, int len)
        {
            RectangleF rectF = rect;

            float m_radio = 1.0F;//缩放比
            if (rectF.Width > rectF.Height)
            {
                m_radio = len / rectF.Width;
            }
            else
            {
                m_radio = len / rectF.Height;
            }

            Zoom zoom = new Zoom();
            zoom.ZoomMode = EmZoomMode.UniformScale;
            zoom.XRadio = m_radio;
            zoom.YRadio = m_radio;

            return zoom;
        }

        #region ICloneable 成员

        public object Clone()
        {
            Zoom zoom = new Zoom();
            zoom.m_dXRadio = m_dXRadio;
            zoom.m_dYRadio = m_dYRadio;
            zoom.m_ZoomMode = m_ZoomMode;
            zoom.m_ZoomSubMode = m_ZoomSubMode;

            return zoom;
        }

        #endregion

        /// <summary>
        /// <para>缩放显示模式</para>
        /// </summary>
        private EmZoomMode m_ZoomMode;//当前的显示状态
        /// <summary>
        /// <para>用户拉伸或收缩图元时的操作模式</para>
        /// </summary>
        private ScaleOpMode m_ZoomSubMode = ScaleOpMode.None;
        /// <summary>
        /// <para>沿X轴缩放比例</para>
        /// </summary>
        private Double m_dXRadio;
        /// <summary>
        /// <para>沿Y轴缩放比例</para>
        /// </summary>
        private Double m_dYRadio;
    }
}
