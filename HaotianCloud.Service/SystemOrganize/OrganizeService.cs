/*******************************************************************************
 * Copyright © 2020 HaotianCloud.Framework 版权所有
 * Author: HaotianCloud
 * Description: HaotianCloud快速开发平台
 * Website：
*********************************************************************************/
using HaotianCloud.Domain.SystemOrganize;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using HaotianCloud.Code;
using Chloe;

namespace HaotianCloud.Service.SystemOrganize
{
    public class OrganizeService : DataFilterService<OrganizeEntity>, IDenpendency
    {
        /// <summary>
        /// 缓存操作类
        /// </summary>

        private string cacheKey = "HaotianCloud_organizedata_";
        //获取类名
        
        public OrganizeService(IDbContext context) : base(context)
        {
        }
        public async Task<List<OrganizeEntity>> GetList()
        {
            var cachedata =await repository.CheckCacheList(cacheKey + "list");
            return cachedata.Where(a=>a.F_DeleteMark==false).ToList();
        }
        //
        #region 获取数据 F_CategoryId == "Company"
        public async Task<List<OrganizeEntity>> GetList(string keyword = "")
        {
            var cachedata = await repository.CheckCacheList(cacheKey + "list");
            if (!string.IsNullOrEmpty(keyword))
            {
                //此处需修改
                cachedata = cachedata.Where(t => t.F_FullName.Contains(keyword) || t.F_EnCode.Contains(keyword)).ToList();
            }
            return cachedata.Where(t => t.F_DeleteMark == false && t.F_CategoryId == "Company").OrderByDescending(t => t.F_CreatorTime).ToList();
        }
        #endregion

        public async Task<List<OrganizeEntity>> GetLookList()
        {
            var query = repository.IQueryable().Where(a => a.F_DeleteMark == false);
            query = GetDataPrivilege("u","",query);
            return query.OrderBy(t => t.F_SortCode).ToList();
        }
        public async Task<OrganizeEntity> GetLookForm(string keyValue)
        {
            var cachedata =await repository.CheckCache(cacheKey, keyValue);
            return GetFieldsFilterData(cachedata);
        }
        public async Task<OrganizeEntity> GetForm(string keyValue)
        {
            var cachedata = await repository.CheckCache(cacheKey, keyValue);
            return cachedata;
        }
        public async Task DeleteForm(string keyValue)
        {
            if (repository.IQueryable(t => t.F_ParentId.Equals(keyValue)).Count() > 0)
            {
                throw new Exception("删除失败！操作的对象包含了下级数据。");
            }
            else
            {
                if (uniwork.IQueryable<UserEntity>(a=>a.F_OrganizeId==keyValue).Count()>0|| uniwork.IQueryable<UserEntity>(a => a.F_DepartmentId == keyValue).Count()>0)
                {
                    throw new Exception("组织使用中，无法删除");
                }
                await repository.Delete(t => t.F_Id == keyValue);
                await CacheHelper.Remove(cacheKey + keyValue);
                await  CacheHelper.Remove(cacheKey + "list");
            }
        }
        public async Task SubmitForm(OrganizeEntity organizeEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                organizeEntity.Modify(keyValue);
                await repository.Update(organizeEntity);
                await CacheHelper.Remove(cacheKey + keyValue);
                await CacheHelper.Remove(cacheKey + "list");
            }
            else
            {
                organizeEntity.F_AllowDelete = false;
                organizeEntity.F_AllowEdit = false;
                organizeEntity.F_DeleteMark = false;
                organizeEntity.Create();
                await repository.Insert(organizeEntity);
                await CacheHelper.Remove(cacheKey + "list");
            }
        }
    }
}
