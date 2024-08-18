using System;
using System.Dynamic;
using ImpromptuInterface;
using PatternsApp;
using static System.Console;
using static PatternsApp.Person;

namespace MyApp // Note: actual namespace depends on the project name.
{


    internal class Program
    {

        static void Main(string[] args)
        {
            var pf = new PersonFactory();

            for (int i = 0; i < 10; i++)
            {
                var person = pf.CreatePerson("user");
            }

            Console.ReadLine();
        }




    }
}