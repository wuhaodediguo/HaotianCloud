using System.Collections.Generic;
using System.Threading.Tasks;
using HaotianCloud.Code;
using HaotianCloud.Domain;

namespace HaotianCloud.Service
{
    public interface IDatabaseTableService
    {
        Task<bool> DatabaseBackup(string backupPath);
        Task<List<TableInfo>> GetTableList(string tableName);
        Task<List<TableInfo>> GetTablePageList(string tableName, Pagination pagination);
        Task<List<TableFieldInfo>> GetTableFieldList(string tableName);
    }
}
