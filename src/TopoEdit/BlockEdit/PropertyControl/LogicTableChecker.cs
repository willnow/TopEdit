using System;
using System.Collections.Generic;
using System.Text;
using CSR.CUIT.Model;

namespace TopoEdit.PropertyControl
{
    class ExpressionItem
    {
        public string m_name = "";
        public bool m_value = false;

        public string Name
        {
            get
            {
                return m_name;
            }
            set
            {
                m_name = value;
            }
        }

        public bool Value
        {
            get
            {
                return m_value;
            }
            set
            {
                m_value = value;
            }
        }

        public ExpressionItem(string name, bool value)
        {
            Name = name;
            Value = value;
        }
    }

    /// <summary>
    /// 单个逻辑表的校验器
    /// </summary>
    class LogicTableChecker
    {
        private List<string> m_exps = new List<string>();//所有布尔表达式
        private static string m_ops = "+*()#!";
        private List<ExpressionItem> m_itemvalues = new List<ExpressionItem>();//表达式项与表达式值的映射关系，在计算表达式值时使用

        public LogicTableChecker(List<string> exps)
        {
            m_exps = exps;
        }

        /// <summary>
        /// 检查逻辑表是否符合要求：任何一个ICON的逻辑表中的所有布尔表达式，在任何情况下都最多有1个表达式取值为真
        /// </summary>
        /// <returns></returns>
        public bool Validate(out List<ErrorInfo> errorList)
        {
            errorList = new List<ErrorInfo>();
            ResetItem();
            GenerateValue(0, Check, errorList);
            return true;
        }

        /// <summary>
        /// 检查某个逻辑表的所有表达式是否符合要求：任何一个ICON的逻辑表中的所有布尔表达式，在任何情况下都最多有1个表达式取值为真
        /// </summary>
        /// <param name="itemValues"></param>
        private ErrorInfo Check(List<ExpressionItem> itemValues)
        {
            List<string> trueExpList = new List<string>();//布尔表达式计算结果为true的表达式集合
            bool result = false;

            foreach (string exp in m_exps)
            {
                ExpressionCalculator.Calculate(exp, GetItemStatus, out result);
                if (result)
                {
                    trueExpList.Add(exp);
                }
            }

            if (trueExpList.Count != 1)
            {
                //不符合要求

                //构造取值字符串
                string valueStr = "";
                foreach (ExpressionItem item in itemValues)
                {
                    valueStr += "(" + item.Name + "," + item.Value + ")";
                }

                if (trueExpList.Count > 1)
                {
                    //构造布尔表达式计算结果为true的表达式字符串
                    string trueExpStr = "";
                    foreach (string trueExp in trueExpList)
                    {
                        trueExpStr += trueExp + ", ";
                    }

                    return new ErrorInfo(ErrorType.error, "当取值为:" + valueStr + "时，有" + trueExpList.Count + "个表达式结果同时为true:" + trueExpStr);
                }
                else//trueExpList.Count == 0
                {
                    return new ErrorInfo(ErrorType.alarm, "当取值为:" + valueStr + "时，所有表达式结果都为false");
                }
            }
            else
            {
                return new ErrorInfo(ErrorType.info, "");
            }
        }

        private bool IsItemExit(string item)
        {
            foreach (ExpressionItem expItem in m_itemvalues)
            {
                if (expItem.Name == item)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 从所有布尔表达式中解析中所有不重复的项
        /// </summary>
        /// <returns></returns>
        private void ResetItem()
        {
            m_itemvalues.Clear();
            foreach (string exp in m_exps)
            {
                string[] itemsInExp = exp.Split(m_ops.ToCharArray());
                foreach (string item in itemsInExp)
                {
                    if ((item.Trim() != "") && !IsItemExit(item))
                    {
                        m_itemvalues.Add(new ExpressionItem(item, false));
                    }
                }
            }
        }

        /// <summary>
        /// 获取表达式中指定项的值
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private Boolean GetItemStatus(string item)
        {
            foreach (ExpressionItem itemvalue in m_itemvalues)
            {
                if (itemvalue.Name == item)
                {
                    return itemvalue.Value;
                }
            }

            throw new ArgumentException("表达式集合中找不到指定项" + item);
        }

        private delegate ErrorInfo OpValue(List<ExpressionItem> itemValues);

        /// <summary>
        /// 从指定位置开始给指定数组的所有元素赋值
        /// </summary>
        /// <param name="beginIndex">开始赋值位置</param>
        /// <param name="op">当所有元素都赋值后，该回调函数被调用</param>
        private void GenerateValue(int beginIndex, OpValue op, List<ErrorInfo> errorList)
        {
            if (beginIndex < m_itemvalues.Count)
            {
                m_itemvalues[beginIndex].Value = true;//首先赋值为TRUE
                GenerateValue(beginIndex + 1, op, errorList);//首先生成前N个数据

                m_itemvalues[beginIndex].Value = false;//首先赋值为TRUE
                GenerateValue(beginIndex + 1, op, errorList);//首先生成前N个数据
            }
            else
            {
                if (op != null)
                {
                    ErrorInfo error = op(m_itemvalues);
                    if (error.ErrType != ErrorType.info)
                    {
                        errorList.Add(error);
                    }
                }
            }
        }
    }
}
