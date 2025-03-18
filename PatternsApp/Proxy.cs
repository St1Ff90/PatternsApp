using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternsApp
{
    public class Person
    {
        public int Age { get; set; }

        public string Drink()
        {
            return "drinking";
        }

        public string Drive()
        {
            return "driving";
        }

        public string DrinkAndDrive()
        {
            return "driving while drunk";
        }
    }

    public class ResponsiblePerson
    {
        private Person _person;
        public ResponsiblePerson(Person person)
        {
            _person = person;
        }

        public int Age
        {
            get { return _person.Age; }
            set { _person.Age = value; }
        }

        private static string toYoungError = "too young";

        public string Drink()
        {
            if (Age >= 18)
            {
                return _person.Drink();
            }
            else
            {
                return toYoungError;
            }
        }

        public string Drive()
        {
            if (Age >= 16)
            {
                return _person.Drive();
            }
            else
            {
                return toYoungError;
            }
        }
        public string DrinkAndDrive()
        {
            return "dead";
        }


    }
}
