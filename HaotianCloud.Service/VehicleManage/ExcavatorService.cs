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
    /// 描 述：挖掘机管理
    /// </summary>
    public class ExcavatorService : DataFilterService<ExcavatorEntity>, IDenpendency
    {
        public ExcavatorService(IDbContext context) : base(context)
        {

        }
        private string cacheKey = "HaotianCloud_cms_Excavatordata_";
        
        #region 获取数据
        public async Task<List<ExcavatorEntity>> GetList(string keyword = "")
        {
            var cachedata = await repository.CheckCacheList(cacheKey + "list");
            if (!string.IsNullOrEmpty(keyword))
            {
                //此处需修改
                cachedata = cachedata.Where(t => t.DeviceNo.Contains(keyword) || t.DeviceName.Contains(keyword)).ToList();
            }
            return cachedata.Where(t => t.F_DeleteMark == false).OrderByDescending(t => t.F_CreatorTime).ToList();
        }

        public async Task<List<ExcavatorEntity>> GetLookList(string keyword = "")
        {
            var query = repository.IQueryable().Where(t => t.F_DeleteMark == false);
            if (!string.IsNullOrEmpty(keyword))
            {
                //此处需修改
                query = query.Where(u => u.DeviceNo.Contains(keyword) || u.DeviceName.Contains(keyword));
            }
            query = GetDataPrivilege("u", "", query);
            return query.OrderByDesc(t => t.F_CreatorTime).ToList();
        }

        public async Task<List<ExcavatorEntity>> GetLookList(Pagination pagination, string keyword = "")
        {
            var query = repository.IQueryable().Where(u => u.F_DeleteMark == false);
            if (!string.IsNullOrEmpty(keyword))
            {
                //此处需修改
                query = query.Where(u => u.DeviceNo.ToLower().Contains(keyword.ToLower()) || u.monitorID.ToLower().Contains(keyword.ToLower()));
                //query = query.Where(u => u.DeviceNo.Contains(keyword) || u.DeviceName.Contains(keyword));
            }
            //权限过滤
            query = GetDataPrivilege("u", "", query);
            return await repository.OrderList(query, pagination);
        }
        /// <summary>
        /// 获取挖掘机详情
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public async Task<ExcavatorEntity> GetForm(string keyValue)
        {
            var cachedata = await repository.CheckCache(cacheKey, keyValue);
            return cachedata;

            //var query = repository.IQueryable(a => a.F_EnabledMark == true)
            //    .LeftJoin<ExcavatorEntity>((a, b) => a.F_Id == b.F_Id && b.F_EnabledMark == true)
            //    .Select((a, b) => new ExcavatorEntity
            //    {
            //        F_Id = a.F_Id,
            //        //F_CategoryId = a.F_CategoryId,
            //        //F_CategoryName = b.F_FullName,
            //        //F_Title = a.F_Title,
            //        //F_LinkUrl = a.F_LinkUrl,
            //        //F_ImgUrl = a.F_ImgUrl,
            //        //F_SeoTitle = a.F_SeoTitle,
            //        //F_SeoKeywords = a.F_SeoKeywords,
            //        //F_SeoDescription = a.F_SeoDescription,
            //        //F_Tags = a.F_Tags,
            //        //F_Zhaiyao = a.F_Zhaiyao,
            //        //F_Description = a.F_Description,
            //        //F_SortCode = a.F_SortCode,
            //        //F_IsTop = a.F_IsTop,
            //        //F_IsHot = a.F_IsHot,
            //        //F_IsRed = a.F_IsRed,
            //        //F_Click = a.F_Click,
            //        //F_Source = a.F_Source,
            //        //F_Author = a.F_Author,
            //        F_EnabledMark = a.F_EnabledMark,
            //        F_CreatorTime = a.F_CreatorTime,
            //        F_CreatorUserId = a.F_CreatorUserId,
            //        F_DeleteMark = a.F_DeleteMark,
            //        F_DeleteTime = a.F_DeleteTime,
            //        F_DeleteUserId = a.F_DeleteUserId,
            //        F_LastModifyTime = a.F_LastModifyTime,
            //        F_LastModifyUserId = a.F_LastModifyUserId,
            //    });
            //if (!string.IsNullOrEmpty(keyValue))
            //{
            //    query = query.Where(a => a.F_Id == keyValue);
            //}
            ////字段权限处理
            //return GetFieldsFilterData(query.FirstOrDefault());
        }
        #endregion

        #region 提交数据
        public async Task SubmitForm(ExcavatorEntity entity, string keyValue)
        {
            //if (string.IsNullOrEmpty(entity.F_Zhaiyao))
            //{
            //    entity.F_Zhaiyao = HaotianCloud.Code.TextHelper.GetSubString(HaotianCloud.Code.WebHelper.NoHtml(entity.F_Description),255);
            //}
            //if (string.IsNullOrEmpty(entity.F_SeoTitle))
            //{
            //    entity.F_SeoTitle = entity.F_Title;
            //}
            //if (string.IsNullOrEmpty(entity.F_SeoKeywords))
            //{
            //    entity.F_SeoKeywords = entity.F_Zhaiyao;
            //}
            //if (string.IsNullOrEmpty(entity.F_SeoDescription))
            //{
            //    entity.F_SeoDescription = entity.F_Zhaiyao;
            //}

            if (string.IsNullOrEmpty(keyValue) || keyValue == "keyValue")
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
