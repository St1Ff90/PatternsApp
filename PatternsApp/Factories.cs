using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace PatternsApp
{
    public class FactoryPerson
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public class PersonFactory
    {
        public FactoryPerson[] FactoryList = new FactoryPerson[0];

        public FactoryPerson CreatePerson(string name)
        {
            int personsCount = FactoryList.Length;
            FactoryPerson factoryPerson = personsCount != 0 ? new FactoryPerson() { Name = name, Id = personsCount } : new FactoryPerson() { Name = name, Id = 0 };
            var temt = FactoryList;
            FactoryList = new FactoryPerson[personsCount + 1];
            for (int i = 0; i < temt.Length; i++)
            {
                FactoryList[i] = temt[i];
            }
            FactoryList[personsCount] = factoryPerson;
            
            return factoryPerson;
        }
    }


}
