//******************************************************************************
//�ļ����� :     EnumType.cs
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
// V1.18.23    JXN    2013-01-07    �޸����Է��ֵ�BUG
//******************************************************************************

namespace TopoEdit.Icon
{
    public enum EmUpdateBitmapType
    {
        /// <summary>
        /// ����������λ��
        /// </summary>
        All = -1,

        /// <summary>
        /// ����������λ
        /// </summary>
        IL = 0, 
        
    }

    /// <summary>
    /// <para>����ͼԪ��ICON����״̬��</para>
    /// </summary>
    public enum EmIconState
    {
        None = 0,
        Dead = 1,
        Normal,
        Focus,

    };
    /// <summary>
    /// <para>��ʾͼԪ�����͡�</para>
    /// </summary>
    public enum EmIconType
    {
        None = 0,
        Circle = 1,
        Line,
        Rectangle,
        Polygon,
        Text
    };
    /// <summary>
    /// <para>��ʾͼԪ�������͡�</para>
    /// </summary>
    public enum EmIconSubType : byte
    {
        None = 0,
        //����CUIT_TSR_SET_TSR_MSG��Ϣ��ָʾ�����Rectangle��ͼԪ�����ͣ�ע������������Ϣ��ֵ�ǽӿڵ�һ���֣��������׸Ķ�
        TrackIcon = 1,//���
        SwitchBeforeRailIcon = 2, //��ǰ
        SwitchNormalIcon = 3,//����λ
        SwitchReverseIcon = 4,//����λ

        //���ڽ�����ʱ����TSR_ATS_SEND_SPEED_MSG��Ϣ���ж�Icon���ͣ���Iconֻ����TextIcon��������ʾ��ʱ���ٵ��ٶ�ֵ
        TrackUpSpeedIcon = 5,
        TrackDownSpeedIcon = 6,
        SwitchBeforeRailSpeedIcon = 7,
        SwitchNormalSpeedIcon = 8,
        SwitchReverseSpeedIcon = 9,
        SpeedIconStartIndex = TrackUpSpeedIcon,
        SpeedIconEndIndex = SwitchReverseSpeedIcon,

        //Train
        TrainOtherIcons = 10,
        TrainIdPartOneTextIcon = 11, //Ŀ�ĵغ�/���
        TrainIdPartTwoTextIcon = 12,//���κ�/�����
        TrainIdPartThreeTextIcon,//���κ�/�����
        TrainDriveModeIcon ,//�г���ʻģʽ
        TrainOffsetTimeIcon,//�����ʱ��
        TrainIcon ,
        TrainPLIcon,
        TrainIconStartIndex = TrainOtherIcons, //��ʼ����
        TrainIconEndIndex = TrainPLIcon,//��������

        //���ƣ�TextIcon�����ڡ�����վ��ͼ����Ԫ�ص���ʾ�����ء�
        NameOfCircuit = 18, //��������
        NameOfSwitch,//��������
        NameOfSignal,//�źŻ�����
        NameOfTrack,//�������
        DestCode, //Ŀ�ĵ���

        //վ̨��Icon��ΪTextIcon
        PlatformDwellTimeIcon = 23, //ͣվʱ��
        PlatformPLIcon, //���еȼ�
        PlatformCountDownIcon, //����ʱ
        PlatformSkipStopIcon,//վ̨��ͣIcon
        PlatformDtiIconPlus,//DTI����ʱ
        PlatformDtiIconMinus,//DTI����ʱ
        PlatformDtiIcon,
        PlatformIconStartIndex = PlatformDwellTimeIcon,
        PlatformIconEndIndex = PlatformSkipStopIcon,
    }

}
