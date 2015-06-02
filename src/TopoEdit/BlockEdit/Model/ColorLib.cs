//******************************************************************************
//文件名称 :     ColorLib.cs
//版权信息 :     北京南车时代信息技术有限公司 版权所有
//创建日期 :     2013-10-08
//文件描述 :
//修改履历 :
// V1.0.0    蒋湘宁    2013-10-08    创建本文件
// V1.1.1    蒋湘宁    2013-10-21    完成第三轮迭代的功能
// V1.2.1    蒋湘宁    2013-10-22    重新编译
// V1.3.2    蒋湘宁    2013-10-25    根据《CUIT软件测试报告》V1.1.1的测试结果修改代码，修复BUG
// V1.4.4    蒋湘宁    2013-11-06    修复测试发现的BUG
// V1.5.5    蒋湘宁    2013-11-18    修复单元测试、系统测试发现的BUG
// V1.6.6    蒋湘宁    2013-11-22    修复系统测试发现的BUG
// V1.14.16    JXN    2013-12-22    修复测试发现的BUG
// V1.15.19    JXN    2013-12-25    修改测试中发现的BUG
//******************************************************************************
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml;

namespace TopoEdit.Model
{
    /// <summary>
    /// <para>颜色方案类。</para>
    /// </summary>
    public class TdeColor
    {
        /// <summary>
        /// <para>获取和设置颜色名称。</para>
        /// </summary>
        /// <remarks>
        /// <para> 蒋湘宁 2013-10-08  创建属性</para>
        /// </remarks>
        public String Name
        {
            get;
            set;
        }
        /// <summary>
        /// <para>获取和设置颜色对应的Color对象。</para>
        /// </summary>
        /// <remarks>
        /// <para> 蒋湘宁 2013-10-08  创建属性</para>
        /// </remarks>
        public Color ColorInArgb
        {
            get;
            set;
        }
        /// <summary>
        /// <para>获取和设置颜色是否闪烁。</para>
        /// </summary>
        /// <remarks>
        /// <para> 蒋湘宁 2013-10-08  创建属性</para>
        /// </remarks>
        public Boolean Flash
        {
            get;
            set;
        }
        /// <summary>
        /// 颜色索引
        /// </summary>
        public int ColorIndex
        {
            get;
            set;
        }
        /// <summary>
        /// <para>当前色是否可见（颜色包含透明度）</para>
        /// </summary>
        /// <remarks>
        /// <para> 蒋湘宁 2013-10-08  创建属性</para>
        /// </remarks>
        public bool IsVisible
        {
            get
            {
                return (ColorInArgb.A != 0);
            }
        }
        /// <summary>
        /// <para>获得本颜色是否为约定的当前色ARGB=(0,0,0,1)。</para>
        /// </summary>
        /// <remarks>
        /// <para> 蒋湘宁 2013-10-08  创建属性</para>
        /// </remarks>
        public bool ReferenceToCurrenctColor
        {
            get
            {
                if((ColorInArgb.A == 0 && ColorInArgb.R == 0 && ColorInArgb.G == 0) &&
                        (ColorInArgb.B == 1))
                {
                    return true;
                }
                return false;
            }
        }
        /// <summary>
        /// <para>初始化颜色的属性。</para>
        /// <para>前置条件：</para>
        /// <para>    无</para>
        /// <para>后置条件：</para>
        /// <para>    属性Name、ColorInArgb、Flash被初始化。</para>
        /// </summary>
        /// <param name="name">颜色名称</param>
        /// <param name="colorValue">颜色的Int32值</param>
        /// <param name="flash">颜色是否闪烁</param>
        /// <param name="colorIndex">颜色索引</param>
        /// <returns>
        /// <para></para>
        /// </returns>
        /// <remarks>
        /// <para> 蒋湘宁 2013-10-08  创建函数</para>
        /// </remarks>
        internal TdeColor(String name, Int32 colorValue, Boolean flash, int colorIndex)
        {
            Name = name;
            ColorInArgb = Color.FromArgb(colorValue);
            Flash = flash;
            ColorIndex = colorIndex;
        }

        public override string ToString()
        {
            return Name;
        }
    }
    /// <summary>
    /// <para>颜色方案库。</para>
    /// </summary>
    public class ColorLib
    {
        internal static readonly ColorLib Instance = new ColorLib();
        /// <summary>
        /// <para>透明色</para>
        /// </summary>
        internal static readonly TdeColor TransParentColor = new TdeColor("透明色", 0, false, 1);
        /// <summary>
        /// <para>无内容。</para>
        /// <para>前置条件：</para>
        /// <para>    无</para>
        /// <para>后置条件：</para>
        /// <para>    无</para>
        /// </summary>
        /// <returns>
        /// <para></para>
        /// </returns>
        /// <remarks>
        /// <para> 蒋湘宁 2013-10-08  创建函数</para>
        /// </remarks>
        private ColorLib() { }

