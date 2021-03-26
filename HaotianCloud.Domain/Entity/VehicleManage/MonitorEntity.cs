using System;
using System.ComponentModel.DataAnnotations;
using Chloe.Annotations;

namespace HaotianCloud.Domain.VehicleManage
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2020-06-09 19:42
    /// 描 述：控制器管理实体类
    /// </summary>
    [TableAttribute("monitor_info")]
    public class MonitorEntity : IEntity<MonitorEntity>,ICreationAudited,IModificationAudited,IDeleteAudited
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

        [Required(ErrorMessage = "IP地址不能为空")]
        public string IPAddress { get; set; }
        [Required(ErrorMessage = "端口号不能为空")]
        public int port { get; set; }
        public string Subnetmask { get; set; } //子网掩码
        public string gateway { get; set; } //网关
        public string remark2 { get; set; } //备用2

        //public int VideochannelID { get; set; } //视频通道

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
