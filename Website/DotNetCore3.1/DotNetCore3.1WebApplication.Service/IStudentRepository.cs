using DotNetCore3._1WebApplication.Model;
using System;
using System.Collections.Generic;

namespace DotNetCore3._1WebApplication.Service
{

    public interface IStudentRepository
    {
        Student GetStudent(int id);
        IEnumerable<Student> GetAllStudents();
    }
}
