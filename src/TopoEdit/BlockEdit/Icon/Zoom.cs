//******************************************************************************
//�ļ����� :     Zoom.cs
//��Ȩ��Ϣ :     �����ϳ�ʱ����Ϣ�������޹�˾ ��Ȩ����
//�������� :     2013-10-08
//�ļ����� :
//�޸����� :
// V1.0.0    ������    2013-10-08    �������ļ�
// V1.1.1    ������    2013-10-21    ��ɵ����ֵ����Ĺ���
// V1.2.1    ������    2013-10-22    ���±���
// V1.3.2    ������    2013-10-25    ���ݡ�CUIT������Ա��桷V1.1.1�Ĳ��Խ���޸Ĵ��룬�޸�BUG
// V1.4.4    ������    2013-11-06    �޸����Է��ֵ�BUG
// V1.5.5    ������    2013-11-18    �޸���Ԫ���ԡ�ϵͳ���Է��ֵ�BUG
// V1.6.6    ������    2013-11-22    �޸�ϵͳ���Է��ֵ�BUG
// V1.18.23    JXN    2013-01-07    �޸����Է��ֵ�BUG
//******************************************************************************
using System;

using CSR.CUIT.GlobalService.ShareLib;
using System.Drawing;
using System.Diagnostics;
using TopoEdit.EventHandler;

namespace TopoEdit.Icon
{
    /// <summary>
    /// <para>��װ�Ի�ͼԪ�ص�;��ε����Ų�����</para>
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
        /// <para>��ʼ���ֶΣ���ʼ����ʾģʽΪ������ʾ��</para>
        /// <para>ǰ��������</para>
        /// <para>    ��</para>
        /// <para>������������</para>
        /// </summary>
        /// <returns>
        /// <para>m_dXRadio��m_dYRadio����ʼ��Ϊ1</para>
        /// </returns>
        /// <remarks>
        /// <para> ������ 2013-10-08  ��������</para>
        /// </remarks>
        public Zoom()
        {
            m_ZoomMode = EmZoomMode.None;
            m_dXRadio = 1;
            m_dYRadio = 1;
        }
        /// <summary>
        /// <para>���ڵȱ�������ʾģʽ���Ƚ�X�����ű�����Y�����ű�������������һ��ֵ��</para>
        /// <para>��X�����ű�����Y�����ű�����С��1ʱ�����������д��ֵ�����򷵻�������С��ֵ��</para>
        /// <para>ǰ��������</para>
        /// <para>    ��</para>
        /// <para>����������</para>
        /// <para>    �ȱ����ű���</para>
        /// </summary>
        /// <returns>
        /// <para></para>
        /// </returns>
        /// <remarks>
        /// <para> ������ 2013-10-08  ��������</para>
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
        /// <para>�������ŵ㡣</para>
        /// <para>ǰ��������</para>
        /// <para>    rgPoints ��Ϊ��</para>
        /// <para>������������</para>
        /// </summary>
        /// <param name="rgPoints">��Ҫ���ŵĵ�ļ���</param>
        /// <returns>
        /// <para>rgPointsֵ���ı�</para>
        /// </returns>
        /// <remarks>
        /// <para> ������ 2013-10-08  ��������</para>
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
        /// <para>���ŵ��㡣</para>
        /// <para>ǰ��������</para>
        /// <para>    point ��Ϊ��</para>
        /// <para>������������</para>
        /// </summary>
        /// <param name="point">��Ҫ���ŵĵ�</param>
        /// <returns>
        /// <para>pointֵ���ı�</para>
        /// </returns>
        /// <remarks>
        /// <para> ������ 2013-10-08  ��������</para>
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
        /// <para>���ŵ������ο�</para>
        /// <para>ǰ��������</para>
        /// <para>    lpRect ��Ϊ��</para>
        /// <para>������������</para>
        /// </summary>
        /// <param name="lpRect">��Ҫ���ŵľ���</param>
        /// <returns>
        /// <para>lpRectֵ���ı�</para>
        /// </returns>
        /// <remarks>
        /// <para> ������ 2013-10-08  ��������</para>
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
        /// <para>���ŵ���Size�ṹ��</para>
        /// <para>ǰ��������</para>
        /// <para>    lpSize ��Ϊ��</para>
        /// <para>������������</para>
        /// </summary>
        /// <param name="lpSize">��Ҫ���ŵ�Size</param>
        /// <returns>
        /// <para>lpSizeֵ���ı�</para>
        /// </returns>
        /// <remarks>
        /// <para> ������ 2013-10-08  ��������</para>
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
        /// <para>���ŵ���SizeF�ṹ��</para>
        /// <para>ǰ��������</para>
        /// <para>    lpSize ��Ϊ��</para>
        /// <para>������������</para>
        /// </summary>
        /// <param name="lpSize">��Ҫ���ŵ�SizeF</param>
        /// <returns>
        /// <para>lpSizeֵ���ı�</para>
        /// </returns>
        /// <remarks>
        /// <para> ������ 2013-10-08  ��������</para>
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
        /// ����һ����ָ���������ŵ�ָ���߳�������������������Ŷ���
        /// </summary>
        /// <param name="rect">�����ž���</param>
        /// <param name="len">�����α߳�</param>
        public static Zoom Create(RectangleF rect, int len)
        {
            RectangleF rectF = rect;

            float m_radio = 1.0F;//���ű�
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

        #region ICloneable ��Ա

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
        /// <para>������ʾģʽ</para>
        /// </summary>
        private EmZoomMode m_ZoomMode;//��ǰ����ʾ״̬
        /// <summary>
        /// <para>�û����������ͼԪʱ�Ĳ���ģʽ</para>
        /// </summary>
        private ScaleOpMode m_ZoomSubMode = ScaleOpMode.None;
        /// <summary>
        /// <para>��X�����ű���</para>
        /// </summary>
        private Double m_dXRadio;
        /// <summary>
        /// <para>��Y�����ű���</para>
        /// </summary>
        private Double m_dYRadio;
    }
}
