//******************************************************************************
//�ļ����� :     IconRectangle.cs
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
// V1.14.16    JXN    2013-12-22    �޸����Է��ֵ�BUG
//******************************************************************************
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using CSR.CUIT.GlobalService.ShareLib;
using TopoEdit.Icon;
using System.Xml;
using TopoEdit.EventHandler;
using TopoEdit.Stratege;
using TopoEdit.Visitor;

namespace TopoEdit.Icon
{
    /// <summary>
    /// <para>���������ͼԪ��ص����ԺͲ�����</para>
    /// </summary>
    public class IconRectangle : IIcon
    {
        #region ��Ա����
        /// <summary>
        /// <para>�������Ͻǵ�����</para>
        /// </summary>
        private PointF m_position = new PointF(0 , 0);
        /// <summary>
        /// <para>���ο�</para>
        /// </summary>
        private float m_width = 1;
        /// <summary>
        /// <para>���θ�</para>
        /// </summary>
        private float m_height = 1;
        /// <summary>
        /// <para>�����߿�</para>
        /// </summary>
        private float m_weight = 1;
        /// <summary>
        /// <para>�����Ƿ����</para>
        /// </summary>
        private Boolean m_fill = false;
        private DashStyle m_panDashStyle = DashStyle.Solid;
        /// <summary>
        /// <para>������ת�Ƕȡ�������ת��Ϊ0��</para>
        /// </summary>
        private int m_degree = 0;
        /// <summary>
        /// <para>������ת���ȡ����㹫ʽ ���� = �� * �Ƕ� / 180</para>
        /// <para>�����δ�����ת�Ƕ�ʱ����ֵ��Ч��</para>
        /// </summary>
        private double m_angle = 0;
        /// <summary>
        /// <para>���ε��ĸ��������ֵ��</para>
        /// <para>�����δ�����ת�Ƕ�ʱ����ֵ��Ч��</para>
        /// </summary>
        private PointF[] m_polygonIconPoint = new PointF[4];
        
        #endregion

        /// <summary>
        /// <para>���ø���Ĺ��캯�����г�ʼ����</para>
        /// <para>ǰ��������</para>
        /// <para>    ��</para>
        /// <para>����������</para>
        /// <para>    �����ֶα�������ֵ��</para>
        /// </summary>
        /// <param name="rtu">ͼԪ����Rtu</param>
        /// <returns>
        /// <para></para>
        /// </returns>
        /// <remarks>
        /// <para> ������ 2013-10-08  ��������</para>
        /// </remarks>
        public IconRectangle()
        : base(EmIconType.Rectangle)
        {

        }
        /// <summary>
        /// <para>���ø���Ĺ��캯�����г�ʼ����</para>
        /// </summary>
        /// <param name="position">�������Ͻ�����</param>
        /// <param name="width">���</param>
        /// <param name="height">�߶�</param>
        /// <param name="isFill">�Ƿ����</param>
        public IconRectangle(PointF position, float width, float height, bool isFill, DashStyle dashStyle)
            : base(EmIconType.Rectangle)
        {
            m_position = position;
            m_width = width;
            m_height = height;
            m_fill = isFill;
            m_panDashStyle = dashStyle;
        }
        /// <summary>
        /// <para>��ȡ�����þ������Ͻ�����</para>
        /// </summary>
        /// <remarks>
        /// <para> ������ 2013-10-08  ��������</para>
        /// </remarks>
        public PointF Position
        {
            get
            {
                return m_position;
            }
            set
            {
                m_position = value;
            }
        }

        /// <summary>
        /// ���ο�
        /// </summary>
        public float Width
        {
            get
            {
                return m_width;
            }
            set
            {
                m_width = value;
                ConvertRectToPolygon();
            }
        }

        /// <summary>
        /// ���θ�
        /// </summary>
        public float Height
        {
            get
            {
                return m_height;
            }
            set
            {
                m_height = value;
                ConvertRectToPolygon();
            }
        }

        public Boolean Fill
        {
            get { return m_fill; }
            set { m_fill = value; }
        }

