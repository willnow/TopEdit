//******************************************************************************
//文件名称 :     IconLine.cs
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
    /// <para>定义与线条图元相关的属性和操作。</para>
    /// </summary>
    public class IconLine : IIcon
    {
        #region 成员变量
        /// <summary>
        /// <para>线条图元的始端顶点。</para>
        /// </summary>
        private PointF m_beginPosition = new PointF(0, 0);
        /// <summary>
        /// <para>线条图元的终端顶点。</para>
        /// </summary>
        private PointF m_endPosition = new PointF(10, 0);
        /// <summary>
        /// <para>圆形图元的线宽。</para>
        /// </summary>
        private float m_weight = 1;
        private DashStyle m_dashStyle = DashStyle.Solid;
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

                //重新根据Len计算RightPos
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
                        //上下不允许缩放
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

        #region 重写IIcon 成员

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
        /// <para>重写IIcon属性，获得图元的默认边界大小。</para>
        /// </summary>
        /// <remarks>
        /// <para> 蒋湘宁 2013-10-08  创建属性</para>
        /// </remarks>
        public override sealed RectangleF BoundsRect
        {
            get
            {
                //初始化包围的矩形。
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

        #region 重写IDraw 成员
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
                throw new ArgumentException("被拷贝图元和目标图元类型不兼容", "src");
            }
        }

        public override string ToString()
        {
            return "Line=> begin:(" + Math.Round(m_beginPosition.X) + "," + Math.Round(m_beginPosition.Y) + "), end:(" + Math.Round(m_endPosition.X) + ","
                + Math.Round(m_endPosition.Y) + ", weight:" + Math.Round(m_weight);
        }
    }
}
