//******************************************************************************
//�ļ����� :     IconText.cs
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
using CSR.CUIT.GlobalService.ShareLib;
using System.Text;
using System.Xml;
using TopoEdit.EventHandler;
using TopoEdit.Stratege;
using System.Drawing.Drawing2D;

namespace TopoEdit.Icon
{
    /// <summary>
    /// <para>�������ı�ͼԪ��ص����ԺͲ�����</para>
    /// </summary>
    public class IconText : IIcon
    {
        #region ��Ա����
        /// <summary>
        /// <para>�ı�ͼԪ���ھ��ο�����Ͻ����ꡣ</para>
        /// </summary>
        private PointF m_position = new PointF(0, 0);
        /// <summary>
        /// <para>�ı�ͼԪ���ھ��ο�Ŀ�</para>
        /// </summary>
        private float m_width = 40;
        /// <summary>
        /// <para>�ı�ͼԪ���ھ��ο�ĸ�</para>
        /// </summary>
        private float m_height = 20;
        /// <summary>
        /// <para>�ı�����</para>
        /// </summary>
        private string m_value = "TEXT";
        /// <summary>
        /// <para>�ı��Ƿ���������б��޸�</para>
        /// </summary>
        private bool m_enable = false;
        /// <summary>
        /// <para>�ı��ھ��ο��еĴ�ֱ�ֲ�</para>
        /// </summary>
        private EmVerticalAlignment m_vAlignment = EmVerticalAlignment.Center;
        /// <summary>
        /// <para>�ı��ھ��ο��е�ˮƽ�ֲ�</para>
        /// </summary>
        private EmHorizontalAlignment m_hAlignment = EmHorizontalAlignment.Center;
        /// <summary>
        /// <para>�ı�Ĭ�ϴ�С����λΪ��ֵ��</para>
        /// </summary>
        private float m_fontSize = 10;
        /// <summary>
        /// <para>��������</para>
        /// </summary>
        private FontStyle m_fontStyle = FontStyle.Regular;
        /// <summary>
        /// <para>��������</para>
        /// </summary>
        private string m_fontName;

        #endregion

        /// <summary>
        /// <para>���ø���Ĺ��캯�����г�ʼ����</para>
        /// <para>ǰ��������</para>
        /// <para>    rtu ��Ϊ��</para>
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
        internal IconText()
        : base(EmIconType.Text)
        {

        }

        public string Value
        {
            get { return m_value; }
            set { m_value = value; }
        }

        /// <summary>
        /// <para>��û�����ͼԪ���ı���ֻ�е��ı������޸�ʱ��m_enable���ſ��Զ�ͼԪ���ı��������á�</para>
        /// </summary>
        /// <remarks>
        /// <para> ������ 2013-10-08  ��������</para>
        /// </remarks>
        internal string Text
        {
            set
            {
                if (m_enable)
                {
                    //if (value.Length >= LogicConstant.MAX_TEXT_LENGTH)
                    //    return;
                    m_value = value;
                }
                else
                {
                    //�˴�Ϊ��������
                }
            }
            get
            {
                return m_value;
            }
        }

        public Boolean Enable
        {
            get { return m_enable; }
            set { m_enable = value; }
        }

        public EmVerticalAlignment VAlign
        {
            get { return m_vAlignment; }
            set { m_vAlignment = value; }
        }

        public EmHorizontalAlignment HAlign
        {
            get { return m_hAlignment; }
            set { m_hAlignment = value; }
        }

        /// <summary>
        /// ������ʽ
        /// </summary>
        public FontStyle FontStyle
        {
            get { return m_fontStyle; }
            set { m_fontStyle = value; }
        }

