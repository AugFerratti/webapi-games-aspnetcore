using System;

namespace WebApiGames_Demo.Services
{
    public class MyService : IMyService
    {
        public string Hello(string name)
        {
            return $"Welcome, {name} \n\n{DateTime.Now}";
        }
    }
}
