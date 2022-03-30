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
        public async Task<IReadOnlyCollection<Student>> Get(string name)
        {
            return await _elasticsearchService.GetInfoOnRequestCompleted(name);
        }

        [HttpGet(nameof(CreateIndex))]
        public async Task CreateIndex(string indexName, string indexAliasName)
        {
            await _elasticsearchService.CreateIndex(indexName,indexAliasName);
        }
    }
}
