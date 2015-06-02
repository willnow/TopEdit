using System;
using System.Collections.Generic;
using System.Text;
using TopoEdit.Model;
using System.Drawing;

namespace TopoEdit.Icon
{
    /// <summary>
    /// 控制点锚点
    /// </summary>
    class AnchorControlPoint : Anchor
    {
        /// <summary>
        /// 端点在关联图元上的路径索引
        /// </summary>
        private int m_pathIndex = 0;
        /// <summary>
        /// 端点关联路径上的控制点索引
        /// </summary>
        private int m_controlIndex = 0;

        public int PathIndex
        {
            get
            {
                return m_pathIndex;
            }
        }

        public int ControlIndex
        {
            get
            {
                return m_controlIndex;
            }
        }

        public  AnchorControlPoint(SelectedDraw draw, AnchorType anchorType, int pathIndex, int controlIndex)
            : base(draw, anchorType)
        {
            //Rect.DefaultColor.ColorInArgb = Color.Yellow;
            m_pathIndex = pathIndex;
            m_controlIndex = controlIndex;
            Rect.Degree = 45;
        }
    }
}
