//******************************************************************************
//文件名称 :     ExpressionCalculator.cs
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
//******************************************************************************
using System;
using System.Collections.Generic;
using System.Diagnostics;
using CSR.CUIT.GlobalService.ShareLib;
using CSR.ShareLib;

namespace CSR.CUIT.Model
{
    /// <summary>
    /// <para>表达式操作符集合。</para>
    /// </summary>
    internal class ExpressionCharCollection : List<Char>
    {
        /// <summary>
        /// <para>在列表中增加操作符项。</para>
        /// <para>前置条件：</para>
        /// <para>    无</para>
        /// <para>后置条件：</para>
        /// <para>    列表中增加 + * ( ) # ! 6个操作符。</para>
        /// </summary>
        /// <returns>
        /// <para></para>
        /// </returns>
        /// <remarks>
        /// <para> 蒋湘宁 2013-10-08  创建函数</para>
        /// </remarks>
        internal ExpressionCharCollection()
        {
            Add('+');
            Add('*');
            Add('(');
            Add(')');
            Add('#');
            Add('!');
        }
    }
    /// <summary>
    /// <para>提供一个回调方法，用于根据码位获取最新状态。</para>
    /// </summary>
    public delegate Boolean GetStatusCallback(string value);
    public static class ExpressionCalculator
    {
        /// <summary>
        /// <para>操作符列表</para>
        /// </summary>
        private static ExpressionCharCollection charCollection = new ExpressionCharCollection();

        //private ExpressionCalculator()
        //{
        //}