        public float Weight
        {
            get { return m_weight; }
            set { m_weight = value; }
        }

        public int Degree
        {
            get { return m_degree; }
            set 
            {
                m_degree = value % 180;
                ConvertRectToPolygon();
            }
        }

        public PointF[] Polygon
        {
            get
            {
                PointF[] polygonPoint = new PointF[4];
                Array.Copy(m_polygonIconPoint, polygonPoint, polygonPoint.Length);

                return polygonPoint;
            }
        }
        #region ��дIIcon ��Ա
        /// <summary>
        /// <para>��дIIcon���ԣ����ͼԪ��Ĭ�ϱ߽��С��</para>
        /// </summary>
        /// <remarks>
        /// <para> ������ 2013-10-08  ��������</para>
        /// </remarks>
        public override sealed RectangleF BoundsRect
        {
            get
            {
                if (m_degree == 0)
                {
                    return GetDefaultBoundOfRectangleNormal();
                }
                else
                {
                    return GetDefaultBoundOfRectangleWithAngle();
                }
            }
        }
        /// <summary>
        /// <para>��дIIcon�������������������ʼ����Ա��</para>
        /// <para>ǰ��������</para>
        /// <para>    iconNode ��Ϊ��</para>
        /// <para>������������</para>
        /// </summary>
        /// <param name="iconNode">�������ļ��ж�ȡ���������ݳ�ʼ���ַ�������</param>
        /// <returns>
        /// <para>true����ʼ���ɹ�</para>
        /// <para>false����ʼ��ʧ��</para>
        /// </returns>
        /// <remarks>
        /// <para> ������ 2013-10-08  ��������</para>
        /// </remarks>
        public override bool Load(System.Xml.XmlNode iconNode)
        {
            Debug.Assert(null != iconNode);

            //���ػ�������
            base.Load(iconNode);

            //������������
            XmlNode rectNode = iconNode.SelectSingleNode("Rect");

            m_position.X = float.Parse(rectNode.SelectSingleNode("Pos/X").InnerText);
            m_position.Y = float.Parse(rectNode.SelectSingleNode("Pos/Y").InnerText);
            m_width = float.Parse(rectNode.SelectSingleNode("Width").InnerText);
            m_height = float.Parse(rectNode.SelectSingleNode("Height").InnerText);
            m_weight = float.Parse(rectNode.SelectSingleNode("Weight").InnerText);

            if (m_weight < 1)
            {
                m_weight = 1;
            }

            m_fill = bool.Parse(rectNode.SelectSingleNode("Fill").InnerText);
            m_degree = int.Parse(rectNode.SelectSingleNode("Angle").InnerText);
            ConvertRectToPolygon();
            return true;
        }

        private void ConvertRectToPolygon()
        {
            m_polygonIconPoint = CSR.ShareLib.Rotate.ConvertRectToPolygon(GetDefaultBoundOfRectangleNormal(), m_degree);
            m_angle = Math.PI * m_degree / 180;
        }

        private void ConvertPolygonToRect()
        {
            double angle;
            RectangleF rect = CSR.ShareLib.Rotate.ConvertPolygonToRect(m_polygonIconPoint, out angle);
            m_position = rect.Location;
            m_width = rect.Width;
            m_height = rect.Height;
            m_degree = (int)Math.Round(m_angle * 180 / Math.PI);
            if (m_degree < 0)
            {
                m_degree += 180;
            }
        }

        public override bool Save(System.Xml.XmlNode iconParentNode)
        {
            System.Xml.XmlNode iconNode = iconParentNode.OwnerDocument.CreateElement("Icon");
            base.Save(iconNode);

            System.Xml.XmlNode node = iconParentNode.OwnerDocument.CreateElement("Rect");

            string fill = "false";
            if (m_fill)
            {
                fill = "true";
            }

            if (m_weight < 1)
            {
                m_weight = 1;
            }

            Round();


            node.InnerXml = "<Pos><X>" + m_position.X + "</X><Y>" + m_position.Y + "</Y></Pos><Width>"
                + m_width + "</Width><Height>" + m_height + "</Height><Weight>" + m_weight + "</Weight><Fill>"
                + fill + "</Fill><Angle>" + m_degree + "</Angle>";
            iconNode.AppendChild(node);
            iconParentNode.AppendChild(iconNode);
            return true;
        }

