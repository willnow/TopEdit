//******************************************************************************
//文件名称 :     IIcon.cs
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
// V1.11.11    JXN    2013-12-15    修复测试发现的BUG
// V1.14.16    JXN    2013-12-22    修复测试发现的BUG
// V1.18.23    JXN    2013-01-07    修复测试发现的BUG
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
    /// <para>定义与图元相关的属性和操作。</para>
    /// </summary>
    public abstract class IIcon : IDraw
    {
        #region 成员变量
        /// <summary>
        /// <para>图元在配置文件中配置的默认颜色。初始值为null。</para>
        /// </summary>
        private TdeColor m_defaultColor = null;
        /// <summary>
        /// <para>表示当前正在闪烁的图元正处于可见还是不可见的状态。初始值为false。</para>
        /// </summary>
        private bool m_flashState = false;
        /// <summary>
        /// <para>图元是否闪烁。初始值为false。</para>
        /// </summary>
        private bool m_flashing = false;
        /// <summary>
        /// <para>图元的码位。初始值为0。</para>
        /// </summary>
        private UInt16 m_handle = 0;
        /// <summary>
        /// <para>图元的状态。初始值为EmIconState.Dead</para>
        /// </summary>
        private EmIconState m_iconState = EmIconState.Dead;
        /// <summary>
        /// <para>图元类型。</para>
        /// </summary>
        private EmIconType m_iconType;
        /// <summary>
        /// <para>图元关联的逻辑表。初始值为null。</para>
        /// </summary>
        private LogicTable m_logicTable = null;
        /// <summary>
        /// <para>图元的子类型。</para>
        /// </summary>
        private string m_iconSubType;
        /// <summary>
        /// 是否显示透明色
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
            string defaultColor = "灰色";
            if (m_defaultColor != null)
            {
                defaultColor = m_defaultColor.Name;
            }
            iconNode.InnerXml = "<Level>" + Level + "</Level><Type>" + m_iconSubType + "</Type><ColorIndex>" + defaultColor + "</ColorIndex>";

            //保存DOCK和FIXED
            iconNode.InnerXml += "<DockLeft>" + Dock[DockType.Left].ToString().ToLower() + "</DockLeft><DockRight>"
                + Dock[DockType.Right].ToString().ToLower() + "</DockRight><DockTop>"
                + Dock[DockType.Top].ToString().ToLower() + "</DockTop><DockBottom>"
                + Dock[DockType.Bottom].ToString().ToLower() + "</DockBottom>"
                + "<Fixed>" + Fixed.ToString().ToLower() + "</Fixed>";
            //保存逻辑表
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
                throw new ArgumentException("被拷贝图元和目标图元类型不兼容", "src");
            }
        }

        #region public属性

        //设置当前图元的属性 死、普通。死图元用缺省颜色画。
        /// <summary>
        /// <para>获得或设置图元的状态。</para>
        /// </summary>
        /// <remarks>
        /// <para> 蒋湘宁 2013-10-08  创建属性</para>
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
        /// <para>获得图元的子类型。</para>
        /// </summary>
        /// <remarks>
        /// <para> 蒋湘宁 2013-10-08  创建属性</para>
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

        #region internal属性
        /// <summary>
        /// <para>获得图元的当前绘制在站场图界面上的颜色。</para>
        /// </summary>
        /// <remarks>
        /// <para> 蒋湘宁 2013-10-08  创建属性</para>
        /// </remarks>
        public Color DisplayColor
        {
            get
            {
                return Color.Black;
            }
        }
        /// <summary>
        /// <para>获得图元的类型。</para>
        /// </summary>
        /// <remarks>
        /// <para> 蒋湘宁 2013-10-08  创建属性</para>
        /// </remarks>
        public EmIconType IconType
        {
            get
            {
                return m_iconType;
            }
        }
        /// <summary>
        /// <para>获得或设置图元当前正处于闪烁中的可见还是不可见的状态，用于辅助绘制图元的闪烁效果。只有在图元正处于闪烁状态时，其值有意义。</para>
        /// </summary>
        /// <remarks>
        /// <para> 蒋湘宁 2013-10-08  创建属性</para>
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

        #region private属性

        /// <summary>
        /// <para>获得图元的默认颜色。</para>
        /// </summary>
        /// <remarks>
        /// <para> 蒋湘宁 2013-10-08  创建属性</para>
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
                //透明色
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
        /// <para>获得或设置图元当前是否闪烁。</para>
        /// </summary>
        /// <remarks>
        /// <para> 蒋湘宁 2013-10-08  创建属性</para>
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
                    //有意留空
                }
            }
            get
            {
                return m_flashing;
            }
        }
        #endregion

        #region IDraw 成员

        #endregion

        public LogicTable LogicTableItem
        {
            get { return m_logicTable; }
            set { m_logicTable = value; }
        }
    }

}
