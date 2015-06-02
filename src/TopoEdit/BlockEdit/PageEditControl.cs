using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using TopoEdit.Icon;
using TopoEdit.EventHandler;
using System.Diagnostics;

namespace TopoEdit
{
    public partial class PageEditControl : UserControl, IHelpHandle
    {
        public event EventHandler<ChangePageEventArgs> ChangePageEvent;
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

        public PageEditControl()
        {
            InitializeComponent();

            panelPage.SetSuccessor(this);
            m_view = panelPage;
            InitBackColor();
        }

        public IBaseDrawPanel View
        {
            get
            {
                return m_view;
            }
        }

        void InitBackColor()
        {
            toolStripComboBoxBackColor.Items.Clear();
            Array colorArray = Enum.GetValues(typeof(KnownColor));
            foreach (object color in colorArray)
            {
                toolStripComboBoxBackColor.Items.Add(color);
            }
            toolStripComboBoxBackColor.SelectedItem = panelPage.BackColor.ToKnownColor();
        }

        public void LoadPage(Page topo)
        {
            panelPage.TopoData = topo;
        }

        public void ResetBlocks(string blockName)
        {
            panelPage.TopoData.ResetBlocks(blockName);//因为BLOCK变化，刷新PAGE
            Book.Instance.ResetPages(panelPage.TopoData.Name);//因为PAGE被刷新，BOOK也需要刷新
        }

        private void toolStripButtonSaveZoomRate_Click(object sender, EventArgs e)
        {
            panelPage.ZoomRate = 1;
            MessageBox.Show("保存默认大小成功");
        }

        private void toolStripButtonRecover_Click(object sender, EventArgs e)
        {
            Zoom zoom = new Zoom();
            zoom.ZoomMode = CSR.CUIT.GlobalService.ShareLib.EmZoomMode.UniformScale;
            zoom.XRadio = 1 / panelPage.ZoomRate;
            zoom.YRadio = zoom.XRadio;

            panelPage.TopoData.RangeZoom = Page.DefaultRangeZoom;
            panelPage.TopoData.Zoom(zoom);
            panelPage.Invalidate();

            panelPage.ZoomRate = (float)(panelPage.ZoomRate * zoom.XRadio);
        }

        private void toolStripButtonZoomOut_Click(object sender, EventArgs e)
        {
            Zoom(1.05F);
        }

        private void toolStripButtonZoomIn_Click(object sender, EventArgs e)
        {
            Zoom(1 / 1.05F);
        }

        private void Zoom(float rate)
        {
            Zoom zoom = new Zoom();
            zoom.ZoomMode = CSR.CUIT.GlobalService.ShareLib.EmZoomMode.UniformScale;

            panelPage.ZoomRate *= rate;
            zoom.XRadio = rate;
            zoom.YRadio = zoom.XRadio;

            panelPage.TopoData.RangeZoom *= rate;
            panelPage.TopoData.Zoom(zoom);
            //刷新绘制
            panelPage.Invalidate(true);
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
                panelPage.RegHandler(m_paintGridEventHandler);
                panelPage.Invalidate();
            }
            else
            {
                if (m_paintGridEventHandler != null)
                {
                    panelPage.UnRegHandler(m_paintGridEventHandler);
                    m_paintGridEventHandler = null;
                    panelPage.Invalidate();
                }
            }
        }

        private void toolStripButtonOnBound_Click(object sender, EventArgs e)
        {
            if (toolStripButtonOnBound.Checked)
            {
                panelPage.TopoData.IntersectIconType = TopoEdit.Visitor.IntersectType.OnBound;
            }
            else
            {
                panelPage.TopoData.IntersectIconType = TopoEdit.Visitor.IntersectType.InBound;
            }
        }

        private void toolStripComboBoxBackColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            panelPage.BackColor = Color.FromKnownColor((KnownColor)toolStripComboBoxBackColor.SelectedItem);
        }

        private void toolStripButtonNotDisplayTransparentColor_Click(object sender, EventArgs e)
        {
            panelPage.TopoData.VisibleTransparentColor = !toolStripButtonNotDisplayTransparentColor.Checked;
            panelPage.Invalidate();
        }

        private void panelPage_ChangePageEvent(object sender, ChangePageEventArgs e)
        {
            if (ChangePageEvent != null)
            {
                Debug.Assert(null != e.PageItem);
                ChangePageEvent(sender, e);
            }
        }
    }
}
