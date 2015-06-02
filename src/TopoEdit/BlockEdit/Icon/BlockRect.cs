using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using TopoEdit.Visitor;
using System.Diagnostics;
using TopoEdit.EventHandler;
using System.Xml;
using System.Windows.Forms;

namespace TopoEdit.Icon
{
    public class BlockRect : RangeRect
    {
        /// <summary>
        /// 如果BLOCK实例是一个仅包含一个文本图元的实例，则需要使用该字段对文本内容实例化
        /// </summary>
        private string m_text = "";

        public string Text
        {
            get { return m_text; }
            set
            {
                m_text = value;
                ResetBlockText();
            }
        }

        public BlockRect(Page parent)
            : base(parent)
        {
            Debug.Assert(Parent != null);
        }

        /// <summary>
        /// 将BLOCK缩放到和Rect一样大
        /// </summary>
        public override void ResetTemplate()
        {
            //创建Block实例
            Block block = BlockContainer.Instance.GetBlockByName(m_templateName);
            if (block != null)
            {
                m_templateCache = (Block)block.Clone();
                m_templateCache.ClearSelectIcon();//清除选中状态

                //1.缩放
                Zoom zoom = new Zoom();
                zoom.ZoomMode = CSR.CUIT.GlobalService.ShareLib.EmZoomMode.UniformScale;
                zoom.SubMode = ScaleOpMode.None;

                if (Parent != null)
                {
                    zoom.XRadio = (Parent as Page).RangeZoom;
                }
                else
                {
                    zoom.XRadio = Page.DefaultRangeZoom;
                }
                zoom.YRadio = zoom.XRadio;
                m_templateCache.Zoom(zoom);

                Movement move = new Movement();

                //将block平移到指定中心点
                move.SubMode = ScaleOpMode.None;
                //block原始中心点
                PointF blockSrcCenter = Utility.GetCenter(m_templateCache.BoundsRect);
                //block目标中心点
                PointF blockDestCenter = new PointF();
                blockDestCenter.X = m_rect.BoundsRect.Left + m_templateCenter.X;
                blockDestCenter.Y = m_rect.BoundsRect.Top + m_templateCenter.Y;
                //计算从原始中心点平移到目标中心点的平移向量
                move.XMovement = blockDestCenter.X - blockSrcCenter.X;
                move.YMovement = blockDestCenter.Y - blockSrcCenter.Y;
                m_templateCache.Move(move);

                //拉伸时不需要缩放向量
                Zoom zoomForScale = new Zoom();

                //向左拉伸
                move.SubMode = ScaleOpMode.Left;
                zoomForScale.SubMode = move.SubMode;
                move.XMovement = m_rect.BoundsRect.Left - m_templateCache.BoundsRect.Left;
                move.YMovement = 0;

                IDrawVisitor visitor = null;

                visitor = new ZoomAndMoveSelBlockRectInPageByAnchorVisitor(zoomForScale, move);
                m_templateCache.Accept(visitor);

                //向右拉伸
                move.SubMode = ScaleOpMode.Right;
                zoomForScale.SubMode = move.SubMode;
                move.XMovement = m_rect.BoundsRect.Right - m_templateCache.BoundsRect.Right;
                move.YMovement = 0;
                visitor = new ZoomAndMoveSelBlockRectInPageByAnchorVisitor(zoomForScale, move);
                m_templateCache.Accept(visitor);

                //向上拉伸
                move.SubMode = ScaleOpMode.Up;
                zoomForScale.SubMode = move.SubMode;
                move.XMovement = 0;
                move.YMovement = m_rect.BoundsRect.Top - m_templateCache.BoundsRect.Top;
                visitor = new ZoomAndMoveSelBlockRectInPageByAnchorVisitor(zoomForScale, move);
                m_templateCache.Accept(visitor);

                //向下拉伸
                move.SubMode = ScaleOpMode.Down;
                zoomForScale.SubMode = move.SubMode;
                move.XMovement = 0;
                move.YMovement = m_rect.BoundsRect.Bottom - m_templateCache.BoundsRect.Bottom;
                visitor = new ZoomAndMoveSelBlockRectInPageByAnchorVisitor(zoomForScale, move);
                m_templateCache.Accept(visitor);

                //如果BLOCK实例仅包含一个文本图元，则使用文本实例化文本内容
                ResetBlockText();
            }
            else
            {
                Remove(m_templateCache);
                m_templateCache = null;
            }
        }

        private void ResetBlockText()
        {
            if ((m_templateCache != null) && (m_templateCache.Count == 1) && (m_templateCache.GetIcon(0) is IconText) && (m_text.Trim() != ""))
            {
                (m_templateCache.GetIcon(0) as IconText).Text = m_text;
                Name = m_text;
            }
        }

        public override void Draw(Graphics cGraphics, RectangleF cRect)
        {
            if (cRect.IsEmpty || BoundsRect.IntersectsWith(cRect))
            {
                if (m_templateCache != null)
                {
                    //绘制BLOCK
                    m_templateCache.Draw(cGraphics, cRect);
                }
                else
                {
                    //绘制矩形框
                    m_rect.Draw(cGraphics, cRect);
                }
            }
        }

