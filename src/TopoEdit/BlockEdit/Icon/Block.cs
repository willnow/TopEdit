using System;
using System.Collections.Generic;
using System.Text;
using TopoEdit.Icon;
using System.Drawing;
using System.Xml;
using System.Diagnostics;

namespace TopoEdit.Icon
{
    public class Block : IRange
    {
        public Block(string name)
        {
            Name = name;
        }

        public override bool Load(System.Xml.XmlNode iconNode)
        {
            m_draws.Clear();
            foreach (XmlNode node in iconNode)
            {
                IIcon icon = null;
                XmlNode nodeType = node.SelectSingleNode("Circle");
                if (nodeType != null)
                {
                    //这是一个圆
                    icon = IconFactory.Instance.CreateIcon(TopoEdit.Icon.EmIconType.Circle, node);
                }

                nodeType = node.SelectSingleNode("Line");
                if (nodeType != null)
                {
                    //这是一条线
                    icon = IconFactory.Instance.CreateIcon(TopoEdit.Icon.EmIconType.Line, node);
                }

                nodeType = node.SelectSingleNode("Rect");
                if (nodeType != null)
                {
                    //这是一个矩形
                    icon = IconFactory.Instance.CreateIcon(TopoEdit.Icon.EmIconType.Rectangle, node);
                }

                nodeType = node.SelectSingleNode("Polygon");
                if (nodeType != null)
                {
                    //这是一个多边形
                    icon = IconFactory.Instance.CreateIcon(TopoEdit.Icon.EmIconType.Polygon, node);
                }

                nodeType = node.SelectSingleNode("Text");
                if (nodeType != null)
                {
                    //这是一个文字
                    icon = IconFactory.Instance.CreateIcon(TopoEdit.Icon.EmIconType.Text, node);
                }

                if (null != icon)
                {
                    m_draws.Add(icon);
                }
            }

            m_draws.Sort();//按level从小到大排序,level越大越靠上层
            return true;
        }

        public override bool Save(System.Xml.XmlNode iconParentNode)
        {
            ClearSelectIcon();

            bool isSave = true;

            //保存名字
            System.Xml.XmlNode node = iconParentNode.OwnerDocument.CreateElement("Name");
            node.InnerXml = Name;
            iconParentNode.AppendChild(node);

            foreach (IDraw icon in m_draws)
            {
                if (!icon.Save(iconParentNode))
                {
                    isSave = false;
                }
            }

            return isSave;
        }

        public override IDraw Clone()
        {
            Block block = new Block(this.Name);

            block.Name = this.Name;

            foreach (IDraw icon in m_draws)
            {
                block.Add(icon.Clone());
            }

            return block;
        }

        public override void Copy(IDraw src)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 将BLOCK平移并缩放到指定矩形框中
        /// </summary>
        internal void Reset(Point destRectCenter, int destRectLen)
        {
            if (BoundsRect.Width <= 0 || BoundsRect.Height <= 0)
            {
                //无法绘制BLOCK
                return;
            }

            //恢复Block到正常大小
            //将BLOCK缩放到合适大小
            TopoEdit.Icon.Zoom zoom = TopoEdit.Icon.Zoom.Create(BoundsRect, destRectLen);
            Zoom(zoom);

            //将BLOCK平移到画板中心
            TopoEdit.Icon.Movement move = TopoEdit.Icon.Movement.Create(BoundsRect, destRectCenter);
            Move(move);
        }

        public override void Accept(TopoEdit.Visitor.IDrawVisitor visitor)
        {
            visitor.VisitorBlock(this);
        }
    }
}
