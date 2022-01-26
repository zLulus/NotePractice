using System;
using System.Collections.Generic;
using System.Text;
using SchoolManagement.Core.Models;

namespace SchoolManagement.EntityFrameworkCore.DataRepositories
{
    public interface IStudentRepository
    {
        Student GetStudent(int id);
        IEnumerable<Student> GetAllStudents();
    }
}
