using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using HaotianCloud.Code;
using Chloe;
using HaotianCloud.Domain.VehicleManage;
using RestSharp;
using Newtonsoft.Json;
using HaotianCloud.Domain.Entity.VehicleManage;
using HaotianCloud.Domain.SystemOrganize;
using HaotianCloud.Service.SystemManage;
using HaotianCloud.Code.Logs;
using System.Net.NetworkInformation;

namespace HaotianCloud.Service.VehicleManage
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2020-06-09 19:42
    /// 描 述：驾驶舱管理
    /// </summary>
    public class CockpitService : DataFilterService<CockpitEntity>, IDenpendency
    {
        public CockpitService(IDbContext context) : base(context)
        {

        }
        private string cacheKey = "HaotianCloud_cms_Cockpitdata_";
        public ExcavatorService _excavatorService { get; set; }
        public MonitorService _monitorService { get; set; }
        public ItemsDataService _itemsDetailService { get; set; }

        #region 获取数据
        public async Task<List<CockpitEntity>> GetList(string keyword = "")
        {
            var cachedata = await repository.CheckCacheList(cacheKey + "list");
            if (!string.IsNullOrEmpty(keyword))
            {
                //此处需修改
                cachedata = cachedata.Where(t => t.DeviceNo.Contains(keyword) || t.DeviceName.Contains(keyword)).ToList();
            }
            return cachedata.Where(t => t.F_DeleteMark == false).OrderByDescending(t => t.F_CreatorTime).ToList();
        }

        public async Task<List<CockpitEntity>> GetLookList(string keyword = "")
        {
            var query = repository.IQueryable().Where(t => t.F_DeleteMark == false);
            if (!string.IsNullOrEmpty(keyword))
            {
                //此处需修改
                query = query.Where(u => u.DeviceNo.ToLower().Contains(keyword.ToLower()) || u.monitorID.ToLower().Contains(keyword.ToLower()));
                //query = query.Where(u => u.DeviceNo.Contains(keyword) || u.DeviceName.Contains(keyword));
            }
            query = GetDataPrivilege("u","", query);
            return query.OrderByDesc(t => t.F_CreatorTime).ToList();
        }

        public async Task<List<CockpitEntity>> GetLookList(Pagination pagination,string keyword = "")
        {
            var query = repository.IQueryable().Where(u => u.F_DeleteMark == false);
            if (!string.IsNullOrEmpty(keyword))
            {
                //此处需修改
                query = query.Where(u => u.DeviceNo.Contains(keyword) || u.DeviceName.Contains(keyword));
            }
            //权限过滤
            query = GetDataPrivilege("u","", query);
            return await repository.OrderList(query, pagination);
        }

        public async Task<CockpitEntity> GetForm(string keyValue)
        {
            var cachedata = await repository.CheckCache(cacheKey, keyValue);
            return cachedata;
        }
        public async Task<CockpitEntity> GetLookForm(string keyValue)
        {
            var cachedata = await repository.CheckCache(cacheKey, keyValue);
            return GetFieldsFilterData(cachedata);
        }
        #endregion

        //取得最后一笔数据
        public async Task<CockpitEntity> GetMaxIDList(string keyword = "")
        {
            var cachedata = await repository.CheckCacheList(cacheKey + "list");
            if (!string.IsNullOrEmpty(keyword))
            {
                //此处需修改
                cachedata = cachedata.Where(t => t.DeviceNo.Contains(keyword) || t.DeviceName.Contains(keyword)).ToList();
            }
            return cachedata.Where(t => t.F_DeleteMark == false).OrderByDescending(t => t.F_CreatorTime).ToList().FirstOrDefault();
        }

        #region 提交数据
        public async Task SubmitForm(CockpitEntity entity, string keyValue)
        {
            if (string.IsNullOrEmpty(keyValue))
            {
                entity.F_DeleteMark = false;
                //此处需修改
                entity.Create();
                await repository.Insert(entity);
                await CacheHelper.Remove(cacheKey + "list");
            }
            else
            {
                    //此处需修改
                entity.Modify(keyValue); 
                await repository.Update(entity);
                await CacheHelper.Remove(cacheKey + keyValue);
                await CacheHelper.Remove(cacheKey + "list");
            }
        }

        public async Task DeleteForm(string keyValue)
        {
            var ids = keyValue.Split(',');
            //if (uniwork.IQueryable<CockpitEntity>(a=> ids.Contains(a.F_Id)).Count()>0)
            //{
            //    throw new Exception("控制器使用中，无法删除");
            //}
            await repository.Delete(t => ids.Contains(t.F_Id));
            foreach (var item in ids)
            {
                await CacheHelper.Remove(cacheKey + item);
            }
            await CacheHelper.Remove(cacheKey + "list");
        }
        #endregion


        //
        //
        public async Task<CockpitNewDto> GetCockpitInfo(object keyValue)
        {
            CockpitNewDto cockpitNewDto = new CockpitNewDto();
            string sql = "select DeviceNo,DeviceName,Networkmode,excavatorID,monitorID from cockpit_info where DeviceNo ='" + keyValue + "'";
            var data1 = await repository.FindList(sql);
            if (data1.Count > 0)
            {
                var data = data1[0];
                cockpitNewDto.CockpitDeviceNo = data.DeviceNo;
                cockpitNewDto.CockpitDeviceName = data.DeviceName;
                cockpitNewDto.CockpitNetworkmode = data.Networkmode;

                if (!string.IsNullOrEmpty(data.monitorID))
                {
                    var temp02 = await _monitorService.FindList(data.monitorID);
                    List<Monitors> Monitor01 = new List<Monitors>();
                    foreach (var temp2 in temp02)
                    {
                        Monitors Monitors = new Monitors();
                        //Monitors.DeviceNo = temp2.DeviceNo;
                        Monitors.TYPE = temp2.devicetype;
                        Monitors.IP = temp2.IPAddress;
                        Monitors.PORT = temp2.port;
                        Monitors.rtspurl = temp2.rtspurl;
                        Monitors.rtmpurl = temp2.rtmpurl;
                        if (data.Networkmode.ToLower().Contains("5g-1"))
                        {
                            Monitors.IP = temp2.IPAddress2;
                            Monitors.PORT = temp2.port2;
                            Monitors.rtspurl = temp2.rtspurl2;
                            Monitors.rtmpurl = temp2.rtmpurl2;
                        }
                        
                        Monitor01.Add(Monitors);

                    }
                    cockpitNewDto.CockpitMonitor = Monitor01;


                }

                if (!string.IsNullOrEmpty(data.excavatorID))
                {
                    List<string> str = new List<string>();
                    foreach (var item in data.excavatorID.Split(','))
                    {
                        var temp = await _excavatorService.GetForm(item);
                        
                        if (temp != null)
                        {
                            //str.Add(temp.DeviceName);
                           
                            cockpitNewDto.ExcavatorDeviceNo = temp.DeviceNo;
                            cockpitNewDto.ExcavatorDeviceName = temp.DeviceName;
                            cockpitNewDto.ExcavatorIP = temp.IPAddress;
                            cockpitNewDto.ExcavatorPort = temp.port;
                            cockpitNewDto.ExcavatorIP2 = temp.IPAddress2;
                            cockpitNewDto.ExcavatorPort2 = temp.port2;
                            if (data.Networkmode.ToLower().Contains("5g-1"))
                            {
                                cockpitNewDto.ExcavatorIP2 = temp.IPAddress;
                                cockpitNewDto.ExcavatorPort2 = temp.port;
                                cockpitNewDto.ExcavatorIP = temp.IPAddress2;
                                cockpitNewDto.ExcavatorPort = temp.port2;

                            }
                            if (!string.IsNullOrEmpty(temp.monitorID))
                            {
                                List<Monitors> Monitor = new List<Monitors>();
                                List<string> str2 = new List<string>();
                                foreach (var item2 in temp.monitorID.Split(','))
                                {
                                    //var itemdata = await _itemsDetailService.GetList();
                                    //var dataItemList = itemdata.FindAll(t => t.F_Id == item2);
                                    //foreach (var itemList in dataItemList)
                                    //{
                                    //    str.Add(itemList.F_ItemName);
                                    //    break;
                                    //}

                                    string sql2 = "select * from monitor_info where DeviceNo ='" + item2 + "'";
                                    //var temp2 = await _monitorService.GetForm(item2);
                                    //MonitorService MonitorService = new MonitorService();
                                    var temp02 = await _monitorService.FindList(item2);
                                    foreach (var temp2 in temp02)
                                    {
                                        Monitors Monitors = new Monitors();
                                        //Monitors.DeviceNo = temp2.DeviceNo;
                                        Monitors.TYPE = temp2.devicetype;
                                        Monitors.IP = temp2.IPAddress;
                                        Monitors.PORT = temp2.port;
                                        Monitors.rtspurl = temp2.rtspurl;
                                        Monitors.rtmpurl = temp2.rtmpurl;
                                        if (data.Networkmode.ToLower().Contains("5g-1"))
                                        {
                                            Monitors.IP = temp2.IPAddress2;
                                            Monitors.PORT = temp2.port2;
                                            Monitors.rtspurl = temp2.rtspurl2;
                                            Monitors.rtmpurl = temp2.rtmpurl2;
                                        }
                                        //Monitors.IP2 = temp2.IPAddress2;
                                        //Monitors.PORT2 = temp2.port2;
                                        Monitor.Add(Monitors);

                                    }

                                }
                                cockpitNewDto.Monitor = Monitor;

                                //data.excavatorName = string.Join("  ", str2.ToArray());
                            }
                        }
                        
                    }

                    //data.excavatorName = string.Join("  ", str.ToArray());
                }
               
            }
            
            return GetFieldsFilterData(cockpitNewDto);
        }

        public UpDto GetConfig(string ip)
        {
            UpDto UpDto = new UpDto();
            try
            {
                
                var url = "http://" + ip + "/getConfig";
               
                List<EncodeDto> listencode = new List<EncodeDto>();
                
                var bodyparam = JsonConvert.SerializeObject(UpDto);
                var client = new RestClient(url);

                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("application/json", "f6fdffe48c908deb0f4c3bd36c032e72", ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
           
                var results = Newtonsoft.Json.JsonConvert.DeserializeObject<UpDto>(response.Content);
                return results;
            }
            catch (Exception ex)
            {

            }

            return UpDto;

        }


        public string updateR(sysconfigs entity)
        {
            var result = "";
            try 
            {
                string cockpitIP1 = GlobalContext.SystemConfig.cockpitIP1;
                var result1 = GetConfig(entity.IPaddress);
                var url = "http://" + entity.IPaddress + "/setConfig";
                if (result1 == null)
                {
                    return "";
                }
                    //UpDto UpDto = new UpDto();
                    //List<EncodeDto> listencode = new List<EncodeDto>();
                    //EncodeDto encode = new EncodeDto();
                    //List<aenc_param> aenc_paramlist = new List<aenc_param>();
                    //aenc_param aenc_param = new aenc_param();
                    //aenc_param.aenc_bitrate = 131072;
                    //aenc_param.aenc_code = 1;
                    //aenc_param.aenc_input = 1;
                    //aenc_param.aenc_samplerate = 48000;
                    //aenc_param.analog_vol = 30;
                    //aenc_param.rtsp_g711 = 0;
                    //aenc_paramlist.Add(aenc_param);

                    //List<vdec_param> vdec_paramlist = new List<vdec_param>();
                    //vdec_param vdec_param = new vdec_param();
                    //vdec_param.vdec_enable = "0";
                    //vdec_param.video_url = "rtmp://127.0.0.1/live/live2";
                    //vdec_paramlist.Add(vdec_param);

                    //List<venc_param> venc_paramlist = new List<venc_param>();
                    //venc_param venc_param = new venc_param();
                    //venc_param.audioEnable = "0";
                    //venc_param.http_flv_enable = "0";
                    //venc_param.http_hls_enable = "0";
                    //venc_param.live_enable = "1";
                    //venc_param.live_publish_url = "rtmp://127.0.0.1/hlsram/record0";
                    //venc_param.multicast_enable = "1";
                    //venc_param.http_ts_uri = "1";
                    //venc_param.http_hls_uri = "";
                    //venc_param.http_flv_uri = "";
                    //venc_param.multicast_ip = "234.5.6.7";
                    //venc_param.multicast_port = "111";
                    //venc_param.multicast_enable = "1";
                    //List<osd> osds = new List<osd>();
                    //osd osd = new osd();
                    //osd.alpha = "0";
                    //osd.bcolor = "0";
                    //osd.enable = "1";
                    //osd.font_color = "16777215";
                    //osd.font_size = "36";
                    //osd.osd_type = "0";
                    //osd.speed = "50";
                    //osd.txt = "xstrive.com";
                    //osd.x = "50";
                    //osd.y = "50";
                    //osds.Add(osd);
                    //venc_param.osd = osds;

                    //venc_param.recordEnable = "0";
                    //venc_param.rtmp_enable = "0";
                    //venc_param.rtmp_publish_enable_write = "0";
                    //venc_param.rtmp_publish_uri = "rtmp://172.17.0.122:1935/live/live0";
                    //venc_param.rtsp_enable = "1";
                    //venc_param.rtsp_uri = "rtsp://192.168.0.101/live/live0";
                    //venc_param.vencFramerate = "15";
                    //venc_param.rtsp_username = "admin";
                    //venc_param.rtsp_password = "admin";
                    //venc_param.transcodeEnable = "0";
                    //venc_param.ts_pid = "0";
                    //venc_param.venc_codec = "0";
                    ////venc_param.vencGop = "15";
                    ////venc_param.vencHeight = "15";
                    ////venc_param.vencFramerate = "15";

                    //venc_paramlist.Add(venc_param);

                    //encode.venc_param = venc_paramlist;
                    //encode.aenc_param = aenc_paramlist;
                    //encode.aenc_param = aenc_paramlist;
                    //listencode.Add(encode);
                    //UpDto.encode = listencode;
                    if (result1.encode != null) {
                    if (!string.IsNullOrEmpty(entity.tongdaolist))
                    {
                        var k = int.Parse(entity.tongdaolist);
                        result1.encode[0].venc_param[k].rtmp_enable = int.Parse(entity.rtmp_enable);
                        result1.encode[0].venc_param[k].rtsp_enable = int.Parse(entity.rtsp_enable);
                        result1.encode[0].venc_param[k].rtmp_publish_uri = entity.rtmp_publish_uri;
                        result1.encode[0].venc_param[k].rtsp_uri = entity.rtsp_uri;
                        result1.encode[0].venc_param[k].rtsp_username = entity.rtsp_username;
                        result1.encode[0].venc_param[k].rtsp_password = entity.rtsp_password;

                        //result1.ipinfo[0].local_net.ip = entity.NewIPaddress;
                        //兼容模式开启
                        result1.encode[0].venc_param[k].compact_mode = "1";
                        //转码功能开启
                        result1.encode[0].venc_param[k].transcodeEnable = "1";

                    }

                } 
                var bodyparam = JsonConvert.SerializeObject(result1);
                var client = new RestClient(url);

                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("application/json", entity.adminkey + bodyparam, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                //Console.WriteLine(response.Content);
                result = response.Content;
            }
            catch(Exception ex)
            {
                
            }
            
            return result;

        }

        public string updateRIP(sysconfigs entity)
        {
            var result = "";
            try
            {
                var aa = PingIp(entity.IPaddress);
                if (!aa)
                {
                    return "error";
                }
                string cockpitIP1 = GlobalContext.SystemConfig.cockpitIP1;
                var result1 = GetConfig(entity.IPaddress);
                var url = "http://" + entity.IPaddress + "/setConfig";
                if (result1 == null)
                {
                    return "error";
                }
               
                if (result1.encode != null)
                {
                    if (!string.IsNullOrEmpty(entity.tongdaolist))
                    {
                        var k = int.Parse(entity.tongdaolist);
                        result1.encode[0].venc_param[0].rtmp_enable = int.Parse(entity.rtmp_enable);
                        result1.encode[0].venc_param[0].rtsp_enable = int.Parse(entity.rtsp_enable);
                        result1.encode[0].venc_param[0].rtmp_publish_uri = entity.rtmp_publish_uri;
                        result1.encode[0].venc_param[0].rtsp_uri = entity.rtsp_uri;
                        //result1.encode[0].venc_param[k].rtsp_username = entity.rtsp_username;
                        //result1.encode[0].venc_param[k].rtsp_password = entity.rtsp_password;

                        result1.ipinfo[0].local_net.ip = entity.NewIPaddress;
                        result1.ipinfo[0].eth_net.ip = entity.NewIPaddress;
                        //兼容模式开启
                        result1.encode[0].venc_param[0].compact_mode = "1";
                        //转码功能开启
                        result1.encode[0].venc_param[0].transcodeEnable = "1";

                    }

                }
                var bodyparam = JsonConvert.SerializeObject(result1);
                var client = new RestClient(url);

                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("application/json", entity.adminkey + bodyparam, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                //Console.WriteLine(response.Content);
                result = response.Content;
            }
            catch (Exception ex)
            {

            }

            return result;

        }

        //reboot
        public string restartRIP(sysconfigs entity)
        {
            var result = "";
            try
            {
                var aa = PingIp(entity.IPaddress);
                if (!aa)
                {
                    return "error";
                }
                //string cockpitIP1 = GlobalContext.SystemConfig.cockpitIP1;
                //var result1 = GetConfig(entity.IPaddress);
                var url = "http://" + entity.IPaddress + "/control?command=reboot&value=1" + "&auth=" + entity.adminkey;
                //if (result1 == null)
                //{
                //    return "";
                //}

                //if (result1.encode != null)
                //{
                //    if (!string.IsNullOrEmpty(entity.tongdaolist))
                //    {
                //        var k = int.Parse(entity.tongdaolist);
                       
                //        result1.ipinfo[0].local_net.ip = entity.NewIPaddress;

                //    }

                //}
                //var bodyparam = JsonConvert.SerializeObject(result1);
                var client = new RestClient(url);

                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");
                //request.AddParameter("application/json", entity.adminkey + bodyparam, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                //Console.WriteLine(response.Content);
                OutDLog.QuickWriteOutLog("response.Content" + response.Content);
                result = response.Content;
            }
            catch (Exception ex)
            {

            }

            return result;

        }

        //public async Task<bool> UptcockpitMes(reqstate entity)
        //{
        //    var result = false;
        //    try
        //    {
        //        entity.F_DeleteMark = false;
        //        //此处需修改
        //        entity.Create();
        //        await _reqstateService.SubmitForm(entity,"");
        //        result = true;
        //    }
        //    catch (Exception ex)
        //    {

        //    }

        //    return result;

        //}


        private bool PingIp(string strIP)
        {
            bool bRet = false;
            try
            {
                Ping pingSend = new Ping();
                PingReply reply = pingSend.Send(strIP, 1000);
                if (reply.Status == IPStatus.Success)
                    bRet = true;
            }
            catch (Exception)
            {
                bRet = false;
            }
            return bRet;
        }


    }
}
