using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.Students;
using SchoolManagement.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolManagement.Mvc.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ElasticsearchTestController:ControllerBase
    {
        private IElasticsearchService _elasticsearchService;
        public ElasticsearchTestController(IElasticsearchService elasticsearchService)
        {
            _elasticsearchService = elasticsearchService;
        }

        [HttpGet(nameof(Get))]
        public async Task<IReadOnlyCollection<StudentForElasticsearch>> Get(string indexAliasName,string name)
        {
            return await _elasticsearchService.GetInfoOnRequestCompleted(indexAliasName,name);
        }

        [HttpGet(nameof(CreateIndex))]
        public async Task CreateIndex(string indexName, string indexAliasName)
        {
            await _elasticsearchService.CreateIndex(indexName,indexAliasName);
        }

        [HttpGet(nameof(AddOrUpdateData))]
        public async Task AddOrUpdateData(string indexAliasName)
        {
            IList<StudentForElasticsearch> datas = new List<StudentForElasticsearch>();
            datas.Add(new StudentForElasticsearch() { Id = 1, Name = "Student1", Email = "11111@qq.com" });
            datas.Add(new StudentForElasticsearch() { Id = 2, Name = "Student2", Email = "2222@qq.com" });
            await _elasticsearchService.AddOrUpdateData(indexAliasName, datas);
        }

        [HttpGet(nameof(DeleteData))]
        public async Task DeleteData(string indexAliasName)
        {
            IList<int> datas = new List<int>();
            datas.Add(1);
            await _elasticsearchService.DeleteData(indexAliasName, datas);
        }
    }
}
