//******************************************************************************
//�ļ����� :     IIcon.cs
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
// V1.11.11    JXN    2013-12-15    �޸����Է��ֵ�BUG
// V1.14.16    JXN    2013-12-22    �޸����Է��ֵ�BUG
// V1.18.23    JXN    2013-01-07    �޸����Է��ֵ�BUG
//******************************************************************************
using System;
using System.Collections.Generic;

using CSR.CUIT.GlobalService.ShareLib;
using System.Drawing;
using System.Diagnostics;
using System.Xml;
using CSR.ShareLib;
using TopoEdit.Model;

namespace TopoEdit.Icon
{
    /// <summary>
    /// <para>������ͼԪ��ص����ԺͲ�����</para>
    /// </summary>
    public abstract class IIcon : IDraw
    {
        #region ��Ա����
        /// <summary>
        /// <para>ͼԪ�������ļ������õ�Ĭ����ɫ����ʼֵΪnull��</para>
        /// </summary>
        private TdeColor m_defaultColor = null;
        /// <summary>
        /// <para>��ʾ��ǰ������˸��ͼԪ�����ڿɼ����ǲ��ɼ���״̬����ʼֵΪfalse��</para>
        /// </summary>
        private bool m_flashState = false;
        /// <summary>
        /// <para>ͼԪ�Ƿ���˸����ʼֵΪfalse��</para>
        /// </summary>
        private bool m_flashing = false;
        /// <summary>
        /// <para>ͼԪ����λ����ʼֵΪ0��</para>
        /// </summary>
        private UInt16 m_handle = 0;
        /// <summary>
        /// <para>ͼԪ��״̬����ʼֵΪEmIconState.Dead</para>
        /// </summary>
        private EmIconState m_iconState = EmIconState.Dead;
        /// <summary>
        /// <para>ͼԪ���͡�</para>
        /// </summary>
        private EmIconType m_iconType;
        /// <summary>
        /// <para>ͼԪ�������߼�����ʼֵΪnull��</para>
        /// </summary>
        private LogicTable m_logicTable = null;
        /// <summary>
        /// <para>ͼԪ�������͡�</para>
        /// </summary>
        private string m_iconSubType;
        /// <summary>
        /// �Ƿ���ʾ͸��ɫ
        /// </summary>
        private bool m_visibleTransparentColor = true;
        #endregion

        public override bool VisibleTransparentColor
        {
            get
            {
                return m_visibleTransparentColor;
            }
            set
            {
                m_visibleTransparentColor = value;
            }
        }


        public IIcon(EmIconType type)
        {
            m_iconType = type;
        }

        public override bool Load(XmlNode iconNode)
        {
            Level = int.Parse(iconNode.SelectSingleNode("Level").InnerText);
            m_iconSubType = iconNode.SelectSingleNode("Type").InnerText;

            Int32 colorIndex = ColorLib.Instance.GetColorIndexByName(iconNode.SelectSingleNode("ColorIndex").InnerText); //colorIndex
            m_defaultColor = ColorLib.Instance.GetColor(colorIndex);
            Debug.Assert(m_defaultColor != null);

            LoadDock(iconNode, "DockLeft", DockType.Left);
            LoadDock(iconNode, "DockRight", DockType.Right);
            LoadDock(iconNode, "DockTop", DockType.Top);
            LoadDock(iconNode, "DockBottom", DockType.Bottom);
           
            XmlNode fixedNode = iconNode.SelectSingleNode("Fixed");
            if (fixedNode != null)
            {
                Fixed = bool.Parse(fixedNode.InnerText);
            }
            else
            {
                Fixed = true;
            }

             XmlNodeList expressionNodes = iconNode.SelectNodes("LogicTable/ComplexLogicTable/LogicExpression");
             if (expressionNodes.Count > 0)
             {
                 m_logicTable = new LogicTable(false);
                 m_logicTable.Load(iconNode);
             }
            return true;
        }

        private void LoadDock(XmlNode iconNode, string dockElementName, DockType dockType)
        {
            XmlNode dockNode = iconNode.SelectSingleNode(dockElementName);
            if (dockNode != null)
            {
                Dock[dockType] = bool.Parse(dockNode.InnerText);
            }
            else
            {
                Dock[dockType] = false;
            }
        }
        public override bool Save(XmlNode iconNode)
        {
            iconNode.Attributes.Append(iconNode.OwnerDocument.CreateAttribute("Index"));
            iconNode.Attributes["Index"].InnerText = "1";
            string defaultColor = "��ɫ";
            if (m_defaultColor != null)
            {
                defaultColor = m_defaultColor.Name;
            }
            iconNode.InnerXml = "<Level>" + Level + "</Level><Type>" + m_iconSubType + "</Type><ColorIndex>" + defaultColor + "</ColorIndex>";

            //����DOCK��FIXED
            iconNode.InnerXml += "<DockLeft>" + Dock[DockType.Left].ToString().ToLower() + "</DockLeft><DockRight>"
                + Dock[DockType.Right].ToString().ToLower() + "</DockRight><DockTop>"
                + Dock[DockType.Top].ToString().ToLower() + "</DockTop><DockBottom>"
                + Dock[DockType.Bottom].ToString().ToLower() + "</DockBottom>"
                + "<Fixed>" + Fixed.ToString().ToLower() + "</Fixed>";
            //�����߼���
            if (null != m_logicTable)
            {
                m_logicTable.Save(iconNode);
            }

            return true;
        }

