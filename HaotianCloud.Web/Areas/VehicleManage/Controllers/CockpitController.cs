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
using HaotianCloud.Domain.SystemOrganize;

namespace HaotianCloud.Web.Areas.VehicleManage.Controllers
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2020-06-09 19:42
    /// 描 述：控制器类
    /// </summary>
    [Area("VehicleManage")]
    //[AllowAnonymous]
    public class cockpitController :  ControllerBase
    {

        //属性注入示例
        public CockpitService _service { get; set; }
        public Cockpit_excavator_relationService _service2 { get; set; }
        public ExcavatorService _excavatorService { get; set; }
        public MonitorService _monitorService { get; set; }
        #region 获取数据
        [HttpGet]
        [HandlerAjaxOnly]
        public async Task<ActionResult> GetGridJson(Pagination pagination, string keyword)
        {
            //此处需修改
            pagination.order = "desc";
            pagination.sort = "DeviceNo desc";
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
                        str.Add(temp.DeviceNo);
                    }
                }
                data.excavatorName = string.Join("  ", str.ToArray());
            }
            if (!string.IsNullOrEmpty(data.monitorID))
            {
                List<string> str = new List<string>();
                foreach (var item in data.monitorID.Split(','))
                {
                    var temp = await _monitorService.GetForm(item);
                    if (temp != null)
                    {
                        str.Add(temp.devicetype);
                    }
                }
                data.MonitorName = string.Join("  ", str.ToArray());
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
                try
                {
                    var Fid = entity.monitorID.Split(',');
                    for (int k = 0; k < Fid.Length;k++)
                    {
                        if (Fid[k] == "") {
                            continue;
                        }
                        var temp = await _monitorService.FindList(Fid[k]);
                        foreach (var item in temp)
                        {
                            if (item.devicetype.Contains("VIDEO") || item.devicetype.Contains("AUDIO"))
                            {
                                sysconfigs temps = new sysconfigs();
                                temps.tongdaolist = "1";
                                temps.rtmp_enable = "1";
                                temps.rtsp_enable = "1";

                                temps.adminkey = "f6fdffe48c908deb0f4c3bd36c032e72";
                                if (entity.Networkmode.ToLower().Contains("5g-1"))
                                {
                                    temps.IPaddress = item.IPAddress;
                                    temps.NewIPaddress = item.IPAddress2;
                                    temps.rtsp_uri = item.rtspurl2;
                                    temps.rtmp_publish_uri = item.rtmpurl2;
                                }
                                else
                                {
                                    temps.IPaddress = item.IPAddress2;
                                    temps.NewIPaddress = item.IPAddress;
                                    temps.rtsp_uri = item.rtspurl;
                                    temps.rtmp_publish_uri = item.rtmpurl;

                                }
                                _service.updateRIP(temps);
                            }
                        }
                        //var temp = await _monitorService.GetForm(Fid[k]);
                        
                    }
                    

                    return await Success("操作成功。", "", keyValue);
                }
                catch (Exception ex)
                {
                    
                }
                return await Success("操作成功。","",keyValue);
            }
            catch (Exception ex)
            {
                return await Error(ex.Message, "", keyValue);
            }
        }

        
        [HttpGet]
        public async Task<ActionResult> SubmitForm3(string keyValue, string network)
        {
            var message = "";
            try
            {

                var data = await _service.GetForm(keyValue);
                if (data != null)
                {
                    var temp = await _monitorService.FindList(data.monitorID);
                    foreach (var item in temp)
                    {
                        if (item.devicetype.Contains("VIDEO") || item.devicetype.Contains("AUDIO"))
                        {
                            sysconfigs temps = new sysconfigs();
                            temps.tongdaolist = "1";
                            temps.rtmp_enable = "1";
                            temps.rtsp_enable = "1";
                            var mess = "";
                            temps.adminkey = "f6fdffe48c908deb0f4c3bd36c032e72";
                            //if (data.Networkmode.ToLower().Contains("5g"))
                            if (network.ToLower().Contains("5g-1"))
                            {
                                temps.IPaddress = item.IPAddress;
                                temps.NewIPaddress = item.IPAddress2;
                                
                                mess = "设备：" + item.devicetype + ",IP地址：" + temps.IPaddress + "修改为" + temps.NewIPaddress;
                            }
                            else
                            {
                                temps.IPaddress = item.IPAddress2;
                                temps.NewIPaddress = item.IPAddress;
                               
                                mess = "设备：" + item.devicetype + ",IP地址：" + temps.IPaddress + "修改为" + temps.NewIPaddress;
                            }
                            if (item.devicetype.Contains("AUDIO") && network.ToLower().Contains("5g-1"))
                            {
                                temps.rtmp_publish_uri = item.rtmpurl2;//源地址（5G-1CPE）
                            }
                            else if (item.devicetype.Contains("AUDIO") && network.ToLower().Contains("5g-2"))
                            {
                                temps.rtmp_publish_uri = item.rtspurl;//源地址（5G-2CPE）
                            }
                            else if (item.devicetype.Contains("AUDIO") && !network.ToLower().Contains("5g"))
                            {
                                temps.rtmp_publish_uri = item.rtspurl2;//源地址（WIFI/ISM）
                            }
                            else if (network.ToLower().Contains("5g-1"))
                            {
                                temps.rtsp_uri = item.rtspurl2;
                                temps.rtmp_publish_uri = item.rtmpurl2;
                            }
                            else
                            {
                                temps.rtsp_uri = item.rtspurl;
                                temps.rtmp_publish_uri = item.rtmpurl;
                            }
                            var t1 = _service.updateRIP(temps);
                            if (t1 =="error" || t1 == "1")
                            {
                                message += mess + "失败\r\n";
                            }
                            else {
                                message += mess + "成功\r\n";
                            }
                        }
                    }


                }

                return await Success(message,"","");
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


        //
        [HttpPost]
        public async Task<ActionResult> Restart(string keyValue, int status)
        {
            try
            {
                var data = await _service.GetForm(keyValue);
                var temp02 = await _monitorService.FindList(data.monitorID);
                foreach (var temp2 in temp02)
                {
                    sysconfigs t1 = new sysconfigs();
                    t1.adminkey = "f6fdffe48c908deb0f4c3bd36c032e72";
                    t1.tongdaolist = "1";
                    t1.IPaddress = temp2.IPAddress;
                    if (data.Networkmode.ToLower().Contains("5g-1"))
                    {
                        t1.IPaddress = temp2.IPAddress2;
                    }
                    var data2 = _service.restartRIP(t1);
                }
                //_service.updateRIP(data.monitorID);
                return await Success("操作成功。", "", keyValue);
            }
            catch (Exception ex)
            {
                return await Error(ex.Message, "", keyValue);
            }
        }
       

    }
}
