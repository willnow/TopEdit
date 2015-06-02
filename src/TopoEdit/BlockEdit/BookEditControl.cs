using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using TopoEdit.EventHandler;
using TopoEdit.Icon;

namespace TopoEdit
{
    public partial class BookEditControl : UserControl, IHelpHandle
    {
        /// <summary>
        /// 帮助责任链对象
        /// </summary>
        HelpHandle m_cHelpHandle = new HelpHandle();
        /// <summary>
        /// 网格线绘图事件处理句柄
        /// </summary>
        private PaintGridEventHandler m_paintGridEventHandler = null;
        /// <summary>
        /// 关联VIEW
        /// </summary>
        private IBaseDrawPanel m_view = null;

        public BookEditControl()
        {
            InitializeComponent();

            panelBook.SetSuccessor(this);
            m_view = panelBook;
            InitZoom();
        }

        public IBaseDrawPanel View
        {
            get
            {
                return m_view;
            }
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

        private void toolStripButtonZoomOut_Click(object sender, EventArgs e)
        {
            Zoom(1.05F);
            UpdateZoomDisplay();
        }

        private void toolStripButtonZoomIn_Click(object sender, EventArgs e)
        {
            Zoom(1 / 1.05F);
            UpdateZoomDisplay();
        }

        void UpdateZoomDisplay()
        {
            toolStripComboBoxCustomZoom.Text = (int)(Math.Round(100 * panelBook.ZoomRate)) + "%";
        }

        private void Zoom(float rate)
        {
            Zoom zoom = new Zoom();
            zoom.ZoomMode = CSR.CUIT.GlobalService.ShareLib.EmZoomMode.UniformScale;
            zoom.XRadio = rate;
            zoom.YRadio = zoom.XRadio;

            if (toolStripButtonZoomSelOnly.Checked)
            {
                if (panelBook.BookData.SelectedRange != null)
                {
                    RectangleF rect = panelBook.BookData.SelectedRange.BoundsRect;
                    panelBook.BookData.SelectedRange.Zoom(zoom);
                    rect = Utility.Union(rect, panelBook.BookData.SelectedRange.BoundsRect);
                    panelBook.Refresh(Utility.ConvertRect(rect));
                }
                else
                {
                    MessageBox.Show("没有元素被选中");
                }
            }
            else
            {
                panelBook.ZoomRate *= rate;

                panelBook.BookData.RangeZoom *= rate;
                panelBook.BookData.Zoom(zoom);
                //刷新绘制
                panelBook.Invalidate(true);
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
                panelBook.RegHandler(m_paintGridEventHandler);
                panelBook.Invalidate();
            }
            else
            {
                if (m_paintGridEventHandler != null)
                {
                    panelBook.UnRegHandler(m_paintGridEventHandler);
                    m_paintGridEventHandler = null;
                    panelBook.Invalidate();
                }
            }
        }

        private void toolStripButtonRecover_Click(object sender, EventArgs e)
        {
            Zoom zoom = new Zoom();
            zoom.ZoomMode = CSR.CUIT.GlobalService.ShareLib.EmZoomMode.UniformScale;
            zoom.XRadio = 1 / panelBook.ZoomRate;
            zoom.YRadio = zoom.XRadio;

            panelBook.BookData.RangeZoom = Book.DefaultRangeZoom;
            panelBook.BookData.Zoom(zoom);
            panelBook.Invalidate();

            panelBook.ZoomRate = (float)(panelBook.ZoomRate * zoom.XRadio);

            UpdateZoomDisplay();
        }

        private void toolStripButtonSaveZoomRate_Click(object sender, EventArgs e)
        {
            panelBook.ZoomRate = 1;
            MessageBox.Show("保存默认大小成功");
        }

        private void toolStripComboBoxCustomZoom_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                float rate;
                string rateString = toolStripComboBoxCustomZoom.Text.Replace("%", "");
                if (float.TryParse(rateString, out rate))
                {
                    Zoom(0.01F * rate / panelBook.ZoomRate);
                }
            }
        }

        private void toolStripComboBoxCustomZoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            Zoom((toolStripComboBoxCustomZoom.SelectedItem as ZoomItem).Rate / panelBook.ZoomRate);
        }

        private void toolStripButtonNotDisplayTransparentColor_Click(object sender, EventArgs e)
        {
            panelBook.BookData.VisibleTransparentColor = !toolStripButtonNotDisplayTransparentColor.Checked;
            panelBook.Invalidate();
        }

        private void toolStripButtonResolutionRatio_Click(object sender, EventArgs e)
        {
            ResolutionRatioForm form = new ResolutionRatioForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                panelBook.SetResolutionRatio(form.RadioX, form.RadioY);
            }
        }
    }
}
