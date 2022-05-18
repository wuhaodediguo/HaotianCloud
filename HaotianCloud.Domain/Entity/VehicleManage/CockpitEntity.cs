using System;
using System.ComponentModel.DataAnnotations;
using Chloe.Annotations;

namespace HaotianCloud.Domain.VehicleManage
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2020-06-09 19:42
    /// 描 述：驾驶舱管理实体类
    /// </summary>
    [TableAttribute("cockpit_info")]
    public class CockpitEntity : IEntity<CockpitEntity>,ICreationAudited,IModificationAudited,IDeleteAudited
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        /// <returns></returns>
        [ColumnAttribute("F_Id", IsPrimaryKey = true)]
        public string F_Id { get; set; }
        /// <summary>
        /// 设备号
        /// </summary>
        /// <returns></returns>
        [Required(ErrorMessage= "设备号不能为空")]
        public string DeviceNo { get; set; }
        public string DeviceSeriNo { get; set; }
        public string DeviceName { get; set; }
        /// <summary>
        /// 品牌
        /// </summary>
        /// <returns></returns>
        //[Required(ErrorMessage = "品牌不能为空")]
        public string Brand { get; set; }

        /// <summary>
        /// 型号
        /// </summary>
        /// <returns></returns>
        //[Required(ErrorMessage = "型号不能为空")]
        public string model { get; set; }

        /// <summary>
        /// 长 宽 高
        /// </summary>
        /// <returns></returns>
        public string devicelength { get; set; }
        public string devicewide { get; set; }
        public string devicehigh { get; set; }
        public string channelsNo { get; set; } //通道数
        public string remark1 { get; set; } //备用1

        public string monitorID { get; set; } //遥控控制器ID

        public string excavatorID { get; set; } //挖掘机
        public string Networkmode { get; set; } //网络模式

        [NotMapped]
        //多选显示字段 挖掘机
        public string excavatorName { get; set; }
        [NotMapped]
        //多选显示字段 挖掘机
        public string MonitorName { get; set; }

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
