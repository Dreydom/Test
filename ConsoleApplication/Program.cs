using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HttpLog;

namespace ConsoleApplication
{
    class Program
    {
        static void Main( string[] args )
        {
            var httpLogHandler = new HttpLogHandler();
            httpLogHandler.Process( "http://test.com?user=max&pass=123456",
                "<auth><user>max</user><pass>123456</pass></auth>",
                "<auth user='max' pass='123456'>",
                "user");

            Console.WriteLine( httpLogHandler.CurrentLog );
            Console.ReadKey();
        }
    }
}
