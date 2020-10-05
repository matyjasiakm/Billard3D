using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GkLAB
{
    public class BucketSortEdge
    {
        public double minY;
        public double maxY;
        public double minX;
        public double maxX;
        public double m;
        public Vertex from;
        public Vertex to;
        public Vector fromB;
        public Vector toB;
        public Color fromColor;
        public Color toColor;
        public Vector normalTo;
        public Vector normalFrom;

        public Vector toLightfrom;
        public Vector toLightto;
        public BucketSortEdge(Edge edge)
        {
            if (edge.From.Y < edge.To.Y)
            {
                from = edge.From;
                to = edge.To;
                fromColor = edge.colorFrom;
                toColor = edge.colorTo;
                toB = edge.toB;
                fromB = edge.fromB;
                normalFrom = edge.normalFrom;
                normalTo = edge.normalTo;
                toLightfrom = edge.toLightfrom;
                toLightto = edge.toLightto;
            }
            else
            {
                from = edge.To;
                to = edge.From;
                toColor = edge.colorFrom;
                fromColor = edge.colorTo;
                toB = edge.fromB;
                fromB = edge.toB;
                normalFrom = edge.normalTo;
                normalTo = edge.normalFrom;

                toLightfrom = edge.toLightto;
                toLightto = edge.toLightfrom;

            }
            minY = Math.Min(edge.From.Y, edge.To.Y);
            maxY = Math.Max(edge.From.Y, edge.To.Y);
            minX = edge.From.Y < edge.To.Y ? edge.From.X : edge.To.X;
            maxX= edge.From.X > edge.To.X ? edge.From.X : edge.To.X;
            m = (edge.From.X - edge.To.X) / (edge.From.Y - edge.To.Y);
        }
    }
}
