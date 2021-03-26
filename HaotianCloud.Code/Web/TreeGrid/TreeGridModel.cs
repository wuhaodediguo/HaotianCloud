/*******************************************************************************
 * Copyright © 2016 HaotianCloud.Framework 版权所有
 * Author: HaotianCloud
 * Description: HaotianCloud快速开发平台
 * Website：
*********************************************************************************/

using System.Collections.Generic;

namespace HaotianCloud.Code
{
    public class TreeGridModel
    {
        public string id { get; set; }
        public string parentId { get; set; }
        public string title { get; set; }
        public object self { get; set; }
        public object checkArr { get; set; }
        public bool? disabled { get; set; }
        public List<TreeGridModel> children { get; set; }
    }
}
