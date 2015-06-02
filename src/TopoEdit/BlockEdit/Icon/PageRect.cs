using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Xml;
using System.Windows.Forms;
using System.Drawing;
using TopoEdit.EventHandler;

namespace TopoEdit.Icon
{
    /// <summary>
    /// 页实例，父容器为book
    /// </summary>
    public class PageRect : RangeRect
    {
        public PageRect(Book book) : base(book)
        {
            Debug.Assert(Parent != null);
        }

        /// <summary>
        /// 初始化Range实例
        /// </summary>
        /// <param name="name">Range实例名称</param>
        /// <param name="blockName">模板名称</param>
        /// <param name="pos">模板实例的左上角坐标</param>
        public override void Init(string name, IRange template, PointF pos)
        {
            base.Init(name, template, pos);
            ResetTemplate();
        }

        /// <summary>
        /// 根据新模板重新刷新模板实例
        /// </summary>
        public override void ResetTemplate()
        {
            //创建Page
            Page page = PageContainer.Instance.GetPageByName(m_templateName);
            if (page != null)
            {
                m_templateCache = (Page)page.Clone();
                m_templateCache.ClearSelectIcon();//清除选中状态

                //1.缩放
                Zoom zoom = new Zoom();
                zoom.ZoomMode = CSR.CUIT.GlobalService.ShareLib.EmZoomMode.UniformScale;
                zoom.SubMode = ScaleOpMode.None;

                if (Parent != null)
                {
                    zoom.XRadio = (Parent as Book).RangeZoom;
                }
                else
                {
                    zoom.XRadio = Book.DefaultRangeZoom;
                }

                zoom.YRadio = zoom.XRadio;

                m_templateCache.Zoom(zoom);

                Movement move = new Movement();

                //将page平移到指定点
                move.SubMode = ScaleOpMode.None;
                //page原始左上角点
                PointF blockSrcLoc = m_templateCache.BoundsRect.Location;
                //page目标左上角点
                PointF blockDestLoc = m_rect.BoundsRect.Location;
                //计算从原始中心点平移到目标中心点的平移向量
                move.XMovement = blockDestLoc.X - blockSrcLoc.X;
                move.YMovement = blockDestLoc.Y - blockSrcLoc.Y;
                m_templateCache.Move(move);

                ResetRectange();
            }
            else
            {
                if (m_templateCache != null)
                {
                    Remove(m_templateCache);
                    m_templateCache = null;
                }
                else
                {
                    //空模板，不需要处理
                }
            }
        }

        public override bool Load(XmlNode node)
        {
            Clear();

            Name = node.SelectSingleNode("Name").InnerText;
            m_templateName = node.SelectSingleNode("PageName").InnerText;
            m_rect.Position = new PointF(float.Parse(node.SelectSingleNode("Pos/X").InnerText), float.Parse(node.SelectSingleNode("Pos/Y").InnerText));
            m_rect.Width = float.Parse(node.SelectSingleNode("Width").InnerText);
            m_rect.Height = float.Parse(node.SelectSingleNode("Height").InnerText);

            //暂时不需要使用这两个字段
            m_templateCenter.X = 0;// float.Parse(node.SelectSingleNode("PageCenter/X").InnerText);
            m_templateCenter.Y = 0;// float.Parse(node.SelectSingleNode("PageCenter/Y").InnerText);

            Debug.Assert((m_templateCenter.X >= 0) && (m_templateCenter.Y >= 0));

            if ((m_templateCenter.X >= 0) && (m_templateCenter.Y >= 0))
            {
                //数据正确
            }
            else
            {
                //数据错误
                MessageBox.Show("PageRect" + Name + "的PageCenter字段包含有负数", "PageCenter字段错误");
            }

            ResetTemplate();

            Add(m_rect);

            if (m_templateCache != null)
            {
                Add(m_templateCache);
            }

            return true;
        }

        public override bool Save(XmlNode iconParentNode)
        {
            m_rect.Round();
            m_templateCenter = Utility.ConvertPos(m_templateCenter);

            System.Xml.XmlNode iconTemplateRect = iconParentNode.OwnerDocument.CreateElement("PageRect");
            iconTemplateRect.InnerXml = "<Name>" + Name + "</Name>" +
                                      "<PageName>" + m_templateName + "</PageName>" +
                                      "<Pos><X>" + m_rect.BoundsRect.Left + "</X><Y>" + m_rect.BoundsRect.Top + "</Y></Pos>" +
                                      "<Width>" + m_rect.BoundsRect.Width + "</Width>" +
                                      "<Height>" + m_rect.BoundsRect.Height + "</Height>" +
                                      "<PageCenter><X>" + m_templateCenter.X + "</X><Y>" + m_templateCenter.Y + "</Y></PageCenter>";

            iconParentNode.AppendChild(iconTemplateRect);

            return true;
        }

        public override void Zoom(Zoom zoom)
        {
            if (zoom.SubMode == ScaleOpMode.None)
            {
                m_rect.Zoom(zoom);
                if (m_templateCache != null)
                {
                    m_templateCache.Zoom(zoom);
                }
            }
        }

        public override void Rotate(CSR.ShareLib.Rotate rotate)
        {
            if (m_templateCache != null)
            {
                m_templateCache.Rotate(rotate);
            }
            ResetRectange();
        }

        /// <summary>
        /// <para>根据被选中的图元类型创建被选中对象</para>
        /// </summary>
        public override SelectedDraw CreateSelectedDraw()
        {
            return new SelectedPageRect(this);
        }

        public override void Accept(TopoEdit.Visitor.IDrawVisitor visitor)
        {
            throw new NotImplementedException();
        }

        public override IDraw Clone()
        {
            PageRect rect = new PageRect(Parent as Book);
            rect.Copy(this);
            return rect;
        }

        public override void Copy(IDraw src)
        {
            if (src is PageRect)
            {
                PageRect srcItem = src as PageRect;

                this.Parent = srcItem.Parent;
                this.Init(srcItem.Name, PageContainer.Instance.GetPageByName(srcItem.TemplateName), srcItem.m_rect.Position);
                this.m_templateCenter = srcItem.m_templateCenter;
                this.m_rect = (IconRectangle)srcItem.m_rect.Clone();
                this.ResetTemplate();
            }
            else
            {
                throw new ArgumentException("被拷贝图元和目标图元类型不兼容", "src");
            }
        }
    }
}
