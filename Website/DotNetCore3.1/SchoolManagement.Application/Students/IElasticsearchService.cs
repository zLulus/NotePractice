using SchoolManagement.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Students
{
    public interface IElasticsearchService
    {
        Task<IReadOnlyCollection<StudentForElasticsearch>> GetInfoOnRequestCompleted(string indexAliasName, string name);
        Task CreateIndex(string indexName, string indexAliasName);
        Task AddOrUpdateData(string indexAliasName, IList<StudentForElasticsearch> datas);
        Task DeleteData(string indexAliasName, IList<int> deleteIds);
    }
}
