using System;
using System.Collections.Generic;
using System.Text;

namespace TopoEdit.Icon
{
    /// <summary>
    /// 被选中的多边形
    /// </summary>
    public class SelectedPolygon : SelectedItem
    {
        public SelectedPolygon(IconPolygon draw)
            : base(draw)
        {
            InitAnchor();
        }

        private IconPolygon Polygon
        {
            get
            {
                return base.Icon as IconPolygon;
            }
        }

        public override void InitAnchor()
        {
            //将端点作为锚点
            AnchorIcons.Clear();

            for (int i = 0; i < Polygon.PathCnt; ++i)
            {
                AddAnchor(new AnchorEndPoint(this, AnchorType.EndPoint, i));

                
                if (Polygon.Paths[i] is BezierInPolygon)
                {
                    //如果是贝塞尔曲线，则还需要增加控制点锚点
                    AddAnchor(new AnchorControlPoint(this, AnchorType.Control, i, 0));//对应贝塞尔控制点1
                    AddAnchor(new AnchorControlPoint(this, AnchorType.Control, i, 1));//对应贝塞尔控制点2
                }
            }
            
            ResetAnchor();
        }

        public override void ResetAnchor()
        {
            foreach (Anchor anchor in AnchorIcons)
            {
                if (anchor is AnchorEndPoint)
                {
                    AnchorEndPoint endPointAnchor = anchor as AnchorEndPoint;
                    //根据锚点类型计算锚点移动向量
                    TopoEdit.Icon.Movement move = TopoEdit.Icon.Movement.Create(anchor.BoundsRect, Polygon.GetEndPoint(endPointAnchor.Index));
                    //移动锚点到指定位置
                    anchor.Move(move);
                }
                else if (anchor is AnchorControlPoint)
                {
                    //控制点
                    AnchorControlPoint controlPointAnchor = anchor as AnchorControlPoint;
                    //根据锚点类型计算锚点移动向量
                    TopoEdit.Icon.Movement move = TopoEdit.Icon.Movement.Create(anchor.BoundsRect
                        , Polygon.GetControlPoint(controlPointAnchor.PathIndex, controlPointAnchor.ControlIndex));
                    //移动锚点到指定位置
                    anchor.Move(move);
                }
            }
        }

        public override void Accept(TopoEdit.Visitor.IDrawVisitor visitor)
        {
            visitor.VisitorSelectedPolygon(this);
        }
    }
}
