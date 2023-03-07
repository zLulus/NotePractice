using Dapper;
using DotNet6WebAPI.Domain.Entities;

namespace DotNet6WebAPI.Dapper.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly DapperContext _context;
        public StudentRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<Student> Get(string id)
        {
            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<Student>("select * from students where id=@id", new { id = id });
            }
        }
    }
}
