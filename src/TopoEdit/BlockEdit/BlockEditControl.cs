using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using TopoEdit.Icon;
using System.Xml;
using System.Diagnostics;
using TopoEdit.EventHandler;

namespace TopoEdit
{
    public partial class BlockEditControl : UserControl, IHelpHandle
    {
        public event EventHandler<ChangeBlockEventArgs> ChangeBlockEvent;
        /// <summary>
        /// 帮助责任链对象
        /// </summary>
        private HelpHandle m_cHelpHandle = new HelpHandle();
        /// <summary>
        /// 网格线绘图事件处理句柄
        /// </summary>
        private PaintGridEventHandler m_paintGridEventHandler = null;
        /// <summary>
        /// 关联VIEW
        /// </summary>
        private IBaseDrawPanel m_view = null;

        public BlockEditControl()
        {
            InitializeComponent();

            this.toolStripButtonCircle.DrawIcon = new IconCircle();
            this.toolStripButtonLine.DrawIcon = new IconLine();
            this.toolStripButtonPolygon.DrawIcon = new IconPolygon();
            this.toolStripButtonRect.DrawIcon = new IconRectangle();
            this.toolStripButtonText.DrawIcon = new IconText();

            this.toolStripButtonCircle.Click += new System.EventHandler(this.toolStripButtonIcon_Click);
            this.toolStripButtonLine.Click += new System.EventHandler(toolStripButtonIcon_Click);
            this.toolStripButtonPolygon.Click += new System.EventHandler(toolStripButtonIcon_Click);
            this.toolStripButtonRect.Click += new System.EventHandler(toolStripButtonIcon_Click);
            this.toolStripButtonText.Click += new System.EventHandler(toolStripButtonIcon_Click);

            panelBlock.SetSuccessor(this);
            m_view = panelBlock;
            InitBackColor();
        }

        public IBaseDrawPanel View
        {
            get
            {
                return m_view;
            }
        }

        internal void DrawBlock(Block block)
        {
            //计算画板中心点
            //Point posCenter = new Point(DrawPanelSize.Width / 4, DrawPanelSize.Height / 4);
            //block.Reset(posCenter, (int)((block.BoundsRect.Width > block.BoundsRect.Height) ? block.BoundsRect.Width : block.BoundsRect.Height));

            panelBlock.BlockData = block;
            panelBlock.Focus();
            panelBlock.Invalidate(true);

            InitLevel(block); 
            InitZoom();
        }

        void InitLevel(Block block)
        {
            toolStripComboBoxLevel.Items.Clear();
            List<int> levels = block.GetLevels();
            toolStripComboBoxLevel.Items.Add(-1);
            foreach (int level in levels)
            {
                toolStripComboBoxLevel.Items.Add(level);
            }
            toolStripComboBoxLevel.Text = "-1";
        }

        void InitZoom()
        {
            toolStripComboBoxCustomZoom.Items.Clear();
            toolStripComboBoxCustomZoom.Items.Add(new ZoomItem(0.75F));
            toolStripComboBoxCustomZoom.Items.Add(new ZoomItem(1F));
            toolStripComboBoxCustomZoom.Items.Add(new ZoomItem(1.5F));
            toolStripComboBoxCustomZoom.Items.Add(new ZoomItem(2F));
            toolStripComboBoxCustomZoom.SelectedIndex = 1;
        }

        void InitBackColor()
        {
            toolStripComboBoxBackColor.Items.Clear();
            Array colorArray = Enum.GetValues(typeof(KnownColor));
            foreach (object color in colorArray)
            {
                toolStripComboBoxBackColor.Items.Add(color);
            }
            toolStripComboBoxBackColor.SelectedItem = panelBlock.BackColor.ToKnownColor();
        }

        public Size DrawPanelSize
        {
            get
            {
                return panelBlock.Size;
            }
        }

        private void toolStripButtonIcon_Click(object sender, EventArgs e)
        {
            toolStripButtonSel.Checked = false;
            panelBlock.ClearDownToolStripButton();
            panelBlock.AddDownToolStripButton(sender as ToolStripButton);
            panelBlock.Cursor = System.Windows.Forms.Cursors.Cross;
        }

        private void toolStripButtonSel_Click(object sender, EventArgs e)
        {
            panelBlock.ClearDownToolStripButton();
            panelBlock.Cursor = System.Windows.Forms.Cursors.Default;
        }

        private void toolStripComboBoxLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            panelBlock.DisplayLevel = int.Parse(toolStripComboBoxLevel.Text);
            panelBlock.BlockData.SetVisible(panelBlock.DisplayLevel);//设置显示指定层级
            panelBlock.Invalidate();
        }

        private void panelBlock_ChangeBlockEvent(object sender, ChangeBlockEventArgs e)
        {
            if (ChangeBlockEvent != null)
            {
                Debug.Assert(null != e.BlockItem);
                ChangeBlockEvent(sender, e);
            }
        }

        private void toolStripButtonSaveZoomRate_Click(object sender, EventArgs e)
        {
            panelBlock.ZoomRate = 1;
            MessageBox.Show("保存默认大小成功");
        }

