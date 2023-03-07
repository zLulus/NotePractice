namespace DotNet6WebAPI.Domain.Entities
{
    /// <summary>
    /// interface for students(学生的仓储接口)
    /// </summary>
    public interface IStudentRepository
    {
        /// <summary>
        /// Search student information by id(根据id查询学生信息)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Student> Get(string id);
    }
}
