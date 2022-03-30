using SchoolManagement.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Students
{
    public interface IElasticsearchService
    {
        Task<IReadOnlyCollection<Student>> GetInfoOnRequestCompleted(string name);
        Task CreateIndex(string indexName, string indexAliasName);
    }
}
