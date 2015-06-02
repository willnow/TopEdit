//******************************************************************************
//文件名称 :     EnumType.cs
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
// V1.18.23    JXN    2013-01-07    修复测试发现的BUG
//******************************************************************************

namespace TopoEdit.Icon
{
    public enum EmUpdateBitmapType
    {
        /// <summary>
        /// 更新整张码位表
        /// </summary>
        All = -1,

        /// <summary>
        /// 更新联锁码位
        /// </summary>
        IL = 0, 
        
    }

    /// <summary>
    /// <para>描述图元（ICON）的状态。</para>
    /// </summary>
    public enum EmIconState
    {
        None = 0,
        Dead = 1,
        Normal,
        Focus,

    };
    /// <summary>
    /// <para>表示图元的类型。</para>
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
    /// <para>表示图元的子类型。</para>
    /// </summary>
    public enum EmIconSubType : byte
    {
        None = 0,
        //用于CUIT_TSR_SET_TSR_MSG消息，指示轨道（Rectangle）图元的类型，注意以下三个消息的值是接口的一部分，不能轻易改动
        TrackIcon = 1,//轨道
        SwitchBeforeRailIcon = 2, //岔前
        SwitchNormalIcon = 3,//道岔定位
        SwitchReverseIcon = 4,//道岔反位

        //用于解析临时限速TSR_ATS_SEND_SPEED_MSG消息后判断Icon类型，该Icon只能是TextIcon，用于显示临时限速的速度值
        TrackUpSpeedIcon = 5,
        TrackDownSpeedIcon = 6,
        SwitchBeforeRailSpeedIcon = 7,
        SwitchNormalSpeedIcon = 8,
        SwitchReverseSpeedIcon = 9,
        SpeedIconStartIndex = TrackUpSpeedIcon,
        SpeedIconEndIndex = SwitchReverseSpeedIcon,

        //Train
        TrainOtherIcons = 10,
        TrainIdPartOneTextIcon = 11, //目的地号/表号
        TrainIdPartTwoTextIcon = 12,//车次号/车组号
        TrainIdPartThreeTextIcon,//车次号/车组号
        TrainDriveModeIcon ,//列车驾驶模式
        TrainOffsetTimeIcon,//早晚点时间
        TrainIcon ,
        TrainPLIcon,
        TrainIconStartIndex = TrainOtherIcons, //开始索引
        TrainIconEndIndex = TrainPLIcon,//结束索引

        //名称，TextIcon，用于“设置站场图界面元素的显示与隐藏”
        NameOfCircuit = 18, //区段名称
        NameOfSwitch,//道岔名称
        NameOfSignal,//信号机名称
        NameOfTrack,//轨道名称
        DestCode, //目的地码

        //站台的Icon，为TextIcon
        PlatformDwellTimeIcon = 23, //停站时间
        PlatformPLIcon, //运行等级
        PlatformCountDownIcon, //倒计时
        PlatformSkipStopIcon,//站台跳停Icon
        PlatformDtiIconPlus,//DTI正计时
        PlatformDtiIconMinus,//DTI倒计时
        PlatformDtiIcon,
        PlatformIconStartIndex = PlatformDwellTimeIcon,
        PlatformIconEndIndex = PlatformSkipStopIcon,
    }

}