        /// <summary>
        /// �����С
        /// </summary>
        public float FontSize
        {
            get { return m_fontSize; }
            set 
            { 
                m_fontSize = value; 
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public string FontName
        {
            get { return m_fontName; }
            set { m_fontName = value; }
        }

        public PointF Pos
        {
            get { return m_position; }
            set { m_position = value; }
        }

        public float Width
        {
            get { return m_width; }
            set { m_width = value; }
        }

        public float Height
        {
            get { return m_height; }
            set { m_height = value; }
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
            XmlNode textNode = iconNode.SelectSingleNode("Text");

            m_position.X = float.Parse(textNode.SelectSingleNode("Pos/X").InnerText);
            m_position.Y = float.Parse(textNode.SelectSingleNode("Pos/Y").InnerText);
            m_width = float.Parse(textNode.SelectSingleNode("Width").InnerText);
            m_height = float.Parse(textNode.SelectSingleNode("Height").InnerText);
            m_value = textNode.SelectSingleNode("Value").InnerText;
            m_enable = bool.Parse(textNode.SelectSingleNode("Enable").InnerText);
            string temp = textNode.SelectSingleNode("Alignment/HAlign").InnerText; //HorizontalAlignment
            switch (temp)
            {
                case "Left":
                    m_hAlignment = EmHorizontalAlignment.Left;
                    break;
                case "Right":
                    m_hAlignment = EmHorizontalAlignment.Right;
                    break;
                case "Center":
                    m_hAlignment = EmHorizontalAlignment.Center;
                    break;

            }
            temp = textNode.SelectSingleNode("Alignment/VAlign").InnerText; //VerticalAlignment
            switch (temp)
            {
                case "Top":
                    m_vAlignment = EmVerticalAlignment.Top;
                    break;
                case "Buttom":
                    m_vAlignment = EmVerticalAlignment.Buttom;
                    break;
                case "Center":
                    m_vAlignment = EmVerticalAlignment.Center;
                    break;
            }

            m_fontSize = float.Parse(textNode.SelectSingleNode("FontSize").InnerText);
            m_fontName = textNode.SelectSingleNode("FontName").InnerText.Trim();

            m_fontStyle = FontStyle.Regular;
            if (bool.Parse(textNode.SelectSingleNode("Bold").InnerText))
            {
                m_fontStyle |= FontStyle.Bold;
            }
            if (bool.Parse(textNode.SelectSingleNode("Italic").InnerText))
            {
                m_fontStyle |= FontStyle.Italic;
            }
            if (bool.Parse(textNode.SelectSingleNode("Underlined").InnerText))
            {
                m_fontStyle |= FontStyle.Underline;
            }
            if (bool.Parse(textNode.SelectSingleNode("StrokedOut").InnerText))
            {
                m_fontStyle |= FontStyle.Strikeout;
            }

            return true;
        }

        public override bool Save(System.Xml.XmlNode iconParentNode)
        {
            System.Xml.XmlNode iconNode = iconParentNode.OwnerDocument.CreateElement("Icon");
            base.Save(iconNode);
            System.Xml.XmlNode node = iconParentNode.OwnerDocument.CreateElement("Text");

            string hAlign = "Center";
            string vAlign = "Center";

            switch (m_hAlignment)
            {
                case EmHorizontalAlignment.Left:
                    hAlign = "Left";
                    break;
                case EmHorizontalAlignment.Right:
                    hAlign = "Right";
                    break;
                case EmHorizontalAlignment.Center:
                    hAlign = "Center";
                    break;
            }

            switch (m_vAlignment)
            {
                case EmVerticalAlignment.Top:
                    vAlign = "Top";
                    break;
                case EmVerticalAlignment.Buttom:
                    vAlign = "Buttom";
                    break;
                case EmVerticalAlignment.Center:
                    vAlign = "Center";
                    break;
            }

            bool isBold = ((int)m_fontStyle & (int)FontStyle.Bold) > 0;
            bool isItalic = ((int)m_fontStyle & (int)FontStyle.Italic) > 0;
            bool isUnderlined = ((int)m_fontStyle & (int)FontStyle.Underline) > 0;
            bool isStrokedOut = ((int)m_fontStyle & (int)FontStyle.Strikeout) > 0;

            node.InnerXml = "<Pos><X>" + m_position.X + "</X><Y>" + m_position.Y + "</Y></Pos><Width>"
                + m_width + "</Width><Height>" + m_height + "</Height><Value>" + m_value + "</Value><Enable>"
                + m_enable.ToString().ToLower() + "</Enable><Alignment><HAlign>" + hAlign + "</HAlign><VAlign>" + vAlign
                + "</VAlign></Alignment><FontSize>" + (int)Math.Round(m_fontSize) + "</FontSize>"
                + "<FontName>" + m_fontName + "</FontName>"
                + "<Bold>" + isBold.ToString().ToLower() + "</Bold>"
                + "<Italic>" + isItalic.ToString().ToLower() + "</Italic>"
                + "<Underlined>" + isUnderlined.ToString().ToLower() + "</Underlined>"
                + "<StrokedOut>" + isStrokedOut.ToString().ToLower() + "</StrokedOut>";

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
            //����������߿�
            switch (zoom.SubMode)
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

            //��������
            switch (zoom.SubMode)
            {
                case ScaleOpMode.Right:
                case ScaleOpMode.Down:
                case ScaleOpMode.Up:
                case ScaleOpMode.Left:
                    {
                        //����������
                        break;
                    }
                case ScaleOpMode.None:
                //case ScaleOpMode.RightDown:
                //case ScaleOpMode.LeftUp:
                //case ScaleOpMode.RightUp:
               // case ScaleOpMode.LeftDown:
                    {
                        zoom.LPtoDP(ref m_fontSize);
                        if (m_fontSize < 1)
                        {
                            m_fontSize = 1;
                        }
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

        void ZoomDiameter(Zoom zoom)
        {
            SizeF size = new SizeF(m_width, m_height);
            zoom.LPtoDP(ref size);
            m_width = size.Width;
            m_height = size.Height;
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
            //��ʱ��֧�ֶԳ�
        }

        public override void Accept(TopoEdit.Visitor.IDrawVisitor visitor)
        {
            visitor.VisitorText(this);
        }

        #region IIcon ��Ա
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
            rgn.AddRectangle(BoundsRect);

            IIntersectStrategy intersectStrategy = new IntersectByGraphicsPathInBoundStrategy(rgn);
            if (intersectStrategy.IsVisible(point))
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
                return  new RectangleF(m_position.X, m_position.Y, m_width, m_height);
            }
        }
        #endregion

        #region IDraw ��Ա
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

            string csText = m_value;

            PointF pt = m_position;
            SizeF size = new SizeF(m_width, m_height);
            Font fnt = null;
            try
            {
                fnt = new Font(m_fontName, m_fontSize, m_fontStyle, GraphicsUnit.Point);
            }
            catch (System.ArgumentException)
            {
                fnt = new Font("����", m_fontSize, m_fontStyle, GraphicsUnit.Point);
            }

            SizeF valueSize = cGraphics.MeasureString(csText, fnt);
            valueSize = cGraphics.MeasureString(csText, fnt, size);

            CalculateLocation(ref pt, size, valueSize);

            //pDC.SetBkMode(TRANSPARENT);
            using (SolidBrush brush = new SolidBrush(GetDisplayColor()))
            {
                cGraphics.DrawString(csText, fnt, brush, new RectangleF(pt, valueSize));
            }
        }

        #endregion
        /// <summary>
        /// <para>�����ı��ھ��α߿��е�ˮƽ�ֲ��ʹ�ֱ�ֲ����ԣ������ı���ʵ��λ�á�</para>
        /// <para>ǰ��������</para>
        /// <para>    attributeCollection��ȡֵ��Χ�ɵ����߱�֤</para>
        /// <para>������������</para>
        /// </summary>
        /// <param name="position">�ı������Ͻ�����</param>
        /// <param name="boundSize">�ı�ͼԪ�ľ��α߿�ı߽�</param>
        /// <param name="valueSize">�ı��ı߽�</param>
        /// <returns>
        /// <para>position��������ֵ��</para>
        /// </returns>
        /// <remarks>
        /// <para> ������ 2013-10-08  ��������</para>
        /// </remarks>
        private void CalculateLocation(ref PointF position, SizeF boundSize, SizeF valueSize)
        {
            switch (m_hAlignment)
            {
                case EmHorizontalAlignment.Center:
                    {
                        position.X += (boundSize.Width - valueSize.Width) / 2;
                        break;
                    }
                case EmHorizontalAlignment.Left:
                    {
                        break;
                    }
                case EmHorizontalAlignment.Right:
                    {
                        position.X += boundSize.Width - valueSize.Width;
                        break;
                    }
            }
            switch (m_vAlignment)
            {
                case EmVerticalAlignment.Center:
                    {
                        position.Y += (boundSize.Height - valueSize.Height) / 2;
                        break;
                    }
                case EmVerticalAlignment.Buttom:
                    {
                        position.Y += boundSize.Height - valueSize.Height;
                        break;
                    }
                case EmVerticalAlignment.Top:
                    {
                        break;
                    }
            }
        }

        public override IDraw Clone()
        {
            IconText text = new IconText();
            text.Copy(this);
            return text;
        }

        public override void Copy(IDraw src)
        {
            if (src is IconText)
            {
                IconText srcItem = src as IconText;

                base.Copy(src);
                this.m_position = srcItem.m_position;
                this.m_width = srcItem.m_width;
                this.m_height = srcItem.m_height;
                this.m_value = srcItem.m_value;
                this.m_enable = srcItem.m_enable;
                this.m_vAlignment = srcItem.m_vAlignment;
                this.m_hAlignment = srcItem.m_hAlignment;
                this.m_fontName = srcItem.m_fontName;
                this.m_fontSize = srcItem.m_fontSize;
                this.m_fontStyle = srcItem.m_fontStyle;
            }
            else
            {
                throw new ArgumentException("������ͼԪ��Ŀ��ͼԪ���Ͳ�����", "src");
            }
        }

         public override string ToString()
         {
             return "Text=> position:(" + Math.Round(m_position.X) + "," + Math.Round(m_position.Y) + "), width:" + Math.Round(m_width) + ", height:" + Math.Round(m_height);
         }
    }
}
