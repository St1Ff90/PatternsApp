using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternsApp
{
    public class Decorator
    {

        public class Bird
        {
            public int Age { get; set; }

            public string Fly()
            {
                return (Age < 10) ? "flying" : "too old";
            }
        }

        public class Lizard
        {
            public int Age { get; set; }

            public string Crawl()
            {
                return (Age > 1) ? "crawling" : "too young";
            }
        }

        public class Dragon // no need for interfaces
        {
            private readonly Lizard _lizard = new Lizard();
            private readonly Bird _bird = new Bird();

            public int Age
            {
                get { return _bird.Age; }
                set { _bird.Age = _lizard.Age = value; }
            }

            public string Fly()
            {
                return _bird.Fly();
            }

            public string Crawl()
            {
                return _lizard.Crawl();
            }
        }

    }
}
