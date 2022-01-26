using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Core.Common;
using SchoolManagement.EntityFrameworkCore.DataRepositories;
using SchoolManagement.Mvc.ViewModels;

namespace SchoolManagement.Mvc.Controllers
{
    //常规路由
    public class StudentController : Controller
    {
        private readonly IStudentRepository _studentRepository;
        public StudentController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        // GET: Student/Details/1
        public ViewResult Details(int id)
        {
            var student = _studentRepository.GetStudent(id);
            if (student == null)
                throw new CustomException($"查询不到id为{id}的学生信息");
            ViewBag.PageTitle = "学生详情";
            return View(student);
        }

        // GET: Student/DetailsWithViewModel/1
        public ViewResult DetailsWithViewModel(int id)
        {
            var student = _studentRepository.GetStudent(id);
            if (student == null)
            {
                Response.StatusCode = 404;
                return View("StudentNotFound", id);
            }
            StudentDetailsViewModel viewModel = new StudentDetailsViewModel()
            {
                Student = student,
                Title = "学生详情-来自vm"
            };
            return View(viewModel);
        }

        // GET: Student/Edit
        public ActionResult Edit()
        {
            return View();
        }

        // GET: Student/List
        public ActionResult List()
        {
            //查询所有的学生信息
            var model = _studentRepository.GetAllStudents();
            //将学生列表传递到视图
            return View(model);
        }
    }
}
