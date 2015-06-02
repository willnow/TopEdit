using System;
using System.Collections.Generic;
using System.Text;
using TopoEdit.Icon;
using System.Drawing;

namespace TopoEdit.Icon
{
    public class SelectedItem : SelectedDraw
    {
        public SelectedItem(IDraw icon)
            : base(icon)
        {
            InitAnchor();
        }

        public override void InitAnchor()
        {
            AnchorIcons.Clear();

            //LeftTop
            AddAnchor(new Anchor(this, AnchorType.LeftTop));
            //RightTop
            AddAnchor(new Anchor(this, AnchorType.RightTop));
            //LeftBottom
            AddAnchor(new Anchor(this, AnchorType.LeftBottom));
            //RightBottom
            AddAnchor(new Anchor(this, AnchorType.RightBottom));
            //Left
            AddAnchor(new Anchor(this, AnchorType.Left));
            //Top
            AddAnchor(new Anchor(this, AnchorType.Top));
            //Right
            AddAnchor(new Anchor(this, AnchorType.Right));
            //Bottom
            AddAnchor(new Anchor(this, AnchorType.Bottom));

            ResetAnchor();
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

        public override void Accept(TopoEdit.Visitor.IDrawVisitor visitor)
        {
            visitor.VisitorSelectedItem(this);
        }
    }
}
