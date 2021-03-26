/*******************************************************************************
 * Copyright © 2020 HaotianCloud.Framework 版权所有
 * Author: HaotianCloud
 * Description: HaotianCloud快速开发平台
 * Website：
*********************************************************************************/
using HaotianCloud.Service.SystemManage;
using HaotianCloud.Code;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HaotianCloud.Web.Areas.SystemManage.Controllers
{
    [Area("SystemManage")]
    public class QuickModuleController : Controller
    {
        public QuickModuleService _moduleService { get; set; }

        [HttpGet]
        public virtual ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [HandlerAjaxOnly]
        public async Task<ActionResult> GetTransferJson()
        {
            var userId = _moduleService.currentuser.UserId;
            var data =await _moduleService.GetTransferList(userId);
            return Content(data.ToJson());
        }
        [HttpPost]
        [HandlerAjaxOnly]
        public async Task<ActionResult> SubmitForm(string permissionIds)
        {
            string[] temp = string.IsNullOrEmpty(permissionIds) ? null : permissionIds.Split(',');
            await _moduleService.SubmitForm(temp);
            return Content(new AlwaysResult { state = ResultType.success.ToString(), message = "操作成功" }.ToJson());
        }
    }
}
