using System;
using Chloe.Annotations;
using System.ComponentModel.DataAnnotations;

namespace HaotianCloud.Domain.SystemOrganize
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2020-06-12 13:50
    /// 描 述：系统设置实体类
    /// </summary>
    [TableAttribute("sys_systemset")]
    public class SystemSetEntity : IEntity<SystemSetEntity>,ICreationAudited,IModificationAudited,IDeleteAudited
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [ColumnAttribute("F_Id", IsPrimaryKey = true)]
        public string F_Id { get; set; }
        /// <summary>
        /// Logo图标
        /// </summary>
        /// <returns></returns>
        [Required(ErrorMessage = "Logo图标不能为空")]
        public string F_Logo { get; set; }
        /// <summary>
        /// Logo编号
        /// </summary>
        /// <returns></returns>
        //[Required(ErrorMessage = "Logo编号不能为空")]
        public string F_LogoCode { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        /// <returns></returns>
        [Required(ErrorMessage = "项目名称不能为空")]
        public string F_ProjectName { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>
        /// <returns></returns>
        [Required(ErrorMessage = "公司名称不能为空")]
        public string F_CompanyName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Required(ErrorMessage = "系统账户不能为空")]
        public string F_AdminAccount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Required(ErrorMessage = "系统密码不能为空")]
        public string F_AdminPassword { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool? F_DeleteMark { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool? F_EnabledMark { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string F_Description { get; set; }
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
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Required(ErrorMessage = "联系电话不能为空")]
        public string F_MobilePhone { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Required(ErrorMessage = "联系人不能为空")]
        public string F_PrincipalMan { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Required(ErrorMessage = "到期时间不能为空")]
        public DateTime? F_EndTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string F_DbString { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string F_DBProvider { get; set; }
        [Required(ErrorMessage = "域名不能为空")]
        public string F_HostUrl { get; set; }


        //
       
        public string audioEnable { get; set; }
        public string http_flv_enable { get; set; }
        public string http_flv_uri { get; set; }
        public string http_hls_enable { get; set; }
        public string http_hls_uri { get; set; }
        public string http_ts_enable { get; set; }
        public string http_ts_uri { get; set; }
        public string live_enable { get; set; }
        public string live_publish_url { get; set; }
        public string multicast_enable { get; set; }
        public string multicast_ip { get; set; }
        public string multicast_port { get; set; }
        public string recordEnable { get; set; }
        public string ts_pid { get; set; }
        public string venc_codec { get; set; }

        public string rtmp_publish_auto_stop { get; set; }
        public string rtmp_publish_enable_write { get; set; }
        public string rtmp_publish_stop_port { get; set; }
        public string rtmp_publish_stop_url { get; set; }
        public string transcodeEnable { get; set; }
        [NotMapped]
        public string tongdaolist { get; set; }

        [NotMapped]
        public string rtmp_enable { get; set; }
        [NotMapped]
        public string rtmp_publish_uri { get; set; }
        [NotMapped]
        public string rtsp_enable { get; set; }
        [NotMapped]
        public string rtsp_password { get; set; }
        [NotMapped]
        public string rtsp_uri { get; set; }
        [NotMapped]
        public string rtsp_username { get; set; }

        [NotMapped]
        public string adminkey { get; set; }






    }

    public class sysconfigs
    {
        [NotMapped]
        public string tongdaolist { get; set; }

        [NotMapped]
        public string rtmp_enable { get; set; }
        [NotMapped]
        public string rtmp_publish_uri { get; set; }
        [NotMapped]
        public string rtsp_enable { get; set; }
        [NotMapped]
        public string rtsp_password { get; set; }
        [NotMapped]
        public string rtsp_uri { get; set; }
        [NotMapped]
        public string rtsp_username { get; set; }

        [NotMapped]
        public string adminkey { get; set; }
        [NotMapped]
        public string adminkey1 { get; set; }
        [NotMapped]
        public string IPaddress { get; set; }
        [NotMapped]
        public string NewIPaddress { get; set; }
    }

   
}
