using System;
using System.Dynamic;
using System.Runtime.InteropServices;
using ImpromptuInterface;
using PatternsApp;
using static System.Console;
using static PatternsApp.Person;

namespace PatternsApp // Note: actual namespace depends on the project name.
{
    internal class Program
    {





        private static readonly List<VectorObject> vectorObjects = new List<VectorObject>()
        {
            new VectorRectangle(1,1,10,10),
            new VectorRectangle(3,3,6,6),
        };

        public static void DrowPoint(AdapterPoint p)
        {
            Console.WriteLine(".");
        }

        private static void DrawPoints()
        {
            foreach (var vo in vectorObjects)
            {
                foreach (var line in vo)
                {
                    var adapter = new LineToPointAdapter(line);
                    foreach (var item in adapter)
                    {
                        DrowPoint(item);
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            MagicSquareGenerator magicSquareGenerator = new MagicSquareGenerator();

            magicSquareGenerator.Generate(3);



        }





    }
}