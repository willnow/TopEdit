//******************************************************************************
//�ļ����� :     Rotate.cs
//��Ȩ��Ϣ :     �����ϳ�ʱ����Ϣ�������޹�˾ ��Ȩ����
//�������� :     2014-07-04
//�ļ����� :     
//�޸����� :

//******************************************************************************
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using TopoEdit.EventHandler;
using System.Diagnostics;

namespace CSR.ShareLib
{
    public class Rotate
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

        public PointF RotataPos
        {
            get { return m_cRotataPos; }
            set { m_cRotataPos = value; }
        }
        /// <summary>
        /// dAngle����0 ˳ʱ�� dAngleС��0 ��ʱ�룬������
        /// </summary>
        public double dAngle
        {
            get { return m_dAngle; }
            set { m_dAngle = value; }
        }

        public void LPtoDP(ref Point[] rgPoints)
        {
            for (int i = 0; i < rgPoints.Length; i++)
            {
                LPtoDP(ref rgPoints[i]);
            }
        }
        public void LPtoDP(ref Point point)
        {
            Point cPos = point;
            point.X = (int)((cPos.X - m_cRotataPos.X) * Math.Cos(m_dAngle) - (cPos.Y - m_cRotataPos.Y) * Math.Sin(m_dAngle) + m_cRotataPos.X);
            point.Y = (int)((cPos.X - m_cRotataPos.X) * Math.Sin(m_dAngle) + (cPos.Y - m_cRotataPos.Y) * Math.Cos(m_dAngle) + m_cRotataPos.Y);
        }

        public void LPtoDP(ref PointF[] rgPoints)
        {
            for (int i = 0; i < rgPoints.Length; i++)
            {
                LPtoDP(ref rgPoints[i]);
            }
        }

        public void LPtoDP(ref PointF point)
        {
            PointF cPos = point;
            point.X = (float)((cPos.X - m_cRotataPos.X) * Math.Cos(m_dAngle) - (cPos.Y - m_cRotataPos.Y) * Math.Sin(m_dAngle) + m_cRotataPos.X);
            point.Y = (float)((cPos.X - m_cRotataPos.X) * Math.Sin(m_dAngle) + (cPos.Y - m_cRotataPos.Y) * Math.Cos(m_dAngle) + m_cRotataPos.Y);
        }

        /// <summary>
        /// ��һ������˳ʱ����תָ���Ƕȣ�������ת��Ķ���ε��ĸ��㣬0:���ϵ� 1:���ϵ� 2:���µ� 3:���µ�
        /// </summary>
        /// <param name="rect">����ת����</param>
        /// <param name="degree">��ת�Ƕȣ�˳ʱ����ת</param>
        /// <returns>��ת��Ķ����</returns>
        public static PointF[] ConvertRectToPolygon(RectangleF rect, float degree)
        {
            PointF[] polygonIconPoint = new PointF[4];
            //����������ĵ�
            PointF center = TopoEdit.Utility.GetCenter(rect);
            //��ʼ����ת����ε��ĸ���
            polygonIconPoint[0] = rect.Location;//���ϵ�
            polygonIconPoint[1] = new PointF(rect.Right, rect.Top);//���ϵ�
            polygonIconPoint[2] = new PointF(rect.Right, rect.Top + rect.Height);//���µ�
            polygonIconPoint[3] = new PointF(rect.Left, rect.Top + rect.Height);//���µ�
            //Χ�Ƹ����ĵ���ת
            CSR.ShareLib.Rotate rotate = new CSR.ShareLib.Rotate();
            rotate.RotataPos = center;
            rotate.dAngle = Math.PI * degree / 180;
            rotate.LPtoDP(ref polygonIconPoint);

            Debug.Assert(polygonIconPoint != null);
            Debug.Assert(polygonIconPoint.Length == 4);

            return polygonIconPoint;
        }

        /// <summary>
        /// ��һ������Σ�б�ŵľ��Σ��������ö�����ĸ��㣺0:���ϵ� 1:���ϵ� 2:���µ� 3:���µ�
        /// </summary>
        /// <param name="polygon">��ת���Σ�ע��ú������ú󱾲������ᱻ�޸�</param>
        /// <returns>������ľ���</returns>
        public static RectangleF ConvertPolygonToRect(PointF[] polygon, out double angle)
        {
            Debug.Assert(polygon != null);
            Debug.Assert(polygon.Length == 4);

            PointF[] polygonPoint = new PointF[4];
            Array.Copy(polygon, polygonPoint, polygon.Length);

            RectangleF rect = new RectangleF();
            //�����������ĵ�
            PointF center = new PointF();
            center.X = (polygonPoint[0].X + polygonPoint[2].X) / 2;
            center.Y = (polygonPoint[0].Y + polygonPoint[2].Y) / 2;
            //������ת�Ƕ�
            angle = Math.Atan2(polygonPoint[0].Y - center.Y, polygonPoint[0].X - center.X);
            //Χ�����ĵ���ת
            CSR.ShareLib.Rotate rotate = new CSR.ShareLib.Rotate();
            rotate.RotataPos = center;
            rotate.dAngle = -angle;//�򷴷������
            for (int i = 0; i < polygonPoint.Length; ++i)
            {
                rotate.LPtoDP(ref polygonPoint[i]);
            }
            rect.Location = polygonPoint[0];
            rect.Width = Math.Abs(polygonPoint[1].X - polygonPoint[0].X);
            rect.Height = Math.Abs(polygonPoint[1].Y - polygonPoint[0].Y);

            return rect;
        }


        private PointF m_cRotataPos;//��ת�ο���
        private double m_dAngle;//��ת�Ƕ�
        /// <summary>
        /// <para>�û����������ͼԪʱ�Ĳ���ģʽ</para>
        /// </summary>
        private ScaleOpMode m_subMode = ScaleOpMode.None;
    }
}
