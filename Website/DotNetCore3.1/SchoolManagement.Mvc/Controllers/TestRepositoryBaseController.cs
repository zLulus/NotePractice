using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Core.Models;
using SchoolManagement.Core.Repositories;
using System.Threading.Tasks;

namespace SchoolManagement.Mvc.Controllers
{
    //测试仓储服务
    [Route("api/[controller]")]
    [ApiController]
    public class TestRepositoryBaseController : ControllerBase
    {
        private readonly IRepository<Student, int> _studentRepository;
        public TestRepositoryBaseController(IRepository<Student, int> studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<string> TestRepositoryBase()
        {
            var student = await _studentRepository.GetAll().FirstOrDefaultAsync();
            var oop = await _studentRepository.SingleAsync(a => a.Id == 2);
            var longCount = await _studentRepository.LongCountAsync();
            var count = _studentRepository.Count();
            return $"{oop?.Name}+{student?.Name}+{longCount}+{count}";
        }
    }
}
