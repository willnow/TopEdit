//******************************************************************************
//�ļ����� :     IconCircle.cs
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

using System.Drawing;
using System.Drawing.Drawing2D;
using System.Diagnostics;
using System.Xml;
using TopoEdit.EventHandler;
using TopoEdit.Stratege;
using TopoEdit.Visitor;

namespace TopoEdit.Icon
{
    /// <summary>
    /// <para>������Բ��ͼԪ��ص����ԺͲ�����</para>
    /// </summary>
    public class IconCircle : IIcon
    {
        #region ��Ա����
        /// <summary>
        /// <para>Բ��ͼԪ���ھ������Ͻǵ��λ�á�</para>
        /// </summary>
        private PointF m_position = new PointF(0, 0);
        /// <summary>
        /// <para>Բ��ͼԪ���ھ��ο�ȡ�</para>
        /// </summary>
        private float m_diameter = 5;
        /// <summary>
        /// <para>Բ��ͼԪ���߿�</para>
        /// </summary>
        private float m_weight = 1;
        /// <summary>
        /// <para>Բ��ͼԪ�Ƿ���䡣</para>
        /// </summary>
        private Boolean m_fill = false;
        #endregion

        /// <summary>
        /// <para>���ø���Ĺ��캯�����г�ʼ����</para>
        /// <para>ǰ��������</para>
        /// <para>    ��</para>
        /// <para>����������</para>
        /// <para>    �����ֶα�������ֵ��</para>
        /// </summary>
        /// <param name="rtu">��ICON������RTU</param>
        /// <returns>
        /// <para></para>
        /// </returns>
        /// <remarks>
        /// <para> ������ 2013-10-08  ��������</para>
        /// </remarks>
        internal IconCircle()
        : base(EmIconType.Circle)
        {
        }

        public float Diameter
        {
            get { return m_diameter; }
            set { m_diameter = value; }
        }

        public float Weight
        {
            get { return m_weight; }
            set { m_weight = value; }
        }

        public Boolean Fill
        {
            get { return m_fill; }
            set { m_fill = value; }
        }

        public PointF GetCenter()
        {
            return Utility.GetCenter(this.BoundsRect);
        }

        public void SetCenter(PointF center, float diameter)
        {
            m_diameter = diameter;
            m_position.X = center.X - m_diameter / 2;
            m_position.Y = center.Y - m_diameter / 2;
        }

        #region IIcon ��Ա
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
            XmlNode circleNode = iconNode.SelectSingleNode("Circle");

            m_position.X = float.Parse(circleNode.SelectSingleNode("Pos/X").InnerText);
            m_position.Y = float.Parse(circleNode.SelectSingleNode("Pos/Y").InnerText);
            m_diameter = float.Parse(circleNode.SelectSingleNode("Diameter").InnerText);
            m_weight = float.Parse(circleNode.SelectSingleNode("Weight").InnerText);

            if (m_weight < 1)
            {
                m_weight = 1;
            }

            m_fill = bool.Parse(circleNode.SelectSingleNode("Fill").InnerText);

            return true;
        }

        public override bool Save(System.Xml.XmlNode iconParentNode)
        {
            System.Xml.XmlNode iconNode = iconParentNode.OwnerDocument.CreateElement("Icon");
            base.Save(iconNode);
            System.Xml.XmlNode node = iconNode.OwnerDocument.CreateElement("Circle");

            string fill = "false";
            if (m_fill)
            {
                fill = "true";
            }

            if (m_weight < 1)
            {
                m_weight = 1;
            }

            node.InnerXml = "<Pos><X>" + m_position.X + "</X><Y>" + m_position.Y + "</Y></Pos><Diameter>" + m_diameter
                + "</Diameter><Weight>" + m_weight + "</Weight><Fill>" + fill + "</Fill>";
            iconNode.AppendChild(node);
            iconParentNode.AppendChild(iconNode);
            return true;
        }

        public override void Round()
        {
            m_position = Utility.ConvertPos(m_position);
            Utility.ConvertValue(ref m_diameter);
        }

