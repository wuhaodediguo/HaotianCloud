using System;
using System.ComponentModel.DataAnnotations;
using Chloe.Annotations;

namespace HaotianCloud.Domain.VehicleManage
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2020-06-09 19:42
    /// 描 述：驾驶舱- 挖掘机 一个舱可以绑定多个挖掘机
    /// </summary>
    [TableAttribute("cockpit_excavator_relation")]
    public class Cockpit_excavator_relationEntity : IEntity<Devicechn_infoEntity>,ICreationAudited,IModificationAudited,IDeleteAudited
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
        [Required(ErrorMessage= "驾驶舱设备号不能为空")]
        public string DeviceNo { get; set; }
        public string DeviceName { get; set; } //摄像机的种类
        public string Model { get; set; }
        public string Brand { get; set; }
        
        public string chnNumber { get; set; }
        public string excavaotr_F_Id { get; set; }
        public string cockpit_F_Id { get; set; }//

        public int EnabledMark { get; set; }
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