        public  override void Copy(IDraw src)
        {
            if (src is IIcon)
            {
                IIcon icon = src as IIcon;
                this.m_defaultColor = icon.m_defaultColor;
                this.m_flashing = icon.m_flashing;
                this.m_flashState = icon.m_flashState;
                this.m_handle = icon.m_handle;
                this.m_iconState = icon.m_iconState;
                this.m_iconSubType = icon.m_iconSubType;
                this.m_iconType = icon.IconType;
                this.Level = icon.Level;

                foreach (DockType dockType in Enum.GetValues(typeof(DockType)))
                {
                    this.Dock[dockType] = icon.Dock[dockType];
                }

                this.Fixed = icon.Fixed;
                if (icon.m_logicTable != null)
                {
                    this.m_logicTable = (LogicTable)icon.m_logicTable.Clone();
                }

                this.Visible = icon.Visible;
            }
            else
            {
                throw new ArgumentException("������ͼԪ��Ŀ��ͼԪ���Ͳ�����", "src");
            }
        }

        #region public����

        //���õ�ǰͼԪ������ ������ͨ����ͼԪ��ȱʡ��ɫ����
        /// <summary>
        /// <para>��û�����ͼԪ��״̬��</para>
        /// </summary>
        /// <remarks>
        /// <para> ������ 2013-10-08  ��������</para>
        /// </remarks>
        public EmIconState IconState
        {
            get
            {
                return m_iconState;
            }
            set
            {
                m_iconState = value;
            }
        }
        /// <summary>
        /// <para>���ͼԪ�������͡�</para>
        /// </summary>
        /// <remarks>
        /// <para> ������ 2013-10-08  ��������</para>
        /// </remarks>
        public string IconSubType
        {
            get
            {
                return m_iconSubType;
            }
            set
            {
                m_iconSubType = value;
            }
        }
        #endregion

        #region internal����
        /// <summary>
        /// <para>���ͼԪ�ĵ�ǰ������վ��ͼ�����ϵ���ɫ��</para>
        /// </summary>
        /// <remarks>
        /// <para> ������ 2013-10-08  ��������</para>
        /// </remarks>
        public Color DisplayColor
        {
            get
            {
                return Color.Black;
            }
        }
        /// <summary>
        /// <para>���ͼԪ�����͡�</para>
        /// </summary>
        /// <remarks>
        /// <para> ������ 2013-10-08  ��������</para>
        /// </remarks>
        public EmIconType IconType
        {
            get
            {
                return m_iconType;
            }
        }
        /// <summary>
        /// <para>��û�����ͼԪ��ǰ��������˸�еĿɼ����ǲ��ɼ���״̬�����ڸ�������ͼԪ����˸Ч����ֻ����ͼԪ��������˸״̬ʱ����ֵ�����塣</para>
        /// </summary>
        /// <remarks>
        /// <para> ������ 2013-10-08  ��������</para>
        /// </remarks>
        public Boolean FlashState
        {
            set
            {
                m_flashState = value;
            }
            get
            {
                return m_flashState;
            }
        }
        
        #endregion

        #region private����

        /// <summary>
        /// <para>���ͼԪ��Ĭ����ɫ��</para>
        /// </summary>
        /// <remarks>
        /// <para> ������ 2013-10-08  ��������</para>
        /// </remarks>
        public TdeColor DefaultColor
        {
            get
            {
                if (m_defaultColor == null)
                {
                    m_defaultColor = ColorLib.Instance.GetDefaultColor();
                }

                return m_defaultColor;
            }
            set
            {
                m_defaultColor = value;
            }
        }

        public Color GetDisplayColor()
        {
            if (DefaultColor.ColorInArgb.A == 0)
            {
                //͸��ɫ
                if (m_visibleTransparentColor)
                {
                    return Color.White;
                }
                else
                {
                    return DefaultColor.ColorInArgb;
                }
            }
            else
            {
                return DefaultColor.ColorInArgb;
            }
        }
        /// <summary>
        /// <para>��û�����ͼԪ��ǰ�Ƿ���˸��</para>
        /// </summary>
        /// <remarks>
        /// <para> ������ 2013-10-08  ��������</para>
        /// </remarks>
        public Boolean Flashing
        {
            set
            {
                if (value != m_flashing)
                {
                    m_flashing = value;
                }
                else
                {
                    //��������
                }
            }
            get
            {
                return m_flashing;
            }
        }
        #endregion

        #region IDraw ��Ա

        #endregion

        public LogicTable LogicTableItem
        {
            get { return m_logicTable; }
            set { m_logicTable = value; }
        }
    }

}
