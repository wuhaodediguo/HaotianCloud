using HaotianCloud.Code;
using HaotianCloud.Domain;
using HaotianCloud.Domain.Entity.VehicleManage;
using HaotianCloud.Domain.SystemOrganize;
using HaotianCloud.Domain.VehicleManage;
using HaotianCloud.Service.VehicleManage;
using HaotianCloud.WebApi.CommonFunction;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HaotianCloud.WebApi.Controllers
{
    /// <summary>
    /// 三一重工服务WebAPI
    /// </summary>
    //[Route("api/[controller]")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class sanyiController : ControllerBase
    {
        /// <summary>
        ///自动注入服务
        /// </summary>
        public CockpitService _cockpitService { get; set; }
        /// <summary>
        ///自动注入服务
        /// </summary>
        public Cms_reqstateService _reqstateService { get; set; }

        /// <summary>
        /// Uptcockpit 的摘要说明
        /// </summary>
        [HttpPost]
        [ActionName("Uptcockpit")]
        public AlwaysResult Uptcockpit(sysconfigs entity)
        {
            var data =  _cockpitService.updateR(entity);
            if (data == null || data == "")
            {
                return new AlwaysResult<string> { state = ResultType.error.ToString(), data = "驾驶舱不存在" };
            }
            return new AlwaysResult<string> { state = ResultType.success.ToString(), data = JsonConvert.SerializeObject(data) };

        }

        /// <summary>
        /// GetAllCockpitInfos 的摘要说明
        /// </summary>
        [HttpPost]
        [ActionName("GetAllCockpitInfos")]
        public async Task<AdminUiCallBack> GetAllCockpitInfos(string deviceNo)
        {
            //string ip = Request.HttpContext.Connection.LocalIpAddress.MapToIPv4().ToString();
            var jm = new AdminUiCallBack();
            var data = await _cockpitService.GetCockpitInfo(deviceNo);
            if (data == null)
            {
                jm.code = 1;
                jm.msg = "驾驶舱不存在";

                return jm;
                
            }
            jm.code = 0;
            jm.state = "success";
            jm.msg = "获取数据成功";
            jm.data = data;
            return jm;
           
        }


        /// <summary>
        /// UptcockpitWJJ 的摘要说明
        /// </summary>
        [HttpPost]
        [ActionName("UptcockpitWJJ")]
        public async Task<AdminUiCallBack> UptcockpitWJJ(ReqstateEntity entity)
        {
            var jm = new AdminUiCallBack();
            var flag = false;
            try
            {
                await _reqstateService.SubmitForm(entity, "");
                flag = true;
            }
            catch (Exception ex)
            {

            }

            if (flag == false)
            {
                jm.code = 1;
                jm.msg = "提交失败";

                return jm;

            }
            jm.code = 0;
            jm.state = "success";
            jm.msg = "提交数据成功";
            return jm;

        }


    }
}
