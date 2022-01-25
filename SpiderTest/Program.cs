using System;
using SpiderLib;


namespace SpiderTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var url = "https://habr.com/ru/post/578588/";
            var xpath = "//a";
            
            Console.WriteLine("Spider started...");
            
            Spider spider = new Spider(url, xpath);
            spider.Start();
        }
    }
}
