using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Chloe.Annotations;

namespace HaotianCloud.Domain.VehicleManage
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2020-06-09 19:42
    /// 描 述：驾驶舱管理实体类
    /// </summary>
   
    public class CockpitDto : IEntity<CockpitDto>
    {
        public CockpitEntity Cockpit { get; set; } //挖掘机

        public ExcavatorEntity Excavator { get; set; } //挖掘机

        public List<MonitorEntity> Monitor { get; set; } //挖掘机

    }

    public class CockpitNewDto
    {
        public string CockpitDeviceNo { get; set; }
        public string CockpitDeviceName { get; set; }
        public string CockpitNetworkmode { get; set; }

        public string ExcavatorDeviceNo { get; set; }
        public string ExcavatorDeviceName { get; set; }
        public string ExcavatorIP { get; set; }
        public string ExcavatorIP2 { get; set; }
        public int ExcavatorPort { get; set; }
        public int ExcavatorPort2 { get; set; }

        public List<Monitors> CockpitMonitor { get; set; } //设备

        public List<Monitors> Monitor { get; set; } //设备

    }

    public class Monitors {
        public string TYPE { get; set; }
        public string IP { get; set; }
        public int PORT { get; set; }
        public string rtspurl { get; set; }
        public string rtmpurl { get; set; }
        //public string IP2 { get; set; }
        //public int PORT2 { get; set; }
        //public string DeviceNo { get; set; }

    }

    public class CockpitMonitors
    {
        public string TYPE { get; set; }
        public string IP { get; set; }
        public int PORT { get; set; }
        public string rtspurl { get; set; }
        public string rtmpurl { get; set; }
        //public string IP2 { get; set; }
        //public int PORT2 { get; set; }
        //public string DeviceNo { get; set; }

    }


}
