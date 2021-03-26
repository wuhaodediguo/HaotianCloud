using System;
using System.ComponentModel.DataAnnotations;
using Chloe.Annotations;

namespace HaotianCloud.Domain.VehicleManage
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2020-06-09 19:42
    /// 描 述：视频通道 视频通道 指定“摄像头信息”管理实体类
    /// </summary>
    [TableAttribute("devicechn_info")]
    public class Devicechn_infoEntity : IEntity<Devicechn_infoEntity>,ICreationAudited,IModificationAudited,IDeleteAudited
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        /// <returns></returns>
        //[ColumnAttribute("F_Id", IsPrimaryKey = true)]
        public string F_Id { get; set; }
        /// <summary>
        /// 设备类型
        /// </summary>
        /// <returns></returns>
        [Required(ErrorMessage= "设备类型不能为空")]
        public int DeviceType { get; set; }
        public string CameraType { get; set; } //摄像机的种类
        public string Model { get; set; }
        public string Manufacturer { get; set; }
        public string ChnName { get; set; }
        public string chnNumber { get; set; }

        public string monitiorid { get; set; }//控制器ID
     
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
