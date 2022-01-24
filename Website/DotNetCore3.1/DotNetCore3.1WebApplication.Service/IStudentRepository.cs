using DotNetCore3._1WebApplication.Model;
using System;

namespace DotNetCore3._1WebApplication.Service
{

    public interface IStudentRepository
    {
        Student GetStudent(int id);
    }
}