        internal bool Load(XmlNode colorLibNode)
        {
            XmlNodeList colorNodes = colorLibNode.SelectNodes("Color");

            foreach (XmlNode colorNode in colorNodes)
            {
                int index = int.Parse(colorNode.Attributes.GetNamedItem("Index").InnerText);
                string name = colorNode.SelectSingleNode("Name").InnerText;
                int color = int.Parse(colorNode.SelectSingleNode("ColorValueInHost").InnerText, System.Globalization.NumberStyles.HexNumber);
                bool flash = bool.Parse(colorNode.SelectSingleNode("Flash").InnerText);
                bool ret = ColorLib.Instance.AddColor(index, name, color, flash);
                if (!ret)
                {
                    return ret;
                }
            }
            ColorLib.Instance.SetDefaultColor(GetColorIndexByName(colorLibNode.SelectSingleNode("DefaultColor").InnerText));
            ColorLib.Instance.SetBackGroundColor(GetColorIndexByName(colorLibNode.SelectSingleNode("BackgroundColor").InnerText));
            ColorLib.Instance.SetFocusColor(GetColorIndexByName(colorLibNode.SelectSingleNode("FocusColor").InnerText));
            ColorLib.Instance.FlashInterval = int.Parse(colorLibNode.SelectSingleNode("FlashingInterval").InnerText);
            return true;
        }

        /// <summary>
        /// <para>获得或设置颜色的闪烁间隔。</para>
        /// </summary>
        /// <remarks>
        /// <para> 蒋湘宁 2013-10-08  创建属性</para>
        /// </remarks>
        public Int32 FlashInterval
        {
            get
            {
                return m_flashInterval;
            }
            set
            {
                m_flashInterval = value;
            }
        }
        /// <summary>
        /// <para>将颜色加入颜色库。</para>
        /// <para>前置条件：</para>
        /// <para>    无</para>
        /// <para>后置条件：无</para>
        /// </summary>
        /// <param name="index">颜色的索引</param>
        /// <param name="name">颜色名称</param>
        /// <param name="colorValue">颜色值</param>
        /// <param name="flash">颜色是否闪烁</param>
        /// <returns>
        /// <para>true：添加成功</para>
        /// <para>false：添加失败。该颜色索引已存在。</para>
        /// </returns>
        /// <remarks>
        /// <para> 蒋湘宁 2013-10-08  创建函数</para>
        /// </remarks>
        internal Boolean AddColor(Int32 index, String name, Int32 colorValue, Boolean flash)
        {
            if (m_ColorDictionary.ContainsKey(index))
            {
                return false;
            }
            TdeColor colorTemp = new TdeColor(name, colorValue, flash, index);
            m_ColorDictionary.Add(index, colorTemp);
            return true;
        }
        /// <summary>
        /// <para>获得背景色。</para>
        /// <para>前置条件：</para>
        /// <para>    无</para>
        /// <para>后置条件：无</para>
        /// </summary>
        /// <returns>
        /// <para>背景色的TdeColor对象</para>
        /// </returns>
        /// <remarks>
        /// <para> 蒋湘宁 2013-10-08  创建函数</para>
        /// </remarks>
        public TdeColor GetBackgroundColor()
        {
            return m_ColorDictionary[m_backgroundColorIndex];
        }
        /// <summary>
        /// <para>获得选中色。</para>
        /// <para>前置条件：</para>
        /// <para>    无</para>
        /// <para>后置条件：无</para>
        /// </summary>
        /// <returns>
        /// <para>选中色的Color对象</para>
        /// </returns>
        /// <remarks>
        /// <para> 蒋湘宁 2013-10-08  创建函数</para>
        /// </remarks>
        public TdeColor GetFocusColor()
        {
            return m_focusColor;
        }
        /// <summary>
        /// <para>获得索引指定的颜色。</para>
        /// <para>前置条件：</para>
        /// <para>    无</para>
        /// <para>后置条件：无</para>
        /// </summary>
        /// <param name="colorIndex">颜色索引</param>
        /// <returns>
        /// <para>TdeColor对象：根据索引找到了指定的颜色</para>
        /// <para>null：在颜色库中不存在该索引对应的颜色。</para>
        /// </returns>
        /// <remarks>
        /// <para> 蒋湘宁 2013-10-08  创建函数</para>
        /// </remarks>
        public TdeColor GetColor(Int32 colorIndex)
        {
            return m_ColorDictionary[colorIndex];
        }

        public int GetColorIndexByName(string name)
        {
            Dictionary<int, TdeColor>.Enumerator itor = m_ColorDictionary.GetEnumerator();
            while (itor.MoveNext())
            {
                if (itor.Current.Value.Name == name)
                {
                    return itor.Current.Key;
                }
            }

            return -1;
        }

