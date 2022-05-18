using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HaotianCloud.Code;
using HaotianCloud.Code.Logs;
using HaotianCloud.WebApi.CommonFunction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HaotianCloud.WebApi.Controllers
{

    /// <summary>
    /// 无人接驳车WebAPI
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class WXApiController : ControllerBase
    {
        //public static string mysqluserName = GlobalContext.SystemConfig.mysqluserName;//
        //public static string mysqluserPassword = System.Configuration.ConfigurationManager.AppSettings["mysqluserPassword"].ToString();//
        //public static string mysqlIP = System.Configuration.ConfigurationManager.AppSettings["mysqlIP"].ToString();//
        /// <summary>
        /// ConnectionString
        /// </summary>
        public static string strconnectsql = GlobalContext.SystemConfig.DBConnectionString;//

        /// <summary>
        /// GetOpenID 的摘要说明
        /// </summary>
        [HttpGet]
        public HttpResponseMessage GetOpenID(string code)
        {
            string codeid = code;
            string APPID = GlobalContext.SystemConfig.WXappidcar;
            string SECRET = GlobalContext.SystemConfig.WXsecretcar;
            string serviceAddress = string.Format("https://api.weixin.qq.com/sns/jscode2session?appid={0}&secret={1}&js_code={2}&grant_type=authorization_code", APPID, SECRET, codeid);
            string openid = CommonHelper.GetMethods(serviceAddress);
            //OBJ转化成JSON
            string json = JsonConvert.SerializeObject(openid);

            //返回json数
            return new HttpResponseMessage()
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json"),
            };
        }

        /// <summary>
        /// CreateOrderNo 创建订单
        /// </summary>
        [HttpPost]
        public async Task<AlwaysResult> CreateOrderNo([FromBody] resultCreateOrder obj)
        {

            OutDLog.QuickWriteOutLog("First" + DateTime.Now.ToString());
            string outmessage = string.Empty;
            var result = AddCreateOrderNo(obj, ref outmessage);

            if (result == 1)
            {
                return new AlwaysResult<string> { state = ResultType.success.ToString(), message = "success", data = outmessage };
            }
            else
            {
                return new AlwaysResult<string> { state = ResultType.error.ToString(), message = outmessage, data = "" };

            }
            
        }

        /// <summary>
        /// getOrdermoney 查询订单价格
        /// </summary>
        [HttpPost]
        public AlwaysResult getOrdermoney([FromBody] resultOrderMoney obj)
        {

            OutDLog.QuickWriteOutLog("First" + DateTime.Now.ToString());
            string outmessage = string.Empty;
           
            return new AlwaysResult<string> { state = ResultType.success.ToString(), message = "success", data = outmessage };

        }

        /// <summary>
        /// Addresultshoudong增加一条数据
        /// </summary>
        private int AddCreateOrderNo(resultCreateOrder model, ref string outmessage)
        {
            //连接mysql数据库的语句
            int result = 0;

            MySqlConnection connect = null;
            try
            {
                resultCreateOrder paramst = new resultCreateOrder();
                model.pushOrderNo = "WRJB" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
                model.OrderState = "1";
                model.ActualTime = DateTime.Now;
                //paramst.startlatitude = result5["from_x"].ToString();
                //paramst.startlongitude = result5["from_y"].ToString();
                //paramst.startplace = result5["applicationOrderUuid"].ToString();
                //paramst.endlatitude = result5["baseAlgorithmName"].ToString();
                //paramst.endlongitude = result5["eventCode"].ToString();
                //paramst.endplace = result5["latitude"].ToString();
                //paramst.token = result5["token"].ToString();
                //paramst.sdate = Convert.ToDateTime(result4["sdate"].ToString());
                //paramst.renshu = Convert.ToInt32(result4["rs"].ToString());


                connect = new MySqlConnection();
                connect.ConnectionString = strconnectsql;
                outmessage = strconnectsql;

                if (connect.State == ConnectionState.Closed)
                {
                    connect.Open();
                }

                MySqlCommand sqlCommond = new MySqlCommand("", connect);

                var uuid = System.Guid.NewGuid().ToString("N");
               


                string sqlinsert = @"insert into wrjb_order(F_Id,pushOrderNo,OrderState,startlatitude,startlongitude,startplace,endlatitude,endlongitude,endplace,token,renshu,NeedTime,ActualTime,paymoney,Ordermoney) 
                                    VALUES('" + uuid + "','" + model.pushOrderNo + "','" + model.OrderState + "','" + model.startlatitude + "','" + model.startlongitude + "','" + model.startplace + "'," +
                                    "'" + model.endlatitude + "'," + model.endlongitude + "" +
                                    ",'" + model.endplace + "','" + model.token + "','" + model.renshu + "','" + model.NeedTime + "'" +
                                    ",'" + model.ActualTime + "','" + model.paymoney + "','" + model.Ordermoney + "')";

                sqlCommond.CommandText = sqlinsert;
                result = sqlCommond.ExecuteNonQuery();

                //如果是insert和update调用MySqlDataReader DataReader = sqlCommond.ExecuteReader();就可以了

                connect.Close();
                outmessage = model.pushOrderNo;
            }
            catch (Exception error)
            {
                outmessage = error.Message;
                Console.WriteLine(error.Message);

            }

            return result;
        }

        //叫车 创建订单
        public class resultCreateOrder
        {
            public string pushOrderNo { get; set; } //订单编号
            public string OrderState { get; set; } //订单状态
            public string startlatitude { get; set; }
            public string startlongitude { get; set; }
            public string startplace { get; set; }
            public string endlatitude { get; set; }
            public string endlongitude { get; set; }
            public string endplace { get; set; } //警告类型
           
            public double Ordermoney { get; set; }
            public double paymoney { get; set; }

            public int renshu { get; set; }
            public DateTime? NeedTime { get; set; }
            public DateTime? ActualTime { get; set; }
            public string token { get; set; }
            


        }

        //叫车 创建订单价格
        public class resultOrderMoney
        {
            public string pushOrderNo { get; set; } //订单编号
            public string OrderState { get; set; } //订单状态
            public string startlatitude { get; set; }
            public string startlongitude { get; set; }
            public string startplace { get; set; }
            public string endlatitude { get; set; }
            public string endlongitude { get; set; }
            public string endplace { get; set; } //警告类型

            public double Ordermoney { get; set; }
            public double paymoney { get; set; }

            public int renshu { get; set; }
            public DateTime? NeedTime { get; set; }
            public DateTime? ActualTime { get; set; }
            public string token { get; set; }



        }



    }
}
