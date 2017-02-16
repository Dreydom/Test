using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HttpLog;

namespace ConsoleApplication
{
    class Program
    {
        enum Days { Sunday, Monday, Tuesday, Wednesday, Thursday, Friday, Saturday };
        static void Main(string[] args)
        {
            /*
            var httpLogHandler = new HttpLogHandler();
            httpLogHandler.Process( "http://test.com?user=max&pass=123456",
                "<auth><user>max</user><pass>123456</pass></auth>",
                "<auth user='max' pass='123456'>",
                "user");

            Console.WriteLine( httpLogHandler.CurrentLog );
            Console.ReadKey();
            */
            Console.Write("");
            
            string s = Enum.GetName(typeof(Days), 4);
            Console.WriteLine(s);

            Console.WriteLine("The values of the Days Enum are:");
            foreach (int i in Enum.GetValues(typeof(Days)))
                Console.WriteLine(i);

            Console.WriteLine("The names of the Days Enum are:");
            foreach (string str in Enum.GetNames(typeof(Days)))
                Console.WriteLine(str);
            Console.ReadKey();
        }
    }
}