        public static Int32 Calculate(String expression, GetStatusCallback GetStatus, out Boolean re)
        {
            List<String> tokenCollection;
            GenerateTokenArray(expression, out tokenCollection);
            return Calculate(tokenCollection, GetStatus, out re);
        }
        /// <summary>
        /// <para>计算表达式的值</para>
        /// <para>前置条件：</para>
        /// <para>    tokenCollection GetStatus 不为空</para>
        /// <para>后置条件：无</para>
        /// </summary>
        /// <param name="tokenCollection">视图对象。</param>
        /// <param name="GetStatus">GetStatusCallback委托的实现</param>
        /// <param name="re">表达式的计算结果</param>
        /// <returns>
        /// <para>无</para>
        /// </returns>
        /// <remarks>
        /// <para> 蒋湘宁 2013-10-08  创建函数</para>
        /// </remarks>
        public static Int32 Calculate(List<String> tokenCollection, GetStatusCallback GetStatus, out Boolean re)
        {
            Debug.Assert(GetStatus != null);
            re = false;
            Int32[,] OpRelationTable = { {  1, -1, -1,  1,  1, -1 },
                {  1,  1, -1,  1,  1, -1 },
                { -1, -1, -1,  0, -2, -1 },
                {  1,  1, -2,  1,  1,  1 },
                { -1, -1, -1, -2,  0, -1 },
                {  1,  1, -1,  1,  1, -1 }
            };


            // calculate expression
            Stack<String> opCollection = new Stack<String>();
            Stack<Boolean> resultCollection = new Stack<Boolean>();//	resultCollection:存储数字；opCollection:存储运算符
            tokenCollection.Add("#");
            opCollection.Push("#");

            Boolean result = false;
            Int32 tokenIndex = 0;
            while (opCollection.Count != 0)
            {
                if (tokenCollection[tokenIndex].IndexOfAny(charCollection.ToArray()) == -1)
                {
                    //cout << "resultCollection.push :" << tokenCollection[i] << '\n';
                    string t = tokenCollection[tokenIndex++];
                   
                    Boolean status = GetStatus(t);
                    resultCollection.Push(status);//非运算符，压入resultCollection，运算符则进入下一步处理
                }
                else
                {
                    if (opCollection.Count == 0)
                    {
                        //cout << "opCollection empty!\n";
                        continue;
                    }

                    // cout << "debug" << coll[opCollection.top()] << ":" << coll[tokenCollection[i]] << '\n';

                    Int32 rowIndex = charCollection.IndexOf(Char.Parse(opCollection.Peek()));
                    Int32 columnIndex = charCollection.IndexOf(Char.Parse(tokenCollection[tokenIndex]));
                    if (OpRelationTable[rowIndex, columnIndex] == 0)
                    {
                        //"()"和"##"
                        //cout << "opCollection.pop :" << opCollection.top() << '\n';
                        if (opCollection.Count != 0)
                            opCollection.Pop();
                        tokenIndex++;
                    }
                    else if (OpRelationTable[rowIndex, columnIndex] == -1)
                    {
                        //cout << "opCollection.push" << tokenCollection[i] << '\n';
                        //opCollection.top()优先级低于tokenCollection[i]，压入高优先级
                        opCollection.Push(tokenCollection[tokenIndex++]);
                    }
                    else if (OpRelationTable[rowIndex, columnIndex] == 1)
                    {
                        //opCollection.top()优先级等于tokenCollection[i]
                        string op = "";
                        Boolean first, second;
                        if (opCollection.Count != 0)
                        {
                            op = opCollection.Pop();//取出运算符
                        }
                        //else
                        //{
                        //    return -1;
                        //}

                        if (op.Equals("!"))
                        {
                            if (resultCollection.Count != 0)
                            {
                                first = resultCollection.Pop();//若第一个操作符为！，则弹出顶端数字
                            }
                            else
                            {
                                return -1;
                            }

                            result = !first;
                            resultCollection.Push(result);//取反后再次压入
                        }
                        else
                        {

                            if (resultCollection.Count != 0)
                            {
                                first = resultCollection.Pop();//弹出第一个数字
                            }
                            else
                            {
                                return -1;
                            }

                            if (resultCollection.Count != 0)
                            {
                                second = resultCollection.Pop();//弹出第二个数字
                            }
                            else
                            {
                                return -1;
                            }

                            switch (op)
                            {
                            case "+":
                                result = first || second;
                                break;

                            case "*":
                                result = first && second;
                                break;

                            default:
                                break;//未知操作符

                            }
                            resultCollection.Push(result);
                        }
                    }
                    else
                    {
                        return -1;//表达式有问题
                    }

                }
            }

            if (resultCollection.Count != 1)
            {
                return -1; //表达式有问题
            }
            else
            {
                re = resultCollection.Peek();

            }

            return 0;
        }
        /// <summary>
        /// <para>解析表达式，将其操作符和操作数都分解成单个的项，存入string列表中。</para>
        /// <para>前置条件：</para>
        /// <para>    无</para>
        /// <para>后置条件：无</para>
        /// </summary>
        /// <param name="expression">待解析的表达式</param>
        /// <param name="tokenCollection">解析结果</param>
        /// <returns>
        /// <para>0：解析成功</para>
        /// <para>非0：解析失败</para>
        /// </returns>
        /// <remarks>
        /// <para> 蒋湘宁 2013-10-08  创建函数</para>
        /// <para> 蒋湘宁 2013-12-21  修复函数，不再直接调用GenerateTokenArray(String,List,List)</para>
        /// </remarks>
        public static Int32 GenerateTokenArray(String expression, out List<String> tokenCollection)
        {
            Int32 begId = 0;
            Int32 endId = 0;
            Int32 bracketPair = 0;
            tokenCollection = new List<String>();
            endId = expression.IndexOfAny(charCollection.ToArray(), begId);

            while (endId != -1)
            {
                if (endId - begId > 0)
                {
                    String handleTemp = expression.Substring(begId, endId - begId);
                    tokenCollection.Add(handleTemp);
                }
                String opTemp = expression.Substring(endId, 1);
                tokenCollection.Add(opTemp);
                switch (opTemp)
                {
                    case "(":
                        bracketPair++;
                        break;
                    case ")":
                        bracketPair--;
                        break;
                }
                begId = endId + 1;
                if (begId >= expression.Length)
                {
                    break;
                }
                endId = expression.IndexOfAny(charCollection.ToArray(), begId);
            }
            if (endId == -1)
            {
                String handleTemp = expression.Substring(begId);
                tokenCollection.Add(handleTemp);
            }
            if (bracketPair != 0)
            {
                LogManager.InfoLog.LogProcError("计算表达式={0} 发现括号不成对", expression);
                return -2;
            }
            return 0;
        }
        /// <summary>
        /// <para>解析表达式，将其操作符和操作数都分解成单个的项，存入string列表中，将操作数存入Uint16列表中。</para>
        /// <para>前置条件：</para>
        /// <para>    无</para>
        /// <para>后置条件：无</para>
        /// </summary>
        /// <param name="expression">待解析的表达式</param>
        /// <param name="tokenCollection">解析结果</param>
        /// <param name="handleCollection">操作数列表</param>
        /// <returns>
        /// <para>无</para>
        /// </returns>
        /// <remarks>
        /// <para> 蒋湘宁 2013-10-08  创建函数</para>
        /// </remarks>
        internal static Int32 GenerateTokenArray(String expression, out List<String> tokenCollection, out List<UInt16> handleCollection)
        {
            Int32 begId = 0;
            Int32 endId = 0;
            Int32 bracketPair = 0;
            tokenCollection = new List<String>();
            handleCollection = new List<ushort>();
            endId = expression.IndexOfAny(charCollection.ToArray(), begId);

            while (endId != -1)
            {
                if (endId - begId > 0)
                {
                    String handleTemp = expression.Substring(begId, endId - begId);
                    tokenCollection.Add(handleTemp);
                    handleCollection.Add(UInt16.Parse(handleTemp));
                }
                String opTemp = expression.Substring(endId, 1);
                tokenCollection.Add(opTemp);
                switch (opTemp)
                {
                case "(":
                    bracketPair++;
                    break;
                case ")":
                    bracketPair--;
                    break;
                }
                begId = endId + 1;
                if (begId >= expression.Length)
                {
                    break;
                }
                endId = expression.IndexOfAny(charCollection.ToArray(), begId);
            }
            if (endId == -1)
            {
                String handleTemp = expression.Substring(begId);
                tokenCollection.Add(handleTemp);
                handleCollection.Add(UInt16.Parse(handleTemp));
            }
            if (bracketPair != 0)
            {
                LogManager.InfoLog.LogProcError("计算表达式={0} 发现括号不成对", expression);
                return -2;
            }
            return 0;
        }
    }
}
