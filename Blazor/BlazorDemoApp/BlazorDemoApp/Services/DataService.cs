namespace BlazorDemoApp.Services
{
    public interface IDataService
    {
        string GetData();
    }

    public class DataService : IDataService
    {
        public string GetData()
        {
            return "Data from the service!";
        }
    }
}
