using System;

namespace DotNet5Website.Services
{
    public interface IMyDependency
    {
        string WriteMessage(string message);
    }

    public class MyDependency : IMyDependency
    {
        public string WriteMessage(string message)
        {
            return $"MyDependency.WriteMessage Message: {message}";
        }
    }

    public class MyDependency2 : IMyDependency
    {
        public string WriteMessage(string message)
        {
            return $"MyDependency2.WriteMessage Message: {message}";
        }
    }
}
