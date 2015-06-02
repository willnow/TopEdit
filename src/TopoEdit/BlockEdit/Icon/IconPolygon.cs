//******************************************************************************
//文件名称 :     IconPolygon.cs
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
    public enum PolygonType
    {
        StraightLine,
        Bezier,
        Arc,
    }
    /// <summary>
    /// <para>定义与多边形图元相关的属性和操作。</para>
    /// </summary>
    public class IconPolygon : IIcon
    {
        /// <summary>
        /// <para>组成多边形的所有路径的集合。</para>
        /// </summary>
        private List<IPath> m_pathCollection = new List<IPath>();
        /// <summary>
        /// <para>多边形的线宽。</para>
        /// </summary>
        private float m_weight = 0;
        /// <summary>
        /// <para>多边形是否填充。</para>
        /// </summary>
        private Boolean m_fill = false;


        public float Weight
        {
            get { return m_weight; }
            set { m_weight = value; }
        }

        public bool Fill
        {
            get { return m_fill; }
            set { m_fill = value; }
        }
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
        public IconPolygon()
        : base(EmIconType.Polygon)
        {

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
            Boolean finish = false;

            //加载基本属性
            base.Load(iconNode);

            //加载特殊属性
            XmlNode polygonNode = iconNode.SelectSingleNode("Polygon");

            XmlNodeList pathNodes = polygonNode.SelectNodes("Paths/Path");
            foreach (XmlNode pathNode in pathNodes)
            {
                switch (GetPolygonType(pathNode))
                {
                    case PolygonType.StraightLine:
                        {
                            XmlNode straightLineNode = pathNode.SelectSingleNode("StraightLine");

                            float x = float.Parse(straightLineNode.SelectSingleNode("BeginPoint/X").InnerText);
                            float y = float.Parse(straightLineNode.SelectSingleNode("BeginPoint/Y").InnerText);
                            PointF beginPoint = new PointF(x, y);
                            x = float.Parse(straightLineNode.SelectSingleNode("EndPoint/X").InnerText);
                            y = float.Parse(straightLineNode.SelectSingleNode("EndPoint/Y").InnerText);
                            PointF endPoint = new PointF(x, y);
                            IPath path = new StraightLineInPolygon(beginPoint, endPoint);
                            m_pathCollection.Add(path);
                            break;
                        }
                    case PolygonType.Bezier:
                        {
                            XmlNode bezierNode = pathNode.SelectSingleNode("Bezier");

                            PointF[] points = new PointF[4];
                            for (int i = 1; i <= 4; i++)
                            {
                                points[i - 1].X = float.Parse(bezierNode.SelectSingleNode("Point" + i + "/X").InnerText);
                                points[i - 1].Y = float.Parse(bezierNode.SelectSingleNode("Point" + i + "/Y").InnerText);
                            }
                            IPath path = new BezierInPolygon(points[0], points[1], points[2], points[3]);
                            m_pathCollection.Add(path);
                            break;
                        }
                    case PolygonType.Arc:
                        {
                            XmlNode arcNode = pathNode.SelectSingleNode("Arc");

                            RectangleF rect = new RectangleF();
                            rect.X = float.Parse(arcNode.SelectSingleNode("Pos/X").InnerText);
                            rect.Y = float.Parse(arcNode.SelectSingleNode("Pos/Y").InnerText);
                            rect.Width = float.Parse(arcNode.SelectSingleNode("Width").InnerText);
                            rect.Height = float.Parse(arcNode.SelectSingleNode("Height").InnerText);
                            float startAngle = float.Parse(arcNode.SelectSingleNode("StartAngle").InnerText);
                            float sweepAngle = float.Parse(arcNode.SelectSingleNode("SweepAngle").InnerText);
                            IPath path = new ArcInPolygon(rect, startAngle, sweepAngle);
                            m_pathCollection.Add(path);
                            break;
                        }
                    default:
                        finish = true;
                        break;
                }
                if (finish)
                {
                    break;
                }
            }
            m_weight = byte.Parse(polygonNode.SelectSingleNode("Weight").InnerText);
            m_fill = bool.Parse(polygonNode.SelectSingleNode("Fill").InnerText);
            return true;
        }

        private PolygonType GetPolygonType(System.Xml.XmlNode iconNode)
        {
            if (iconNode.SelectSingleNode("StraightLine") != null)
            {
                return PolygonType.StraightLine;
            }

            if (iconNode.SelectSingleNode("Bezier") != null)
            {
                return PolygonType.Bezier;
            }

            if (iconNode.SelectSingleNode("Arc") != null)
            {
                return PolygonType.Arc;
            }

            return PolygonType.StraightLine;
        }

        public override bool Save(System.Xml.XmlNode iconParentNode)
        {
            System.Xml.XmlNode iconNode = iconParentNode.OwnerDocument.CreateElement("Icon");
            base.Save(iconNode);
            System.Xml.XmlNode node = iconNode.OwnerDocument.CreateElement("Polygon");
            System.Xml.XmlNode pathsNode = node.OwnerDocument.CreateElement("Paths");
            node.AppendChild(pathsNode);

            foreach (IPath path in m_pathCollection)
            {
                System.Xml.XmlNode pathNode = node.OwnerDocument.CreateElement("Path");
                path.Save(pathNode);
                pathsNode.AppendChild(pathNode);
            }

            string fill = "false";
            if (m_fill)
            {
                fill = "true";
            }

            if (m_weight < 1)
            {
                m_weight = 1;
            }

            node.InnerXml += "<Weight>" + m_weight + "</Weight><Fill>" + fill + "</Fill>";

            iconNode.AppendChild(node);
            iconParentNode.AppendChild(iconNode);
            return true;
        }

        public override void Round()
        {
            foreach (IPath path in m_pathCollection)
            {
                path.Round();
            }
        }

        public override void Zoom(Zoom zoom)
        {
            foreach (IPath path in m_pathCollection)
            {
                path.Zoom(zoom);
            }
        }

        public override void Rotate(CSR.ShareLib.Rotate rotate)
        {
            throw new NotImplementedException();
        }

        public override void Move(Movement move)
        {
            if (move.SubMode == ScaleOpMode.None)
            {
                foreach (IPath path in m_pathCollection)
                {
                    path.Move(move);
                }
            }
        }

        public override void Symmetry(Symmetry synm)
        {
            foreach (IPath path in m_pathCollection)
            {
                path.Symmetry(synm);
            }
        }

        /// <summary>
        /// 移动多边形的一个顶点
        /// </summary>
        /// <param name="index">路径索引</param>
        /// <param name="move">移动向量</param>
        public void MoveEndPoint(int index, Movement move)
        {
            if ((index <0) || (index > m_pathCollection.Count))
            {
                throw new System.ArgumentOutOfRangeException();
            }

            PointF endPoint = GetEndPoint(index);
            List<IPath> paths = new List<IPath>();

            //修改这两条路径的端点
            foreach (IPath path in m_pathCollection)
            {
                if (path.EndPoints.Contains(endPoint))
                {
                    path.MoveEndPoint(endPoint, move);
                }
            }
        }

        /// <summary>
        /// 移动多边形的一个控制点
        /// </summary>
        /// <param name="pathIndex"></param>
        /// <param name="controlIndex"></param>
        /// <param name="m_move"></param>
        public void MoveControlPoint(int pathIndex, int controlIndex, Movement move)
        {
            if ((pathIndex >= 0) && (pathIndex < m_pathCollection.Count))
            {
                if ((controlIndex >= 0) && (controlIndex < m_pathCollection[pathIndex].ControlPoints.Count))
                {
                    m_pathCollection[pathIndex].MoveControlPoint(m_pathCollection[pathIndex].ControlPoints[controlIndex], move);
                }
                else
                {
                    throw new System.ArgumentOutOfRangeException();
                }
            }
            else
            {
                throw new System.ArgumentOutOfRangeException();
            }
        }

        public List<IPath> Paths
        {
            get
            {
                return m_pathCollection;
            }
            set
            {
                m_pathCollection = value;
            }
        }

        public void AddPath(IPath path)
        {
            m_pathCollection.Add(path);
        }

        public void RemovePath(IPath path)
        {
            m_pathCollection.Remove(path);
        }

        public void ClearPath()
        {
            m_pathCollection.Clear();
        }

        /// <summary>
        /// 多边形中的路径条数
        /// </summary>
        public int PathCnt
        {
            get
            {
                return m_pathCollection.Count;
            }
        }

        /// <summary>
        /// 获取多边形的端点集合
        /// </summary>
        /// <returns></returns>
        public List<PointF> GetEndPoints()
        {
            List<PointF> endPoints = new List<PointF>();
            for (int i = 0; i < m_pathCollection.Count; ++i)
            {
                if (!endPoints.Contains(m_pathCollection[i].EndPoints[0]))
                {
                    endPoints.Add(m_pathCollection[i].EndPoints[0]);
                }
                
                if (!endPoints.Contains(m_pathCollection[i].EndPoints[1]))
                {
                    endPoints.Add(m_pathCollection[i].EndPoints[1]);
                }
            }

            return endPoints;
        }

        /// <summary>
        /// 获取指定路径的起始端点
        /// </summary>
        /// <param name="index">路径索引，从0开始</param>
        /// <returns></returns>
        public PointF GetEndPoint(int index)
        {
            if ((index >= 0) && (index < m_pathCollection.Count))
            {
                return m_pathCollection[index].EndPoints[0];
            }
            else
            {
                throw new System.ArgumentOutOfRangeException();
            }
        }

        public PointF GetControlPoint(int pathIndex, int controlIndex)
        {
            if ((pathIndex >= 0) && (pathIndex < m_pathCollection.Count))
            {
                if ((controlIndex >= 0) && (controlIndex < m_pathCollection[pathIndex].ControlPoints.Count))
                {
                    return m_pathCollection[pathIndex].ControlPoints[controlIndex];
                }
                else
                {
                    throw new System.ArgumentOutOfRangeException();
                }
            }
            else
            {
                throw new System.ArgumentOutOfRangeException();
            }
        }

        GraphicsPath GetGraphicsPath()
        {
            GraphicsPath graphicsPath = new GraphicsPath(FillMode.Alternate);
            foreach (IPath path in m_pathCollection)
            {
                path.AddPath(ref graphicsPath);
            }
            return graphicsPath;
        }
        /// <summary>
        /// 指定点是否是多边形的端点
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public bool IsEndPoint(PointF pos)
        {
            if (m_pathCollection.Count == 0)
            {
                return false;
            }
            else
            {
                List<PointF> endPoints = GetEndPoints();

                return endPoints.Contains(pos);
            }
        }

        public override void Accept(TopoEdit.Visitor.IDrawVisitor visitor)
        {
            visitor.VisitorPolygon(this);
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
                GraphicsPath graphicsPath = GetGraphicsPath();
                return graphicsPath.GetBounds();
            }
        }

        //判断点是否和图形相交
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

            GraphicsPath graphicsPath = GetGraphicsPath();

            Pen pen = new Pen(DefaultColor.ColorInArgb, m_weight);
            IntersectVisitor visitor = new IntersectIconVisitor(IntersectIconType, point, graphicsPath, pen);
            Accept(visitor);

            if (visitor.Visible)
            {
                return this;
            }
            return null;
        }
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

            GraphicsPath graphicsPath = GetGraphicsPath();

            Pen pen = new Pen(GetDisplayColor(), m_weight);
            pen.DashStyle = DashStyle.Solid;
            SolidBrush brush = new SolidBrush(GetDisplayColor());
            if (m_fill)
            {
                cGraphics.FillPath(brush, graphicsPath);
            }
            cGraphics.DrawPath(pen, graphicsPath);

        }

        /// <summary>
        /// <para>根据被选中的图元类型创建被选中对象</para>
        /// </summary>
        public override SelectedDraw CreateSelectedDraw()
        {
            return new SelectedPolygon(this);
        }

        public override IDraw Clone()
        {
            IconPolygon polygon = new IconPolygon();
            polygon.Copy(this);
            return polygon;
        }

        public override void Copy(IDraw src)
        {
            if (src is IconPolygon)
            {
                IconPolygon srcItem = src as IconPolygon;

                base.Copy(srcItem);
                this.m_pathCollection.Clear();
                foreach (IPath path in srcItem.m_pathCollection)
                {
                    this.m_pathCollection.Add(path.Clone());
                }

                this.m_weight = srcItem.m_weight;
                this.m_fill = srcItem.m_fill;
            }
            else
            {
                throw new ArgumentException("被拷贝图元和目标图元类型不兼容", "src");
            }
        }

        public override string ToString()
        {
            GraphicsPath graphicsPath = GetGraphicsPath();

            if (m_pathCollection.Count > 0)
            {
                string tip = "Polygon=> ";
                for (int i = 1; i < graphicsPath.PathPoints.Length - 1; ++i)
                {
                    tip += "Point" + i + ":(" + Math.Round(graphicsPath.PathPoints[i].X) + "," + Math.Round(graphicsPath.PathPoints[i].Y) + "), ";
                }
                return tip;
            }
            else
            {
                return "";
            }
        }
    }
    /// <summary>
    /// <para>定义路径的相关方法。</para>
    /// </summary>
    public abstract class IPath
    {
        protected PolygonType m_pathType = PolygonType.StraightLine;

        public PolygonType PathType
        {
            get
            {
                return m_pathType;
            }
        }

        /// <summary>
        /// <para>将路径经过平移、缩放后，添加进路径集合。</para>
        /// </summary>
        /// <returns>
        /// <para></para>
        /// </returns>
        /// <remarks>
        /// <para> 蒋湘宁 2013-10-08  创建函数</para>
        /// </remarks>
        internal abstract void AddPath(ref GraphicsPath graphicsPath);
        /// <summary>
        /// <para>将默认大小的路径添加进路径集合。</para>
        /// </summary>
        /// <returns>
        /// <para></para>
        /// </returns>
        /// <remarks>
        /// <para> 蒋湘宁 2013-10-08  创建函数</para>
        /// </remarks>
        internal abstract void AddDefaultPath(ref GraphicsPath graphicsPath);
        public abstract void Zoom(Zoom zoom);
        public abstract void Rotate(CSR.ShareLib.Rotate rotate);
        public abstract void Move(Movement move);
        public abstract void Symmetry(Symmetry synm);
        /// <summary>
        /// 移动路径的其中一个端点
        /// </summary>
        /// <param name="pos">被移动端点坐标</param>
        /// <param name="move">移动向量</param>
        public abstract void MoveEndPoint(PointF pos, Movement move);
        public abstract void MoveControlPoint(PointF pos, Movement move);
        public abstract IPath Clone();
        public abstract bool Save(XmlNode parentNode);
        public abstract void Round();
        /// <summary>
        /// 转换前后的端点不能改变
        /// </summary>
        /// <param name="newPolygonType">转换的目标类型</param>
        public abstract IPath ConvertTo(PolygonType newPolygonType);
        /// <summary>
        /// 路径的端点，第0个:起始端点，第1个：终止端点
        /// </summary>
        public abstract List<PointF> EndPoints
        {
            get;
        }
        /// <summary>
        /// 路径的控制点，从起始端点到终止端点顺序编号
        /// </summary>
        public abstract List<PointF> ControlPoints
        {
            get;
        }
    }
    /// <summary>
    /// <para>直线路径的相关方法。</para>
    /// </summary>
    internal class StraightLineInPolygon : IPath
    {
        /// <summary>
        /// <para>线段的起点。</para>
        /// </summary>
        private PointF m_beginPoint = new PointF();
        /// <summary>
        /// <para>线段的终点</para>
        /// </summary>
        private PointF m_endPoint = new PointF();
        /// <summary>
        /// <para>根据输入参数初始化字段。</para>
        /// <para>前置条件：</para>
        /// <para>    beginPoint endPoint 不为空</para>
        /// <para>后置条件：</para>
        /// <para>    字段m_beginPoint、m_endPoint被赋予新值。</para>
        /// </summary>
        /// <param name="beginPoint">线段起点</param>
        /// <param name="endPoint">线段终点</param>
        /// <returns>
        /// <para></para>
        /// </returns>
        /// <remarks>
        /// <para> 蒋湘宁 2013-10-08  创建函数</para>
        /// </remarks>
        internal StraightLineInPolygon(PointF beginPoint, PointF endPoint)
        {
            m_pathType = PolygonType.StraightLine;
            m_beginPoint = beginPoint;
            m_endPoint = endPoint;
        }
        /// <summary>
        /// <para>重写IPath方法，将路径经过平移、缩放后，添加进路径集合。</para>
        /// <para>前置条件：</para>
        /// <para>    graphicsPath rtu 不为空</para>
        /// <para>后置条件：无</para>
        /// </summary>
        /// <param name="graphicsPath">路径集合</param>
        /// <param name="rtu">图元所在的设备集中站对象，提供图元的位移、缩放方法。</param>
        /// <returns>
        /// <para>经过平移、缩放后的路径被添加进路径集合graphicsPath。</para>
        /// </returns>
        /// <remarks>
        /// <para> 蒋湘宁 2013-10-08  创建函数</para>
        /// </remarks>
        internal override sealed void AddPath(ref GraphicsPath graphicsPath)
        {
            PointF pt1 = m_beginPoint;
            PointF pt2 = m_endPoint;

            graphicsPath.AddLine(pt1, pt2);
        }
        /// <summary>
        /// <para>重写IPath方法，将默认大小的路径添加进路径集合。</para>
        /// <para>前置条件：</para>
        /// <para>    graphicsPath 不为空</para>
        /// <para>后置条件：无</para>
        /// </summary>
        /// <param name="graphicsPath">路径集合</param>
        /// <returns>
        /// <para>路径被添加进路径集合graphicsPath。</para>
        /// </returns>
        /// <remarks>
        /// <para> 蒋湘宁 2013-10-08  创建函数</para>
        /// </remarks>
        internal override sealed void AddDefaultPath(ref GraphicsPath graphicsPath)
        {
            PointF pt1 = m_beginPoint;
            PointF pt2 = m_endPoint;
            graphicsPath.AddLine(pt1, pt2);
        }

        public override void Zoom(Zoom zoom)
        {
            switch (zoom.SubMode)
            {
                case ScaleOpMode.None:
                    {
                        zoom.LPtoDP(ref m_beginPoint);
                        zoom.LPtoDP(ref m_endPoint);
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
            if (move.SubMode == ScaleOpMode.None)
            {
                move.LPtoDP(ref m_beginPoint);
                move.LPtoDP(ref m_endPoint);
            }
        }

        public override void Symmetry(Symmetry synm)
        {
            synm.LPtoDP(ref m_beginPoint);
            synm.LPtoDP(ref m_endPoint);
        }

        public override IPath Clone()
        {
            return new StraightLineInPolygon(this.m_beginPoint, this.m_endPoint);
        }

        public override bool Save(XmlNode iconParentNode)
        {
            Round();
            System.Xml.XmlNode node = iconParentNode.OwnerDocument.CreateElement("StraightLine");
            node.InnerXml = "<BeginPoint><X>" + m_beginPoint.X + "</X><Y>" + m_beginPoint.Y + "</Y></BeginPoint>"
                + "<EndPoint><X>" + m_endPoint.X + "</X><Y>" + m_endPoint.Y + "</Y></EndPoint>";
            iconParentNode.AppendChild(node);
            return true;
        }

        public override void Round()
        {
            m_beginPoint = Utility.ConvertPos(m_beginPoint);
            m_endPoint = Utility.ConvertPos(m_endPoint);
        }

        public override void MoveEndPoint(PointF pos, Movement move)
        {
            if (m_beginPoint == pos)
            {
                move.LPtoDP(ref m_beginPoint);
            }
            else if (m_endPoint == pos)
            {
                move.LPtoDP(ref m_endPoint);
            }
            else
            {
                //不是任意一个点，不处理
            }
        }

        public override IPath ConvertTo(PolygonType newPolygonType)
        {
            switch (newPolygonType)
            {
                case PolygonType.Arc:
                    {
                        //暂不支持
                        return this;
                    }
                case PolygonType.Bezier:
                    {
                        //端点不变
                        //根据端点计算两个初始的贝塞尔控制点
                        PointF point3 = new PointF((m_beginPoint.X + m_endPoint.X) / 2, (m_beginPoint.Y + m_endPoint.Y) / 2);
                        PointF point2 = new PointF((m_beginPoint.X + point3.X) / 2, (m_beginPoint.Y + point3.Y) / 2);

                        return new BezierInPolygon(m_beginPoint, point2, point3, m_endPoint);
                    }
                default:
                    {
                        //不需要处理
                        return this;
                    }
            }
        }

        public override List<PointF> EndPoints
        {
            get
            {
                List<PointF> endPoints = new List<PointF>();
                endPoints.Add(m_beginPoint);
                endPoints.Add(m_endPoint);
                return endPoints;
            }
        }

        public override List<PointF> ControlPoints
        {
            get
            {
                //直线没有控制点
                List<PointF> endPoints = new List<PointF>();
                return endPoints;
            }
        }

        public override void MoveControlPoint(PointF pos, Movement move)
        {
            //没有控制点，不需要处理
        }
    }
    /// <summary>
    /// <para>贝塞尔曲线路径的相关方法。</para>
    /// </summary>
    internal class BezierInPolygon : IPath
    {
        /// <summary>
        /// <para>构造曲线必需点1</para>
        /// </summary>
        private PointF m_point1;
        /// <summary>
        /// <para>构造曲线必需点2</para>
        /// </summary>
        private PointF m_point2;
        /// <summary>
        /// <para>构造曲线必需点3</para>
        /// </summary>
        private PointF m_point3;
        /// <summary>
        /// <para>构造曲线必需点4</para>
        /// </summary>
        private PointF m_point4;
        /// <summary>
        /// <para>根据输入参数初始化字段。</para>
        /// <para>前置条件：</para>
        /// <para>    point1 point2 point3 point4 不为空</para>
        /// <para>后置条件：</para>
        /// <para>    字段m_point1、m_point2、m_point3、m_point4被赋予新值。</para>
        /// </summary>
        /// <param name="point1">构造曲线必需点1</param>
        /// <param name="point2">构造曲线必需点2</param>
        /// <param name="point3">构造曲线必需点3</param>
        /// <param name="point4">构造曲线必需点4</param>
        /// <returns>
        /// <para></para>
        /// </returns>
        /// <remarks>
        /// <para> 蒋湘宁 2013-10-08  创建函数</para>
        /// </remarks>
        internal BezierInPolygon(PointF point1, PointF point2, PointF point3, PointF point4)
        {
            m_pathType = PolygonType.Bezier;
            m_point1 = point1;
            m_point2 = point2;
            m_point3 = point3;
            m_point4 = point4;
        }
        /// <summary>
        /// <para>重写IPath方法，将路径经过平移、缩放后，添加进路径集合。</para>
        /// <para>前置条件：</para>
        /// <para>    graphicsPath rtu 不为空</para>
        /// <para>后置条件：无</para>
        /// </summary>
        /// <param name="graphicsPath">路径集合</param>
        /// <param name="rtu">图元所在的设备集中站对象，提供图元的位移、缩放方法。</param>
        /// <returns>
        /// <para>经过平移、缩放后的路径被添加进路径集合graphicsPath。</para>
        /// </returns>
        /// <remarks>
        /// <para> 蒋湘宁 2013-10-08  创建函数</para>
        /// </remarks>
        internal override sealed void AddPath(ref GraphicsPath graphicsPath)
        {
            PointF pt1 = m_point1;
            PointF pt2 = m_point2;
            PointF pt3 = m_point3;
            PointF pt4 = m_point4;

            graphicsPath.AddBezier(pt1, pt2, pt3, pt4);
        }
        /// <summary>
        /// <para>重写IPath方法，将默认大小的路径添加进路径集合。</para>
        /// <para>前置条件：</para>
        /// <para>    graphicsPath 不为空</para>
        /// <para>后置条件：无</para>
        /// </summary>
        /// <param name="graphicsPath">路径集合</param>
        /// <returns>
        /// <para>路径被添加进路径集合graphicsPath。</para>
        /// </returns>
        /// <remarks>
        /// <para> 蒋湘宁 2013-10-08  创建函数</para>
        /// </remarks>
        internal override sealed void AddDefaultPath(ref GraphicsPath graphicsPath)
        {
            PointF pt1 = m_point1;
            PointF pt2 = m_point2;
            PointF pt3 = m_point3;
            PointF pt4 = m_point4;
            graphicsPath.AddBezier(pt1, pt2, pt3, pt4);
        }

        public override void Zoom(Zoom zoom)
        {
            switch (zoom.SubMode)
            {
                case ScaleOpMode.None:
                    {
                        zoom.LPtoDP(ref m_point1);
                        zoom.LPtoDP(ref m_point2);
                        zoom.LPtoDP(ref m_point3);
                        zoom.LPtoDP(ref m_point4);
                        break;
                    }
                default:
                    {
                        //不能缩放
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
            if (move.SubMode == ScaleOpMode.None)
            {
                move.LPtoDP(ref m_point1);
                move.LPtoDP(ref m_point2);
                move.LPtoDP(ref m_point3);
                move.LPtoDP(ref m_point4);
            }
        }

        public override IPath Clone()
        {
            return new BezierInPolygon(this.m_point1, this.m_point2, this.m_point3, this.m_point4);
        }

        public override bool Save(XmlNode iconParentNode)
        {
            Round();
            System.Xml.XmlNode node = iconParentNode.OwnerDocument.CreateElement("Bezier");
            node.InnerXml = "<Point1><X>" + m_point1.X + "</X><Y>" + m_point1.Y + "</Y></Point1>"
                + "<Point2><X>" + m_point2.X + "</X><Y>" + m_point2.Y + "</Y></Point2>"
                + "<Point3><X>" + m_point3.X + "</X><Y>" + m_point3.Y + "</Y></Point3>"
                + "<Point4><X>" + m_point4.X + "</X><Y>" + m_point4.Y + "</Y></Point4>";
            iconParentNode.AppendChild(node);
            return true;
        }

        public override void Round()
        {
            m_point1 = Utility.ConvertPos(m_point1);
            m_point2 = Utility.ConvertPos(m_point2);
            m_point3 = Utility.ConvertPos(m_point3);
            m_point4 = Utility.ConvertPos(m_point4);
        }


        public override void MoveEndPoint(PointF pos, Movement move)
        {
            if (m_point1 == pos)
            {
                move.LPtoDP(ref m_point1);
            }
            else if (m_point4 == pos)
            {
                move.LPtoDP(ref m_point4);
            }
            else
            {
                //不是贝塞尔曲线的起点或终点，不处理
            }
        }

        public override void Symmetry(Symmetry synm)
        {
            synm.LPtoDP(ref m_point1);
            synm.LPtoDP(ref m_point2);
            synm.LPtoDP(ref m_point3);
            synm.LPtoDP(ref m_point4);
        }

        public override IPath ConvertTo(PolygonType newPolygonType)
        {
            switch (newPolygonType)
            {
                case PolygonType.Arc:
                    {
                        //暂不支持
                        return this;
                    }
                case PolygonType.StraightLine:
                    {
                        //端点不变
                        return new StraightLineInPolygon(m_point1, m_point4);
                    }
                default:
                    {
                        //不需要处理
                        return this;
                    }
            }
        }

        public override List<PointF> EndPoints
        {
            get 
            {
                List<PointF> endPoints = new List<PointF>();
                endPoints.Add(m_point1);
                endPoints.Add(m_point4);
                return endPoints;
            }
        }

        public override List<PointF> ControlPoints
        {
            get
            {
                List<PointF> endPoints = new List<PointF>();
                endPoints.Add(m_point2);
                endPoints.Add(m_point3);
                return endPoints;
            }
        }

        public override void MoveControlPoint(PointF pos, Movement move)
        {
            if (m_point2 == pos)
            {
                move.LPtoDP(ref m_point2);
            }
            else if (m_point3 == pos)
            {
                move.LPtoDP(ref m_point3);
            }
            else
            {
                //该点不是控制点，不需要移动
            }
        }
    }
    /// <summary>
    /// <para>椭圆弧路径的相关方法。</para>
    /// </summary>
    internal class ArcInPolygon : IPath
    {
        /// <summary>
        /// <para>表示从中截取弧线的椭圆的矩形边界。</para>
        /// </summary>
        private RectangleF m_rect;
        /// <summary>
        /// <para>弧线的起始角度，以度为单位从X轴顺时针测量。</para>
        /// </summary>
        private float m_startAngle;
        /// <summary>
        /// <para>startAngle 和弧线末尾之间的角度</para>
        /// </summary>
        private float m_sweepAngle;
        /// <summary>
        /// <para>根据输入参数初始化字段。</para>
        /// <para>前置条件：</para>
        /// <para>    rect 不为空</para>
        /// <para>后置条件：</para>
        /// <para>    字段m_rect、m_startAngle、m_sweepAngle被赋予新值。</para>
        /// </summary>
        /// <param name="rect">表示从中截取弧线的椭圆的矩形边界。</param>
        /// <param name="startAngle">弧线的起始角度，以度为单位从X轴顺时针测量。</param>
        /// <param name="sweepAngle">startAngle 和弧线末尾之间的角度</param>
        /// <returns>
        /// <para></para>
        /// </returns>
        /// <remarks>
        /// <para> 蒋湘宁 2013-10-08  创建函数</para>
        /// </remarks>
        internal ArcInPolygon(RectangleF rect, float startAngle, float sweepAngle)
        {
            m_pathType = PolygonType.Arc;
            m_rect = rect;
            m_startAngle = startAngle;
            m_sweepAngle = sweepAngle;
        }
        /// <summary>
        /// <para>重写IPath方法，将路径经过平移、缩放后，添加进路径集合。</para>
        /// <para>前置条件：</para>
        /// <para>    graphicsPath rtu 不为空</para>
        /// <para>后置条件：无</para>
        /// </summary>
        /// <param name="graphicsPath">路径集合</param>
        /// <param name="rtu">图元所在的设备集中站对象，提供图元的位移、缩放方法。</param>
        /// <returns>
        /// <para>经过平移、缩放后的路径被添加进路径集合graphicsPath。</para>
        /// </returns>
        /// <remarks>
        /// <para> 蒋湘宁 2013-10-08  创建函数</para>
        /// </remarks>
        internal override sealed void AddPath(ref GraphicsPath graphicsPath)
        {
            RectangleF rect = m_rect;
            SizeF diameter = new SizeF(rect.Width, rect.Height);
            rect.Width = diameter.Width;
            rect.Height = diameter.Height;
            graphicsPath.AddArc(m_rect, m_startAngle, m_sweepAngle);
        }
        /// <summary>
        /// <para>重写IPath方法，将默认大小的路径添加进路径集合。</para>
        /// <para>前置条件：</para>
        /// <para>    graphicsPath 不为空</para>
        /// <para>后置条件：无</para>
        /// </summary>
        /// <param name="graphicsPath">路径集合</param>
        /// <returns>
        /// <para>路径被添加进路径集合graphicsPath。</para>
        /// </returns>
        /// <remarks>
        /// <para> 蒋湘宁 2013-10-08  创建函数</para>
        /// </remarks>
        internal override sealed void AddDefaultPath(ref GraphicsPath graphicsPath)
        {
            RectangleF rect = m_rect;
            graphicsPath.AddArc(m_rect, m_startAngle, m_sweepAngle);
        }

        public override void Zoom(Zoom zoom)
        {
            switch (zoom.SubMode)
            {
                case ScaleOpMode.None:
                    {
                        SizeF diameter = new SizeF(m_rect.Width, m_rect.Height);
                        zoom.LPtoDP(ref diameter);
                        m_rect.Width = diameter.Width;
                        m_rect.Height = diameter.Height;
                        break;
                    }
                default:
                    {
                        //不能缩放
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
            if (move.SubMode == ScaleOpMode.None)
            {
                move.LPtoDP(ref m_rect);
            }
        }

        public override void Symmetry(Symmetry synm)
        {
            if (synm.Point1.X == synm.Point2.X)
            {
                PointF location = new PointF(m_rect.Right, m_rect.Top);
                synm.LPtoDP(ref location);
                m_rect.Location = location;
            }
            else
            {
                //暂不支持
            }
        }

        public override IPath Clone()
        {
            return new ArcInPolygon(this.m_rect, this.m_startAngle, this.m_sweepAngle);
        }

        public override bool Save(XmlNode iconParentNode)
        {
            Round();
            System.Xml.XmlNode node = iconParentNode.OwnerDocument.CreateElement("Arc");
            node.InnerXml = "<Pos><X>" + m_rect.X + "</X><Y>" + m_rect.Y + "</Y></Pos><Width>" +
                m_rect.Width + "</Width><Height>" + m_rect.Height + "</Height><StartAngle>" +
                m_startAngle + "</StartAngle><SweepAngle>" + m_sweepAngle + "</SweepAngle>";
            iconParentNode.AppendChild(node);
            return true;
        }

        public override void Round()
        {
            m_rect = Utility.ConvertRect(m_rect);
        }

        public override void MoveEndPoint(PointF pos, Movement move)
        {
            throw new NotImplementedException();
        }

        public override IPath ConvertTo(PolygonType newPolygonType)
        {
            throw new NotImplementedException();
        }

        public override List<PointF> EndPoints
        {
            get { throw new NotImplementedException(); }
        }

        public override List<PointF> ControlPoints
        {
            get { throw new NotImplementedException(); }
        }

        public override void MoveControlPoint(PointF pos, Movement move)
        {
            throw new NotImplementedException();
        }
    }
}