        public override void Zoom(Zoom zoom)
        {
            switch(zoom.SubMode)
            {
                case ScaleOpMode.Left:
                case ScaleOpMode.Right:
                case ScaleOpMode.Up:
                case ScaleOpMode.Down:
                    {
                        //��֧������
                        break;
                    }
                case ScaleOpMode.LeftUp:
                    {
                        //��¼����ǰ�߽�
                        RectangleF rect = BoundsRect;
                        //���ų���
                        ZoomDiameter(zoom);
                        //���¼������Ͻ�����
                        m_position.Y = rect.Bottom - m_diameter;
                        m_position.X = rect.Right - m_diameter;
                        break;
                    }
                case ScaleOpMode.RightUp:
                    {
                        //��¼����ǰ�߽�
                        RectangleF rect = BoundsRect;
                        //���ų���
                        ZoomDiameter(zoom);
                        //���¼������Ͻ�����
                        m_position.Y = rect.Bottom - m_diameter;
                        break;
                    }
                case ScaleOpMode.LeftDown:
                    {
                        //��¼����ǰ�߽�
                        RectangleF rect = BoundsRect;
                        //���ų���
                        ZoomDiameter(zoom);
                        //���¼������Ͻ�����
                        m_position.X = rect.Right - m_diameter;
                        break;
                    }
                case ScaleOpMode.RightDown:
                    {
                        ZoomDiameter(zoom);
                        break;
                    }
                default:
                    {
                        zoom.LPtoDP(ref m_position);
                        ZoomDiameter(zoom);
                        break;
                    }
            }
        }

        void ZoomDiameter(Zoom zoom)
        {
            SizeF diameter = new SizeF();
            diameter.Width = m_diameter;
            diameter.Height = m_diameter;
            zoom.LPtoDP(ref diameter);
            m_diameter = diameter.Width;
        }

        public override void Rotate(CSR.ShareLib.Rotate rotate)
        {
            throw new NotImplementedException();
        }

        public override void Move(Movement move)
        {
            if (move.SubMode == ScaleOpMode.None)
            {
                move.LPtoDP(ref m_position);
            }
        }

        public override void Symmetry(Symmetry synm)
        {
            synm.LPtoDP(ref m_position);
        }

        public override void Accept(TopoEdit.Visitor.IDrawVisitor visitor)
        {
            visitor.VisitorCircle(this);
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

            GraphicsPath rgn = new GraphicsPath();
            RectangleF rect = BoundsRect;
            rgn.AddEllipse(rect);

            Pen pen = new Pen(DefaultColor.ColorInArgb, m_weight);
            IntersectVisitor visitor = new IntersectIconVisitor(IntersectIconType, point, rgn, pen);
            Accept(visitor);

            if (visitor.Visible)
                return this;
            else
                return null;
        }
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
                RectangleF cRect = new RectangleF();
                cRect.X = m_position.X;
                cRect.Y = m_position.Y;
                cRect.Width = m_diameter;
                cRect.Height = m_diameter;
                return cRect;
            }
        }
        /// <summary>
        /// <para>��дIIcon����������ͼԪ��</para>
        /// <para>ǰ��������</para>
        /// <para>    cGraphics cRect ��Ϊ��</para>
        /// <para>������������</para>
        /// </summary>
        /// <param name="cGraphics">��װһ��GDI+��ͼͼ��</param>
        /// <param name="cRect">���Ʒ�Χ</param>
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

            PointF pt = m_position;
            SizeF diameter = new SizeF();
            diameter.Width = m_diameter;
            diameter.Height = m_diameter;

            Pen pen = new Pen(GetDisplayColor(), m_weight);
            if (IconState == EmIconState.Focus)
            {
                pen.DashStyle = DashStyle.Dot;
            }
            else
            {
                pen.DashStyle = DashStyle.Solid;
            }
            SolidBrush brush = new SolidBrush(GetDisplayColor());

            if (m_fill)
            {
                cGraphics.FillEllipse(brush, pt.X, pt.Y, diameter.Width, diameter.Height);
            }
            cGraphics.DrawEllipse(pen, pt.X, pt.Y, diameter.Width, diameter.Height);

            pen.Dispose();
            brush.Dispose();
        }
        #endregion

        public override IDraw Clone()
        {
            IconCircle circle = new IconCircle();
            circle.Copy(this);
            return circle;
        }

        public override void Copy(IDraw src)
        {
            if (src is IconCircle)
            {
                IconCircle srcItem = src as IconCircle;

                base.Copy(src);
                m_position = srcItem.m_position;
                m_diameter = srcItem.m_diameter;
                m_weight = srcItem.m_weight;
                m_fill = srcItem.m_fill;
            }
            else
            {
                throw new ArgumentException("������ͼԪ��Ŀ��ͼԪ���Ͳ�����", "src");
            }
        }

        public override string ToString()
        {
            return "Circle=> position:(" + Math.Round(m_position.X) + "," + Math.Round(m_position.Y) + "), diameter:" + Math.Round(m_diameter) + ", weight:" + Math.Round(m_weight);
        }
    }
}

