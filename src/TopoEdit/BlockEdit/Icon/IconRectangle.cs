//******************************************************************************
//文件名称 :     IconRectangle.cs
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
    /// <para>定义与矩形图元相关的属性和操作。</para>
    /// </summary>
    public class IconRectangle : IIcon
    {
        #region 成员变量
        /// <summary>
        /// <para>矩形左上角的坐标</para>
        /// </summary>
        private PointF m_position = new PointF(0 , 0);
        /// <summary>
        /// <para>矩形宽</para>
        /// </summary>
        private float m_width = 1;
        /// <summary>
        /// <para>矩形高</para>
        /// </summary>
        private float m_height = 1;
        /// <summary>
        /// <para>矩形线宽</para>
        /// </summary>
        private float m_weight = 1;
        /// <summary>
        /// <para>矩形是否填充</para>
        /// </summary>
        private Boolean m_fill = false;
        private DashStyle m_panDashStyle = DashStyle.Solid;
        /// <summary>
        /// <para>矩形旋转角度。若不旋转，为0。</para>
        /// </summary>
        private int m_degree = 0;
        /// <summary>
        /// <para>矩形旋转弧度。计算公式 弧度 = π * 角度 / 180</para>
        /// <para>当矩形存在旋转角度时，该值有效。</para>
        /// </summary>
        private double m_angle = 0;
        /// <summary>
        /// <para>矩形的四个点的坐标值。</para>
        /// <para>当矩形存在旋转角度时，该值有效。</para>
        /// </summary>
        private PointF[] m_polygonIconPoint = new PointF[4];
        
        #endregion

        /// <summary>
        /// <para>调用父类的构造函数进行初始化。</para>
        /// <para>前置条件：</para>
        /// <para>    无</para>
        /// <para>后置条件：</para>
        /// <para>    所有字段被赋予新值。</para>
        /// </summary>
        /// <param name="rtu">图元所属Rtu</param>
        /// <returns>
        /// <para></para>
        /// </returns>
        /// <remarks>
        /// <para> 蒋湘宁 2013-10-08  创建函数</para>
        /// </remarks>
        public IconRectangle()
        : base(EmIconType.Rectangle)
        {

        }
        /// <summary>
        /// <para>调用父类的构造函数进行初始化。</para>
        /// </summary>
        /// <param name="position">矩形左上角坐标</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <param name="isFill">是否填充</param>
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
        /// <para>获取或设置矩形左上角坐标</para>
        /// </summary>
        /// <remarks>
        /// <para> 蒋湘宁 2013-10-08  创建属性</para>
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
        /// 矩形宽
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
        /// 矩形高
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
        #region 重写IIcon 成员
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
                ConvertRectToPolygon();//修改了原始矩形，则重新计算多边形
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
                            //记录缩放前边界
                            RectangleF rect = BoundsRect;
                            //缩放长宽
                            //向左或上拉时，move向量的x和y小于0
                            //但对于长度来讲则正好相反
                            move.XMovement = -move.XMovement;
                            move.YMovement = -move.YMovement;
                            MoveDiameter(move);
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
                            move.YMovement = -move.YMovement;
                            MoveDiameter(move);
                            //从新计算左上角坐标
                            m_position.Y = rect.Bottom - m_height;
                            break;
                        }
                    case ScaleOpMode.LeftDown:
                        {
                            //记录缩放前边界
                            RectangleF rect = BoundsRect;
                            //缩放长宽
                            move.XMovement = -move.XMovement;
                            MoveDiameter(move);
                            //从新计算左上角坐标
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
                        //当角度 > 90 时有效
                        if (m_degree > 90)
                        {
                            Movement rectMove = CalcMoveByAnchorMove(move);


                            rectMove.LPtoDP(ref m_polygonIconPoint[1]);
                            rectMove.LPtoDP(ref m_polygonIconPoint[2]);

                            ConvertPolygonToRect();//修改了多边形，则重新计算原始矩形
                        }
                        break;
                    }
                case ScaleOpMode.RightDown:
                    {
                        //当角度 < 90 时有效
                        if (m_degree > 90)
                        {
                            Movement rectMove = CalcMoveByAnchorMove(move);

                            rectMove.LPtoDP(ref m_polygonIconPoint[1]);
                            rectMove.LPtoDP(ref m_polygonIconPoint[2]);

                            ConvertPolygonToRect();//修改了多边形，则重新计算原始矩形
                        }
                        break;
                    }
                case ScaleOpMode.RightUp:
                    {
                        //当角度 > 90 时有效
                        if (m_degree > 90)
                        {
                            Movement rectMove = CalcMoveByAnchorMove(move);

                            rectMove.LPtoDP(ref m_polygonIconPoint[0]);
                            rectMove.LPtoDP(ref m_polygonIconPoint[3]);

                            ConvertPolygonToRect();//修改了多边形，则重新计算原始矩形
                        }
                        break;
                    }
                case ScaleOpMode.LeftUp:
                    {
                        //当角度 < 90 时有效
                        if (m_degree > 90)
                        {
                            Movement rectMove = CalcMoveByAnchorMove(move);

                            rectMove.LPtoDP(ref m_polygonIconPoint[0]);
                            rectMove.LPtoDP(ref m_polygonIconPoint[3]);

                            ConvertPolygonToRect();//修改了多边形，则重新计算原始矩形
                        }
                        break;
                    }
                case ScaleOpMode.Up:
                    {
                        Movement rectMove = CalcMoveByAnchorMove(move);

                        rectMove.LPtoDP(ref m_polygonIconPoint[0]);
                        rectMove.LPtoDP(ref m_polygonIconPoint[3]);

                        ConvertPolygonToRect();//修改了多边形，则重新计算原始矩形
                        break;
                    }
                case ScaleOpMode.Down:
                    {
                        Movement rectMove = CalcMoveByAnchorMove(move);

                        rectMove.LPtoDP(ref m_polygonIconPoint[1]);
                        rectMove.LPtoDP(ref m_polygonIconPoint[2]);

                        ConvertPolygonToRect();//修改了多边形，则重新计算原始矩形

                        break;
                    }
                case ScaleOpMode.None:
                    {
                        //等比例放大缩小，只需要缩放每个点即可
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
        /// 将锚点的移动向量转换为旋转矩形端点的移动向量
        /// </summary>
        /// <param name="move"></param>
        /// <returns></returns>
        private Movement CalcMoveByAnchorMove(Movement move)
        {
            if((m_degree == 90) || (m_degree == 0))
            {
                //垂直和水平方向上两个移动向量相同
                return move.Clone() as Movement;
            }
            else
            {
                //矩形是一个斜线
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

        #region IDraw 成员
        /// <summary>
        /// <para>重写IIcon方法，绘制图元。</para>
        /// <para>前置条件：</para>
        /// <para>    cGraphics 不为空</para>
        /// <para>后置条件：无</para>
        /// </summary>
        /// <param name="cGraphics">封装一个GDI+绘图图面</param>
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
        /// <para>绘制无旋转矩形。</para>
        /// <para>前置条件：</para>
        /// <para>    cGraphics 不为空</para>
        /// <para>后置条件：无</para>
        /// </summary>
        /// <param name="cGraphics">封装一个GDI+绘图图面</param>
        /// <returns>
        /// <para>无</para>
        /// </returns>
        /// <remarks>
        /// <para> 蒋湘宁 2013-10-08  创建函数</para>
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
        /// <para>绘制旋转矩形。</para>
        /// <para>前置条件：</para>
        /// <para>    cGraphics 不为空</para>
        /// <para>后置条件：无</para>
        /// </summary>
        /// <param name="cGraphics">封装一个GDI+绘图图面</param>
        /// <returns>
        /// <para>无</para>
        /// </returns>
        /// <remarks>
        /// <para> 蒋湘宁 2013-10-08  创建函数</para>
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
        /// <para>获得非旋转矩形的默认边界。</para>
        /// <para>前置条件：</para>
        /// <para>    无</para>
        /// <para>后置条件：无</para>
        /// </summary>
        /// <returns>
        /// <para>非旋转矩形的默认边界</para>
        /// </returns>
        /// <remarks>
        /// <para> 蒋湘宁 2013-10-08  创建函数</para>
        /// </remarks>
        private RectangleF GetDefaultBoundOfRectangleNormal()
        {
            return new RectangleF(m_position, new SizeF(m_width, m_height));
        }
        /// <summary>
        /// <para>获得旋转矩形的默认边界。</para>
        /// <para>前置条件：</para>
        /// <para>    无</para>
        /// <para>后置条件：无</para>
        /// </summary>
        /// <returns>
        /// <para>旋转矩形的默认边界</para>
        /// </returns>
        /// <remarks>
        /// <para> 蒋湘宁 2013-10-08  创建函数</para>
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
        /// <para>判断输入点是否位于非旋转矩形的所在区域内。</para>
        /// <para>前置条件：</para>
        /// <para>    point 不为空</para>
        /// <para>后置条件：无</para>
        /// </summary>
        /// <param name="point">输入点</param>
        /// <returns>
        /// <para>true：输入点位于非旋转矩形的所在区域内</para>
        /// <para>false：输入点没有位于非旋转矩形的所在区域内</para>
        /// </returns>
        /// <remarks>
        /// <para> 蒋湘宁 2013-10-08  创建函数</para>
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
        /// <para>判断输入点是否位于旋转矩形的所在区域内。</para>
        /// <para>前置条件：</para>
        /// <para>    point 不为空</para>
        /// <para>后置条件：无</para>
        /// </summary>
        /// <param name="point">输入点</param>
        /// <returns>
        /// <para>true：输入点位于旋转矩形的所在区域内</para>
        /// <para>false：输入点没有位于旋转矩形的所在区域内</para>
        /// </returns>
        /// <remarks>
        /// <para> 蒋湘宁 2013-10-08  创建函数</para>
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
                throw new ArgumentException("被拷贝图元和目标图元类型不兼容", "src");
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
