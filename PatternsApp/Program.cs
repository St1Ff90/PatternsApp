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
            var b = new PersonFBuilder();
            var p = b.Called("Name").WorksAsA("Tester").Build() ;
        }




    }
}