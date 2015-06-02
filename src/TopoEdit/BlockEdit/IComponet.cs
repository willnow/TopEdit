using System;
using System.Collections.Generic;
using System.Text;

namespace TopoEdit
{
    public interface IComponent
    {
        void Load();
        void UnLoad();
    }
}
