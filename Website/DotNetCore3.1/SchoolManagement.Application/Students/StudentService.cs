using SchoolManagement.Core.Models;
using SchoolManagement.Core.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SchoolManagement.Application.Students
{
    public class StudentService : IStudentService
    {
        private readonly IRepository<Student, int> _studentRepository;
        public StudentService(IRepository<Student, int> studentRepository)
        {
            _studentRepository = studentRepository;
        }
        public async Task<List<Student>> GetPaginatedResult(string searchString, int currentPage,   int pageSize = 10)
        {
            return await GetQueryable(searchString).Skip((currentPage - 1) * pageSize).
                Take(pageSize).AsNoTracking().ToListAsync();
        }

        public async Task<int> GetCount(string searchString)
        {
            return await GetQueryable(searchString).CountAsync();
        }

        public IQueryable<Student> GetQueryable(string searchString)
        {
            var query = _studentRepository.GetAll();
            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(s => s.Name.Contains(searchString)
                                         || s.Email.Contains(searchString));
            }

            return query;
        }
    }
}
