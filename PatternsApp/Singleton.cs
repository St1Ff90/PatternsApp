using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternsApp
{
    public class SingeltonDatabase
    {
        private Dictionary<string, int> capitals;
        private static int instanceCount;
        public static int Cout => instanceCount;

        private SingeltonDatabase()
        {
            capitals = new Dictionary<string, int>();
        }

        //public static SingeltonDatabase Instance = new SingeltonDatabase();

        private static Lazy<SingeltonDatabase> instance = new Lazy<SingeltonDatabase>(() =>
        {
            instanceCount++;
            return new SingeltonDatabase();
        });

        public static SingeltonDatabase Instance => instance.Value;

    }
    public class SingeltonRecordFinger
    {
        public int TotalPopulation()
        {
            return 0;
        }

       

    }
}
