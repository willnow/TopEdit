//******************************************************************************
//�ļ����� :     IconPolygon.cs
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
    public enum PolygonType
    {
        StraightLine,
        Bezier,
        Arc,
    }
    /// <summary>
    /// <para>����������ͼԪ��ص����ԺͲ�����</para>
    /// </summary>
    public class IconPolygon : IIcon
    {
        /// <summary>
        /// <para>��ɶ���ε�����·���ļ��ϡ�</para>
        /// </summary>
        private List<IPath> m_pathCollection = new List<IPath>();
        /// <summary>
        /// <para>����ε��߿�</para>
        /// </summary>
        private float m_weight = 0;
        /// <summary>
        /// <para>������Ƿ���䡣</para>
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
        public IconPolygon()
        : base(EmIconType.Polygon)
        {

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
            Boolean finish = false;

            //���ػ�������
            base.Load(iconNode);

            //������������
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
        /// �ƶ�����ε�һ������
        /// </summary>
        /// <param name="index">·������</param>
        /// <param name="move">�ƶ�����</param>
        public void MoveEndPoint(int index, Movement move)
        {
            if ((index <0) || (index > m_pathCollection.Count))
            {
                throw new System.ArgumentOutOfRangeException();
            }

            PointF endPoint = GetEndPoint(index);
            List<IPath> paths = new List<IPath>();

            //�޸�������·���Ķ˵�
            foreach (IPath path in m_pathCollection)
            {
                if (path.EndPoints.Contains(endPoint))
                {
                    path.MoveEndPoint(endPoint, move);
                }
            }
        }

        /// <summary>
        /// �ƶ�����ε�һ�����Ƶ�
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
        /// ������е�·������
        /// </summary>
        public int PathCnt
        {
            get
            {
                return m_pathCollection.Count;
            }
        }

        /// <summary>
        /// ��ȡ����εĶ˵㼯��
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
        /// ��ȡָ��·������ʼ�˵�
        /// </summary>
        /// <param name="index">·����������0��ʼ</param>
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
        /// ָ�����Ƿ��Ƕ���εĶ˵�
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
        /// <para>��дIIcon���ԣ����ͼԪ��Ĭ�ϱ߽��С��</para>
        /// </summary>
        /// <remarks>
        /// <para> ������ 2013-10-08  ��������</para>
        /// </remarks>
        public override sealed RectangleF BoundsRect
        {
            get
            {
                GraphicsPath graphicsPath = GetGraphicsPath();
                return graphicsPath.GetBounds();
            }
        }

        //�жϵ��Ƿ��ͼ���ཻ
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
        /// <para>���ݱ�ѡ�е�ͼԪ���ʹ�����ѡ�ж���</para>
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
                throw new ArgumentException("������ͼԪ��Ŀ��ͼԪ���Ͳ�����", "src");
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
    /// <para>����·������ط�����</para>
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
        /// <para>��·������ƽ�ơ����ź���ӽ�·�����ϡ�</para>
        /// </summary>
        /// <returns>
        /// <para></para>
        /// </returns>
        /// <remarks>
        /// <para> ������ 2013-10-08  ��������</para>
        /// </remarks>
        internal abstract void AddPath(ref GraphicsPath graphicsPath);
        /// <summary>
        /// <para>��Ĭ�ϴ�С��·����ӽ�·�����ϡ�</para>
        /// </summary>
        /// <returns>
        /// <para></para>
        /// </returns>
        /// <remarks>
        /// <para> ������ 2013-10-08  ��������</para>
        /// </remarks>
        internal abstract void AddDefaultPath(ref GraphicsPath graphicsPath);
        public abstract void Zoom(Zoom zoom);
        public abstract void Rotate(CSR.ShareLib.Rotate rotate);
        public abstract void Move(Movement move);
        public abstract void Symmetry(Symmetry synm);
        /// <summary>
        /// �ƶ�·��������һ���˵�
        /// </summary>
        /// <param name="pos">���ƶ��˵�����</param>
        /// <param name="move">�ƶ�����</param>
        public abstract void MoveEndPoint(PointF pos, Movement move);
        public abstract void MoveControlPoint(PointF pos, Movement move);
        public abstract IPath Clone();
        public abstract bool Save(XmlNode parentNode);
        public abstract void Round();
        /// <summary>
        /// ת��ǰ��Ķ˵㲻�ܸı�
        /// </summary>
        /// <param name="newPolygonType">ת����Ŀ������</param>
        public abstract IPath ConvertTo(PolygonType newPolygonType);
        /// <summary>
        /// ·���Ķ˵㣬��0��:��ʼ�˵㣬��1������ֹ�˵�
        /// </summary>
        public abstract List<PointF> EndPoints
        {
            get;
        }
        /// <summary>
        /// ·���Ŀ��Ƶ㣬����ʼ�˵㵽��ֹ�˵�˳����
        /// </summary>
        public abstract List<PointF> ControlPoints
        {
            get;
        }
    }
    /// <summary>
    /// <para>ֱ��·������ط�����</para>
    /// </summary>
    internal class StraightLineInPolygon : IPath
    {
        /// <summary>
        /// <para>�߶ε���㡣</para>
        /// </summary>
        private PointF m_beginPoint = new PointF();
        /// <summary>
        /// <para>�߶ε��յ�</para>
        /// </summary>
        private PointF m_endPoint = new PointF();
        /// <summary>
        /// <para>�������������ʼ���ֶΡ�</para>
        /// <para>ǰ��������</para>
        /// <para>    beginPoint endPoint ��Ϊ��</para>
        /// <para>����������</para>
        /// <para>    �ֶ�m_beginPoint��m_endPoint��������ֵ��</para>
        /// </summary>
        /// <param name="beginPoint">�߶����</param>
        /// <param name="endPoint">�߶��յ�</param>
        /// <returns>
        /// <para></para>
        /// </returns>
        /// <remarks>
        /// <para> ������ 2013-10-08  ��������</para>
        /// </remarks>
        internal StraightLineInPolygon(PointF beginPoint, PointF endPoint)
        {
            m_pathType = PolygonType.StraightLine;
            m_beginPoint = beginPoint;
            m_endPoint = endPoint;
        }
        /// <summary>
        /// <para>��дIPath��������·������ƽ�ơ����ź���ӽ�·�����ϡ�</para>
        /// <para>ǰ��������</para>
        /// <para>    graphicsPath rtu ��Ϊ��</para>
        /// <para>������������</para>
        /// </summary>
        /// <param name="graphicsPath">·������</param>
        /// <param name="rtu">ͼԪ���ڵ��豸����վ�����ṩͼԪ��λ�ơ����ŷ�����</param>
        /// <returns>
        /// <para>����ƽ�ơ����ź��·������ӽ�·������graphicsPath��</para>
        /// </returns>
        /// <remarks>
        /// <para> ������ 2013-10-08  ��������</para>
        /// </remarks>
        internal override sealed void AddPath(ref GraphicsPath graphicsPath)
        {
            PointF pt1 = m_beginPoint;
            PointF pt2 = m_endPoint;

            graphicsPath.AddLine(pt1, pt2);
        }
        /// <summary>
        /// <para>��дIPath��������Ĭ�ϴ�С��·����ӽ�·�����ϡ�</para>
        /// <para>ǰ��������</para>
        /// <para>    graphicsPath ��Ϊ��</para>
        /// <para>������������</para>
        /// </summary>
        /// <param name="graphicsPath">·������</param>
        /// <returns>
        /// <para>·������ӽ�·������graphicsPath��</para>
        /// </returns>
        /// <remarks>
        /// <para> ������ 2013-10-08  ��������</para>
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
                //��������һ���㣬������
            }
        }

        public override IPath ConvertTo(PolygonType newPolygonType)
        {
            switch (newPolygonType)
            {
                case PolygonType.Arc:
                    {
                        //�ݲ�֧��
                        return this;
                    }
                case PolygonType.Bezier:
                    {
                        //�˵㲻��
                        //���ݶ˵����������ʼ�ı��������Ƶ�
                        PointF point3 = new PointF((m_beginPoint.X + m_endPoint.X) / 2, (m_beginPoint.Y + m_endPoint.Y) / 2);
                        PointF point2 = new PointF((m_beginPoint.X + point3.X) / 2, (m_beginPoint.Y + point3.Y) / 2);

                        return new BezierInPolygon(m_beginPoint, point2, point3, m_endPoint);
                    }
                default:
                    {
                        //����Ҫ����
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
                //ֱ��û�п��Ƶ�
                List<PointF> endPoints = new List<PointF>();
                return endPoints;
            }
        }

        public override void MoveControlPoint(PointF pos, Movement move)
        {
            //û�п��Ƶ㣬����Ҫ����
        }
    }
    /// <summary>
    /// <para>����������·������ط�����</para>
    /// </summary>
    internal class BezierInPolygon : IPath
    {
        /// <summary>
        /// <para>�������߱����1</para>
        /// </summary>
        private PointF m_point1;
        /// <summary>
        /// <para>�������߱����2</para>
        /// </summary>
        private PointF m_point2;
        /// <summary>
        /// <para>�������߱����3</para>
        /// </summary>
        private PointF m_point3;
        /// <summary>
        /// <para>�������߱����4</para>
        /// </summary>
        private PointF m_point4;
        /// <summary>
        /// <para>�������������ʼ���ֶΡ�</para>
        /// <para>ǰ��������</para>
        /// <para>    point1 point2 point3 point4 ��Ϊ��</para>
        /// <para>����������</para>
        /// <para>    �ֶ�m_point1��m_point2��m_point3��m_point4��������ֵ��</para>
        /// </summary>
        /// <param name="point1">�������߱����1</param>
        /// <param name="point2">�������߱����2</param>
        /// <param name="point3">�������߱����3</param>
        /// <param name="point4">�������߱����4</param>
        /// <returns>
        /// <para></para>
        /// </returns>
        /// <remarks>
        /// <para> ������ 2013-10-08  ��������</para>
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
        /// <para>��дIPath��������·������ƽ�ơ����ź���ӽ�·�����ϡ�</para>
        /// <para>ǰ��������</para>
        /// <para>    graphicsPath rtu ��Ϊ��</para>
        /// <para>������������</para>
        /// </summary>
        /// <param name="graphicsPath">·������</param>
        /// <param name="rtu">ͼԪ���ڵ��豸����վ�����ṩͼԪ��λ�ơ����ŷ�����</param>
        /// <returns>
        /// <para>����ƽ�ơ����ź��·������ӽ�·������graphicsPath��</para>
        /// </returns>
        /// <remarks>
        /// <para> ������ 2013-10-08  ��������</para>
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
        /// <para>��дIPath��������Ĭ�ϴ�С��·����ӽ�·�����ϡ�</para>
        /// <para>ǰ��������</para>
        /// <para>    graphicsPath ��Ϊ��</para>
        /// <para>������������</para>
        /// </summary>
        /// <param name="graphicsPath">·������</param>
        /// <returns>
        /// <para>·������ӽ�·������graphicsPath��</para>
        /// </returns>
        /// <remarks>
        /// <para> ������ 2013-10-08  ��������</para>
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
                        //��������
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
                //���Ǳ��������ߵ������յ㣬������
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
                        //�ݲ�֧��
                        return this;
                    }
                case PolygonType.StraightLine:
                    {
                        //�˵㲻��
                        return new StraightLineInPolygon(m_point1, m_point4);
                    }
                default:
                    {
                        //����Ҫ����
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
                //�õ㲻�ǿ��Ƶ㣬����Ҫ�ƶ�
            }
        }
    }
    /// <summary>
    /// <para>��Բ��·������ط�����</para>
    /// </summary>
    internal class ArcInPolygon : IPath
    {
        /// <summary>
        /// <para>��ʾ���н�ȡ���ߵ���Բ�ľ��α߽硣</para>
        /// </summary>
        private RectangleF m_rect;
        /// <summary>
        /// <para>���ߵ���ʼ�Ƕȣ��Զ�Ϊ��λ��X��˳ʱ�������</para>
        /// </summary>
        private float m_startAngle;
        /// <summary>
        /// <para>startAngle �ͻ���ĩβ֮��ĽǶ�</para>
        /// </summary>
        private float m_sweepAngle;
        /// <summary>
        /// <para>�������������ʼ���ֶΡ�</para>
        /// <para>ǰ��������</para>
        /// <para>    rect ��Ϊ��</para>
        /// <para>����������</para>
        /// <para>    �ֶ�m_rect��m_startAngle��m_sweepAngle��������ֵ��</para>
        /// </summary>
        /// <param name="rect">��ʾ���н�ȡ���ߵ���Բ�ľ��α߽硣</param>
        /// <param name="startAngle">���ߵ���ʼ�Ƕȣ��Զ�Ϊ��λ��X��˳ʱ�������</param>
        /// <param name="sweepAngle">startAngle �ͻ���ĩβ֮��ĽǶ�</param>
        /// <returns>
        /// <para></para>
        /// </returns>
        /// <remarks>
        /// <para> ������ 2013-10-08  ��������</para>
        /// </remarks>
        internal ArcInPolygon(RectangleF rect, float startAngle, float sweepAngle)
        {
            m_pathType = PolygonType.Arc;
            m_rect = rect;
            m_startAngle = startAngle;
            m_sweepAngle = sweepAngle;
        }
        /// <summary>
        /// <para>��дIPath��������·������ƽ�ơ����ź���ӽ�·�����ϡ�</para>
        /// <para>ǰ��������</para>
        /// <para>    graphicsPath rtu ��Ϊ��</para>
        /// <para>������������</para>
        /// </summary>
        /// <param name="graphicsPath">·������</param>
        /// <param name="rtu">ͼԪ���ڵ��豸����վ�����ṩͼԪ��λ�ơ����ŷ�����</param>
        /// <returns>
        /// <para>����ƽ�ơ����ź��·������ӽ�·������graphicsPath��</para>
        /// </returns>
        /// <remarks>
        /// <para> ������ 2013-10-08  ��������</para>
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
        /// <para>��дIPath��������Ĭ�ϴ�С��·����ӽ�·�����ϡ�</para>
        /// <para>ǰ��������</para>
        /// <para>    graphicsPath ��Ϊ��</para>
        /// <para>������������</para>
        /// </summary>
        /// <param name="graphicsPath">·������</param>
        /// <returns>
        /// <para>·������ӽ�·������graphicsPath��</para>
        /// </returns>
        /// <remarks>
        /// <para> ������ 2013-10-08  ��������</para>
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
                        //��������
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
                //�ݲ�֧��
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
