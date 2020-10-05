using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;
using System.Drawing;
using static System.Math;

namespace GkLAB
{
    public class MyMatrix
    {
        public double[,] matrix;
        public int row;
        public int col;
        public MyMatrix(int x, int y)
        {
            matrix = new double[x, y];
            row = x;
            col = y;
        }

        public double this[int x, int y]
        {
            get { return matrix[x, y]; }
            set { matrix[x, y] = value; }
        }

        public Vertex GetVertex()
        {

            return new Vertex(matrix[0, 0], matrix[1, 0], matrix[2, 0]);
        }
        public static MyMatrix operator *(MyMatrix m1, MyMatrix m2)
        {
            MyMatrix matrix = new MyMatrix(m1.row, m2.col);
            for (int i = 0; i < m1.row; i++)
            {
                for (int j = 0; j < m2.col; j++)
                {
                    for (int p = 0; p < m2.row; p++)
                        matrix.matrix[i, j] += m1.matrix[i, p] * m2.matrix[p, j];
                }
            }
            return matrix;

        }

        public static MyMatrix operator /(MyMatrix m1, double d)
        {
            MyMatrix matrix = new MyMatrix(m1.row, m1.col);
            for (int i = 0; i < m1.row; i++)
            {
                for (int j = 0; j < m1.col; j++)
                {
                    matrix.matrix[i, j] = m1.matrix[i, j] / d;
                }
            }
            return matrix;

        }

        public Vector getNormalize3DVector()
        {
            double sum = 0;
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    sum += matrix[i, j] * matrix[i, j];
                }
            }
            if (sum == 0) return new Vector();
            sum = Sqrt(sum);

            return new Vector(matrix[0, 0] / sum, matrix[1, 0] / sum, matrix[2, 0] / sum);
        }

        public Vector get3DVector()
        {
            return new Vector(matrix[0, 0], matrix[1, 0], matrix[2, 0]);
        }

    }

    public class Point3d
    {
        public double x;
        public double y;
        public double z;
        public double w;
        public double q;
        public MyMatrix matrix;
        public Point3d(double _x, double _y, double _z)
        {
            x = _x;
            y = _y;
            z = _z;
            w = 1;
            q = Sqrt(x * x + y * y + z * z + w * w);
            matrix = new MyMatrix(4, 1);
            matrix.matrix[0, 0] = x;
            matrix.matrix[1, 0] = y;
            matrix.matrix[2, 0] = z;
            matrix.matrix[3, 0] = w;
        }

    }
}
