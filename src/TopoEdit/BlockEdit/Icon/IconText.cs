//******************************************************************************
//文件名称 :     IconText.cs
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

// V1.14.16    JXN    2013-12-22    修复测试发现的BUG
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
    /// <para>定义与文本图元相关的属性和操作。</para>
    /// </summary>
    public class IconText : IIcon
    {
        #region 成员变量
        /// <summary>
        /// <para>文本图元所在矩形框的左上角坐标。</para>
        /// </summary>
        private PointF m_position = new PointF(0, 0);
        /// <summary>
        /// <para>文本图元所在矩形框的宽</para>
        /// </summary>
        private float m_width = 40;
        /// <summary>
        /// <para>文本图元所在矩形框的高</para>
        /// </summary>
        private float m_height = 20;
        /// <summary>
        /// <para>文本内容</para>
        /// </summary>
        private string m_value = "TEXT";
        /// <summary>
        /// <para>文本是否可在运行中被修改</para>
        /// </summary>
        private bool m_enable = false;
        /// <summary>
        /// <para>文本在矩形框中的垂直分布</para>
        /// </summary>
        private EmVerticalAlignment m_vAlignment = EmVerticalAlignment.Center;
        /// <summary>
        /// <para>文本在矩形框中的水平分布</para>
        /// </summary>
        private EmHorizontalAlignment m_hAlignment = EmHorizontalAlignment.Center;
        /// <summary>
        /// <para>文本默认大小。单位为磅值。</para>
        /// </summary>
        private float m_fontSize = 10;
        /// <summary>
        /// <para>字体类型</para>
        /// </summary>
        private FontStyle m_fontStyle = FontStyle.Regular;
        /// <summary>
        /// <para>字体名称</para>
        /// </summary>
        private string m_fontName;

        #endregion

        /// <summary>
        /// <para>调用父类的构造函数进行初始化。</para>
        /// <para>前置条件：</para>
        /// <para>    rtu 不为空</para>
        /// <para>后置条件：</para>
        /// <para>    所有字段被赋予新值。</para>
        /// </summary>
        /// <param name="rtu">该ICON所属的RTU</param>
        /// <returns>
        /// <para></para>
        /// </returns>
        /// <remarks>
        /// <para> 蒋湘宁 2013-10-08  创建函数</para>
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
        /// <para>获得或设置图元的文本。只有当文本可以修改时（m_enable）才可以对图元的文本进行设置。</para>
        /// </summary>
        /// <remarks>
        /// <para> 蒋湘宁 2013-10-08  创建属性</para>
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
                    //此处为有意留空
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
        /// 字体样式
        /// </summary>
        public FontStyle FontStyle
        {
            get { return m_fontStyle; }
            set { m_fontStyle = value; }
        }

        /// <summary>
        /// 字体大小
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
        /// 字体名称
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
        /// <para>重写IIcon方法，根据输入参数初始化成员。</para>
        /// <para>前置条件：</para>
        /// <para>    iconNode 不为空</para>
        /// <para>后置条件：无</para>
        /// </summary>
        /// <param name="iconNode">从配置文件中读取出来的数据初始化字符串序列</param>
        /// <returns>
        /// <para>true：初始化成功</para>
        /// <para>false：初始化失败</para>
        /// </returns>
        /// <remarks>
        /// <para> 蒋湘宁 2013-10-08  创建函数</para>
        /// </remarks>
        public override bool Load(System.Xml.XmlNode iconNode)
        {
            Debug.Assert(null != iconNode);

            //加载基本属性
            base.Load(iconNode);

            //加载特殊属性
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
            //缩放字体外边框
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
                        //记录缩放前边界
                        RectangleF rect = BoundsRect;
                        //缩放长宽
                        ZoomDiameter(zoom);
                        //从新计算左上角坐标
                        m_position.Y = rect.Bottom - m_height;
                        m_position.X = rect.Right - m_width;
                        break;
                    }
                case ScaleOpMode.RightUp:
                    {
                        //记录缩放前边界
                        RectangleF rect = BoundsRect;
                        //缩放长宽
                        ZoomDiameter(zoom);
                        //从新计算左上角坐标
                        m_position.Y = rect.Bottom - m_height;
                        break;
                    }
                case ScaleOpMode.LeftDown:
                    {
                        //记录缩放前边界
                        RectangleF rect = BoundsRect;
                        //缩放长宽
                        ZoomDiameter(zoom);
                        //从新计算左上角坐标
                        m_position.X = rect.Right - m_width;
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

            //缩放字体
            switch (zoom.SubMode)
            {
                case ScaleOpMode.Right:
                case ScaleOpMode.Down:
                case ScaleOpMode.Up:
                case ScaleOpMode.Left:
                    {
                        //不缩放字体
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
            //暂时不支持对称
        }

        public override void Accept(TopoEdit.Visitor.IDrawVisitor visitor)
        {
            visitor.VisitorText(this);
        }

        #region IIcon 成员
        /// <summary>
        /// <para>重写IIcon方法，判断输入点是否位于图元的所在区域内。</para>
        /// <para>前置条件：</para>
        /// <para>    point 不为空</para>
        /// <para>后置条件：无</para>
        /// </summary>
        /// <param name="point">输入点</param>
        /// <returns>
        /// <para>true：输入点位于图元的所在区域内</para>
        /// <para>false：输入点没有位于图元的所在区域内</para>
        /// </returns>
        /// <remarks>
        /// <para> 蒋湘宁 2013-10-08  创建函数</para>
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
        /// <para>重写IIcon属性，获得图元的默认边界大小。</para>
        /// </summary>
        /// <remarks>
        /// <para> 蒋湘宁 2013-10-08  创建属性</para>
        /// </remarks>
        public override sealed RectangleF BoundsRect
        {
            get
            {
                return  new RectangleF(m_position.X, m_position.Y, m_width, m_height);
            }
        }
        #endregion

        #region IDraw 成员
        /// <summary>
        /// <para>重写IIcon方法，绘制图元。</para>
        /// <para>前置条件：</para>
        /// <para>    cGraphics cRect 不为空</para>
        /// <para>后置条件：无</para>
        /// </summary>
        /// <param name="cGraphics">封装一个GDI+绘图图面</param>
        /// <param name="cRect">绘制范围</param>
        /// <returns>
        /// <para>无</para>
        /// </returns>
        /// <remarks>
        /// <para> 蒋湘宁 2013-10-08  创建函数</para>
        /// </remarks>
        public override sealed void Draw(Graphics cGraphics, RectangleF cRect)
        {
            //图元不可见，或图元不为空且没有和绘制区域有交集，此时不需要绘制当前图元
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
                fnt = new Font("宋体", m_fontSize, m_fontStyle, GraphicsUnit.Point);
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
        /// <para>根据文本在矩形边框中的水平分布和垂直分布属性，计算文本的实际位置。</para>
        /// <para>前置条件：</para>
        /// <para>    attributeCollection的取值范围由调用者保证</para>
        /// <para>后置条件：无</para>
        /// </summary>
        /// <param name="position">文本的左上角坐标</param>
        /// <param name="boundSize">文本图元的矩形边框的边界</param>
        /// <param name="valueSize">文本的边界</param>
        /// <returns>
        /// <para>position被赋予新值。</para>
        /// </returns>
        /// <remarks>
        /// <para> 蒋湘宁 2013-10-08  创建函数</para>
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
                throw new ArgumentException("被拷贝图元和目标图元类型不兼容", "src");
            }
        }

         public override string ToString()
         {
             return "Text=> position:(" + Math.Round(m_position.X) + "," + Math.Round(m_position.Y) + "), width:" + Math.Round(m_width) + ", height:" + Math.Round(m_height);
         }
    }
}
