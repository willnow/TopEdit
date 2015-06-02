using System;
using System.Collections.Generic;
using System.Text;

namespace TopoEdit.Icon
{
    public abstract class ZoomableRange : IRange
    {
        public static float DefaultRangeZoom = 1F;
        /// <summary>
        /// 实例化BLOCK时，BLOCK实例的缩放比
        /// </summary>
        protected float m_rangeZoom = DefaultRangeZoom;

        public float RangeZoom
        {
            get { return m_rangeZoom; }
            set { m_rangeZoom = value; }
        }
    }
}
