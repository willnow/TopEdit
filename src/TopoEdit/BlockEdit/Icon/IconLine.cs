//******************************************************************************
//�ļ����� :     IconLine.cs
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
using CSR.CUIT.GlobalService.ShareLib;
using System.Drawing.Drawing2D;
using System.Diagnostics;
using System.Xml;
using TopoEdit.EventHandler;
using TopoEdit.Stratege;
using TopoEdit.Visitor;

namespace TopoEdit.Icon
{
    /// <summary>
    /// <para>����������ͼԪ��ص����ԺͲ�����</para>
    /// </summary>
    public class IconLine : IIcon
    {
        #region ��Ա����
        /// <summary>
        /// <para>����ͼԪ��ʼ�˶��㡣</para>
        /// </summary>
        private PointF m_beginPosition = new PointF(0, 0);
        /// <summary>
        /// <para>����ͼԪ���ն˶��㡣</para>
        /// </summary>
        private PointF m_endPosition = new PointF(10, 0);
        /// <summary>
        /// <para>Բ��ͼԪ���߿�</para>
        /// </summary>
        private float m_weight = 1;
        private DashStyle m_dashStyle = DashStyle.Solid;
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
        internal IconLine()
        : base(EmIconType.Line)
        {
        }

        internal float Weight
        {
            get { return m_weight; }
            set { m_weight = value; }
        }

        internal DashStyle LineDashStyle
        {
            get { return m_dashStyle; }
            set { m_dashStyle = value; }
        }

        public PointF BeginPosition
        {
            get { return m_beginPosition; }
            set { m_beginPosition = value; }
        }

        public PointF EndPosition
        {
            get { return m_endPosition; }
            set { m_endPosition = value; }
        }

