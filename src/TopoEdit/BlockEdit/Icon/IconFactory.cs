//******************************************************************************
//�ļ����� :     IconFactory.cs
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
//******************************************************************************
using System;
using System.Collections.Generic;
using CSR.CUIT.GlobalService.ShareLib;
using CSR.ShareLib;
using System.Xml;

namespace TopoEdit.Icon
{
    /// <summary>
    /// <para>�ṩһ������ͼԪ��ͳһ�ӿڡ�</para>
    /// </summary>
    public class IconFactory
    {
        public static IconFactory Instance = new IconFactory();
        private IconFactory()
        {

        }
        /// <summary>
        /// <para>����ָ��ͼԪ��</para>
        /// <para>ǰ��������</para>
        /// <para>    point��ȡֵ��Χ�ɵ����߱�֤</para>
        /// <para>������������</para>
        /// </summary>
        /// <param name="type">��������ͼԪ���͡�CreateIcon�����ݸ����ͣ����ò�ͬ�Ĺ��캯����</param>
        /// <param name="attributeList">�������ļ��ж�ȡ���������ݳ�ʼ���ַ�������</param>
        /// <param name="logicTable">�߼���</param>
        /// <param name="rtu">��ICON������RTU</param>
        /// <returns>
        /// <para>IIcon���󣺴����ɹ�</para>
        /// <para>null������ʧ��</para>
        /// </returns>
        /// <remarks>
        /// <para> ������ 2013-10-08  ��������</para>
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
                LogManager.InfoLog.LogProcError("��ͼ����δ֪��ICON����");
                return null;
            }
            }
            icon.Load(node);
            return icon;
        }
    }
}
