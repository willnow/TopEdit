//******************************************************************************
//�ļ����� :     Movement.cs
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
//******************************************************************************
using System;

using System.Drawing;
using TopoEdit.EventHandler;

namespace TopoEdit.Icon
{
    /// <summary>
    /// <para>��װ�Ի�ͼԪ�ص�;��ε�λ�Ʋ�����</para>
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
        /// <para>���úͷ�����X��ƫ����</para>
        /// </summary>
        /// <remarks>
        /// <para> ������ 2013-10-08  ��������</para>
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
        /// <para>���úͷ�����Y��ƫ����</para>
        /// </summary>
        /// <remarks>
        /// <para> ������ 2013-10-08  ��������</para>
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
        /// <para>��ʼ���ֶ�m_nXMovement��m_nYMovementΪ0</para>
        /// <para>ǰ��������</para>
        /// <para>    ��</para>
        /// <para>����������</para>
        /// <para>    �ֶ�m_nXMovement��m_nYMovement����ʼ��Ϊ0</para>
        /// </summary>
        /// <returns>
        /// <para></para>
        /// </returns>
        /// <remarks>
        /// <para> ������ 2013-10-08  ��������</para>
        /// </remarks>
        public Movement()
        {
            m_nXMovement = 0;
            m_nYMovement = 0;
        }
        /// <summary>
        /// <para>�����޸ĵ��λ�á�</para>
        /// <para>ǰ��������</para>
        /// <para>    rgPoints ��Ϊ��</para>
        /// <para>������������</para>
        /// </summary>
        /// <param name="rgPoints">��Ҫ�޸�λ�õĵ�ļ���</param>
        /// <returns>
        /// <para>�������rgPoints���޸�</para>
        /// </returns>
        /// <remarks>
        /// <para> ������ 2013-10-08  ��������</para>
        /// </remarks>
        public void LPtoDP(ref Point[] rgPoints)
        {
            for (int i = 0; i < rgPoints.Length; i++)
            {
                LPtoDP(ref rgPoints[i]);
            }
        }
        /// <summary>
        /// <para>�����޸ĵ��λ�á�</para>
        /// <para>ǰ��������</para>
        /// <para>    rgPoints ��Ϊ��</para>
        /// <para>������������</para>
        /// </summary>
        /// <param name="rgPoints">��Ҫ�޸�λ�õĵ�ļ���</param>
        /// <returns>
        /// <para>�������rgPoints���޸�</para>
        /// </returns>
        /// <remarks>
        /// <para> ������ 2013-10-08  ��������</para>
        /// </remarks>
        public void LPtoDP(ref PointF[] rgPoints)
        {
            for (int i = 0; i < rgPoints.Length; i++)
            {
                LPtoDP(ref rgPoints[i]);
            }
        }
        /// <summary>
        /// <para>�޸ĵ����λ�á���point������nXMovement������nYMovement��</para>
        /// <para>ǰ��������</para>
        /// <para>    point ��Ϊ��</para>
        /// <para>������������</para>
        /// </summary>
        /// <param name="point">��Ҫ�޸�λ�õĵ�</param>
        /// <returns>
        /// <para>�������point���޸ġ�</para>
        /// </returns>
        /// <remarks>
        /// <para> ������ 2013-10-08  ��������</para>
        /// </remarks>
        public void LPtoDP(ref Point point)//Ĭ��nCount=1
        {
            point.X = point.X + (int)m_nXMovement;
            point.Y = point.Y + (int)m_nYMovement;
        }
        /// <summary>
        /// <para>�޸ĵ����λ�á���point������nXMovement������nYMovement��</para>
        /// <para>ǰ��������</para>
        /// <para>    point ��Ϊ��</para>
        /// <para>������������</para>
        /// </summary>
        /// <param name="point">��Ҫ�޸�λ�õĵ�</param>
        /// <returns>
        /// <para>�������point���޸ġ�</para>
        /// </returns>
        /// <remarks>
        /// <para> ������ 2013-10-08  ��������</para>
        /// </remarks>
        public void LPtoDP(ref PointF point)
        {
            point.X += m_nXMovement;
            point.Y += m_nYMovement;
        }
        /// <summary>
        /// <para>�޸ľ��ο��λ�á���lpRect����nXMovement������nYMovement��</para>
        /// <para>ǰ��������</para>
        /// <para>    lpRect ��Ϊ��</para>
        /// <para>������������</para>
        /// </summary>
        /// <param name="lpRect">��Ҫ�޸�λ�õľ��ο�</param>
        /// <returns>
        /// <para>�������lpRect���޸�</para>
        /// </returns>
        /// <remarks>
        /// <para> ������ 2013-10-08  ��������</para>
        /// </remarks>
        public void LPtoDP(ref Rectangle lpRect)
        {
            //LPRECT
            lpRect.X = lpRect.X + (int)m_nXMovement;
            lpRect.Y = lpRect.Y + (int)m_nYMovement;
        }
        /// <summary>
        /// <para>�޸ľ��ο��λ�á���lpRect����nXMovement������nYMovement��</para>
        /// <para>ǰ��������</para>
        /// <para>    lpRect ��Ϊ��</para>
        /// <para>������������</para>
        /// </summary>
        /// <param name="lpRect">��Ҫ�޸�λ�õľ��ο�</param>
        /// <returns>
        /// <para>�������lpRect���޸�</para>
        /// </returns>
        /// <remarks>
        /// <para> ������ 2013-10-08  ��������</para>
        /// </remarks>
        public void LPtoDP(ref RectangleF lpRect)
        {
            //LPRECT
            lpRect.X = lpRect.X + m_nXMovement;
            lpRect.Y = lpRect.Y + m_nYMovement;
        }

        /// <summary>
        /// <para>�޸Ĵ�С����size�Ŀ������nXMovement���߶�����nYMovement��</para>
        /// <para>ǰ��������</para>
        /// <para>    size ��Ϊ��</para>
        /// <para>������������</para>
        /// </summary>
        /// <param name="size">��Ҫ�޸ĵĴ�С</param>
        /// <returns>
        /// <para>�������size���޸�</para>
        /// </returns>
        /// <remarks>
        /// <para> ��˸� 2014-09-18  ��������</para>
        /// </remarks>
        public void LPtoDP(ref SizeF size)
        {
            size.Width += m_nXMovement;
            size.Height += m_nYMovement;
        }

        /// <summary>
        /// ����һ����ָ������ƽ�Ƶ�ָ������ƶ�����
        /// </summary>
        /// <param name="rect">��ƽ�ƾ���</param>
        /// <param name="destRectCenter">Ŀ��������ĵ�</param>
        /// <returns></returns>
        public static Movement Create(RectangleF rect, PointF destRectCenter)
        {
            PointF rectCenter = new PointF(rect.Left + rect.Width / 2.0F, rect.Top + rect.Height / 2.0F);
            Movement move = new Movement();
            move.XMovement = destRectCenter.X - rectCenter.X;
            move.YMovement = destRectCenter.Y - rectCenter.Y;

            return move;
        }

        #region ICloneable ��Ա

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
        /// <para>��X��ƫ����������Ϊ����ƫ�ƣ�����Ϊ����ƫ�ơ�</para>
        /// </summary>
        private float m_nXMovement;
        /// <summary>
        /// <para>��Y��ƫ����������Ϊ����ƫ�ƣ�����Ϊ����ƫ�ơ�</para>
        /// </summary>
        private float m_nYMovement;
        /// <summary>
        /// <para>�û����������ͼԪʱ�Ĳ���ģʽ</para>
        /// </summary>
        private ScaleOpMode m_subMode = ScaleOpMode.None;
    }
}
