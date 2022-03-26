using System.Threading.Tasks;

namespace SchoolManagement.Application.Students
{
    public interface IElasticsearchService
    {
        Task GetInfoOnRequestCompleted();
    }
}
