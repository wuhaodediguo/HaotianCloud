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
    public class CockpitController :  ControllerBase
    {

        //属性注入示例
        public CockpitService _service { get; set; }
        public Cockpit_excavator_relationService _service2 { get; set; }
        public ExcavatorService _excavatorService { get; set; }
        #region 获取数据
        [HttpGet]
        [HandlerAjaxOnly]
        public async Task<ActionResult> GetGridJson(Pagination pagination, string keyword)
        {
            //此处需修改
            pagination.order = "desc";
            pagination.sort = "F_CreatorTime desc";
            var data = await _service.GetLookList(pagination,keyword);
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
        [HandlerAjaxOnly]
        public async Task<ActionResult> GetFormJson(string keyValue)
        {
            var data = await _service.GetForm(keyValue);
            if (!string.IsNullOrEmpty(data.excavatorID))
            {
                List<string> str = new List<string>();
                foreach (var item in data.excavatorID.Split(','))
                {
                    var temp = await _excavatorService.GetForm(item);
                    if (temp != null)
                    {
                        str.Add(temp.DeviceName);
                    }
                }
                data.excavatorName = string.Join("  ", str.ToArray());
            }
            return Content(data.ToJson());
        }
        #endregion

        #region 提交数据
        [HttpPost]
        [HandlerAjaxOnly]
        public async Task<ActionResult> SubmitForm(CockpitEntity entity, string keyValue)
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



        [HttpPost]
        [HandlerAjaxOnly]
        public async Task<ActionResult> SubmitForm2(CockpitEntity entity, string rowsdetail, string keyValue)
        {
            try
            {
                if (keyValue != null)
                {
                    JArray jarrayinfo = JArray.Parse(rowsdetail);
                    await _service.SubmitForm(entity, keyValue);

                    var data = _service.GetMaxIDList("");
                    string maxF_Id = data.Result.F_Id;
                    //
                    await _service2.DeleteForm(maxF_Id);
                    var excavaotr_F_Id = entity.excavatorID.Split(',');
                    List<Cockpit_excavator_relationEntity> carrierListinfo = new List<Cockpit_excavator_relationEntity>();
                    if (excavaotr_F_Id.Length == jarrayinfo.Count)
                    {
                        for (int ij = 0; ij < excavaotr_F_Id.Length; ij++)
                        {
                            Cockpit_excavator_relationEntity newData = new Cockpit_excavator_relationEntity();
                            var temp = await _excavatorService.GetForm(excavaotr_F_Id[ij]);
                            if (temp != null)
                            {
                                newData.DeviceNo = temp.DeviceNo;
                                newData.DeviceName = temp.DeviceName;
                                newData.Brand = temp.Brand;
                                newData.Model = temp.model;
                            }
                            newData.cockpit_F_Id = maxF_Id;
                            newData.excavaotr_F_Id = excavaotr_F_Id[ij];
                            if (((JObject)jarrayinfo[ij])["F_EnabledMark"] == null)
                            {
                                newData.F_EnabledMark = false;
                            }
                            else if (((JObject)jarrayinfo[ij])["F_EnabledMark"].ToString().ToLower() == "false")
                            {
                                newData.F_EnabledMark = false;
                            }
                            else
                            {
                                newData.F_EnabledMark = true;
                            }

                            await _service2.SubmitForm(newData, "");
                        }
                    }
                }
                else {
                    await _service.SubmitForm(entity, keyValue);

                    var data = _service.GetMaxIDList("");
                    string maxF_Id = data.Result.F_Id;
                    //
                    await _service2.DeleteForm(maxF_Id);
                    var excavaotr_F_Id = entity.excavatorID.Split(',');
                    List<Cockpit_excavator_relationEntity> carrierListinfo = new List<Cockpit_excavator_relationEntity>();
                    for (int ij = 0; ij < excavaotr_F_Id.Length; ij++)
                    {
                        Cockpit_excavator_relationEntity newData = new Cockpit_excavator_relationEntity();
                        var temp = await _excavatorService.GetForm(excavaotr_F_Id[ij]);
                        if (temp != null)
                        {
                            newData.DeviceNo = temp.DeviceNo;
                            newData.DeviceName = temp.DeviceName;
                            newData.Brand = temp.Brand;
                            newData.Model = temp.model;
                        }
                        newData.cockpit_F_Id = maxF_Id;
                        newData.excavaotr_F_Id = excavaotr_F_Id[ij];
                        newData.EnabledMark = 2;

                        await _service2.SubmitForm(newData, "");
                    }

                }

                return await Success("操作成功。", "", keyValue);
            }
            catch (Exception ex)
            {
                return await Error(ex.Message, "", keyValue);
            }
        }


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
