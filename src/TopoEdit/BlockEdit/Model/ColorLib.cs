//******************************************************************************
//�ļ����� :     ColorLib.cs
//��Ȩ��Ϣ :     �����ϳ�ʱ����Ϣ�������޹�˾ ��Ȩ����
//�������� :     2013-10-08
//�ļ����� :
//�޸����� :
// V1.0.0    ������    2013-10-08    �������ļ�
// V1.1.1    ������    2013-10-21    ��ɵ����ֵ����Ĺ���
// V1.2.1    ������    2013-10-22    ���±���
// V1.3.2    ������    2013-10-25    ���ݡ�CUIT������Ա��桷V1.1.1�Ĳ��Խ���޸Ĵ��룬�޸�BUG
// V1.4.4    ������    2013-11-06    �޸����Է��ֵ�BUG
// V1.5.5    ������    2013-11-18    �޸���Ԫ���ԡ�ϵͳ���Է��ֵ�BUG
// V1.6.6    ������    2013-11-22    �޸�ϵͳ���Է��ֵ�BUG
// V1.14.16    JXN    2013-12-22    �޸����Է��ֵ�BUG
// V1.15.19    JXN    2013-12-25    �޸Ĳ����з��ֵ�BUG
//******************************************************************************
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml;

namespace TopoEdit.Model
{
    /// <summary>
    /// <para>��ɫ�����ࡣ</para>
    /// </summary>
    public class TdeColor
    {
        /// <summary>
        /// <para>��ȡ��������ɫ���ơ�</para>
        /// </summary>
        /// <remarks>
        /// <para> ������ 2013-10-08  ��������</para>
        /// </remarks>
        public String Name
        {
            get;
            set;
        }
        /// <summary>
        /// <para>��ȡ��������ɫ��Ӧ��Color����</para>
        /// </summary>
        /// <remarks>
        /// <para> ������ 2013-10-08  ��������</para>
        /// </remarks>
        public Color ColorInArgb
        {
            get;
            set;
        }
        /// <summary>
        /// <para>��ȡ��������ɫ�Ƿ���˸��</para>
        /// </summary>
        /// <remarks>
        /// <para> ������ 2013-10-08  ��������</para>
        /// </remarks>
        public Boolean Flash
        {
            get;
            set;
        }
        /// <summary>
        /// ��ɫ����
        /// </summary>
        public int ColorIndex
        {
            get;
            set;
        }
        /// <summary>
        /// <para>��ǰɫ�Ƿ�ɼ�����ɫ����͸���ȣ�</para>
        /// </summary>
        /// <remarks>
        /// <para> ������ 2013-10-08  ��������</para>
        /// </remarks>
        public bool IsVisible
        {
            get
            {
                return (ColorInArgb.A != 0);
            }
        }
        /// <summary>
        /// <para>��ñ���ɫ�Ƿ�ΪԼ���ĵ�ǰɫARGB=(0,0,0,1)��</para>
        /// </summary>
        /// <remarks>
        /// <para> ������ 2013-10-08  ��������</para>
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
        /// <para>��ʼ����ɫ�����ԡ�</para>
        /// <para>ǰ��������</para>
        /// <para>    ��</para>
        /// <para>����������</para>
        /// <para>    ����Name��ColorInArgb��Flash����ʼ����</para>
        /// </summary>
        /// <param name="name">��ɫ����</param>
        /// <param name="colorValue">��ɫ��Int32ֵ</param>
        /// <param name="flash">��ɫ�Ƿ���˸</param>
        /// <param name="colorIndex">��ɫ����</param>
        /// <returns>
        /// <para></para>
        /// </returns>
        /// <remarks>
        /// <para> ������ 2013-10-08  ��������</para>
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
    /// <para>��ɫ�����⡣</para>
    /// </summary>
    public class ColorLib
    {
        internal static readonly ColorLib Instance = new ColorLib();
        /// <summary>
        /// <para>͸��ɫ</para>
        /// </summary>
        internal static readonly TdeColor TransParentColor = new TdeColor("͸��ɫ", 0, false, 1);
        /// <summary>
        /// <para>�����ݡ�</para>
        /// <para>ǰ��������</para>
        /// <para>    ��</para>
        /// <para>����������</para>
        /// <para>    ��</para>
        /// </summary>
        /// <returns>
        /// <para></para>
        /// </returns>
        /// <remarks>
        /// <para> ������ 2013-10-08  ��������</para>
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
        /// <para>��û�������ɫ����˸�����</para>
        /// </summary>
        /// <remarks>
        /// <para> ������ 2013-10-08  ��������</para>
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
        /// <para>����ɫ������ɫ�⡣</para>
        /// <para>ǰ��������</para>
        /// <para>    ��</para>
        /// <para>������������</para>
        /// </summary>
        /// <param name="index">��ɫ������</param>
        /// <param name="name">��ɫ����</param>
        /// <param name="colorValue">��ɫֵ</param>
        /// <param name="flash">��ɫ�Ƿ���˸</param>
        /// <returns>
        /// <para>true����ӳɹ�</para>
        /// <para>false�����ʧ�ܡ�����ɫ�����Ѵ��ڡ�</para>
        /// </returns>
        /// <remarks>
        /// <para> ������ 2013-10-08  ��������</para>
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
        /// <para>��ñ���ɫ��</para>
        /// <para>ǰ��������</para>
        /// <para>    ��</para>
        /// <para>������������</para>
        /// </summary>
        /// <returns>
        /// <para>����ɫ��TdeColor����</para>
        /// </returns>
        /// <remarks>
        /// <para> ������ 2013-10-08  ��������</para>
        /// </remarks>
        public TdeColor GetBackgroundColor()
        {
            return m_ColorDictionary[m_backgroundColorIndex];
        }
        /// <summary>
        /// <para>���ѡ��ɫ��</para>
        /// <para>ǰ��������</para>
        /// <para>    ��</para>
        /// <para>������������</para>
        /// </summary>
        /// <returns>
        /// <para>ѡ��ɫ��Color����</para>
        /// </returns>
        /// <remarks>
        /// <para> ������ 2013-10-08  ��������</para>
        /// </remarks>
        public TdeColor GetFocusColor()
        {
            return m_focusColor;
        }
        /// <summary>
        /// <para>�������ָ������ɫ��</para>
        /// <para>ǰ��������</para>
        /// <para>    ��</para>
        /// <para>������������</para>
        /// </summary>
        /// <param name="colorIndex">��ɫ����</param>
        /// <returns>
        /// <para>TdeColor���󣺸��������ҵ���ָ������ɫ</para>
        /// <para>null������ɫ���в����ڸ�������Ӧ����ɫ��</para>
        /// </returns>
        /// <remarks>
        /// <para> ������ 2013-10-08  ��������</para>
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
        /// <para>�������ָ������ɫ��Color����</para>
        /// <para>ǰ��������</para>
        /// <para>    ��</para>
        /// <para>������������</para>
        /// </summary>
        /// <param name="colorIndex">��ɫ����</param>
        /// <returns>
        /// <para>Color����</para>
        /// </returns>
        /// <remarks>
        /// <para> ������ 2013-10-08  ��������</para>
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
        /// <para>���Ĭ����ɫ��</para>
        /// <para>ǰ��������</para>
        /// <para>    ��</para>
        /// <para>������������</para>
        /// </summary>
        /// <returns>
        /// <para>Ĭ����ɫ��TdeColor����</para>
        /// </returns>
        /// <remarks>
        /// <para> ������ 2013-10-08  ��������</para>
        /// </remarks>
        public TdeColor GetDefaultColor()
        {
            return m_ColorDictionary[m_defaultColorIndex];
        }
        /// <summary>
        /// <para>���ñ���ɫ��</para>
        /// <para>ǰ��������</para>
        /// <para>    ��</para>
        /// <para>������������</para>
        /// </summary>
        /// <param name="colorIndex">����ɫ����ɫ���е���ɫ����</param>
        /// <returns>
        /// <para>true�����ñ���ɫ�ɹ�</para>
        /// <para>false�����ñ���ɫʧ��</para>
        /// </returns>
        /// <remarks>
        /// <para> ������ 2013-10-08  ��������</para>
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
        /// <para>����Ĭ��ɫ��</para>
        /// <para>ǰ��������</para>
        /// <para>    ��</para>
        /// <para>������������</para>
        /// </summary>
        /// <param name="colorIndex">����ɫ����ɫ���е���ɫ����</param>
        /// <returns>
        /// <para>true������Ĭ��ɫ�ɹ�</para>
        /// <para>false������Ĭ��ɫʧ��</para>
        /// </returns>
        /// <remarks>
        /// <para> ������ 2013-10-08  ��������</para>
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
        /// <para>����ѡ��ɫ��</para>
        /// <para>ǰ��������</para>
        /// <para>    ��</para>
        /// <para>������������</para>
        /// </summary>
        /// <param name="colorIndex">ѡ��ɫ����ɫ���е���ɫ����</param>
        /// <returns>
        /// <para>true������ѡ��ɫ�ɹ�</para>
        /// <para>false������ѡ��ɫʧ��</para>
        /// </returns>
        /// <remarks>
        /// <para> ������ 2013-10-08  ��������</para>
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
        /// <para>��ɫ�⣬��Ϊint��ֵΪTdeColor����</para>
        /// </summary>
        private Dictionary<Int32, TdeColor> m_ColorDictionary = new Dictionary<Int32, TdeColor>();
        /// <summary>
        /// <para>Ĭ����ɫ����ɫ���е�����</para>
        /// </summary>
        private Int32 m_defaultColorIndex = 0;
        /// <summary>
        /// <para>����ɫ����ɫ���е�����</para>
        /// </summary>
        private Int32 m_backgroundColorIndex = 0;
        /// <summary>
        /// <para>����ɫ��˸������˸���</para>
        /// </summary>
        private Int32 m_flashInterval = 0;
        /// <summary>
        /// <para>ѡ��ɫ��</para>
        /// </summary>
        private TdeColor m_focusColor;
    }
}
