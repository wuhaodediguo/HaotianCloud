using System;
using System.ComponentModel.DataAnnotations;
using Chloe.Annotations;

namespace HaotianCloud.Domain.VehicleManage
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2021-11-09 11:32
    /// 描 述：设备实体类
    /// </summary>
    [TableAttribute("device_info")]
    public class device_infoEntity
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [AutoIncrement]
        [Column("ID", IsPrimaryKey = true)]
        public int ID { get; set; }
        /// <summary>
        /// 设备编号,唯一标识
        /// </summary>
        /// <returns></returns>
        public string DevIDNO { get; set; }
        /// <summary>
        /// 所属公司ID(表jt808_company列ID)
        /// </summary>
        /// <returns></returns>
        public int? CompanyID { get; set; }
        /// <summary>
        /// SIM卡号
        /// </summary>
        /// <returns></returns>
        public string SimCard { get; set; }
        /// <summary>
        /// 设备序列号
        /// </summary>
        /// <returns></returns>
        public string SerialID { get; set; }
        /// <summary>
        /// 产品品牌
        /// </summary>
        /// <returns></returns>
        public string Brand { get; set; }
        /// <summary>
        /// 设备型号
        /// </summary>
        /// <returns></returns>
        public string Model { get; set; }
        /// <summary>
        /// 软件版本
        /// </summary>
        /// <returns></returns>
        public string SoftwareVer { get; set; }
        /// <summary>
        /// 硬件版本
        /// </summary>
        /// <returns></returns>
        public string HardwareVer { get; set; }
        /// <summary>
        /// 产品商家
        /// </summary>
        /// <returns></returns>
        public string Factory { get; set; }
        /// <summary>
        /// 设备类型,0为其他,1部标机,2北斗终端,3视频终端
        /// </summary>
        /// <returns></returns>
        public int? DevType { get; set; }
        /// <summary>
        /// 设备子类型
        /// </summary>
        /// <returns></returns>
        public int? DevSubType { get; set; }
        /// <summary>
        /// 通信协议类型
        /// </summary>
        /// <returns></returns>
        public int? Protocol { get; set; }
        /// <summary>
        /// 硬盘类型，1 SD，2 硬盘，3 SSD
        /// </summary>
        /// <returns></returns>
        public int? DiskType { get; set; }
        /// <summary>
        /// 音频类型，双向对讲用
        /// </summary>
        /// <returns></returns>
        public int? AudioCodec { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        /// <returns></returns>
        public string Remark { get; set; }
        /// <summary>
        /// 安装  1已安装  0未安装
        /// </summary>
        /// <returns></returns>
        public bool? Install { get; set; }
        /// <summary>
        /// 安装日期
        /// </summary>
        /// <returns></returns>
        public DateTime? InstallTime { get; set; }
        /// <summary>
        /// 月流量超限：0超限后忽略，1表示超限后禁用媒体业务,同步dev_flow_config表
        /// </summary>
        /// <returns></returns>
        public int? nFlowLimitType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DateTime? UpdateTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int? NetAddrType { get; set; }
        /// <summary>
        /// 厂家类型
        /// </summary>
        /// <returns></returns>
        public int? FactoryType { get; set; }
        /// <summary>
        /// 厂家设备类型
        /// </summary>
        /// <returns></returns>
        public int? FactoryDevType { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        /// <returns></returns>
        public string Password { get; set; }
        /// <summary>
        /// 是否启用 0 启用 1禁用
        /// </summary>
        /// <returns></returns>
        public bool? UseStatus { get; set; }
        /// <summary>
        /// 服务器端口
        /// </summary>
        /// <returns></returns>
        public int? ServerPort { get; set; }
        /// <summary>
        /// 协议类型（0未知、1 808GPS， 2 .808视频） 
        /// </summary>
        /// <returns></returns>
        public int? ProtocolType { get; set; }
        /// <summary>
        /// 拾音器数量 
        /// </summary>
        /// <returns></returns>
        public int? PickupNumber { get; set; }
        /// <summary>
        /// 摄像头数目 
        /// </summary>
        /// <returns></returns>
        public int? CameraNumber { get; set; }
        /// <summary>
        /// 服务器地址
        /// </summary>
        /// <returns></returns>
        public string ServerAddress { get; set; }
        /// <summary>
        /// 运输企业
        /// </summary>
        /// <returns></returns>
        public string TransportEnterprise { get; set; }
        /// <summary>
        /// 接入线路方式
        /// </summary>
        /// <returns></returns>
        public string AccessLineWay { get; set; }
        /// <summary>
        /// 安装地点
        /// </summary>
        /// <returns></returns>
        public string InstallationPlace { get; set; }
        /// <summary>
        /// 录像存储介质类型
        /// </summary>
        /// <returns></returns>
        public string StorageMediumType { get; set; }
        /// <summary>
        /// 型号
        /// </summary>
        /// <returns></returns>
        public string StorageMediumModel { get; set; }
        /// <summary>
        /// 容量
        /// </summary>
        /// <returns></returns>
        public string StorageMediumSize { get; set; }
        /// <summary>
        /// 设备所用SIM卡的集成电路卡识别码（ICCID）编码
        /// </summary>
        /// <returns></returns>
        public string SIMMolectronCode { get; set; }
        /// <summary>
        /// imei
        /// </summary>
        /// <returns></returns>
        public string IMEICode { get; set; }
        /// <summary>
        /// 扩展协议,0:无, 1:苏标
        /// </summary>
        /// <returns></returns>
        public int? ExtendedProtocol { get; set; }
        /// <summary>
        /// 省域ID
        /// </summary>
        /// <returns></returns>
        public int? ProvinceId { get; set; }
        /// <summary>
        /// 市县ID
        /// </summary>
        /// <returns></returns>
        public int? CityId { get; set; }
        /// <summary>
        /// 制造商ID
        /// </summary>
        /// <returns></returns>
        public string ManufacturerId { get; set; }
        /// <summary>
        /// 登录账户
        /// </summary>
        /// <returns></returns>
        public string loginAccount { get; set; }
        /// <summary>
        /// 登录密码
        /// </summary>
        /// <returns></returns>
        public string loginPwd { get; set; }
        /// <summary>
        /// 国标编码
        /// </summary>
        /// <returns></returns>
        public string GbCode { get; set; }
        /// <summary>
        /// Mac地址
        /// </summary>
        /// <returns></returns>
        public string MacAddress { get; set; }
        /// <summary>
        /// 生产日期
        /// </summary>
        /// <returns></returns>
        public DateTime? ProductionDate { get; set; }
        /// <summary>
        /// 安装日期
        /// </summary>
        /// <returns></returns>
        public DateTime? InstallDateTime { get; set; }
        /// <summary>
        /// 否有集群对讲功能 0:否 1:是 
        /// </summary>
        /// <returns></returns>
        public bool? Ptt { get; set; }
        /// <summary>
        /// 不解析GPS双设备时使用 1 不解析  
        /// </summary>
        /// <returns></returns>
        public bool? parseGps { get; set; }
        /// <summary>
        /// 检验标准规范
        /// </summary>
        /// <returns></returns>
        public string inspection_standards { get; set; }
        /// <summary>
        /// 检验合格日期
        /// </summary>
        /// <returns></returns>
        public DateTime? inspection_date { get; set; }
        /// <summary>
        /// 人脸库协议,默认无，数值和主动安全协议匹配,18湘标
        /// </summary>
        /// <returns></returns>
        public int? face_protocol { get; set; }
        /// <summary>
        /// 对讲物理通道,默认不填, 范围 0-15
        /// </summary>
        /// <returns></returns>
        public int? talk_physical_channel { get; set; }

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