        public override bool Load(System.Xml.XmlNode node)
        {
            Clear();

            Name = node.SelectSingleNode("Name").InnerText;
            m_templateName = node.SelectSingleNode("BlockName").InnerText;
            m_rect.Position = new PointF(float.Parse(node.SelectSingleNode("Pos/X").InnerText), float.Parse(node.SelectSingleNode("Pos/Y").InnerText));
            m_rect.Width = float.Parse(node.SelectSingleNode("Width").InnerText);
            m_rect.Height = float.Parse(node.SelectSingleNode("Height").InnerText);
            m_templateCenter.X = float.Parse(node.SelectSingleNode("BlockCenter/X").InnerText);
            m_templateCenter.Y = float.Parse(node.SelectSingleNode("BlockCenter/Y").InnerText);

            XmlNode textNode = node.SelectSingleNode("Text");
            if (textNode != null)
            {
                m_text = textNode.InnerText;
            }
            else
            {
                m_text = "";
            }

            Debug.Assert((m_templateCenter.X >= 0) && (m_templateCenter.Y >= 0));

            if ((m_templateCenter.X >= 0) && (m_templateCenter.Y >= 0))
            {
                //数据正确
            }
            else
            {
                //数据错误
                //MessageBox.Show("BlockRect" + Name + "的BlockCenter字段包含有负数", "BlockCenter字段错误");
            }

            ResetTemplate();

            Add(m_rect);

            if (m_templateCache != null)
            {
                Add(m_templateCache);
            }

            return true;
        }

        public override bool Save(System.Xml.XmlNode iconParentNode)
        {
            m_rect.Round();
            m_templateCenter = Utility.ConvertPos(m_templateCenter);

            System.Xml.XmlNode iconBlockRect = iconParentNode.OwnerDocument.CreateElement("BlockRect");
            iconBlockRect.InnerXml = "<Name>" + Name + "</Name>" +
                                      "<BlockName>" + m_templateName + "</BlockName>" +
                                      "<Pos><X>" + m_rect.BoundsRect.Left + "</X><Y>" + m_rect.BoundsRect.Top + "</Y></Pos>" +
                                      "<Width>" + m_rect.BoundsRect.Width + "</Width>" +
                                      "<Height>" + m_rect.BoundsRect.Height + "</Height>" +
                                      "<BlockCenter><X>" + m_templateCenter.X + "</X><Y>" + m_templateCenter.Y + "</Y></BlockCenter>";

            if (m_text.Trim() != "")
            {
                iconBlockRect.InnerXml += "<Text>" + m_text + "</Text>";
            }

            iconParentNode.AppendChild(iconBlockRect);

            return true;
        }

        public override void Zoom(Zoom zoom)
        {
            if (m_templateCache != null)
            {
                m_templateCache.Zoom(zoom);
            }

            PointF srcRectPos = m_rect.Position;
            m_rect.Zoom(zoom);
            PointF destRectPos = m_rect.Position;
            if (zoom.SubMode != ScaleOpMode.None)
            {
                //拉伸时才需要重新计算center
                CalcCenter(srcRectPos, destRectPos);
            }
        }

        public override void Rotate(CSR.ShareLib.Rotate rotate)
        {
            if (m_templateCache != null)
            {
                m_templateCache.Rotate(rotate);
            }

            PointF srcRectPos = m_rect.Position;
            m_rect.Rotate(rotate);
            PointF destRectPos = m_rect.Position;
            if (rotate.SubMode != ScaleOpMode.None)
            {
                //拉伸时才需要重新计算center
                CalcCenter(srcRectPos, destRectPos);
            }
        }

        public override IDraw Intersect(Point point)
        {
            if (this.IntersectIconType == IntersectType.InBound)
            {
                if (null != m_rect.Intersect(point))
                {
                    return this;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                if ((null != m_templateCache) && (null != m_templateCache.Intersect(point)))
                {
                    return this;
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// <para>根据被选中的图元类型创建被选中对象</para>
        /// </summary>
        public override SelectedDraw CreateSelectedDraw()
        {
            return new SelectedBlockRect(this);
        }

        public override IDraw Clone()
        {
            BlockRect rect = new BlockRect(Parent as Page);
            rect.Copy(this);
            return rect;
        }

        public override void Copy(IDraw src)
        {
            if (src is BlockRect)
            {
                BlockRect srcItem = src as BlockRect;

                this.Init(srcItem.Name, BlockContainer.Instance.GetBlockByName(srcItem.TemplateName), srcItem.m_rect.Position);
                this.Text = srcItem.Text;
                this.m_templateCenter = srcItem.m_templateCenter;
                this.m_rect = (IconRectangle)srcItem.m_rect.Clone();
                this.ResetTemplate();
            }
            else
            {
                throw new ArgumentException("被拷贝图元和目标图元类型不兼容", "src");
            }
        }

        public override void Accept(TopoEdit.Visitor.IDrawVisitor visitor)
        {
            visitor.VisitorBlockRect(this);
        }
    }
}
