using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HaotianCloud.WebApi.Controllers
{

    /// <summary>
    /// 瑶光WebAPI
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ygController : ControllerBase
    {

        public static string changdu = "123";
        public static string kuandu = "456";
        public static string gaodu = "123";
        public static string sudu = "45.6";
        /// <summary>
        /// WebAPI
        /// </summary>
        [HttpGet("{keyword}")]
        public IEnumerable<string> Getvehicleshape(string keyword)
        {
            //初始化测试对象
            var data = "[123,456]";
            //OBJ转化成JSON
            string json = JsonConvert.SerializeObject(data);

            return new string[] { changdu, kuandu, gaodu, sudu };
            //返回json数
            //return new HttpResponseMessage()
            //{
            //    Content = new StringContent(json, Encoding.UTF8, "application/json"),
            //};
        }

        /// <summary>
        /// WebAPI
        /// </summary>
        [HttpGet]
        public HttpResponseMessage Getvehicleshape2(string keyword)
        {
            //初始化测试对象
            var data = "[123,456]";
            //OBJ转化成JSON
            string json = JsonConvert.SerializeObject(data);

            //返回json数
            return new HttpResponseMessage()
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json"),
            };
        }

        ///// <summary>
        ///// 车辆外型参数
        ///// </summary>
        ///// <param name="request">请求对象</param>
        ///// <returns></returns>
        //[HttpPost("{obj}")]
        //public HttpResponseMessage Postvehicleshape(dynamic obj)
        //{
            
        //    try
        //    {
        //        JObject jo = (JObject)JsonConvert.DeserializeObject(obj.ToString());

        //        if (jo["id"] != null)
        //        {
        //            var JJo = jo["id"].ToString();
        //            var station_code = jo["station_code"].ToString();
        //            var station_name = jo["station_name"].ToString();



        //        }

        //        //返回json数
        //        return new HttpResponseMessage()
        //        {
        //            Content = new StringContent("", Encoding.UTF8, "application/json"),
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        //返回json数
        //        return new HttpResponseMessage()
        //        {
        //            Content = new StringContent("", Encoding.UTF8, "application/json"),
        //        };
        //    }

        //}

        /// <summary>
        /// 车辆外型参数
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage Postvehicleshape(dynamic obj)
        {

            try
            {
                var jsonstr = JsonConvert.SerializeObject(obj);
                var jsonParams = JsonConvert.DeserializeObject<dynamic>(jsonstr);
                string changdu1 = jsonParams.changdu;
                string kuandu1 = jsonParams.kuandu;
                string gaodu1 = jsonParams.gaodu;
                string sudu1 = jsonParams.sudu;
                changdu = changdu1;
                kuandu = kuandu1;
                gaodu = gaodu1;
                sudu = sudu1;
                
                //返回json数
                return new HttpResponseMessage()
                {
                    Content = new StringContent("", Encoding.UTF8, "application/json"),
                };
            }
            catch (Exception ex)
            {
                //返回json数
                return new HttpResponseMessage()
                {
                    Content = new StringContent("", Encoding.UTF8, "application/json"),
                };
            }

        }

        /// <summary>
        /// 车辆外型参数
        /// </summary>
        /// <returns></returns>
        //[HttpPost]
        //public HttpResponseMessage Postvehicleshape(JObject jsonobj)
        //{

        //    try
        //    {
        //        //var a = Request.QueryString["gaodu"].ToString();
        //        //JObject jo = (JObject)JsonConvert.DeserializeObject(jsonobj);
        //        //var jsonstr = JsonConvert.SerializeObject(jsonobj);
        //        //var jsonParams = JsonConvert.DeserializeObject<dynamic>(jsonstr);
        //        //string changdu = jsonParams.changdu;

        //        //返回json数
        //        return new HttpResponseMessage()
        //        {
        //            Content = new StringContent("", Encoding.UTF8, "application/json"),
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        //返回json数
        //        return new HttpResponseMessage()
        //        {
        //            Content = new StringContent("", Encoding.UTF8, "application/json"),
        //        };
        //    }

        //}




    }
}
