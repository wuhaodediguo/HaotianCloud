/*******************************************************************************
 * Copyright © 2020 HaotianCloud.Framework 版权所有
 * Author: HaotianCloud
 * Description: HaotianCloud快速开发平台
 * Website：
*********************************************************************************/
using Microsoft.AspNetCore.Mvc;
using HaotianCloud.Code;
using HaotianCloud.Service.SystemOrganize;

namespace HaotianCloud.Web.Controllers
{
    public class HomeController : Controller
    {
        public SystemSetService _setService { get; set; }
        [HttpGet]
        [ServiceFilter(typeof(HandlerLoginAttribute))]
        public ActionResult Index()
        {
            //主页信息获取
            if (_setService.currentuser.UserId == null)
            {
                return View();
            }
            var systemset = _setService.GetForm(_setService.currentuser.CompanyId).GetAwaiter().GetResult();
            ViewBag.ProjectName = systemset.F_ProjectName;
            ViewBag.LogoIcon = ".." + systemset.F_Logo;
            return View();
        }
        [HttpGet]
        [ServiceFilter(typeof(HandlerLoginAttribute))]
        public ActionResult Default()
        {
            return View();
        }
        [HttpGet]
        [ServiceFilter(typeof(HandlerLoginAttribute))]
        public ActionResult UserSetting()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Error()
        {
            return View();
        }
        [HttpGet]
        [ServiceFilter(typeof(HandlerLoginAttribute))]
        public ActionResult Message()
        {
            return View();
        }
    }
}
