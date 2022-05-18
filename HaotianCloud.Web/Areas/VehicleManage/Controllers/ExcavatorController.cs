using HaotianCloud.Code;
using HaotianCloud.Domain.VehicleManage;
using HaotianCloud.Service;
using HaotianCloud.Service.SystemManage;
using HaotianCloud.Service.VehicleManage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HaotianCloud.Web.Areas.VehicleManage.Controllers
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2020-06-09 19:42
    /// 描 述：控制器类
    /// </summary>
    [Area("VehicleManage")]
    //[AllowAnonymous]
    public class excavatorController :  ControllerBase
    {

        //属性注入示例
        public ExcavatorService _service { get; set; }
        public MonitorService _monitorService { get; set; }
        public Devicechn_infoService _service2 { get; set; }
        //public ItemsDataService _service3 { get; set; }
        public ItemsDataService _itemsDetailService { get; set; }
        public Cms_reqstateService _reqstateService { get; set; }
        #region 获取数据
        [HttpGet]
        [HandlerAjaxOnly]
        public async Task<ActionResult> GetGridJson(Pagination pagination, string keyword)
        {
            //此处需修改
            pagination.order = "desc";
            pagination.sort = "DeviceNo asc";
            var data = await _service.GetLookList(pagination, keyword);
            return Success(pagination.records, data);
        }
        [HttpGet]
        [HandlerAjaxOnly]
        public async Task<ActionResult> GetTreeGridJson(string keyword)
        {
            var data = await _service.GetLookList();
            if (!string.IsNullOrEmpty(keyword))
            {
                data = data.TreeWhere(t => t.DeviceNo.Contains(keyword));
            }
            return Success(data.Count, data);
        }
        [HttpGet]
        [HandlerAjaxOnly]
        public async Task<ActionResult> GetListJson(string keyword)
        {
            var data = await _service.GetList(keyword);
            return Content(data.ToJson());
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetDetailListJson(string keyValue2)
        {
            var data = _service2.GetList(keyValue2);
            return Content(data.ToJson());
        }

        [HttpGet]
        public virtual ActionResult AddForm()
        {
            return View();
        }

        [HttpGet]
        public virtual ActionResult Details2()
        {
            return View();
        }


        [HttpGet]
        [HandlerAjaxOnly]
        public async Task<ActionResult> GetFormGridJson(string keyword)
        {
            var data = await _service.GetLookList(keyword);
            
            return Success(data.Count, data);
        }


        [HttpGet]
        [HandlerAjaxOnly]
        public async Task<ActionResult> GetFormJson(string keyValue)
        {
            var data = await _service.GetForm(keyValue);
            if (!string.IsNullOrEmpty(data.monitorID))
            {
                List<string> str = new List<string>();
                foreach (var item in data.monitorID.Split(','))
                {
                    var itemdata = await _itemsDetailService.GetList();
                    var dataItemList = itemdata.FindAll(t => t.F_Id == item);
                    foreach (var itemList in dataItemList)
                    {
                        str.Add(itemList.F_ItemName);
                        break;
                    }

                    //var temp = await _monitorService.GetForm(item);
                    //if (temp != null)
                    //{
                    //    //var name = await _service3.GetForm(temp.devicetype);


                    //    //var itemdata = await _itemsDetailService.GetList();
                    //    //var dataItemList = itemdata.FindAll(t => t.F_ItemCode == temp.devicetype);
                    //    //foreach (var itemList in dataItemList)
                    //    //{
                    //    //    str.Add(itemList.F_ItemName);

                    //    //}
                    //    var itemdata = await _itemsDetailService.GetList();
                    //    var dataItemList = itemdata.FindAll(t => t.F_ItemCode == temp.DeviceNo);
                    //    foreach (var itemList in dataItemList)
                    //    {
                    //        str.Add(itemList.F_ItemName);
                    //        break;
                    //    }
                       
                    //}
                    break;
                }
                data.monitorName = string.Join("  ", str.ToArray());
            }
            return Content(data.ToJson());
        }
        #endregion

        [HttpGet]
        [HandlerAjaxOnly]
        public async Task<ActionResult> GetWJJFormJson(string keyValue)
        {
            var data = await _service.GetForm(keyValue);
            if (!string.IsNullOrEmpty(data.DeviceNo))
            {
                var itemdata = await _reqstateService.Getcms_reqstate(data.DeviceNo);
                return Success(itemdata.Count, itemdata);
            }
        
            return Success(0, "");
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public async Task<ActionResult> GetWJJFormJson2(string keyValue, string keyword, string startTime, string endTime)
        {
            var data = await _service.GetForm(keyValue);
            if (!string.IsNullOrEmpty(data.DeviceNo))
            {
                var itemdata = await _reqstateService.Getcms_reqstate2(data.DeviceNo, keyword, startTime, endTime);
                return Success(itemdata.Count, itemdata);
            }

            return Success(0, "");
        }



        #region 提交数据
        [HttpPost]
        [HandlerAjaxOnly]
        public async Task<ActionResult> SubmitForm(ExcavatorEntity entity, string keyValue)
        {
            try
            {
                await _service.SubmitForm(entity, keyValue);
                return await Success("操作成功。","",keyValue);
            }
            catch (Exception ex)
            {
                return await Error(ex.Message, "", keyValue);
            }
        }

        //[HttpPost]
        //[HandlerAjaxOnly]
        //public async Task<ActionResult> SubmitForm2(ExcavatorEntity entity, string rowsdetail, string keyValue)
        //{
        //    try
        //    {
        //        JArray jarrayinfo = JArray.Parse(rowsdetail);

        //        await _service.SubmitForm(entity, keyValue);

        //        var data = _service.GetMaxIDList("");
        //        string maxF_Id = data.Result.F_Id;
        //        //
        //        await _service2.DeleteForm(maxF_Id);
        //        List<Devicechn_infoEntity> carrierListinfo = new List<Devicechn_infoEntity>();
        //        for (int ij = 0; ij < jarrayinfo.Count; ij++)
        //        {
        //            Devicechn_infoEntity newData = new Devicechn_infoEntity();
        //            newData.monitiorid = maxF_Id;
        //            newData.DeviceType = ((JObject)jarrayinfo[ij])["DeviceType"] == null ? 1 : int.Parse(((JObject)jarrayinfo[ij])["DeviceType"].ToString());
        //            newData.CameraType = ((JObject)jarrayinfo[ij])["CameraType"] == null ? "" : ((JObject)jarrayinfo[ij])["CameraType"].ToString();
        //            newData.Model = ((JObject)jarrayinfo[ij])["Model"] == null ? "" : ((JObject)jarrayinfo[ij])["Model"].ToString();

        //            newData.Manufacturer = ((JObject)jarrayinfo[ij])["Manufacturer"] == null ? "" : ((JObject)jarrayinfo[ij])["Manufacturer"].ToString();
        //            newData.ChnName = ((JObject)jarrayinfo[ij])["ChnName"] == null ? "" : ((JObject)jarrayinfo[ij])["ChnName"].ToString();
        //            newData.chnNumber = ((JObject)jarrayinfo[ij])["chnNumber"] == null ? "" : ((JObject)jarrayinfo[ij])["chnNumber"].ToString();

        //            await _service2.SubmitForm(newData, "");
        //        }

        //        return await Success("操作成功。", "", keyValue);
        //    }
        //    catch (Exception ex)
        //    {
        //        return await Error(ex.Message, "", keyValue);
        //    }
        //}


        [HttpPost]
        [HandlerAjaxOnly]
        [ServiceFilter(typeof(HandlerAuthorizeAttribute))]
        public async Task<ActionResult> DeleteForm(string keyValue)
        {
            try
            {
                await _service.DeleteForm(keyValue);
                return await Success("操作成功。", "", keyValue, DbLogType.Delete);
            }
            catch (Exception ex)
            {
                return await Error(ex.Message, "", keyValue, DbLogType.Delete);
            }
        }
        #endregion
    }
}