        public override void Round()
        {
            m_position = Utility.ConvertPos(m_position);
            Utility.ConvertValue(ref m_width);
            Utility.ConvertValue(ref m_height);
        }

        public override void Zoom(Zoom zoom)
        {
            if (m_degree == 0)
            {
                switch(zoom.SubMode)
                {
                    case ScaleOpMode.Right:
                    case ScaleOpMode.RightDown:
                    case ScaleOpMode.Down:
                        {
                            ZoomDiameter(zoom);
                            break;
                        }
                    case ScaleOpMode.None:
                        {
                            zoom.LPtoDP(ref m_position);
                            ZoomDiameter(zoom);
                            break;
                        }
                    case ScaleOpMode.Up:
                    case ScaleOpMode.LeftUp:
                    case ScaleOpMode.Left:
                        {
                            //��¼����ǰ�߽�
                            RectangleF rect = BoundsRect;
                            //���ų���
                            ZoomDiameter(zoom);
                            //���¼������Ͻ�����
                            m_position.Y = rect.Bottom - m_height;
                            m_position.X = rect.Right - m_width;
                            break;
                        }
                    case ScaleOpMode.RightUp:
                        {
                            //��¼����ǰ�߽�
                            RectangleF rect = BoundsRect;
                            //���ų���
                            ZoomDiameter(zoom);
                            //���¼������Ͻ�����
                            m_position.Y = rect.Bottom - m_height;
                            break;
                        }
                    case ScaleOpMode.LeftDown:
                        {
                            //��¼����ǰ�߽�
                            RectangleF rect = BoundsRect;
                            //���ų���
                            ZoomDiameter(zoom);
                            //���¼������Ͻ�����
                            m_position.X = rect.Right - m_width;
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
                ConvertRectToPolygon();//�޸���ԭʼ���Σ������¼�������
            }
            else
            {
                zoom.LPtoDP(ref m_position);
                ZoomDiameter(zoom);
                zoom.LPtoDP(ref m_polygonIconPoint);
            }

            Debug.Assert(m_width >= 0.01);
            Debug.Assert(m_height >= 0.01);
        }

        private void ZoomDiameter(Zoom zoom)
        {
            SizeF diameter = new SizeF();
            diameter.Width = m_width;
            diameter.Height = m_height;
            zoom.LPtoDP(ref diameter);
            m_width = Math.Max(0.02F, diameter.Width);
            m_height = Math.Max(0.02F, diameter.Height);

            Debug.Assert(m_width >= 0.01);
            Debug.Assert(m_height >= 0.01);
        }

        private void MoveDiameter(Movement move)
        {
            SizeF diameter = new SizeF();
            diameter.Width = m_width;
            diameter.Height = m_height;
            move.LPtoDP(ref diameter);

            if (diameter.Width > 0.01)
            {
                m_width = diameter.Width;
            }

            if (diameter.Height > 0.01)
            {
                m_height = diameter.Height;
            }
            

            Debug.Assert(m_width >= 0.01);
            Debug.Assert(m_height >= 0.01);
        }

        public override void Rotate(CSR.ShareLib.Rotate rotate)
        {
            throw new NotImplementedException();
        }

        public override void Move(Movement paraMove)
        {
            Movement move = paraMove.Clone() as Movement;
            if (m_degree == 0)
            {
                switch (move.SubMode)
                {
                    case ScaleOpMode.Right:
                    case ScaleOpMode.RightDown:
                    case ScaleOpMode.Down:
                        {
                            MoveDiameter(move);
                            break;
                        }
                    case ScaleOpMode.None:
                        {
                            move.LPtoDP(ref m_position);
                            break;
                        }
                    case ScaleOpMode.Up:
                    case ScaleOpMode.LeftUp:
                    case ScaleOpMode.Left:
                        {
                            //��¼����ǰ�߽�
                            RectangleF rect = BoundsRect;
                            //���ų���
                            //���������ʱ��move������x��yС��0
                            //�����ڳ��������������෴
                            move.XMovement = -move.XMovement;
                            move.YMovement = -move.YMovement;
                            MoveDiameter(move);
                            //���¼������Ͻ�����
                            m_position.Y = rect.Bottom - m_height;
                            m_position.X = rect.Right - m_width;
                            break;
                        }
                    case ScaleOpMode.RightUp:
                        {
                            //��¼����ǰ�߽�
                            RectangleF rect = BoundsRect;
                            //���ų���
                            move.YMovement = -move.YMovement;
                            MoveDiameter(move);
                            //���¼������Ͻ�����
                            m_position.Y = rect.Bottom - m_height;
                            break;
                        }
                    case ScaleOpMode.LeftDown:
                        {
                            //��¼����ǰ�߽�
                            RectangleF rect = BoundsRect;
                            //���ų���
                            move.XMovement = -move.XMovement;
                            MoveDiameter(move);
                            //���¼������Ͻ�����
                            m_position.X = rect.Right - m_width;
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }

            }
            else
            {
                if (move.SubMode != ScaleOpMode.None)
                {
                    MovePolygon(move, new Zoom());
                }
                else
                {
                    move.LPtoDP(ref m_polygonIconPoint);
                    move.LPtoDP(ref m_position);
                }
            }

            if (m_width < 0.01)
            {
                m_width = 0.01F;
            }

            if (m_height < 0.01)
            {
                m_height = 0.01F;
            }

            //Debug.Assert(m_width >= 0.01);
            //Debug.Assert(m_height >= 0.01);
        }

        public void MovePolygon(Movement move, Zoom zoom)
        {
            switch (move.SubMode)
            {
                case ScaleOpMode.LeftDown:
                    {
                        //���Ƕ� > 90 ʱ��Ч
                        if (m_degree > 90)
                        {
                            Movement rectMove = CalcMoveByAnchorMove(move);


                            rectMove.LPtoDP(ref m_polygonIconPoint[1]);
                            rectMove.LPtoDP(ref m_polygonIconPoint[2]);

                            ConvertPolygonToRect();//�޸��˶���Σ������¼���ԭʼ����
                        }
                        break;
                    }
                case ScaleOpMode.RightDown:
                    {
                        //���Ƕ� < 90 ʱ��Ч
                        if (m_degree > 90)
                        {
                            Movement rectMove = CalcMoveByAnchorMove(move);

                            rectMove.LPtoDP(ref m_polygonIconPoint[1]);
                            rectMove.LPtoDP(ref m_polygonIconPoint[2]);

                            ConvertPolygonToRect();//�޸��˶���Σ������¼���ԭʼ����
                        }
                        break;
                    }
                case ScaleOpMode.RightUp:
                    {
                        //���Ƕ� > 90 ʱ��Ч
                        if (m_degree > 90)
                        {
                            Movement rectMove = CalcMoveByAnchorMove(move);

                            rectMove.LPtoDP(ref m_polygonIconPoint[0]);
                            rectMove.LPtoDP(ref m_polygonIconPoint[3]);

                            ConvertPolygonToRect();//�޸��˶���Σ������¼���ԭʼ����
                        }
                        break;
                    }
                case ScaleOpMode.LeftUp:
                    {
                        //���Ƕ� < 90 ʱ��Ч
                        if (m_degree > 90)
                        {
                            Movement rectMove = CalcMoveByAnchorMove(move);

                            rectMove.LPtoDP(ref m_polygonIconPoint[0]);
                            rectMove.LPtoDP(ref m_polygonIconPoint[3]);

                            ConvertPolygonToRect();//�޸��˶���Σ������¼���ԭʼ����
                        }
                        break;
                    }
                case ScaleOpMode.Up:
                    {
                        Movement rectMove = CalcMoveByAnchorMove(move);

                        rectMove.LPtoDP(ref m_polygonIconPoint[0]);
                        rectMove.LPtoDP(ref m_polygonIconPoint[3]);

                        ConvertPolygonToRect();//�޸��˶���Σ������¼���ԭʼ����
                        break;
                    }
                case ScaleOpMode.Down:
                    {
                        Movement rectMove = CalcMoveByAnchorMove(move);

                        rectMove.LPtoDP(ref m_polygonIconPoint[1]);
                        rectMove.LPtoDP(ref m_polygonIconPoint[2]);

                        ConvertPolygonToRect();//�޸��˶���Σ������¼���ԭʼ����

                        break;
                    }
                case ScaleOpMode.None:
                    {
                        //�ȱ����Ŵ���С��ֻ��Ҫ����ÿ���㼴��
                        zoom.LPtoDP(ref m_position);
                        ZoomDiameter(zoom);
                        zoom.LPtoDP(ref m_polygonIconPoint);

                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

        public override void Symmetry(Symmetry symn)
        {
            symn.LPtoDP(ref m_polygonIconPoint);
            ConvertPolygonToRect();
        }

        public override void Accept(TopoEdit.Visitor.IDrawVisitor visitor)
        {
            visitor.VisitorRectangle(this);
        }

        /// <summary>
        /// ��ê����ƶ�����ת��Ϊ��ת���ζ˵���ƶ�����
        /// </summary>
        /// <param name="move"></param>
        /// <returns></returns>
        private Movement CalcMoveByAnchorMove(Movement move)
        {
            if((m_degree == 90) || (m_degree == 0))
            {
                //��ֱ��ˮƽ�����������ƶ�������ͬ
                return move.Clone() as Movement;
            }
            else
            {
                //������һ��б��
                Movement rectMove = move.Clone() as Movement;

                if (Math.Abs(rectMove.XMovement) > Math.Abs(rectMove.YMovement))
                {
                    rectMove.YMovement = (float)(rectMove.XMovement * Math.Tan(m_angle));
                }
                else
                {
                    rectMove.XMovement = (float)(rectMove.YMovement / Math.Tan(m_angle));
                }
                
                return rectMove;
            }
        }

        /// <summary>
        /// <para>��дIIcon�������ж�������Ƿ�λ��ͼԪ�����������ڡ�</para>
        /// <para>ǰ��������</para>
        /// <para>    point ��Ϊ��</para>
        /// <para>������������</para>
        /// </summary>
        /// <param name="point">�����</param>
        /// <returns>
        /// <para>true�������λ��ͼԪ������������</para>
        /// <para>false�������û��λ��ͼԪ������������</para>
        /// </returns>
        /// <remarks>
        /// <para> ������ 2013-10-08  ��������</para>
        /// </remarks>
        public override sealed IDraw Intersect(Point point)
        {
            if (!Visible)
            {
                return null;
            }

            if (m_degree == 0)
            {
                return IntersectInRectangleNormal(point);
            }
            else
            {
                return IntersectInRectangleWithAngle(point);
            }
        }



        #endregion

        #region IDraw ��Ա
        /// <summary>
        /// <para>��дIIcon����������ͼԪ��</para>
        /// <para>ǰ��������</para>
        /// <para>    cGraphics ��Ϊ��</para>
        /// <para>������������</para>
        /// </summary>
        /// <param name="cGraphics">��װһ��GDI+��ͼͼ��</param>
        /// <returns>
        /// <para>��</para>
        /// </returns>
        /// <remarks>
        /// <para> ������ 2013-10-08  ��������</para>
        /// </remarks>
        public override sealed void Draw(Graphics cGraphics, RectangleF cRect)
        {
            //ͼԪ���ɼ�����ͼԪ��Ϊ����û�кͻ��������н�������ʱ����Ҫ���Ƶ�ǰͼԪ
            if (!Visible || (!cRect.IsEmpty && !this.BoundsRect.IntersectsWith(cRect)))
            {
                return;
            }

            Debug.Assert(m_degree >= 0 && m_degree < 360);
            if (m_degree == 0)
            {
                DrawRectangleNormal(cGraphics);
            }
            else
            {
                DrawRectangleWithAngle(cGraphics);
            }
        }
        /// <summary>
        /// <para>��������ת���Ρ�</para>
        /// <para>ǰ��������</para>
        /// <para>    cGraphics ��Ϊ��</para>
        /// <para>������������</para>
        /// </summary>
        /// <param name="cGraphics">��װһ��GDI+��ͼͼ��</param>
        /// <returns>
        /// <para>��</para>
        /// </returns>
        /// <remarks>
        /// <para> ������ 2013-10-08  ��������</para>
        /// </remarks>
        private void DrawRectangleNormal(Graphics cGraphics)
        {
            PointF pt = m_position;
            SizeF diameter = new SizeF();
            diameter.Width = m_width;
            diameter.Height = m_height;

            SolidBrush brush = new SolidBrush(GetDisplayColor());
            Pen pen = new Pen(GetDisplayColor(), m_weight);
            pen.DashStyle = m_panDashStyle;

            if (pen.DashStyle == DashStyle.Dot)
            {
                pen.DashPattern = new float[] { 5, 5 };
            }
            

            RectangleF rect = new RectangleF(pt.X, pt.Y, diameter.Width, diameter.Height);

            if (m_fill)
            {
                cGraphics.FillRectangle(brush, rect);
            }
            cGraphics.DrawRectangle(pen, TopoEdit.Utility.ConvertRect(rect));
        }
        /// <summary>
        /// <para>������ת���Ρ�</para>
        /// <para>ǰ��������</para>
        /// <para>    cGraphics ��Ϊ��</para>
        /// <para>������������</para>
        /// </summary>
        /// <param name="cGraphics">��װһ��GDI+��ͼͼ��</param>
        /// <returns>
        /// <para>��</para>
        /// </returns>
        /// <remarks>
        /// <para> ������ 2013-10-08  ��������</para>
        /// </remarks>
        private void DrawRectangleWithAngle(Graphics cGraphics)
        {
            PointF[] polygonPoint = new PointF[m_polygonIconPoint.Length];
            Array.Copy(m_polygonIconPoint, polygonPoint, m_polygonIconPoint.Length);

            GraphicsPath path = new GraphicsPath();
            path.AddPolygon(polygonPoint);

            if (m_fill)
            {
                SolidBrush brush = new SolidBrush(GetDisplayColor());
                cGraphics.FillPath(brush, path);
            }
            else
            {
                Pen pen = new Pen(DefaultColor.ColorInArgb, m_weight);
                cGraphics.DrawPath(pen, path);
            }
        }


        #endregion
        /// <summary>
        /// <para>��÷���ת���ε�Ĭ�ϱ߽硣</para>
        /// <para>ǰ��������</para>
        /// <para>    ��</para>
        /// <para>������������</para>
        /// </summary>
        /// <returns>
        /// <para>����ת���ε�Ĭ�ϱ߽�</para>
        /// </returns>
        /// <remarks>
        /// <para> ������ 2013-10-08  ��������</para>
        /// </remarks>
        private RectangleF GetDefaultBoundOfRectangleNormal()
        {
            return new RectangleF(m_position, new SizeF(m_width, m_height));
        }
        /// <summary>
        /// <para>�����ת���ε�Ĭ�ϱ߽硣</para>
        /// <para>ǰ��������</para>
        /// <para>    ��</para>
        /// <para>������������</para>
        /// </summary>
        /// <returns>
        /// <para>��ת���ε�Ĭ�ϱ߽�</para>
        /// </returns>
        /// <remarks>
        /// <para> ������ 2013-10-08  ��������</para>
        /// </remarks>
        private RectangleF GetDefaultBoundOfRectangleWithAngle()
        {
            RectangleF rect = new RectangleF();
            GraphicsPath path = new GraphicsPath();
            path.AddPolygon(m_polygonIconPoint);
            rect.X = path.GetBounds().X;
            rect.Y = path.GetBounds().Y;
            rect.Width = path.GetBounds().Width;
            rect.Height = path.GetBounds().Height;
            return rect;
        }
        /// <summary>
        /// <para>�ж�������Ƿ�λ�ڷ���ת���ε����������ڡ�</para>
        /// <para>ǰ��������</para>
        /// <para>    point ��Ϊ��</para>
        /// <para>������������</para>
        /// </summary>
        /// <param name="point">�����</param>
        /// <returns>
        /// <para>true�������λ�ڷ���ת���ε�����������</para>
        /// <para>false�������û��λ�ڷ���ת���ε�����������</para>
        /// </returns>
        /// <remarks>
        /// <para> ������ 2013-10-08  ��������</para>
        /// </remarks>
        private IIcon IntersectInRectangleNormal(Point point)
        {
            PointF pt = m_position;
            SizeF size = new SizeF(m_width, m_height);

            RectangleF rect = new RectangleF(pt, size);

            GraphicsPath rgn = new GraphicsPath();
            rgn.AddRectangle(rect);

            Pen pen = new Pen(DefaultColor.ColorInArgb, m_weight);

            IntersectVisitor visitor = new IntersectIconVisitor(IntersectIconType, point, rgn, pen);
            Accept(visitor);

            if (visitor.Visible)
            {
                return this;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// <para>�ж�������Ƿ�λ����ת���ε����������ڡ�</para>
        /// <para>ǰ��������</para>
        /// <para>    point ��Ϊ��</para>
        /// <para>������������</para>
        /// </summary>
        /// <param name="point">�����</param>
        /// <returns>
        /// <para>true�������λ����ת���ε�����������</para>
        /// <para>false�������û��λ����ת���ε�����������</para>
        /// </returns>
        /// <remarks>
        /// <para> ������ 2013-10-08  ��������</para>
        /// </remarks>
        private IIcon IntersectInRectangleWithAngle(Point point)
        {
            PointF[] polygonPoint = new PointF[m_polygonIconPoint.Length];
            Array.Copy(m_polygonIconPoint, polygonPoint, m_polygonIconPoint.Length);

            GraphicsPath path = new GraphicsPath();
            path.AddPolygon(polygonPoint);

            Pen pen = new Pen(DefaultColor.ColorInArgb, m_weight);

            IntersectVisitor visitor = new IntersectIconVisitor(IntersectIconType, point, path, pen);
            Accept(visitor);

            if (visitor.Visible)
            {
                return this;
            }
            else
            {
                return null;
            }
        }

        public override IDraw Clone()
        {
            IconRectangle rect = new IconRectangle();
            rect.Copy(this);
            return rect;
        }

        public override void Copy(IDraw src)
        {
            if (src is IconRectangle)
            {
                IconRectangle srcItem = src as IconRectangle;

                base.Copy(srcItem);
                this.m_position = srcItem.m_position;
                this.m_width = srcItem.m_width;
                this.m_height = srcItem.m_height;
                this.m_weight = srcItem.m_weight;
                this.m_fill = srcItem.m_fill;
                this.m_panDashStyle = srcItem.m_panDashStyle;
                this.m_degree = srcItem.m_degree;
                this.m_angle = srcItem.m_angle;

                this.m_polygonIconPoint = new PointF[4];
                srcItem.m_polygonIconPoint.CopyTo(this.m_polygonIconPoint, 0);
            }
            else
            {
                throw new ArgumentException("������ͼԪ��Ŀ��ͼԪ���Ͳ�����", "src");
            }
        }

        public override string ToString()
        {
            return "Rectangle=> position:(" + Math.Round(m_position.X) + "," + Math.Round(m_position.Y) + "), width:"
                + Math.Round(m_width) + ", weight:" + Math.Round(m_weight) + ", degree:" + m_degree;

            //return "Rectangle=> position:(" + (m_position.X) + "," + (m_position.Y) + "), width:"
            //    + (m_width) + ", weight:" + (m_weight) + ", degree:" + m_degree;
        }
    }
}
