/*******************************************************************************
 * Copyright © 2020 HaotianCloud.Framework 版权所有
 * Author: HaotianCloud
 * Description: HaotianCloud快速开发平台
 * Website：
*********************************************************************************/
using System;

namespace HaotianCloud.Domain
{
    public interface IDeleteAudited 
    {
        /// <summary>
        /// 逻辑删除标记
        /// </summary>
        bool? F_DeleteMark { get; set; }

        /// <summary>
        /// 删除实体的用户
        /// </summary>
        string F_DeleteUserId { get; set; }

        /// <summary>
        /// 删除实体时间
        /// </summary>
        DateTime? F_DeleteTime { get; set; } 
    }
}