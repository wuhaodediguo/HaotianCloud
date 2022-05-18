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
    [TableAttribute("cms_reqstate")]
    public class ReqstateEntity : IEntity<ReqstateEntity>,IDeleteAudited
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        /// <returns></returns>
        [ColumnAttribute("F_Id", IsPrimaryKey = true)]
        public string F_Id { get; set; }
        public string wjj { get; set; }
        public string rpm { get; set; }
        public string gear { get; set; }
        public string rpy_yaw { get; set; }
        public string rpy_roll { get; set; }
        public string rpy_pitch { get; set; }
        public string oil_pressure { get; set; }
        public string oil_position { get; set; }
        public string oil_temperature { get; set; }
        public string high_speed_mode { get; set; }
        public string water_temperature { get; set; }
        public string water_temperature_alarm { get; set; }
        public string oil_pressure_alarm { get; set; }
        public string oil_filter_blocked { get; set; }
        public string air_filter_blocked { get; set; }
        public string lead_switch_on { get; set; }

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
