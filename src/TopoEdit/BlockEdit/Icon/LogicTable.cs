//******************************************************************************
//文件名称 :     LogicTable.cs
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
// V1.8.8    蒋湘宁    2013-12-12    修复测试发现的BUG
// V1.14.16    JXN    2013-12-22    修复测试发现的BUG
//******************************************************************************
using System;
using System.Collections.Generic;
using CSR.CUIT.GlobalService.ShareLib;
using System.Diagnostics;
using TopoEdit.Icon;
using CSR.ShareLib;
using System.Xml;
using TopoEdit.Model;

namespace TopoEdit.Icon
{
    /// <summary>
    /// <para>一个图元拥有一张逻辑表，本类封装了逻辑表相关的操作。</para>
    /// </summary>
    public class LogicTable : ICloneable
    {
        /// <summary>
        /// <para>根据输入参数初始化字段。</para>
        /// <para>前置条件：无</para>
        /// <para>后置条件：</para>
        /// <para>    字段m_isSimple被赋予新值。</para>
        /// </summary>
        /// <param name="isSimpleLogicTable">是否简单逻辑表</param>
        /// <returns>
        /// <para></para>
        /// </returns>
        /// <remarks>
        /// <para> 蒋湘宁 2013-10-08  创建函数</para>
        /// </remarks>
        public LogicTable(Boolean isSimpleLogicTable)
        {
            m_isSimple = isSimpleLogicTable;
        }
        /// <summary>
        /// <para>初始化逻辑表。</para>
        /// <para>前置条件：</para>
        /// <para>    handleList colorList 不为空</para>
        /// <para>后置条件：无</para>
        /// </summary>
        /// <param name="handleList">对于SimpleLogicTable，传入handle列表； 对于ComplexLogicTable，传入表达式列表</param>
        /// <param name="colorList">颜色列表</param>
        /// <returns>
        /// <para>无</para>
        /// </returns>
        /// <remarks>
        /// <para> 蒋湘宁 2013-10-08  创建函数</para>
        /// </remarks>
        public Boolean Initialize(List<String> expList, List<int> colorList)
        {
            Boolean ret;
            if (m_isSimple)
            {
                //按SimgleLogicTable进行初始化
                if (colorList.Count != Math.Pow(2, expList.Count))
                {
                    LogManager.InfoLog.LogProcError("初始化SimpleLogicTable出错，颜色列表中颜色的个数，不是2的handle个数次方倍， handle列表为：{0}", expList.ToString());
                    return false;
                }
                foreach (String handleTemp in expList)
                {
                    AddExpression(handleTemp);
                }
            }
            else
            {
                //按ComplexLogicTable进行初始化
                if (expList.Count != colorList.Count)
                {
                    LogManager.InfoLog.LogProcError("初始化ComplexLogiTable出错，表达式列表的个数和颜色列表中颜色的个数不相同， 表达式列表为：{0}", expList.ToString());
                    return false;//handle和color数目不一致
                }
                foreach (String expression in expList)
                {
                    ret = AddExpression(expression);
                    if (!ret)
                    {
                        return false;
                    }
                }
            }
            foreach (int color in colorList)
            {
                AddColor(color);
            }
            return true;
        }

        public void Save(System.Xml.XmlNode iconNode)
        {
            if (m_complexExpressionCollection.Count > 0)
            {
                string xml = "<LogicTable><ComplexLogicTable>";

                for (int i = 0; i < m_complexExpressionCollection.Count; ++i)
                {
                    xml += "<LogicExpression><Expression>" + m_complexExpressionCollection[i]
                        + "</Expression><ColorIndex>" + TopoEdit.Model.ColorLib.Instance.GetColor(m_colorCollection[i]).Name + "</ColorIndex></LogicExpression>";
                }

                xml += "</ComplexLogicTable></LogicTable>";

                iconNode.InnerXml += xml;
            }
        }

        public void Load(System.Xml.XmlNode iconNode)
        {
            XmlNodeList expressionNodes = iconNode.SelectNodes("LogicTable/ComplexLogicTable/LogicExpression");
            if (expressionNodes.Count > 0)
            {
                //初始化逻辑表
                List<String> expList = new List<String>();
                List<int> colorList = new List<int>();

                //初始化逻辑表中的表达式
                foreach (XmlNode expNode in expressionNodes)
                {
                    expList.Add(expNode.SelectSingleNode("Expression").InnerText);
                    colorList.Add(ColorLib.Instance.GetColorIndexByName(expNode.SelectSingleNode("ColorIndex").InnerText));
                }

                Initialize(expList, colorList);
            }
            else
            {
                //该图元没有逻辑表
            }
        }
        /// <summary>
        /// <para>初始化时向颜色列表Color列表增加项，用于简单逻辑表</para>
        /// <para>前置条件：</para>
        /// <para>    无</para>
        /// <para>后置条件：无</para>
        /// </summary>
        /// <param name="colorIndex">待加入列表的颜色索引</param>
        /// <returns>
        /// <para>无</para>
        /// </returns>
        /// <remarks>
        /// <para> 蒋湘宁 2013-10-08  创建函数</para>
        /// </remarks>
        private void AddColor(Int32 colorIndex)
        {
            m_colorCollection.Add(colorIndex);
        }
        /// <summary>
        /// <para>解析表达式字符串并添加进逻辑表的表达式列表中。</para>
        /// <para>前置条件：</para>
        /// <para>    无</para>
        /// <para>后置条件：无</para>
        /// </summary>
        /// <param name="expression">待加入列表的表达式字符串</param>
        /// <returns>
        /// <para>无</para>
        /// </returns>
        /// <remarks>
        /// <para> 蒋湘宁 2013-10-08  创建函数</para>
        /// </remarks>
        private Boolean AddExpression(String expression)
        {
            m_complexExpressionCollection.Add(expression);
            return true;
        }

        public object Clone()
        {
            LogicTable lt = new LogicTable(false);
            lt.m_colorCollection = new List<int>();
            foreach (int color in m_colorCollection)
            {
                lt.m_colorCollection.Add(color);
            }
            
            lt.m_complexExpressionCollection = new List<string>();
            foreach (string expression in m_complexExpressionCollection)
            {
                lt.m_complexExpressionCollection.Add(expression);
            }

            lt.m_isSimple = this.m_isSimple;
            return lt;
        }

        public List<String> ExpressionCollection
        {
            get { return m_complexExpressionCollection; }
        }

        public List<Int32> ColorCollection
        {
            get { return m_colorCollection; }
        }

        /// <summary>
        /// <para>复杂逻辑表表达式</para>
        /// </summary>
        private List<String> m_complexExpressionCollection = new List<String>();
        /// <summary>
        /// <para>保存图元的所有显示方案。</para>
        /// </summary>
        private List<Int32> m_colorCollection = new List<Int32>();
        /// <summary>
        /// <para>标识本逻辑表属于简单逻辑表，还是复杂逻辑表。</para>
        /// </summary>
        private Boolean m_isSimple = true;
    }
}
