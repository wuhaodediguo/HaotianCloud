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
    public class MonitorService : DataFilterService<MonitorEntity>, IDenpendency
    {
        public MonitorService(IDbContext context) : base(context)
        {

        }
        private string cacheKey = "HaotianCloud_cms_Monitordata_";
        
        #region 获取数据
        public async Task<List<MonitorEntity>> GetList(string keyword = "")
        {
            var cachedata = await repository.CheckCacheList(cacheKey + "list");
            if (!string.IsNullOrEmpty(keyword))
            {
                //此处需修改
                cachedata = cachedata.Where(t => t.DeviceNo.Contains(keyword) || t.DeviceName.Contains(keyword)).ToList();
            }
            return cachedata.Where(t => t.F_DeleteMark == false).OrderByDescending(t => t.F_CreatorTime).ToList();
        }

        public async Task<List<MonitorEntity>> GetLookList(string keyword = "")
        {
            var query = repository.IQueryable().Where(t => t.F_DeleteMark == false);
            if (!string.IsNullOrEmpty(keyword))
            {
                //此处需修改
                query = query.Where(u => u.DeviceNo.Contains(keyword) || u.DeviceName.Contains(keyword));
            }
            query = GetDataPrivilege("u","", query);
            return query.OrderByDesc(t => t.F_CreatorTime).ToList();
        }

        public async Task<List<MonitorEntity>> GetLookList(Pagination pagination,string keyword = "")
        {
            var query = repository.IQueryable().Where(u => u.F_DeleteMark == false);
            if (!string.IsNullOrEmpty(keyword))
            {
                //此处需修改
                query = query.Where(u => u.DeviceNo.Contains(keyword) || u.DeviceName.Contains(keyword));
            }
            //权限过滤
            query = GetDataPrivilege("u","", query);
            return await repository.OrderList(query, pagination);
        }

        public async Task<MonitorEntity> GetForm(string keyValue)
        {
            var cachedata = await repository.CheckCache(cacheKey, keyValue);
            return cachedata;
        }
        public async Task<MonitorEntity> GetLookForm(string keyValue)
        {
            var cachedata = await repository.CheckCache(cacheKey, keyValue);
            return GetFieldsFilterData(cachedata);
        }
        #endregion

        //取得最后一笔数据
        public async Task<MonitorEntity> GetMaxIDList(string keyword = "")
        {
            var cachedata = await repository.CheckCacheList(cacheKey + "list");
            if (!string.IsNullOrEmpty(keyword))
            {
                //此处需修改
                cachedata = cachedata.Where(t => t.DeviceNo.Contains(keyword) || t.DeviceName.Contains(keyword)).ToList();
            }
            return cachedata.Where(t => t.F_DeleteMark == false).OrderByDescending(t => t.F_CreatorTime).ToList().FirstOrDefault();
        }

        #region 提交数据
        public async Task SubmitForm(MonitorEntity entity, string keyValue)
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
            var ids = keyValue.Split(',');
            //if (uniwork.IQueryable<MonitorEntity>(a=> ids.Contains(a.F_Id)).Count()>0)
            //{
            //    throw new Exception("控制器使用中，无法删除");
            //}
            await repository.Delete(t => ids.Contains(t.F_Id));
            foreach (var item in ids)
            {
                await CacheHelper.Remove(cacheKey + item);
            }
            await CacheHelper.Remove(cacheKey + "list");
        }
        #endregion

    }
}
