using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SchoolManagement.Core.Models;

namespace SchoolManagement.Application.Students
{
    public interface IStudentService
    {
        Task<List<Student>> GetPaginatedResult(string searchString, int currentPage,int pageSize = 10);
        Task<int> GetCount(string searchString);
    }
}
