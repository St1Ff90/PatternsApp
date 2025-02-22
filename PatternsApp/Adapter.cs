using Dynamitey;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternsApp
{

    public class SquareAdapter
    {
        public int Side;
    }

    public interface IRectangle
    {
        int Width { get; }
        int Height { get; }
    }

    public static class ExtensionMethods
    {
        public static int Area(this IRectangle rc)
        {
            return rc.Width * rc.Height;
        }
    }

    public class SquareToRectangleAdapter : IRectangle
    {
        private SquareAdapter MySquare;
        public SquareToRectangleAdapter(SquareAdapter square)
        {
            MySquare = square;
        }

        public int Width => MySquare.Side;

        public int Height => MySquare.Side;
        // todo
    }

    public class AdapterPoint
    {
        public int X, Y;

        public AdapterPoint(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return X + " " + Y;
        }


    }

    public class AdapterLine
    {
        public AdapterPoint Start, End;

        public AdapterLine(AdapterPoint start, AdapterPoint end)
        {
            Start = start;
            End = end;
        }
    }

    public abstract class VectorObject : Collection<AdapterLine> { }

    public class VectorRectangle : VectorObject
    {
        public VectorRectangle(int x, int y, int width, int height)
        {
            Add(new AdapterLine(new AdapterPoint(x, y), new AdapterPoint(x + width, y)));
            Add(new AdapterLine(new AdapterPoint(x + width, y), new AdapterPoint(x + width, y + height)));
            Add(new AdapterLine(new AdapterPoint(x, y), new AdapterPoint(x, y + height)));
            Add(new AdapterLine(new AdapterPoint(x, y + height), new AdapterPoint(x + width, y + height)));
        }
    }

    public class LineToPointAdapter : Collection<AdapterPoint>
    {
        private static int count = 0;


        public LineToPointAdapter(AdapterLine line)
        {
            Console.WriteLine($"{++count}: Generating points for line"
        + $" [{line.Start.X},{line.Start.Y}]-"
        + $"[{line.End.X},{line.End.Y}] (no caching)");

            int left = Math.Min(line.Start.X, line.End.X);
            int right = Math.Max(line.Start.X, line.End.X);
            int top = Math.Min(line.Start.Y, line.End.Y);
            int bottom = Math.Max(line.Start.Y, line.End.Y);

            if (right - left == 0)
            {
                for (int y = top; y <= bottom; ++y)
                {
                    Add(new AdapterPoint(left, y));
                }
            }
            else if (line.End.Y - line.Start.Y == 0)
            {
                for (int x = left; x <= right; ++x)
                {
                    Add(new AdapterPoint(x, top));
                }
            }
        }

    }


}