        private void toolStripButtonRecover_Click(object sender, EventArgs e)
        {
            Zoom zoom = new Zoom();
            zoom.ZoomMode = CSR.CUIT.GlobalService.ShareLib.EmZoomMode.UniformScale;
            zoom.XRadio = 1 / panelBlock.ZoomRate;
            zoom.YRadio = zoom.XRadio;
            panelBlock.BlockData.Zoom(zoom);
            panelBlock.Invalidate();

            panelBlock.ZoomRate = (float)(panelBlock.ZoomRate * zoom.XRadio);

            UpdateZoomDisplay();
        }

        private void toolStripButtonZoomOut_Click(object sender, EventArgs e)
        {
            Zoom(1.05F);
            UpdateZoomDisplay();
            
        }

        private void toolStripButtonZoomIn_Click(object sender, EventArgs e)
        {
            Zoom(1/1.05F);
            UpdateZoomDisplay();
        }

        void UpdateZoomDisplay()
        {
            toolStripComboBoxCustomZoom.Text = (int)(Math.Round(100 * panelBlock.ZoomRate)) + "%";
        }

        private void Zoom(float rate)
        {
            Zoom zoom = new Zoom();
            zoom.ZoomMode = CSR.CUIT.GlobalService.ShareLib.EmZoomMode.UniformScale;

            panelBlock.ZoomRate *= rate;
            zoom.XRadio = rate;
            zoom.YRadio = zoom.XRadio;

            if (((panelBlock.BlockData.BoundsRect.Width < 20) ||
                (panelBlock.BlockData.BoundsRect.Height < 20)) &&
                (zoom.XRadio < 1.0))
            {
                //BLOCK太小，不能在缩放
                return;
            }
            else if (((panelBlock.BlockData.BoundsRect.Width > 1000) ||
                (panelBlock.BlockData.BoundsRect.Height > 1000)) &&
                (zoom.XRadio > 1.0))
            {
                //BLOCK太大，不能在缩放
                return;
            }
            else
            {
                ////计算缩放前中心点
                //PointF posBeforeZoom = TopoEdit.Utility.GetCenter(e.RangeData.BoundsRect);
                ////缩放
                //e.RangeData.Zoom(zoom);
                ////计算缩放后中心点
                //PointF posAfterZoom = TopoEdit.Utility.GetCenter(e.RangeData.BoundsRect);
                ////将放大后BLOCK移动到放大前中心点所在位置
                //Movement move = new Movement();
                //move.XMovement = (int)(-posAfterZoom.X + posBeforeZoom.X);
                //move.YMovement = (int)(-posAfterZoom.Y + posBeforeZoom.Y);
                //e.RangeData.Move(move);

                panelBlock.BlockData.Zoom(zoom);
                //刷新绘制
                panelBlock.Invalidate(true);
            }
        }

        #region IHelpHandle 成员

        public virtual void SetSuccessor(IHelpHandle handle)
        {
            m_cHelpHandle.SetSuccessor(handle);
        }

        public virtual void HandleHelp(string text)
        {
            m_cHelpHandle.HandleHelp(text);
        }

        #endregion

        private void toolStripButtonDisplayGrid_Click(object sender, EventArgs e)
        {
            if (toolStripButtonDisplayGrid.Checked)
            {
                m_paintGridEventHandler = new PaintGridEventHandler();
                panelBlock.RegHandler(m_paintGridEventHandler);
                panelBlock.Invalidate();
            }
            else
            {
                if (m_paintGridEventHandler != null)
                {
                    panelBlock.UnRegHandler(m_paintGridEventHandler);
                    m_paintGridEventHandler = null;
                    panelBlock.Invalidate();
                }
            }
        }

        private void toolStripComboBoxLevel_DropDown(object sender, EventArgs e)
        {
            InitLevel(panelBlock.BlockData);
        }

        private void toolStripButtonOnBound_Click(object sender, EventArgs e)
        {
            if (toolStripButtonOnBound.Checked)
            {
                panelBlock.BlockData.IntersectIconType = TopoEdit.Visitor.IntersectType.OnBound;
            }
            else
            {
                panelBlock.BlockData.IntersectIconType = TopoEdit.Visitor.IntersectType.InBound;
            }
        }

        private void toolStripComboBoxCustomZoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            Zoom((toolStripComboBoxCustomZoom.SelectedItem as ZoomItem).Rate / panelBlock.ZoomRate);
        }

        private void toolStripComboBoxCustomZoom_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                float rate;
                string rateString = toolStripComboBoxCustomZoom.Text.Replace("%", "");
                if (float.TryParse(rateString, out rate))
                {
                    Zoom(0.01F * rate / panelBlock.ZoomRate);
                }
            }
        }

        private void toolStripComboBoxBackColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            panelBlock.BackColor = Color.FromKnownColor((KnownColor)toolStripComboBoxBackColor.SelectedItem);
        }

        private void toolStripButtonNotDisplayTransparentColor_Click(object sender, EventArgs e)
        {
            panelBlock.BlockData.VisibleTransparentColor = !toolStripButtonNotDisplayTransparentColor.Checked;
            panelBlock.Invalidate();
        }
    }

    public class ZoomItem
    {
        private float m_rate;

        public float Rate
        {
            get
            {
                return m_rate;
            }
            set
            {
                m_rate = value;
            }
        }

        public ZoomItem(float rate)
        {
            m_rate = rate;
        }

        public override string ToString()
        {
            return m_rate * 100 + "%";
        }
    }
}