        public float Length
        {
            get
            {
                return (float)Math.Sqrt((EndPosition.X - BeginPosition.X) * (EndPosition.X - BeginPosition.X)
                    + (EndPosition.Y - BeginPosition.Y) * (EndPosition.Y - BeginPosition.Y));
            }
            set
            {
                PointF leftPos = m_beginPosition;
                PointF rightPos = m_endPosition;
                if (m_endPosition.X <= leftPos.X)
                {
                    leftPos = m_endPosition;
                    rightPos = m_beginPosition;
                }

                //���¸���Len����RightPos
                float rate = (rightPos.Y - leftPos.Y) / (rightPos.X - leftPos.X);
                float dX = (float)(value / Math.Sqrt(1 + rate * rate));
                rightPos.X = leftPos.X + dX;
                rightPos.Y = leftPos.Y + rate * dX;

                if (leftPos == m_beginPosition)
                {
                    m_endPosition = rightPos;
                }
                else
                {
                    m_beginPosition = rightPos;
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
            XmlNode lineNode = iconNode.SelectSingleNode("Line");

            //beginPosX
            String temp = lineNode.SelectSingleNode("BeginPos/X").InnerText;
            m_beginPosition.X = float.Parse(temp);
            //beginPosY
            temp = lineNode.SelectSingleNode("BeginPos/Y").InnerText;
            m_beginPosition.Y = float.Parse(temp);
            //endPosX
            temp = lineNode.SelectSingleNode("EndPos/X").InnerText;
            m_endPosition.X = float.Parse(temp);
            //endPosY
            temp = lineNode.SelectSingleNode("EndPos/Y").InnerText;
            m_endPosition.Y = float.Parse(temp);
            //weight
            temp = lineNode.SelectSingleNode("Weight").InnerText;
            m_weight = float.Parse(temp);
            if (m_weight < 1)
            {
                m_weight = 1;
            }

            //dashStyle
            temp = lineNode.SelectSingleNode("DashStyle").InnerText;
            m_dashStyle = (DashStyle)(int.Parse(temp));
            return true;
        }

        public override bool Save(System.Xml.XmlNode iconParentNode)
        {
            System.Xml.XmlNode iconNode = iconParentNode.OwnerDocument.CreateElement("Icon");
            base.Save(iconNode);
            System.Xml.XmlNode node = iconParentNode.OwnerDocument.CreateElement("Line");

            if (m_weight < 1)
            {
                m_weight = 1;
            }

            node.InnerXml = "<BeginPos><X>" + m_beginPosition.X + "</X><Y>" + m_beginPosition.Y + "</Y></BeginPos>"
                + "<EndPos><X>" + m_endPosition.X + "</X><Y>" + m_endPosition.Y + "</Y></EndPos><Weight>"
                + m_weight + "</Weight><DashStyle>" + (byte)m_dashStyle + "</DashStyle>";
            iconNode.AppendChild(node);
            iconParentNode.AppendChild(iconNode);
            return true;
        }

        public override void Round()
        {
            m_beginPosition = Utility.ConvertPos(m_beginPosition);
            m_endPosition = Utility.ConvertPos(m_endPosition);
        }

        private void MoveLeftPoint(Movement move)
        {
            if (m_beginPosition.X == m_endPosition.X)
            {
                if (m_beginPosition.Y < m_endPosition.Y)
                {
                    move.LPtoDP(ref m_beginPosition);
                }
                else
                {
                    move.LPtoDP(ref m_endPosition);
                }
            }
            else if (m_beginPosition.X < m_endPosition.X)
            {
                move.LPtoDP(ref m_beginPosition);
            }
            else
            {
                move.LPtoDP(ref m_endPosition);
            }
        }

        private void MoveRightPoint(Movement move)
        {
            if (m_beginPosition.X == m_endPosition.X)
            {
                if (m_beginPosition.Y > m_endPosition.Y)
                {
                    move.LPtoDP(ref m_beginPosition);
                }
                else
                {
                    move.LPtoDP(ref m_endPosition);
                }
            }
            else if (m_beginPosition.X > m_endPosition.X)
            {
                move.LPtoDP(ref m_beginPosition);
            }
            else
            {
                move.LPtoDP(ref m_endPosition);
            }
        }

        public override void Zoom(Zoom zoom)
        {
            SizeF defaultSize = new SizeF(Math.Abs(m_endPosition.X - m_beginPosition.X), Math.Abs(m_endPosition.Y - m_beginPosition.Y));
            if (defaultSize.Width == 0)
            {
                defaultSize.Width = 1;
            }

            if (defaultSize.Height == 0)
            {
                defaultSize.Height = 1;
            }

            SizeF size = defaultSize;
            zoom.LPtoDP(ref size);

            Movement move = new Movement();
            move.SubMode = zoom.SubMode;
            switch (zoom.SubMode)
            {
                case ScaleOpMode.Left:
                    {
                        move.XMovement = -(size.Width - defaultSize.Width);
                        move.YMovement = 0;
                        Move(move);
                        break;
                    }
                case ScaleOpMode.LeftUp:
                    {
                        move.XMovement = -(size.Width - defaultSize.Width);
                        move.YMovement = -(size.Height - defaultSize.Height);
                        Move(move);
                        break;
                    }
                case ScaleOpMode.LeftDown:
                    {
                        move.XMovement = -(size.Width - defaultSize.Width);
                        move.YMovement = (size.Height - defaultSize.Height);
                        Move(move);
                        break;
                    }
                case ScaleOpMode.Right:
                    {
                        move.XMovement = (size.Width - defaultSize.Width);
                        move.YMovement = 0;
                        Move(move);
                        break;
                    }
                case ScaleOpMode.RightUp:
                    {
                        move.XMovement = (size.Width - defaultSize.Width);
                        move.YMovement = -(size.Height - defaultSize.Height);
                        Move(move);
                        break;
                    }
                case ScaleOpMode.RightDown:
                    {
                        move.XMovement = (size.Width - defaultSize.Width);
                        move.YMovement = (size.Height - defaultSize.Height);
                        Move(move);
                        break;
                    }
                case ScaleOpMode.Up:
                    {
                        move.XMovement = 0;
                        move.YMovement = -(size.Height - defaultSize.Height);
                        Move(move);
                        break;
                    }
                case ScaleOpMode.Down:
                    {
                        move.XMovement = 0;
                        move.YMovement = (size.Height - defaultSize.Height);
                        Move(move);
                        break;
                    }
                case ScaleOpMode.None:
                    {
                        zoom.LPtoDP(ref m_beginPosition);
                        zoom.LPtoDP(ref m_endPosition);
                        break;
                    }
                default:
                    {
                        //���²���������
                        break;
                    }
            }
        }

        public override void Rotate(CSR.ShareLib.Rotate rotate)
        {
            throw new NotImplementedException();
        }

        public override void Move(Movement move)
        {
            switch (move.SubMode)
            {
                case ScaleOpMode.Left:
                case ScaleOpMode.LeftUp:
                case ScaleOpMode.LeftDown:
                case ScaleOpMode.Up:
                    {
                        MoveLeftPoint(move);
                        break;
                    }
                case ScaleOpMode.Right:
                case ScaleOpMode.RightUp:
                case ScaleOpMode.RightDown:
                case ScaleOpMode.Down:
                    {
                        MoveRightPoint(move);
                        break;
                    }
                case ScaleOpMode.None:
                    {
                        move.LPtoDP(ref m_beginPosition);
                        move.LPtoDP(ref m_endPosition);
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
            symn.LPtoDP(ref m_beginPosition);
            symn.LPtoDP(ref m_endPosition);
        }

        public override void Accept(TopoEdit.Visitor.IDrawVisitor visitor)
        {
            visitor.VisitorLine(this);
        }

        #region ��дIIcon ��Ա

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

            Point pt1 = new Point();
            Point pt2 = new Point();

            pt1.X = (int)m_beginPosition.X;
            pt1.Y = (int)m_beginPosition.Y;

            pt2.X = (int)m_endPosition.X;
            pt2.Y = (int)m_endPosition.Y;

            Size lineSize = new Size();

            lineSize.Width = (int)m_weight + 5;
            lineSize.Height = (int)m_weight + 5;

            IntersectVisitor visitor = new IntersectLineVisitor(this.IntersectIconType, point, pt1, pt2, lineSize.Width);
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
        /// <para>��дIIcon���ԣ����ͼԪ��Ĭ�ϱ߽��С��</para>
        /// </summary>
        /// <remarks>
        /// <para> ������ 2013-10-08  ��������</para>
        /// </remarks>
        public override sealed RectangleF BoundsRect
        {
            get
            {
                //��ʼ����Χ�ľ��Ρ�
                RectangleF rect = new RectangleF();
                float width = m_weight / 2;
                if (m_beginPosition.X < m_endPosition.X)
                {

                    rect.X = m_beginPosition.X - width;
                    rect.Width = m_endPosition.X - m_beginPosition.X + m_weight;
                }
                else
                {
                    rect.X = m_endPosition.X - width;
                    rect.Width = m_beginPosition.X - m_endPosition.X + m_weight;
                }
                if (m_beginPosition.Y < m_endPosition.Y)
                {
                    rect.Y = m_beginPosition.Y - width;
                    rect.Height = m_endPosition.Y - m_beginPosition.Y + m_weight;
                }
                else
                {
                    rect.Y = m_endPosition.Y - width;
                    rect.Height = m_beginPosition.Y - m_endPosition.Y + m_weight;
                }



                return rect;
            }
        }


        #endregion

        #region ��дIDraw ��Ա
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

            SizeF lineSize = new SizeF();
            PointF p1 = m_beginPosition;
            PointF p2 = m_endPosition;

            lineSize.Width = m_weight;
            lineSize.Height = m_weight;

            Pen pen = new Pen(GetDisplayColor(), lineSize.Width);//pen.CreatePen(PS_SOLID | PS_GEOMETRIC | PS_ENDCAP_FLAT, lineSize.cy, &logBrush);
            pen.DashStyle = m_dashStyle;
            pen.EndCap = LineCap.Flat;

            cGraphics.DrawLine(pen, p1, p2);
            pen.Dispose();
        }

        #endregion

        public override IDraw Clone()
        {
            IconLine line = new IconLine();
            line.Copy(this);
            return line;
        }

        public override void Copy(IDraw src)
        {
            if (src is IconLine)
            {
                IconLine srcItem = src as IconLine;

                base.Copy(srcItem);
                this.m_beginPosition = srcItem.m_beginPosition;
                this.m_endPosition = srcItem.m_endPosition;
                this.m_weight = srcItem.m_weight;
                this.m_dashStyle = srcItem.m_dashStyle;
            }
            else
            {
                throw new ArgumentException("������ͼԪ��Ŀ��ͼԪ���Ͳ�����", "src");
            }
        }

        public override string ToString()
        {
            return "Line=> begin:(" + Math.Round(m_beginPosition.X) + "," + Math.Round(m_beginPosition.Y) + "), end:(" + Math.Round(m_endPosition.X) + ","
                + Math.Round(m_endPosition.Y) + ", weight:" + Math.Round(m_weight);
        }
    }
}
