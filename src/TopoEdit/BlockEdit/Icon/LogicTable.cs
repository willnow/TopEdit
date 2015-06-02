//******************************************************************************
//�ļ����� :     LogicTable.cs
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
// V1.8.8    ������    2013-12-12    �޸����Է��ֵ�BUG
// V1.14.16    JXN    2013-12-22    �޸����Է��ֵ�BUG
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
    /// <para>һ��ͼԪӵ��һ���߼��������װ���߼�����صĲ�����</para>
    /// </summary>
    public class LogicTable : ICloneable
    {
        /// <summary>
        /// <para>�������������ʼ���ֶΡ�</para>
        /// <para>ǰ����������</para>
        /// <para>����������</para>
        /// <para>    �ֶ�m_isSimple��������ֵ��</para>
        /// </summary>
        /// <param name="isSimpleLogicTable">�Ƿ���߼���</param>
        /// <returns>
        /// <para></para>
        /// </returns>
        /// <remarks>
        /// <para> ������ 2013-10-08  ��������</para>
        /// </remarks>
        public LogicTable(Boolean isSimpleLogicTable)
        {
            m_isSimple = isSimpleLogicTable;
        }
        /// <summary>
        /// <para>��ʼ���߼���</para>
        /// <para>ǰ��������</para>
        /// <para>    handleList colorList ��Ϊ��</para>
        /// <para>������������</para>
        /// </summary>
        /// <param name="handleList">����SimpleLogicTable������handle�б� ����ComplexLogicTable��������ʽ�б�</param>
        /// <param name="colorList">��ɫ�б�</param>
        /// <returns>
        /// <para>��</para>
        /// </returns>
        /// <remarks>
        /// <para> ������ 2013-10-08  ��������</para>
        /// </remarks>
        public Boolean Initialize(List<String> expList, List<int> colorList)
        {
            Boolean ret;
            if (m_isSimple)
            {
                //��SimgleLogicTable���г�ʼ��
                if (colorList.Count != Math.Pow(2, expList.Count))
                {
                    LogManager.InfoLog.LogProcError("��ʼ��SimpleLogicTable������ɫ�б�����ɫ�ĸ���������2��handle�����η����� handle�б�Ϊ��{0}", expList.ToString());
                    return false;
                }
                foreach (String handleTemp in expList)
                {
                    AddExpression(handleTemp);
                }
            }
            else
            {
                //��ComplexLogicTable���г�ʼ��
                if (expList.Count != colorList.Count)
                {
                    LogManager.InfoLog.LogProcError("��ʼ��ComplexLogiTable�������ʽ�б�ĸ�������ɫ�б�����ɫ�ĸ�������ͬ�� ���ʽ�б�Ϊ��{0}", expList.ToString());
                    return false;//handle��color��Ŀ��һ��
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
                //��ʼ���߼���
                List<String> expList = new List<String>();
                List<int> colorList = new List<int>();

                //��ʼ���߼����еı��ʽ
                foreach (XmlNode expNode in expressionNodes)
                {
                    expList.Add(expNode.SelectSingleNode("Expression").InnerText);
                    colorList.Add(ColorLib.Instance.GetColorIndexByName(expNode.SelectSingleNode("ColorIndex").InnerText));
                }

                Initialize(expList, colorList);
            }
            else
            {
                //��ͼԪû���߼���
            }
        }
        /// <summary>
        /// <para>��ʼ��ʱ����ɫ�б�Color�б���������ڼ��߼���</para>
        /// <para>ǰ��������</para>
        /// <para>    ��</para>
        /// <para>������������</para>
        /// </summary>
        /// <param name="colorIndex">�������б����ɫ����</param>
        /// <returns>
        /// <para>��</para>
        /// </returns>
        /// <remarks>
        /// <para> ������ 2013-10-08  ��������</para>
        /// </remarks>
        private void AddColor(Int32 colorIndex)
        {
            m_colorCollection.Add(colorIndex);
        }
        /// <summary>
        /// <para>�������ʽ�ַ�������ӽ��߼���ı��ʽ�б��С�</para>
        /// <para>ǰ��������</para>
        /// <para>    ��</para>
        /// <para>������������</para>
        /// </summary>
        /// <param name="expression">�������б�ı��ʽ�ַ���</param>
        /// <returns>
        /// <para>��</para>
        /// </returns>
        /// <remarks>
        /// <para> ������ 2013-10-08  ��������</para>
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
        /// <para>�����߼�����ʽ</para>
        /// </summary>
        private List<String> m_complexExpressionCollection = new List<String>();
        /// <summary>
        /// <para>����ͼԪ��������ʾ������</para>
        /// </summary>
        private List<Int32> m_colorCollection = new List<Int32>();
        /// <summary>
        /// <para>��ʶ���߼������ڼ��߼������Ǹ����߼���</para>
        /// </summary>
        private Boolean m_isSimple = true;
    }
}
