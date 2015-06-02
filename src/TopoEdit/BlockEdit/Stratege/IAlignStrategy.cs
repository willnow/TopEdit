using System;
using System.Collections.Generic;
using System.Text;
using TopoEdit.Icon;

namespace TopoEdit.Stratege
{
    interface IAlignStrategy
    {
        void SetRef(float pos);
        void Align(IBaseDrawPanel view, SelectedRange range);
    }
}
