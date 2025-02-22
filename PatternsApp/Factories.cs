using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace PatternsAppFactories
{

    #region lessonTask
    public class FactoryPerson
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public class PersonFactory
    {
        private int i = 0;

        public FactoryPerson CreatePerson(string name)
        {
            return new FactoryPerson { Id = i++, Name = name };
        }
    }
    #endregion

    public class Point
    {
        private double x, y;

        protected Point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public override string ToString()
        {
            return $"{nameof(x)}: {x}, {nameof(y)}: {y}";
        }

        public Point(double a,
          double b, // names do not communicate intent
          CoordinateSystem cs = CoordinateSystem.Cartesian)
        {
            switch (cs)
            {
                case CoordinateSystem.Polar:
                    x = a * Math.Cos(b);
                    y = a * Math.Sin(b);
                    break;
                default:
                    x = a;
                    y = b;
                    break;
            }

            // steps to add a new system
            // 1. augment CoordinateSystem
            // 2. change ctor
        }

        // factory method

        public static Point NewCartesianPoint(double x, double y) //Factory method
        {
            return new Point(x, y);
        }

        public static Point NewPolarPoint(double rho, double theta) //Factory method
        {
            return new Point(rho * Math.Cos(theta), rho * Math.Sin(theta));
        }

        public enum CoordinateSystem
        {
            Cartesian,
            Polar
        }

        // make it lazy
        public static class Factory
        {
            public static Point NewCartesianPoint(double x, double y)
            {
                return new Point(x, y);
            }
        }
    }

    class PointFactory
    {
        public static Point NewCartesianPoint(float x, float y)
        {
            return new Point(x, y); // needs to be public
        }
    }

}
