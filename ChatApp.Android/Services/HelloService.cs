using System;
using ChatApp.Services;

namespace ChatApp.Android.Services
{
    public class HelloService : IHelloService
    {
        public void GetMessage()
        {
            Console.WriteLine("This is an Android");
        }
    }
}