using System;
using System.Collections.Generic;
using System.Text;
using TopoEdit.Icon;
using TopoEdit.Command;

namespace TopoEdit.Stratege
{
    /// <summary>
    /// 水平对齐(左或右)策略
    /// </summary>
    class AlignLeftStrategy : IAlignStrategy
    {
        /// <summary>
        /// 左对齐坐标
        /// </summary>
        private float m_xPos = 0;

        /// <summary>
        /// 设置做对齐的坐标
        /// </summary>
        /// <param name="xPos"></param>
        public void SetRef(float posRef)
        {
            m_xPos = posRef;
        }

        #region IAlignStrategy 成员

        public void Align(IBaseDrawPanel view, SelectedRange range)
        {
            ComplexCommand command = new ComplexCommand();
            command.Tip = "批量左对齐命令";
            Movement move = new Movement();
            //移动所有图元的左端点到指定位置
            foreach (IDraw draw in range.SelIcons)
            {
                move.XMovement = m_xPos - draw.BoundsRect.Left;
                command.AddCommand(new MoveDrawCommand(view, draw, move));
            }

            view.CmdMgr.AddThenExec(command);
        }

        #endregion
    }
}
