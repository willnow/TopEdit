using System;
using System.Collections.Generic;
using System.Text;
using TopoEdit.Icon;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using TopoEdit.Command;

namespace TopoEdit.EventHandler
{
    class MoveSelectedIconHandler : IRangeEventHandler
    {
        private static readonly Keys NotDirKey = Keys.Attn;
        /// <summary>
        /// 上一次按下的方向键，初始化为一个非方向键（表示无效值）
        /// </summary>
        Keys m_lastKey = NotDirKey;
        /// <summary>
        /// 元素的平移向量
        /// </summary>
        Movement m_move = new Movement();

        internal override void KeyDown(object sender, RangeKeyEventArgs e)
        {
            Trace.WriteLine(e.KeyData.ToString());

            if (!e.IsMouseDown && (e.MouseSelectedIcon != null) && (e.MouseAnchor == null))
            {
                TopoEdit.Icon.Movement move = new TopoEdit.Icon.Movement();
                int adjustLen = 0;

                adjustLen = 1;

                if (IsDirKey(e.KeyData))
                {
                    if (e.KeyData == Keys.Right)
                    {
                        move.XMovement = adjustLen;
                    }
                    else if (e.KeyData == Keys.Left)
                    {
                        move.XMovement = -adjustLen;
                    }
                    else if (e.KeyData == Keys.Up)
                    {
                        move.YMovement = -adjustLen;
                    }
                    else if (e.KeyData == Keys.Down)
                    {
                        move.YMovement = adjustLen;
                    }

                    m_lastKey = e.KeyData;
                    MoveSelectRange(e.RangeData, e.RangeView, move);
                    m_move.XMovement += move.XMovement;
                    m_move.YMovement += move.YMovement;
                }
                else
                {
                    //方向键没有按下
                }
            }
        }

        private bool IsDirKey(Keys key)
        {
            return (key == Keys.Right) || (key == Keys.Left) || (key == Keys.Up) || (key == Keys.Down);
        }

        internal override void KeyUp(object sender, RangeKeyEventArgs e)
        {
            if (m_lastKey == NotDirKey)
            {
                //上一次按下的不是方向键，不用快照
            }
            else
            {
                if ((m_move.YMovement == 0) && (m_move.XMovement == 0))
                {
                    //没有平移，不用添加平移命令
                }
                else
                {
                    //构建批量平移命令
                    ComplexCommand command = new ComplexCommand();
                    command.Tip = "批量平移命令";
                    //将批量移动被选图元命令看做由很多移动单个被选图元命令组成
                    for (int i = 0; i < e.MouseSelectedIcon.Count; ++i)
                    {
                        command.AddCommand(new MoveDrawCommand(e.RangeView, (e.MouseSelectedIcon.SelIcons[i] as SelectedDraw), m_move));
                    }
                    e.RangeView.CmdMgr.AddCommand(command);//由于移动过程已经执行，因此此处仅添加该命令，但不重复执行
                }

                m_lastKey = NotDirKey;
                m_move.XMovement = 0;
                m_move.YMovement = 0;
            }
        }

        internal override void MouseMove(object sender, RangeMouseEventArgs e)
        {
            if (e.DownToopStripButton.Count > 0)
            {
                return;
            }

            if (!e.IsCtrlDown && e.IsMouseDown && (e.MouseSelectedIcon != null) && (e.MouseAnchor == null))
            {
                //ctrl没有按下，且鼠标拖动，且有元素被选中，且没有单击在锚点上，则移动被选元素
                TopoEdit.Icon.Movement move = new TopoEdit.Icon.Movement();
                move.XMovement = e.Location.X - e.MouseLastPos.X;
                move.YMovement = e.Location.Y - e.MouseLastPos.Y;

                MoveSelectRange(e.RangeData, e.RangeView, move);

                e.RangeView.Cursor = Cursors.SizeAll;

                m_move.XMovement += move.XMovement;
                m_move.YMovement += move.YMovement;
            }
        }

        internal override void MouseUp(object sender, RangeMouseEventArgs e)
        {
            if (!e.IsCtrlDown && e.IsMouseDown && (e.MouseSelectedIcon != null) && (e.MouseAnchor == null))
            {
                if ((m_move.YMovement == 0) && (m_move.XMovement == 0))
                {
                    //没有平移，不用添加平移命令
                }
                else
                {
                    //构建批量平移命令
                    ComplexCommand command = new ComplexCommand();
                    command.Tip = "批量平移命令";
                    //将批量移动被选图元命令看做由很多移动单个被选图元命令组成
                    for (int i = 0; i < e.MouseSelectedIcon.Count; ++i)
                    {
                        command.AddCommand(new MoveDrawCommand(e.RangeView, (e.MouseSelectedIcon.SelIcons[i] as SelectedDraw), m_move));
                    }
                    e.RangeView.CmdMgr.AddCommand(command);//由于移动过程已经执行，因此此处仅添加该命令，但不重复执行
                }

                m_move.XMovement = 0;
                m_move.YMovement = 0;
            }
        }

        void MoveSelectRange(IRange range, IBaseDrawPanel panel, TopoEdit.Icon.Movement move)
        {
            RectangleF rect = range.MoveSelectIcon(move);
            if (rect.IsEmpty)
            {
                //没有被选中图标，不需要刷新
            }
            else
            {
                panel.Refresh(Utility.AdjustRect(rect, 10));
            }
        }
    }
}
