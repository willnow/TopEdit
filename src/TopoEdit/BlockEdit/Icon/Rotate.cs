//******************************************************************************
//文件名称 :     Rotate.cs
//版权信息 :     北京南车时代信息技术有限公司 版权所有
//创建日期 :     2014-07-04
//文件描述 :     
//修改履历 :

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
        /// dAngle大于0 顺时针 dAngle小于0 逆时针，弧度制
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
        /// 将一个矩形顺时针旋转指定角度，返回旋转后的多边形的四个点，0:左上点 1:右上点 2:右下点 3:左下点
        /// </summary>
        /// <param name="rect">被旋转矩形</param>
        /// <param name="degree">旋转角度，顺时针旋转</param>
        /// <returns>旋转后的多边形</returns>
        public static PointF[] ConvertRectToPolygon(RectangleF rect, float degree)
        {
            PointF[] polygonIconPoint = new PointF[4];
            //计算矩形中心点
            PointF center = TopoEdit.Utility.GetCenter(rect);
            //初始化旋转后矩形的四个点
            polygonIconPoint[0] = rect.Location;//左上点
            polygonIconPoint[1] = new PointF(rect.Right, rect.Top);//右上点
            polygonIconPoint[2] = new PointF(rect.Right, rect.Top + rect.Height);//右下点
            polygonIconPoint[3] = new PointF(rect.Left, rect.Top + rect.Height);//左下点
            //围绕该中心点旋转
            CSR.ShareLib.Rotate rotate = new CSR.ShareLib.Rotate();
            rotate.RotataPos = center;
            rotate.dAngle = Math.PI * degree / 180;
            rotate.LPtoDP(ref polygonIconPoint);

            Debug.Assert(polygonIconPoint != null);
            Debug.Assert(polygonIconPoint.Length == 4);

            return polygonIconPoint;
        }

        /// <summary>
        /// 将一个多边形（斜着的矩形）回正。该多边形四个点：0:左上点 1:右上点 2:右下点 3:左下点
        /// </summary>
        /// <param name="polygon">旋转矩形，注意该函数调用后本参数不会被修改</param>
        /// <returns>回正后的矩形</returns>
        public static RectangleF ConvertPolygonToRect(PointF[] polygon, out double angle)
        {
            Debug.Assert(polygon != null);
            Debug.Assert(polygon.Length == 4);

            PointF[] polygonPoint = new PointF[4];
            Array.Copy(polygon, polygonPoint, polygon.Length);

            RectangleF rect = new RectangleF();
            //计算多边形中心点
            PointF center = new PointF();
            center.X = (polygonPoint[0].X + polygonPoint[2].X) / 2;
            center.Y = (polygonPoint[0].Y + polygonPoint[2].Y) / 2;
            //计算旋转角度
            angle = Math.Atan2(polygonPoint[0].Y - center.Y, polygonPoint[0].X - center.X);
            //围绕中心点旋转
            CSR.ShareLib.Rotate rotate = new CSR.ShareLib.Rotate();
            rotate.RotataPos = center;
            rotate.dAngle = -angle;//向反方向回正
            for (int i = 0; i < polygonPoint.Length; ++i)
            {
                rotate.LPtoDP(ref polygonPoint[i]);
            }
            rect.Location = polygonPoint[0];
            rect.Width = Math.Abs(polygonPoint[1].X - polygonPoint[0].X);
            rect.Height = Math.Abs(polygonPoint[1].Y - polygonPoint[0].Y);

            return rect;
        }


        private PointF m_cRotataPos;//旋转参考点
        private double m_dAngle;//旋转角度
        /// <summary>
        /// <para>用户拉伸或收缩图元时的操作模式</para>
        /// </summary>
        private ScaleOpMode m_subMode = ScaleOpMode.None;
    }
}
