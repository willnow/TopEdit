using System;
using System.Collections.Generic;
using System.Text;
using TopoEdit.Icon;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using TopoEdit.Command;
using TopoEdit.Visitor;

namespace TopoEdit.EventHandler
{
    enum CopyMode
    {
        CtrlAltMove,///mouse down -> ctrl + alt + mouse move 直线对称拷贝（已被拷贝图元边框的中心垂直线为对称线）
        CtrlMove,//mouse down -> ctrl + mouse move
        CtrlCV,//ctrl + c -> ctrl + v
        None,
    }

    class CopyIconEventHandler : IRangeEventHandler
    {
        /// <summary>
        /// 拷贝的区域
        /// </summary>
        private IRange m_copyRange = null;
        private CopyMode m_mode = CopyMode.None;

        internal override void MouseDown(object sender, RangeMouseEventArgs e)
        {
            if ((m_mode == CopyMode.CtrlMove) || (m_mode == CopyMode.None) || (m_mode == CopyMode.CtrlAltMove))
            {
                m_copyRange = null;
            }
            else if (m_mode == CopyMode.CtrlCV)
            {
                //保留拷贝内容
            }
            else
            {
                //考虑了所有情况，肯定不会进入
                Debug.Assert(false);
            }
        }

        internal override void MouseMove(object sender, RangeMouseEventArgs e)
        {
            if (e.DownToopStripButton.Count > 0)
            {
                return;
            }
            
            if (e.IsCtrlDown && e.IsMouseDown && (e.Location.X != e.MouseDownPos.X) && (e.Location.Y != e.MouseDownPos.Y))
            {
                if (m_copyRange == null)
                {
                    if (e.MouseSelectedIcon != null)
                    {
                        //当前没有拷贝，且ctrl按下，且鼠标按下拖动

                        //拷贝当前选中元素，并将拷贝后元素设置为选中元素
                        if (m_mode == CopyMode.CtrlAltMove)
                        {
                            m_copyRange = (e.RangeData.SelectedRange.Clone() as IRange);
                            
                            RectangleF bound = m_copyRange.BoundsRect;
                            PointF point1 = new PointF(bound.Left + bound.Width / 2, bound.Top);
                            PointF point2 = new PointF(point1.X, bound.Bottom);
                            Symmetry symm = new Symmetry(point1, point2);
                            m_copyRange.Symmetry(symm);
                        }
                        else
                        {
                            m_copyRange = (e.RangeData.SelectedRange.Clone() as IRange);
                        }

                        Movement move = new Movement();
                        move.XMovement = e.Location.X - e.MouseDownPos.X;
                        move.YMovement = e.Location.Y - e.MouseDownPos.Y;

                        e.RangeData.ClearSelectIcon();

                        //构建添加命令
                        e.RangeView.CmdMgr.AddThenExec(new AddDrawCommand(e.RangeView, e.RangeData, m_copyRange.Icons));

                        //拷贝后赋新值，前置条件：拷贝元素必须添加到容器中
                        SetNewNameAfterCopyVisitor setNewNameVisitor = new SetNewNameAfterCopyVisitor();
                        m_copyRange.Accept(setNewNameVisitor);

                        //移动并刷新视图
                        MoveCopyDraw(move, e.RangeView);

                        e.RangeView.Cursor = System.Windows.Forms.Cursors.Default;

                        m_mode = CopyMode.CtrlMove;
                    }
                    else
                    {
                        //没有被选图元，无法拷贝
                    }
                }
                else
                {
                    //移动该图元
                    TopoEdit.Icon.Movement move = new TopoEdit.Icon.Movement();
                    move.XMovement = e.Location.X - e.MouseLastPos.X;
                    move.YMovement = e.Location.Y - e.MouseLastPos.Y;

                    MoveCopyDraw(move, e.RangeView);
                }
            }
            else
            {
                //不处理
            }
        }

        //ctrl+c ->  ctrl +v 的拷贝方式
        internal override void KeyDown(object sender, RangeKeyEventArgs e)
        {
            if (e.DownToopStripButton.Count > 0)
            {
                return;
            }

            if (e.IsCtrlDown && e.KeyCode == Keys.C)
            {
                //按下了ctrl+c
                if (m_copyRange == null)
                {
                    //拷贝当前选中元素
                    m_copyRange = (e.RangeData.SelectedRange.Clone() as IRange);
                    m_mode = CopyMode.CtrlCV;
                }
            }
            else if (e.IsCtrlDown && e.KeyCode == Keys.V)
            {
                if (m_copyRange != null)
                {
                    //被拷贝图元的位置
                    PointF center = Utility.GetCenter(m_copyRange.BoundsRect);

                    TopoEdit.Icon.Movement move = new TopoEdit.Icon.Movement();
                    move.XMovement = e.MouseLastPos.X - center.X;
                    move.YMovement = e.MouseLastPos.Y - center.Y;

                    e.RangeData.ClearSelectIcon();
                    e.RangeData.AddRange(m_copyRange.Icons);

                    //拷贝后赋新值
                    SetNewNameAfterCopyVisitor setNewNameVisitor = new SetNewNameAfterCopyVisitor();
                    m_copyRange.Accept(setNewNameVisitor);

                    MoveCopyDraw(move, e.RangeView);
                    m_copyRange = null;
                }
            }
            else if ((Control.ModifierKeys & Keys.Alt) == Keys.Alt && (Control.ModifierKeys & Keys.Control) == Keys.Control)
            {
                m_mode = CopyMode.CtrlAltMove;
            }
            else if (e.IsCtrlDown)
            {
                m_mode = CopyMode.CtrlMove;
            }
            else
            {
                m_mode = CopyMode.None;
            }
        }

        void MoveCopyDraw(TopoEdit.Icon.Movement move, IBaseDrawPanel rangeView)
        {
            RectangleF rect = new RectangleF();
            //将移动前区域加入刷新区域
            rect = TopoEdit.Utility.Union(rect, m_copyRange.BoundsRect);
            //移动
            m_copyRange.Move(move);
            //将移动后区域加入刷新区域
            rect = TopoEdit.Utility.Union(rect, m_copyRange.BoundsRect);
            rangeView.Refresh(Utility.AdjustRect(rect, 10));
        }
    }
}
