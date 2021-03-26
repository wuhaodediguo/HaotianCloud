using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using HaotianCloud.Code;
using Chloe;
using HaotianCloud.Domain.VehicleManage;

namespace HaotianCloud.Service.VehicleManage
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2020-06-09 19:42
    /// 描 述：控制器管理
    /// </summary>
    public class Devicechn_infoService : DataFilterService<Devicechn_infoEntity>, IDenpendency
    {
        public Devicechn_infoService(IDbContext context) : base(context)
        {

        }
        private string cacheKey = "HaotianCloud_cms_Devicechn_infodata_";
        
        #region 获取数据
        public async Task<List<Devicechn_infoEntity>> GetList(string keyword = "")
        {
            var cachedata = await repository.CheckCacheList(cacheKey + "list");
            if (!string.IsNullOrEmpty(keyword))
            {
                //此处需修改
                cachedata = cachedata.Where(t => t.monitiorid.Contains(keyword)).ToList();
            }
            return cachedata.Where(t => t.F_DeleteMark == false).OrderByDescending(t => t.F_CreatorTime).ToList();
        }

        public async Task<List<Devicechn_infoEntity>> GetLookList(string keyword = "")
        {
            var query = repository.IQueryable().Where(t => t.F_DeleteMark == false);
            if (!string.IsNullOrEmpty(keyword))
            {
                //此处需修改
                query = query.Where(u => u.CameraType.Contains(keyword) || u.CameraType.Contains(keyword));
            }
            query = GetDataPrivilege("u","", query);
            return query.OrderByDesc(t => t.F_CreatorTime).ToList();
        }

        public async Task<List<Devicechn_infoEntity>> GetLookList(Pagination pagination,string keyword = "")
        {
            var query = repository.IQueryable().Where(u => u.F_DeleteMark == false);
            if (!string.IsNullOrEmpty(keyword))
            {
                //此处需修改
                query = query.Where(u => u.CameraType.Contains(keyword) || u.CameraType.Contains(keyword));
            }
            //权限过滤
            query = GetDataPrivilege("u","", query);
            return await repository.OrderList(query, pagination);
        }

        public async Task<Devicechn_infoEntity> GetForm(string keyValue)
        {
            var cachedata = await repository.CheckCache(cacheKey, keyValue);
            return cachedata;
        }
        public async Task<Devicechn_infoEntity> GetLookForm(string keyValue)
        {
            var cachedata = await repository.CheckCache(cacheKey, keyValue);
            return GetFieldsFilterData(cachedata);
        }
        #endregion

        #region 提交数据
        public async Task SubmitForm(Devicechn_infoEntity entity, string keyValue)
        {
            if (string.IsNullOrEmpty(keyValue))
            {
                entity.F_DeleteMark = false;
                //此处需修改
                entity.Create();
                await repository.Insert(entity);
                await CacheHelper.Remove(cacheKey + "list");
            }
            else
            {
                    //此处需修改
                entity.Modify(keyValue); 
                await repository.Update(entity);
                await CacheHelper.Remove(cacheKey + keyValue);
                await CacheHelper.Remove(cacheKey + "list");
            }
        }

        public async Task DeleteForm(string keyValue)
        {
            
            await repository.Delete(t => t.monitiorid == keyValue);
            await CacheHelper.Remove(cacheKey + keyValue);
           
            await CacheHelper.Remove(cacheKey + "list");
        }
        #endregion

    }
}
