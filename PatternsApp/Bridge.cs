using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PatternsApp
{
    public abstract class Shape
    {
        public string Name { get; set; }
        protected IRenderer renderer { get; set; }
        public Shape(IRenderer renderer)
        {
            this.renderer = renderer;
            Name = string.Empty;
        }

        public override string ToString()
        {
            return $"Drawing {Name} as {renderer.WhatToRenderAs}";
        }

    }
    public interface IRenderer
    {
        string WhatToRenderAs { get; }
    }

    public class Triangle : Shape
    {
        public Triangle(IRenderer renderer) : base(renderer)
        {
            Name = "Triangle";
        }
    }

    public class Square : Shape
    {
        public Square(IRenderer renderer) : base(renderer)
        {
            Name = "Square";
        }
    }

    public class VectorRenderer : IRenderer
    {
        public string WhatToRenderAs => "lines";
    }
    public class RasterRenderer : IRenderer
    {
        public string WhatToRenderAs => "pixels";
    }



}
