using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GkLAB
{
    public class Vector
    {

        public double U { get; set; }
        public double V { get; set; }
        public double W { get; set; }
        public double Z { get; set; }

        MyMatrix matrix;
        public Vector(Vertex from, Vertex to)
        {
            U = to.X - from.X;
            V = to.Y - from.Y;
            W = to.Z - from.Z;
            Z = 0;
        }
        public Vector(double u, double v, double w, double z = 0)
        {
            U = u;
            V = v;
            W = w;
            Z = z;
        }
        public Vector()
        {
            U = 0;
            V = 0;
            W = 0;
        }

        public static Vector operator *(Vector v, double q)
        {
            return new Vector(v.U * q, v.V * q, v.W * q);
        }
        public MyMatrix getMatrix()
        {
            MyMatrix my = new MyMatrix(4, 1);
            my[0, 0] = U;
            my[1, 0] = V;
            my[2, 0] = W;
            my[3, 0] = Z;
            return my;
        }
        public static Vector CrossProduct(Vector v1, Vector v2)
        {
            Vector v = new Vector();
            v.U = v1.V * v2.W - v1.W * v2.V;
            v.V = v1.W * v2.U - v1.U * v2.W;
            v.W = v1.U * v2.V - v1.V * v2.U;

            return v;
        }
        public static Vector operator -(Vector v1, Vector v2)
        {
            Vector v = new Vector();
            v.U = v1.U - v2.U;
            v.V = v1.V - v2.V;
            v.W = v1.W - v2.W;

            return v;
        }
        public static Vector operator +(Vector v1, Vector v2)
        {
            Vector v = new Vector();
            v.U = v1.U + v2.U;
            v.V = v1.V + v2.V;
            v.W = v1.W + v2.W;

            return v;
        }

        public static Vertex operator +(Vertex vertex, Vector vector)
        {
            return new Vertex(vertex.X + vector.U, vertex.Y + vector.V, vertex.Z + vector.W);
        }
        public Vector InverseVector()
        {
            return new Vector(-U, -V, -W);
        }
        public MyMatrix getNormalizedVectorInMatrix3D()
        {
            MyMatrix my = new MyMatrix(3, 1);
            double pom = U * U + V * V + W * W;
            pom = Math.Sqrt(pom);
            my[0, 0] = U / pom;
            my[1, 0] = V / pom;
            my[2, 0] = W / pom;

            return my;
        }

        public Vector getNormalizedVectorIn3D()
        {
            Vector my = new Vector();
            double pom = U * U + V * V + W * W;
            if (pom == 0)
            {
                my.U = U;
                my.W = W;
                my.V = V;
                return my;
            }
            pom = Math.Sqrt(pom);

            my.U = U / pom;
            my.V = V / pom;
            my.W = W / pom;

            return my;
        }
        public static double DotProduct(Vector v1, Vector v2)
        {
            return v1.U * v2.U + v1.V * v2.V + v1.W * v2.W;
        }
        public String toString()
        {
            return U + " " + V + " " + W;
        }
        public double Length3D()
        {
            return Math.Sqrt(U * U + V * V + W * W);
        }
    }
}
