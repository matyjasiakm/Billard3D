using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GkLAB
{
    public class Vertex
    {
        public double X;
        public double Y;
        public double Z;
        public double W;
        double[,] matrix;
        public Vertex(double x, double y,double z=1)
        {
            X = x;
            Y = y;
            Z = z;
            W = 1;
            
        }
        public void Translate(Vector vector)
        {
            X += vector.U;
            Y += vector.V;
            Z += vector.W;
        }
        
        public MyMatrix Matrix()
        {
            
            MyMatrix matrix = new MyMatrix(4, 1);
            matrix.matrix[0, 0] = X;
            matrix.matrix[1, 0] = Y;
            matrix.matrix[2, 0] = Z;
            matrix.matrix[3, 0] = W;
            
            return matrix;
        }
        public Vector get3DVector()
        {
            return new Vector(X, Y, Z);
        }
        public void Normalize()
        {
            double sum = Math.Sqrt(X * X + Y * Y + Z * Z);

        }
        public static double Distance(Vertex v1,Vertex v2)
        {
            return Math.Sqrt((v1.X - v2.X) * (v1.X - v2.X) + (v1.Y - v2.Y) * (v1.Y - v2.Y) + (v1.Z - v2.Z) * (v1.Z - v2.Z));
        }
    }
}