        public TdeColor GetColorByName(string name)
        {
            Dictionary<int, TdeColor>.Enumerator itor = m_ColorDictionary.GetEnumerator();
            while (itor.MoveNext())
            {
                if(itor.Current.Value.Name == name)
                {
                    return itor.Current.Value;
                }
            }

            return null;
        }
        /// <summary>
        /// <para>获得索引指定的颜色的Color对象。</para>
        /// <para>前置条件：</para>
        /// <para>    无</para>
        /// <para>后置条件：无</para>
        /// </summary>
        /// <param name="colorIndex">颜色索引</param>
        /// <returns>
        /// <para>Color对象</para>
        /// </returns>
        /// <remarks>
        /// <para> 蒋湘宁 2013-10-08  创建函数</para>
        /// </remarks>
        public Color GetColorInARGB(Int32 colorIndex)
        {
            TdeColor color;
            Boolean ret = m_ColorDictionary.TryGetValue(colorIndex, out color);
            if (!ret)
            {
                return GetDefaultColor().ColorInArgb;
            }
            return color.ColorInArgb;
        }
        /// <summary>
        /// <para>获得默认颜色。</para>
        /// <para>前置条件：</para>
        /// <para>    无</para>
        /// <para>后置条件：无</para>
        /// </summary>
        /// <returns>
        /// <para>默认颜色的TdeColor对象</para>
        /// </returns>
        /// <remarks>
        /// <para> 蒋湘宁 2013-10-08  创建函数</para>
        /// </remarks>
        public TdeColor GetDefaultColor()
        {
            return m_ColorDictionary[m_defaultColorIndex];
        }
        /// <summary>
        /// <para>设置背景色。</para>
        /// <para>前置条件：</para>
        /// <para>    无</para>
        /// <para>后置条件：无</para>
        /// </summary>
        /// <param name="colorIndex">背景色在颜色库中的颜色索引</param>
        /// <returns>
        /// <para>true：设置背景色成功</para>
        /// <para>false：设置背景色失败</para>
        /// </returns>
        /// <remarks>
        /// <para> 蒋湘宁 2013-10-08  创建函数</para>
        /// </remarks>
        internal Boolean SetBackGroundColor(Int32 colorIndex)
        {
            if (m_ColorDictionary.ContainsKey(colorIndex))
            {
                m_backgroundColorIndex = colorIndex;
                return true;
            }
            return false;
        }
        /// <summary>
        /// <para>设置默认色。</para>
        /// <para>前置条件：</para>
        /// <para>    无</para>
        /// <para>后置条件：无</para>
        /// </summary>
        /// <param name="colorIndex">背景色在颜色库中的颜色索引</param>
        /// <returns>
        /// <para>true：设置默认色成功</para>
        /// <para>false：设置默认色失败</para>
        /// </returns>
        /// <remarks>
        /// <para> 蒋湘宁 2013-10-08  创建函数</para>
        /// </remarks>
        internal Boolean SetDefaultColor(Int32 colorIndex)
        {
            if (m_ColorDictionary.ContainsKey(colorIndex))
            {
                m_defaultColorIndex = colorIndex;
                return true;
            }
            return false;
        }
        /// <summary>
        /// <para>设置选中色。</para>
        /// <para>前置条件：</para>
        /// <para>    无</para>
        /// <para>后置条件：无</para>
        /// </summary>
        /// <param name="colorIndex">选中色在颜色库中的颜色索引</param>
        /// <returns>
        /// <para>true：设置选中色成功</para>
        /// <para>false：设置选中色失败</para>
        /// </returns>
        /// <remarks>
        /// <para> 蒋湘宁 2013-10-08  创建函数</para>
        /// </remarks>
        internal Boolean SetFocusColor(Int32 colorIndex)
        {
            if (m_ColorDictionary.ContainsKey(colorIndex))
            {
                m_focusColor = m_ColorDictionary[colorIndex];
                return true;
            }
            return false;
        }

        public List<TdeColor> GetAllColor()
        {
            List<TdeColor> color = new List<TdeColor>();

            Dictionary<Int32, TdeColor>.Enumerator itor = m_ColorDictionary.GetEnumerator();
            while(itor.MoveNext())
            {
                color.Add(itor.Current.Value);
            }

            return color;
        }

        /// <summary>
        /// <para>颜色库，键为int，值为TdeColor对象</para>
        /// </summary>
        private Dictionary<Int32, TdeColor> m_ColorDictionary = new Dictionary<Int32, TdeColor>();
        /// <summary>
        /// <para>默认颜色在颜色库中的索引</para>
        /// </summary>
        private Int32 m_defaultColorIndex = 0;
        /// <summary>
        /// <para>背景色在颜色库中的索引</para>
        /// </summary>
        private Int32 m_backgroundColorIndex = 0;
        /// <summary>
        /// <para>若颜色闪烁，其闪烁间隔</para>
        /// </summary>
        private Int32 m_flashInterval = 0;
        /// <summary>
        /// <para>选中色。</para>
        /// </summary>
        private TdeColor m_focusColor;
    }
}
