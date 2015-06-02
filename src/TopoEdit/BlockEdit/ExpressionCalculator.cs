//******************************************************************************
//�ļ����� :     ExpressionCalculator.cs
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
//******************************************************************************
using System;
using System.Collections.Generic;
using System.Diagnostics;
using CSR.CUIT.GlobalService.ShareLib;
using CSR.ShareLib;

namespace CSR.CUIT.Model
{
    /// <summary>
    /// <para>���ʽ���������ϡ�</para>
    /// </summary>
    internal class ExpressionCharCollection : List<Char>
    {
        /// <summary>
        /// <para>���б������Ӳ������</para>
        /// <para>ǰ��������</para>
        /// <para>    ��</para>
        /// <para>����������</para>
        /// <para>    �б������� + * ( ) # ! 6����������</para>
        /// </summary>
        /// <returns>
        /// <para></para>
        /// </returns>
        /// <remarks>
        /// <para> ������ 2013-10-08  ��������</para>
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
    /// <para>�ṩһ���ص����������ڸ�����λ��ȡ����״̬��</para>
    /// </summary>
    public delegate Boolean GetStatusCallback(string value);
    public static class ExpressionCalculator
    {
        /// <summary>
        /// <para>�������б�</para>
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
        /// <para>������ʽ��ֵ</para>
        /// <para>ǰ��������</para>
        /// <para>    tokenCollection GetStatus ��Ϊ��</para>
        /// <para>������������</para>
        /// </summary>
        /// <param name="tokenCollection">��ͼ����</param>
        /// <param name="GetStatus">GetStatusCallbackί�е�ʵ��</param>
        /// <param name="re">���ʽ�ļ�����</param>
        /// <returns>
        /// <para>��</para>
        /// </returns>
        /// <remarks>
        /// <para> ������ 2013-10-08  ��������</para>
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
            Stack<Boolean> resultCollection = new Stack<Boolean>();//	resultCollection:�洢���֣�opCollection:�洢�����
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
                    resultCollection.Push(status);//���������ѹ��resultCollection��������������һ������
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
                        //"()"��"##"
                        //cout << "opCollection.pop :" << opCollection.top() << '\n';
                        if (opCollection.Count != 0)
                            opCollection.Pop();
                        tokenIndex++;
                    }
                    else if (OpRelationTable[rowIndex, columnIndex] == -1)
                    {
                        //cout << "opCollection.push" << tokenCollection[i] << '\n';
                        //opCollection.top()���ȼ�����tokenCollection[i]��ѹ������ȼ�
                        opCollection.Push(tokenCollection[tokenIndex++]);
                    }
                    else if (OpRelationTable[rowIndex, columnIndex] == 1)
                    {
                        //opCollection.top()���ȼ�����tokenCollection[i]
                        string op = "";
                        Boolean first, second;
                        if (opCollection.Count != 0)
                        {
                            op = opCollection.Pop();//ȡ�������
                        }
                        //else
                        //{
                        //    return -1;
                        //}

                        if (op.Equals("!"))
                        {
                            if (resultCollection.Count != 0)
                            {
                                first = resultCollection.Pop();//����һ��������Ϊ�����򵯳���������
                            }
                            else
                            {
                                return -1;
                            }

                            result = !first;
                            resultCollection.Push(result);//ȡ�����ٴ�ѹ��
                        }
                        else
                        {

                            if (resultCollection.Count != 0)
                            {
                                first = resultCollection.Pop();//������һ������
                            }
                            else
                            {
                                return -1;
                            }

                            if (resultCollection.Count != 0)
                            {
                                second = resultCollection.Pop();//�����ڶ�������
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
                                break;//δ֪������

                            }
                            resultCollection.Push(result);
                        }
                    }
                    else
                    {
                        return -1;//���ʽ������
                    }

                }
            }

            if (resultCollection.Count != 1)
            {
                return -1; //���ʽ������
            }
            else
            {
                re = resultCollection.Peek();

            }

            return 0;
        }
        /// <summary>
        /// <para>�������ʽ������������Ͳ��������ֽ�ɵ����������string�б��С�</para>
        /// <para>ǰ��������</para>
        /// <para>    ��</para>
        /// <para>������������</para>
        /// </summary>
        /// <param name="expression">�������ı��ʽ</param>
        /// <param name="tokenCollection">�������</param>
        /// <returns>
        /// <para>0�������ɹ�</para>
        /// <para>��0������ʧ��</para>
        /// </returns>
        /// <remarks>
        /// <para> ������ 2013-10-08  ��������</para>
        /// <para> ������ 2013-12-21  �޸�����������ֱ�ӵ���GenerateTokenArray(String,List,List)</para>
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
                LogManager.InfoLog.LogProcError("������ʽ={0} �������Ų��ɶ�", expression);
                return -2;
            }
            return 0;
        }
        /// <summary>
        /// <para>�������ʽ������������Ͳ��������ֽ�ɵ����������string�б��У�������������Uint16�б��С�</para>
        /// <para>ǰ��������</para>
        /// <para>    ��</para>
        /// <para>������������</para>
        /// </summary>
        /// <param name="expression">�������ı��ʽ</param>
        /// <param name="tokenCollection">�������</param>
        /// <param name="handleCollection">�������б�</param>
        /// <returns>
        /// <para>��</para>
        /// </returns>
        /// <remarks>
        /// <para> ������ 2013-10-08  ��������</para>
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
                LogManager.InfoLog.LogProcError("������ʽ={0} �������Ų��ɶ�", expression);
                return -2;
            }
            return 0;
        }
    }
}
