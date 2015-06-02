//******************************************************************************
//文件名称 :     IconCircle.cs
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
    /// <para>定义与圆形图元相关的属性和操作。</para>
    /// </summary>
    public class IconCircle : IIcon
    {
        #region 成员变量
        /// <summary>
        /// <para>圆形图元所在矩形左上角点的位置。</para>
        /// </summary>
        private PointF m_position = new PointF(0, 0);
        /// <summary>
        /// <para>圆形图元所在矩形宽度。</para>
        /// </summary>
        private float m_diameter = 5;
        /// <summary>
        /// <para>圆形图元的线宽。</para>
        /// </summary>
        private float m_weight = 1;
        /// <summary>
        /// <para>圆形图元是否填充。</para>
        /// </summary>
        private Boolean m_fill = false;
        #endregion

        /// <summary>
        /// <para>调用父类的构造函数进行初始化。</para>
        /// <para>前置条件：</para>
        /// <para>    无</para>
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

        #region IIcon 成员
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
                        //不支持缩放
                        break;
                    }
                case ScaleOpMode.LeftUp:
                    {
                        //记录缩放前边界
                        RectangleF rect = BoundsRect;
                        //缩放长宽
                        ZoomDiameter(zoom);
                        //从新计算左上角坐标
                        m_position.Y = rect.Bottom - m_diameter;
                        m_position.X = rect.Right - m_diameter;
                        break;
                    }
                case ScaleOpMode.RightUp:
                    {
                        //记录缩放前边界
                        RectangleF rect = BoundsRect;
                        //缩放长宽
                        ZoomDiameter(zoom);
                        //从新计算左上角坐标
                        m_position.Y = rect.Bottom - m_diameter;
                        break;
                    }
                case ScaleOpMode.LeftDown:
                    {
                        //记录缩放前边界
                        RectangleF rect = BoundsRect;
                        //缩放长宽
                        ZoomDiameter(zoom);
                        //从新计算左上角坐标
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
        /// <para>重写IIcon属性，获得图元的默认边界大小。</para>
        /// </summary>
        /// <remarks>
        /// <para> 蒋湘宁 2013-10-08  创建属性</para>
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
                throw new ArgumentException("被拷贝图元和目标图元类型不兼容", "src");
            }
        }

        public override string ToString()
        {
            return "Circle=> position:(" + Math.Round(m_position.X) + "," + Math.Round(m_position.Y) + "), diameter:" + Math.Round(m_diameter) + ", weight:" + Math.Round(m_weight);
        }
    }
}

