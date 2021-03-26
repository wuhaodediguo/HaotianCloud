using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HaotianCloud.Code;
using HaotianCloud.Domain.VehicleManage;
using HaotianCloud.Service;
using HaotianCloud.Service.VehicleManage;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace HaotianCloud.Web.Areas.VehicleManage.Controllers
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2020-06-09 19:42
    /// 描 述：控制器类
    /// </summary>
    [Area("VehicleManage")]
    [AllowAnonymous]
    public class ExcavatorController :  ControllerBase
    {

        //属性注入示例
        public ExcavatorService _service { get; set; }
        public Devicechn_infoService _service2 { get; set; }
        #region 获取数据
        [HttpGet]
        [HandlerAjaxOnly]
        public async Task<ActionResult> GetGridJson(Pagination pagination, string keyword)
        {
            //此处需修改
            pagination.order = "desc";
            pagination.sort = "F_CreatorTime desc";
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
            return Content(data.ToJson());
        }
        #endregion

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
