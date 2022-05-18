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
    /// 日 期：2021-11-09 11:32
    /// 描 述：test1服务类
    /// </summary>
    public class device_infoService : DataFilterService<device_infoEntity>, IDenpendency
    {
        public device_infoService(IDbContext context) : base(context)
        {
        }
        #region 获取数据
        public async Task<List<device_infoEntity>> GetList(string keyword = "")
        {
            var data = repository.IQueryable();
            if (!string.IsNullOrEmpty(keyword))
            {
                data = data.Where(t => t.DevIDNO.Contains(keyword)
                || t.SerialID.Contains(keyword));
            }
            return data.Where(t => t.F_DeleteMark == false).OrderByDesc(t => t.F_CreatorTime).ToList();
        }

        public async Task<List<device_infoEntity>> GetLookList(string keyword = "")
        {
            var query = repository.IQueryable().Where(t => t.F_DeleteMark == false);
            if (!string.IsNullOrEmpty(keyword))
            {
                //此处需修改
                query = query.Where(t => t.DevIDNO.Contains(keyword)
                || t.SerialID.Contains(keyword));
            }
             //权限过滤
             query = GetDataPrivilege("u", "", query);
            return query.OrderByDesc(t => t.F_CreatorTime).ToList();
        }

        public async Task<List<device_infoEntity>> GetLookList(SoulPage<device_infoEntity> pagination,string keyword = "",int id=1)
        {
            var query = repository.IQueryable().Where(u => u.F_DeleteMark==false);
            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(t => t.DevIDNO.Contains(keyword)
                || t.SerialID.Contains(keyword));
            }
            if(id==0)
            {
                query= query.Where(u=>u.ID==id);
            }
            //权限过滤
            query = GetDataPrivilege("u","",query);
            return await repository.OrderList(query, pagination);
        }

        public async Task<device_infoEntity> GetForm(string keyValue)
        {
            var data = await repository.FindEntity(keyValue);
            return data;
        }
        #endregion

        public async Task<device_infoEntity> GetLookForm(object keyValue)
        {
            var data = await repository.FindEntity(keyValue);
            return GetFieldsFilterData(data);
        }

        #region 提交数据
        public async Task SubmitForm(device_infoEntity entity, string keyValue)
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
