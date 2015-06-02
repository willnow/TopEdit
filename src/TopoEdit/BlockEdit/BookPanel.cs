using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using TopoEdit.Icon;
using TopoEdit.EventHandler;
using TopoEdit.Command;

namespace TopoEdit
{
    public partial class BookPanel : IBaseDrawPanel
    {
        private PaintBoundEventHandler paintBoundEventHandler = new PaintBoundEventHandler(1920, 1080);

        public BookPanel()
        {
            InitializeComponent();
        }

        public BookPanel(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        protected override void Init()
        {
            base.Init();

            RegHandler(new MoveSelectedIconHandler());
            RegHandler(new SelectRangeHandler());
            RegHandler(new DelectIconEventHandler());
            RegHandler(new SaveBookEventHandler());
            RegHandler(paintBoundEventHandler);
            RegHandler(new UnDoEventHandler());

            RangeData = Book.Instance;
        }

        internal Book BookData
        {
            get { return RangeData as Book; }
        }

        private void BookPanel_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
        {
            //处理拖拽
            e.Effect = DragDropEffects.Copy;
        }

        private void BookPanel_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
        {
            //处理拖拽
            Point pos = this.PointToClient(new Point(e.X, e.Y));
            Page page = e.Data.GetData((typeof(Page))) as Page;

            if (page != null)
            {
                PageRect pageRect = new PageRect(BookData);
                CmdMgr.AddThenExec(new AddDrawCommand(this, BookData, pageRect));//添加后则建立了父子关系
                Page pageTemplate = PageContainer.Instance.GetPageByName(page.Name);
                pageTemplate.ClearSelectIcon();
                pageRect.Init(page.Name + Book.Instance.GetNewNameIndex(page.Name), pageTemplate, pos);
                this.Refresh(Utility.AdjustRect(pageRect.BoundsRect, 10));
            }
        }

        //设置分辨率显示框
        public void SetResolutionRatio(int resolutionRatioX, int resolutionRatioY)
        {
            paintBoundEventHandler.ResolutionRatioX = resolutionRatioX;
            paintBoundEventHandler.ResolutionRatioY = resolutionRatioY;

            Invalidate();
        }

        protected override void DoSetScrollMinSize()
        {
            this.AutoScrollMinSize = new Size((int)(Math.Max(RangeData.BoundsRect.Size.Width, paintBoundEventHandler.ResolutionRatioX) + 500)
                , (int)(Math.Max(RangeData.BoundsRect.Size.Height, paintBoundEventHandler.ResolutionRatioY) + 500));
        }
    }
}
