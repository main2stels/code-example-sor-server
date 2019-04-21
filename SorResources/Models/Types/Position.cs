using System;

namespace SorResources.Models.Types
{
    public sealed class Position
    {
        public float X { get; set; }

        public float Y { get; set; }

        public Position() { }

        public Position(double x, double y)
        {
            X = (float)x;
            Y = (float)y;
        }

        public static float Distance(Position a, Position b)
        {
            return (float)Math.Sqrt((b.X - a.X) * (double)(b.X - a.X) + (b.Y - a.Y) * (double)(b.Y - a.Y));
        }
    }
}
