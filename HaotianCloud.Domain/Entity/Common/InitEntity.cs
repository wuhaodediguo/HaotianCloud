using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HaotianCloud.Domain
{
    public class InitEntity
    {
        public HomeInfoEntity homeInfo { get; set; }
        public LogoInfoEntity logoInfo { get; set; }
        public List<MenuInfoEntity> menuInfo { get; set; }
    }

    /// <summary>
    ///     LayUIAdmin后端UI回调Json实体
    /// </summary>
    public class AdminUiCallBack
    {
        /// <summary>
        ///     状态码(ok = 0, error = 1, timeout = 401)
        /// </summary>
        public int code { get; set; } = 1;

        /// <summary>
        ///     可选。信息内容。
        /// </summary>
        public string msg { get; set; } = "空数据返回";

        public string state { get; set; } = "error";

        public object data { get; set; }

        public object otherData { get; set; }

        public int count { get; set; }
    }

}