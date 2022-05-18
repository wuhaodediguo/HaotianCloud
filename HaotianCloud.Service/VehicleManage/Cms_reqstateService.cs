using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using HaotianCloud.Code;
using Chloe;
using HaotianCloud.Domain.VehicleManage;
using HaotianCloud.Domain.SystemOrganize;
using Microsoft.AspNetCore.Mvc;

namespace HaotianCloud.Service.VehicleManage
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2020-06-09 19:42
    /// 描 述：控制器管理
    /// </summary>
    public class Cms_reqstateService : DataFilterService<ReqstateEntity>, IDenpendency
    {
        public Cms_reqstateService(IDbContext context) : base(context)
        {

        }
        private string cacheKey = "HaotianCloud_cms_reqstatedata_";
        
        #region 获取数据
        public async Task<List<ReqstateEntity>> GetList(string keyword = "")
        {
            var cachedata = await repository.CheckCacheList(cacheKey + "list");
            //if (!string.IsNullOrEmpty(keyword))
            //{
            //    //此处需修改
            //    cachedata = cachedata.Where(t => t.DeviceNo.Contains(keyword) || t.DeviceName.Contains(keyword)).ToList();
            //}
            return cachedata.Where(t => t.F_DeleteMark == false).OrderByDescending(t => t.F_CreatorTime).ToList();
        }

        public async Task<List<ReqstateEntity>> GetLookList(string keyword = "")
        {
            var query = repository.IQueryable().Where(t => t.F_DeleteMark == false);
            //if (!string.IsNullOrEmpty(keyword))
            //{
            //    //此处需修改
            //    query = query.Where(u => u.DeviceNo.Contains(keyword) || u.DeviceName.Contains(keyword));
            //}
            query = GetDataPrivilege("u","", query);
            return query.OrderByDesc(t => t.F_CreatorTime).ToList();
        }

        public async Task<List<ReqstateEntity>> GetLookList(Pagination pagination,string keyword = "")
        {
            var query = repository.IQueryable().Where(u => u.F_DeleteMark == false);
            //if (!string.IsNullOrEmpty(keyword))
            //{
            //    //此处需修改
            //    query = query.Where(u => u.DeviceNo.Contains(keyword) || u.DeviceName.Contains(keyword));
            //}
            //权限过滤
            query = GetDataPrivilege("u","", query);
            return await repository.OrderList(query, pagination);
        }

        public async Task<ReqstateEntity> GetForm(string keyValue)
        {
            var cachedata = await repository.CheckCache(cacheKey, keyValue);
            return cachedata;
        }
        public async Task<ReqstateEntity> GetLookForm(string keyValue)
        {
            var cachedata = await repository.CheckCache(cacheKey, keyValue);
            return GetFieldsFilterData(cachedata);
        }
        #endregion

        //取得最后一笔数据
        public async Task<ReqstateEntity> GetMaxIDList(string keyword = "")
        {
            var cachedata = await repository.CheckCacheList(cacheKey + "list");
            //if (!string.IsNullOrEmpty(keyword))
            //{
            //    //此处需修改
            //    cachedata = cachedata.Where(t => t.DeviceNo.Contains(keyword) || t.DeviceName.Contains(keyword)).ToList();
            //}
            return cachedata.Where(t => t.F_DeleteMark == false).OrderByDescending(t => t.F_CreatorTime).ToList().FirstOrDefault();
        }

        #region 提交数据
        public async Task SubmitForm(ReqstateEntity entity, string keyValue)
        {
            if (string.IsNullOrEmpty(keyValue))
            {
                entity.F_DeleteMark = false;
                //此处需修改
                entity.Create();
                entity.F_Id = Utils.GuId();
                await repository.Insert(entity);
                //await CacheHelper.Remove(cacheKey + "list");
            }
            else
            {
                    //此处需修改
                entity.Modify(keyValue); 
                await repository.Update(entity);
                //await CacheHelper.Remove(cacheKey + keyValue);
                //await CacheHelper.Remove(cacheKey + "list");
            }
        }


        #endregion

        public async Task<List<ReqstateEntity>> Getcms_reqstate(object keyValue)
        {
            CockpitNewDto cockpitNewDto = new CockpitNewDto();
            string sql = "select * from cms_reqstate where wjj ='" + keyValue + "' order by F_CreatorTime desc limit 10";
            var data1 = await repository.FindList(sql);
           
            return data1;
        }

        public async Task<List<ReqstateEntity>> Getcms_reqstate2(object keyValue,string keyword, string startTime, string endTime)
        {
            CockpitNewDto cockpitNewDto = new CockpitNewDto();
            string sql = "select * from cms_reqstate where wjj ='" + keyValue + "' ";
            if (!string.IsNullOrEmpty(keyword))
            {
                switch (keyword)
                {
                    case "1": sql += "and oil_position < 2 "; break;
                    case "2": sql += "and oil_pressure < 48 "; break;
                    case "3": sql += "and oil_pressure_alarm = '1' "; break;
                    case "4": sql += "and water_temperature_alarm = '1' "; break;
                    case "5": sql += "and oil_filter_blocked = '1' "; break;
                    case "6": sql += "and air_filter_blocked = '1' "; break;
                    case "7": sql += "and rpy_pitch > 10 "; break;
                    case "8": sql += "and rpy_roll > 20 "; break;
                }
            }
            if (!string.IsNullOrEmpty(startTime))
            {
                sql += "and F_CreatorTime >= '"+ startTime +"' ";
            }
            if (!string.IsNullOrEmpty(endTime))
            {
                sql += "and F_CreatorTime <= '" + endTime + "' ";
            }
            var data1 = await repository.FindList(sql);

            return data1;
        }



    }
}
