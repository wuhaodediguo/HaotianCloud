using System.IO;
using System.Net;
using System.Text;


namespace HaotianCloud.WebApi.CommonFunction
{
    public class CommonHelper
    {
        public static string PostMethods(string url, string param)
        {
            string strURL = url;
            System.Net.HttpWebRequest request;
            request = (System.Net.HttpWebRequest)WebRequest.Create(strURL);
            request.Method = "POST";
            request.ContentType = "application/json";
            string paraUrlCoded = param;
            byte[] payload;
            payload = System.Text.Encoding.UTF8.GetBytes(paraUrlCoded);
            request.ContentLength = payload.Length;
            Stream writer = request.GetRequestStream();
            writer.Write(payload, 0, payload.Length);
            writer.Close();
            System.Net.HttpWebResponse response;
            response = (System.Net.HttpWebResponse)request.GetResponse();
            StreamReader mystreamreader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            string txt = mystreamreader.ReadToEnd();
            return txt;
        }
        public static string GetMethods(string url)
        {
            string serviceAddress = url;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serviceAddress);
            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8);
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();
            return retString;
        }


        //public static DataTable Retrieve(int pageNumber, int pageSize, string sort, Dictionary<string, object> criteria, string TableName, out int recordCount)
        //{
        //    WinformPager pager = new WinformPager();
        //    pager.ConnStr = Connection.ConnString();
        //    pager.TableName = TableName;
        //    pager.fldName = sort;
        //    pager.strGetFields = "*";
        //    pager.where = " 1=1 ";
        //    pager.where += CommonDicHelper.BoundWhereCriteria(criteria);

        //    pager.CurrentPageIndex = pageNumber;
        //    pager.PageSize = pageSize;
        //    recordCount = pager.GetRecordCount();
        //    return pager.GetTable();
        //}
    
    }

    public class Result
    {
        public bool result { set; get; }
        public string msg { set; get; }
        public int total { set; get; }
        public object data { set; get; }
        
    }
}