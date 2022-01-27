using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Core.Models;
using SchoolManagement.Core.Repositories;

namespace SchoolManagement.Mvc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestRepositoryBaseController : ControllerBase
    {
        private readonly IRepository<Student,int> _studentRepository;
        public TestRepositoryBaseController(IRepository<Student,int> studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<string> TestRepositoryBase()
        {
            var student =  _studentRepository.GetAllList().FirstOrDefault();
            var oop = await _studentRepository.SingleAsync(a => a.Id == 2);
            var longCount = await _studentRepository.LongCountAsync();
            var count = _studentRepository.Count();
            return $"{oop?.Name}+{student?.Name}+{longCount}+{count}";
        }
    }
}
