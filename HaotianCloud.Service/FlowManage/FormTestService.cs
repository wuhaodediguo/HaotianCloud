/*******************************************************************************
 * Copyright © 2020 HaotianCloud.Framework 版权所有
 * Author: HaotianCloud
 * Description: HaotianCloud快速开发平台
 * Website：
*********************************************************************************/
using System.Threading.Tasks;
using HaotianCloud.Code;
using Chloe;
using HaotianCloud.Domain.FlowManage;

namespace HaotianCloud.Service.FlowManage
{
    public class FormTestService : DataFilterService<FormTestEntity>, IDenpendency,ICustomerForm
    {
        public FormTestService(IDbContext context) : base(context)
        {
        }
        public async Task Add(string flowInstanceId, string frmData)
        {
            var req = frmData.ToObject<FormTestEntity>();
            req.F_FlowInstanceId = flowInstanceId;
            req.Create();
            req.F_CreatorUserName = currentuser.UserName;
            await repository.Insert(req);
        }
        public async Task Edit(string flowInstanceId, string frmData)
        {
            var req = frmData.ToObject<FormTestEntity>();
            req.F_FlowInstanceId = flowInstanceId;
            await repository.Update(a => a.F_FlowInstanceId == req.F_FlowInstanceId, a => new FormTestEntity
            {
                F_Attachment = req.F_Attachment,
                F_EndTime = req.F_EndTime,
                F_StartTime = a.F_StartTime,
                F_RequestComment = a.F_RequestComment,
                F_RequestType = a.F_RequestType,
                F_UserName = a.F_UserName

            });
        }
    }
}
