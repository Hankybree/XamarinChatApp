using System;
using ChatApp.Services;

namespace ChatApp.iOS.Services
{
    public class HelloService : IHelloService
    {
        public void GetMessage()
        {
            Console.WriteLine("This is an iPhone");
        }
    }
}