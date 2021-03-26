/*******************************************************************************
 * Copyright © 2020 HaotianCloud.Framework 版权所有
 * Author: HaotianCloud
 * Description: HaotianCloud快速开发平台
 * Website：
*********************************************************************************/
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using HaotianCloud.Service.SystemSecurity;
using HaotianCloud.Code;
using System.Threading.Tasks;

namespace HaotianCloud.Web.Areas.SystemSecurity.Controllers
{
    [Area("SystemSecurity")]
    public class ServerMonitoringController : ControllerBase
    {
        public ServerStateService _serverStateService { get; set; }

        [HttpGet]
        public async Task<ActionResult> GetServerDataJson()
        {
            //windows环境
            var computer = ComputerHelper.GetComputerInfo();
            var arm = computer.RAMRate;
            var cpu = computer.CPURate;
            var iis = computer.RunTime;
            var TotalRAM = computer.TotalRAM;
            string ip = WebHelper.GetWanIp();
            string ipLocation = WebHelper.GetIpLocation(ip);
            var IP = string.Format("{0} ({1})", ip, ipLocation);
            return Content(new { ARM = arm, CPU = cpu, IIS = iis , TotalRAM = TotalRAM,IP=IP }.ToJson());
        }
        [HttpGet]
        public async Task<ActionResult> GetServerData()
        {
            var data =(await _serverStateService.GetList(2)).OrderBy(a => a.F_Date).ToList() ;
            return Content(data.ToJson());
        }
    }
}
