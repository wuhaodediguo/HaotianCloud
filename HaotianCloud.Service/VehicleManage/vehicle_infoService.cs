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
    /// 日 期：2021-11-25 10:07
    /// 描 述：车辆管理服务类
    /// </summary>
    public class vehicle_infoService : DataFilterService<vehicle_infoEntity>, IDenpendency
    {
        public vehicle_infoService(IDbContext context) : base(context)
        {
        }
        #region 获取数据
        public async Task<List<vehicle_infoEntity>> GetList(string keyword = "")
        {
            var data = repository.IQueryable();
            if (!string.IsNullOrEmpty(keyword))
            {
                data = data.Where(t => t.VehiName.Contains(keyword)
                || t.VehiName.Contains(keyword));
            }
            return data.Where(t => t.F_DeleteMark == false).OrderByDesc(t => t.F_CreatorTime).ToList();
        }

        public async Task<List<vehicle_infoEntity>> GetLookList(string keyword = "")
        {
            var query = repository.IQueryable().Where(t => t.F_DeleteMark == false);
            if (!string.IsNullOrEmpty(keyword))
            {
                //此处需修改
                query = query.Where(t => t.VehiName.Contains(keyword)
                || t.VehiName.Contains(keyword));
            }
             //权限过滤
             query = GetDataPrivilege("u", "", query);
            return query.OrderByDesc(t => t.F_CreatorTime).ToList();
        }

        public async Task<List<vehicle_infoEntity>> GetLookList(SoulPage<vehicle_infoEntity> pagination,string keyword = "")
        {
            var query = repository.IQueryable().Where(u => u.F_DeleteMark==false);
            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(t => t.CompanyID.ToString().Contains(keyword)
                || t.CompanyID.ToString().Contains(keyword));
            }
           
            //权限过滤
            query = GetDataPrivilege("u","",query);
            return await repository.OrderList(query, pagination);
        }

        public async Task<vehicle_infoEntity> GetForm(string keyValue)
        {
            var data = await repository.FindEntity(keyValue);
            return data;
        }
        #endregion

        public async Task<vehicle_infoEntity> GetLookForm(object keyValue)
        {
            var data = await repository.FindEntity(keyValue);
            return GetFieldsFilterData(data);
        }

       
        #region 提交数据
        public async Task SubmitForm(vehicle_infoEntity entity, string keyValue)
        {
            if (string.IsNullOrEmpty(keyValue))
            {
                    //初始值添加
                entity.F_DeleteMark = false;
                entity.ID=0;
                entity.F_CreatorTime = DateTime.Now;
                await repository.Insert(entity);
            }
            else
            {
                    //此处需修改
                entity.ID=Convert.ToInt32(keyValue);
                await repository.Update(entity);
            }
        }

        public async Task DeleteForm(string keyValue)
        {
            var ids = keyValue.Split(',');
            await repository.Delete(t => ids.Contains(t.ID.ToString()));
        }
        #endregion

    }
}
