using System.Threading.Tasks;
using HaotianCloud.Code;

namespace HaotianCloud.Service.AutoJob
{
    public interface IJobTask
    {
        //执行方法
        Task<AlwaysResult> Start();
    }
}
