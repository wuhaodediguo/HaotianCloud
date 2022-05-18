using System;
using System.ComponentModel.DataAnnotations;
using Chloe.Annotations;

namespace HaotianCloud.Domain.VehicleManage
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2021-11-25 10:07
    /// 描 述：车辆管理实体类
    /// </summary>
    [TableAttribute("vehicle_info")]
    public class vehicle_infoEntity
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [AutoIncrement]
        [Column("ID", IsPrimaryKey = true)]
        public int ID { get; set; }
        /// <summary>
        /// 车牌号,唯一标识
        /// </summary>
        /// <returns></returns>
        public string VehiIDNO { get; set; }

        public string VehiName { get; set; }
        /// <summary>
        /// 所属公司ID(表jt808_company列ID)
        /// </summary>
        /// <returns></returns>
        public string CompanyID { get; set; }
        /// <summary>
        /// 设备类型,0为其他,1部标机
        /// </summary>
        /// <returns></returns>
        public int DevType { get; set; }
        /// <summary>
        /// 使用状态,0为正常,1为维修,2为停用,3为欠费
        /// </summary>
        /// <returns></returns>
        public int UseStatus { get; set; }
        /// <summary>
        /// 车牌类型,0为其他,1为黄牌,2为蓝牌,3为黑牌,4为白牌
        /// </summary>
        /// <returns></returns>
        public int PlateType { get; set; }
        /// <summary>
        /// 车辆颜色
        /// </summary>
        /// <returns></returns>
        public string VehiColor { get; set; }
        /// <summary>
        /// 车辆品牌
        /// </summary>
        /// <returns></returns>
        public string VehiBand { get; set; }
        /// <summary>
        /// 车辆类型
        /// </summary>
        /// <returns></returns>
        public string VehiType { get; set; }
        /// <summary>
        /// 车辆用途
        /// </summary>
        /// <returns></returns>
        public string VehiUse { get; set; }
        /// <summary>
        /// 司机ID,关联司机表
        /// </summary>
        /// <returns></returns>
        public string DriverId { get; set; }
        /// <summary>
        /// 生产日期
        /// </summary>
        /// <returns></returns>
        public DateTime? DateProduct { get; set; }
        /// <summary>
        /// 图标类型，区分出货车或者客运车
        /// </summary>
        /// <returns></returns>
        public int? Icon { get; set; }
        /// <summary>
        /// 通道数目
        /// </summary>
        /// <returns></returns>
        public int? ChnCount { get; set; }
        /// <summary>
        /// 通道名称
        /// </summary>
        /// <returns></returns>
        public string ChnName { get; set; }
        /// <summary>
        /// IO输入的数目
        /// </summary>
        /// <returns></returns>
        public int? IOInCount { get; set; }
        /// <summary>
        /// IO输入名称
        /// </summary>
        /// <returns></returns>
        public string IOInName { get; set; }
        /// <summary>
        /// IO输出的数目
        /// </summary>
        /// <returns></returns>
        public int? IOOutCount { get; set; }
        /// <summary>
        /// IO输出名称
        /// </summary>
        /// <returns></returns>
        public string IOOutName { get; set; }
        /// <summary>
        /// 温度传感器个数
        /// </summary>
        /// <returns></returns>
        public int? TempCount { get; set; }
        /// <summary>
        /// 温度传感器名称，用,分隔
        /// </summary>
        /// <returns></returns>
        public string TempName { get; set; }
        /// <summary>
        /// 表示外设参数属性，用位表示，每位表示一种外设, 0-视频,1-油量控制,2-电路控制,3-tts语音,4-对讲,5-抓拍,6-监听
        /// </summary>
        /// <returns></returns>
        public int? Module { get; set; }
        /// <summary>
        /// 是否启用缴费功能
        /// </summary>
        /// <returns></returns>
        public bool? PayEnable { get; set; }
        /// <summary>
        /// 合约开始日
        /// </summary>
        /// <returns></returns>
        public DateTime? PayBegin { get; set; }
        /// <summary>
        /// 合约期限（以月为单位）
        /// </summary>
        /// <returns></returns>
        public string PayPeriod { get; set; }
        /// <summary>
        /// 已付费
        /// </summary>
        /// <returns></returns>
        public string PayMonth { get; set; }
        /// <summary>
        /// 宽限天数
        /// </summary>
        /// <returns></returns>
        public string PayDelayDay { get; set; }
        /// <summary>
        /// 存储天数(0, 3, 7, 30)
        /// </summary>
        /// <returns></returns>
        public string StoDay { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DateTime? UpdateTime { get; set; }
        /// <summary>
        /// 保险失效日期
        /// </summary>
        /// <returns></returns>
        public DateTime? SafeEndDate { get; set; }
        /// <summary>
        /// 行驶证编号
        /// </summary>
        /// <returns></returns>
        public string DrivingNum { get; set; }
        /// <summary>
        /// 行驶证到期时间
        /// </summary>
        /// <returns></returns>
        public DateTime? DrivingEndDate { get; set; }
        /// <summary>
        /// 运营证编号
        /// </summary>
        /// <returns></returns>
        public string OperatingNum { get; set; }
        /// <summary>
        /// 运营证到期时间
        /// </summary>
        /// <returns></returns>
        public DateTime? OperatingEndDate { get; set; }
        /// <summary>
        /// 安装日期
        /// </summary>
        /// <returns></returns>
        public DateTime? InstallTime { get; set; }
        /// <summary>
        /// 车辆简称或者别名
        /// </summary>
        /// <returns></returns>
        public string Abbreviation { get; set; }
        /// <summary>
        /// 载重
        /// </summary>
        /// <returns></returns>
        public double? Loading { get; set; }
        /// <summary>
        /// 更多内容ID,对应wh_vehicle_info ID字段
        /// </summary>
        /// <returns></returns>
        public string MoreInfoID { get; set; }
        /// <summary>
        /// 车辆等级
        /// </summary>
        /// <returns></returns>
        public string VehicleGrade { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        /// <returns></returns>
        public string Remark { get; set; }
        /// <summary>
        /// 车辆自定义菜单
        /// </summary>
        /// <returns></returns>
        public string CustomizeMenu { get; set; }
        /// <summary>
        /// 公众号车辆登录时候存储OpenId
        /// </summary>
        /// <returns></returns>
        public string WxOpenid { get; set; }
        /// <summary>
        /// 体积
        /// </summary>
        /// <returns></returns>
        public string transport_volume { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        /// <returns></returns>
        public bool? F_EnabledMark { get; set; }
        /// <summary>
        /// 删除标志
        /// </summary>
        /// <returns></returns>
        public bool? F_DeleteMark { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DateTime? F_CreatorTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string F_CreatorUserId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DateTime? F_LastModifyTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string F_LastModifyUserId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DateTime? F_DeleteTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string F_DeleteUserId { get; set; }

    }


   



}
