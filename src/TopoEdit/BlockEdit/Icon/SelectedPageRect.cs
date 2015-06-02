using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Diagnostics;

namespace TopoEdit.Icon
{
    public class SelectedPageRect : SelectedDraw
    {
        public SelectedPageRect(IDraw icon)
            : base(icon)
        {
            InitAnchor();
        }

        public override void InitAnchor()
        {
            AnchorIcons.Clear();

            //根据BLOCK中包含图元的DOCK类型，确定锚点类型
            Debug.Assert(this.Icon is RangeRect);
            IRange range = (this.Icon as RangeRect).Template as IRange;

            //if (range != null)
            //{
            //    //Left
            //    if (range.Dock[DockType.Left])
            //    {
            //        AddAnchor(new Anchor(this, AnchorType.LeftTop));
            //    }

            //    //Top
            //    if (range.Dock[DockType.Top])
            //    {
            //        AddAnchor(new Anchor(this, AnchorType.RightTop));
            //    }

            //    //Right
            //    if (range.Dock[DockType.Right])
            //    {
            //        AddAnchor(new Anchor(this, AnchorType.RightBottom));
            //    }

            //    //Bottom
            //    if (range.Dock[DockType.Bottom])
            //    {
            //        AddAnchor(new Anchor(this, AnchorType.LeftBottom));
            //    }
            //}

            //ResetAnchor();
        }

        /// <summary>
        /// 根据ICON初始化锚点
        /// </summary>
        public override void ResetAnchor()
        {
            foreach (Anchor anchor in AnchorIcons)
            {
                //根据锚点类型计算锚点移动向量
                TopoEdit.Icon.Movement move = TopoEdit.Icon.Movement.Create(anchor.BoundsRect, anchor.GetAnchorCenter());
                //移动锚点到指定位置
                anchor.Move(move);
            }
        }


        public override void Draw(System.Drawing.Graphics cGraphics, RectangleF rect)
        {
            //绘制虚线矩形边框
            Debug.Assert(this.Icon is RangeRect);
            IconRectangle iconRectange = new IconRectangle(Icon.BoundsRect.Location, Icon.BoundsRect.Width
                , Icon.BoundsRect.Height, false, System.Drawing.Drawing2D.DashStyle.DashDot);

            iconRectange.Draw(cGraphics, rect);
            //绘制原始图元和锚点
            base.Draw(cGraphics, rect);
        }

        public override void Accept(TopoEdit.Visitor.IDrawVisitor visitor)
        {
            visitor.VisitorSelectedPageRect(this);
        }
    }
}
