//******************************************************************************
//文件名称 :     IconFactory.cs
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
//******************************************************************************
using System;
using System.Collections.Generic;
using CSR.CUIT.GlobalService.ShareLib;
using CSR.ShareLib;
using System.Xml;

namespace TopoEdit.Icon
{
    /// <summary>
    /// <para>提供一个构造图元的统一接口。</para>
    /// </summary>
    public class IconFactory
    {
        public static IconFactory Instance = new IconFactory();
        private IconFactory()
        {

        }
        /// <summary>
        /// <para>构造指定图元。</para>
        /// <para>前置条件：</para>
        /// <para>    point的取值范围由调用者保证</para>
        /// <para>后置条件：无</para>
        /// </summary>
        /// <param name="type">待创建的图元类型。CreateIcon将根据该类型，调用不同的构造函数。</param>
        /// <param name="attributeList">从配置文件中读取出来的数据初始化字符串序列</param>
        /// <param name="logicTable">逻辑表</param>
        /// <param name="rtu">该ICON所属的RTU</param>
        /// <returns>
        /// <para>IIcon对象：创建成功</para>
        /// <para>null：创建失败</para>
        /// </returns>
        /// <remarks>
        /// <para> 蒋湘宁 2013-10-08  创建函数</para>
        /// </remarks>
        internal IIcon CreateIcon(EmIconType type, XmlNode node)
        {
            IIcon icon;

            switch (type)
            {
            case EmIconType.Circle:
            {
                icon = new IconCircle();
                break;
            }
            case EmIconType.Line:
            {
                icon = new IconLine();
                break;
            }
            case EmIconType.Polygon:
            {
                icon = new IconPolygon();
                break;
            }
            case EmIconType.Rectangle:
            {
                icon = new IconRectangle();
                break;
            }
            case EmIconType.Text:
            {
                icon = new IconText();
                break;
            }
            default:
            {
                LogManager.InfoLog.LogProcError("试图创建未知的ICON类型");
                return null;
            }
            }
            icon.Load(node);
            return icon;
        }
    }
}
