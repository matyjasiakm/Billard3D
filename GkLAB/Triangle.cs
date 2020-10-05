
using GkLAB;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Math;
namespace GkLAB
{
    public enum FillType
    {
        none,
        color,
        texture
    };
    public class Triangle
    {
        public Edge[] edges;
        public Vertex[] vertices;
        public Color color;
        public Vector normal1, normal2, normal3;
        public Triangle(Vertex a, Vertex b, Vertex c)
        {
            edges = new Edge[] { new Edge(a, b), new Edge(b, c), new Edge(c, a) };
            vertices = new Vertex[] { a, b, c };
            Random r = new Random(Guid.NewGuid().GetHashCode());
            color = Color.FromArgb(255, r.Next(255), r.Next(255), r.Next(255));
        }
        public Triangle(Vertex a, Vertex b, Vertex c, Color color)
        {
            edges = new Edge[] { new Edge(a, b), new Edge(b, c), new Edge(c, a) };
            vertices = new Vertex[] { a, b, c };

            this.color = color;

            normal1 = Vector.CrossProduct(new Vector(a, c), new Vector(a, b));
            normal2 = Vector.CrossProduct(new Vector(b, a), new Vector(b, c));
            normal3 = Vector.CrossProduct(new Vector(c, b), new Vector(c, a));

        }
        public Triangle(Vertex a, Vertex b, Vertex c, Vector normal, Color color)
        {
            edges = new Edge[] { new Edge(a, b), new Edge(b, c), new Edge(c, a) };
            vertices = new Vertex[] { a, b, c };

            this.color = color;

            normal1 = normal;
            normal2 = normal;
            normal3 = normal;

        }
        public Triangle(Vertex a, Vertex b, Vertex c, Vector normal1, Vector normal2, Vector normal3, Color color)
        {
            edges = new Edge[] { new Edge(a, b), new Edge(b, c), new Edge(c, a) };
            vertices = new Vertex[] { a, b, c };

            this.color = color;

            this.normal1 = normal1.getNormalizedVectorIn3D();
            this.normal2 = normal2.getNormalizedVectorIn3D();
            this.normal3 = normal3.getNormalizedVectorIn3D();
        }
        public void SetColor(Color color)
        {
            this.color = color;
        }
    }
}
