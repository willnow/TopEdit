using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace TopoCom
{
    //说明：在VS命令提示符中执行regasm COM文件名 /regfile 导出注册表，然后执行注册表导入（注意可执行程序和COM DLL必须在同一目录下）即可在VBS/JS中执行
    //      使用guidgen.exe生成GUID号

    [ComVisible(true)]
    [Guid("C6B218AB-BD45-4bd0-A9A5-0127F727E537")]
    public interface IMenu
    {
        void Add(System.Windows.Forms.ToolStripMenuItem menu);
        void Remvoe(System.Windows.Forms.ToolStripMenuItem menu);
    }
}
