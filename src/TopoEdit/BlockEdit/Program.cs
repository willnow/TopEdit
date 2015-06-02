using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace TopoEdit
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            TopoHelper.TopoForm = new MainForm();
            Application.Run(TopoHelper.TopoForm);
        }
    }

    public static class TopoHelper
    {
        public static MainForm TopoForm = null;
    }
}
