using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using TopoEdit.Icon;
using TopoEdit;

namespace TopoCom
{
    [ComVisible(true)]
    [Guid("B448D6F4-CFAC-4269-A869-1BCEC9AD70E6")]
    public interface ITopoContainer
    {
        Book GetBook();
        BlockContainer GetAllBlocks();
    }
}
