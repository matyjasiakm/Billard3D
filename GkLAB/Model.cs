using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Math;

namespace GkLAB
{
    public abstract class Model
    {
        public Triangle[] triangle;
        public Vertex[] vertex;
        public MyMatrix Translation;
        public MyMatrix RotationZ;
        public MyMatrix RotationX;
        public double angleInRadian;
        public double angleInRadianX;
        public Model()
        {
            Translation = new MyMatrix(4, 4);
            Translation[0, 0] = 1;
            Translation[1, 1] = 1;
            Translation[2, 2] = 1;
            Translation[3, 3] = 1;

            RotationZ = new MyMatrix(4, 4);
            RotationX = new MyMatrix(4, 4);

            angleInRadian = 0;
            angleInRadianX = 0;

            RotationZ.matrix = new double[,]
            {
                { 1, 0, 0, 0 },
                 { 0, 1, 0, 0 },
                 { 0, 0, 1, 0},
                 { 0, 0, 0, 1}
            };

            RotationX.matrix = new double[,]
            {
                { 1, 0, 0, 0},
                 { 0, 1, 0, 0},
                 { 0, 0, 1, 0},
                 { 0, 0, 0, 1}
            };

        }

        public MyMatrix getModelMatrix()
        {
            RotationZ.matrix = new double[,]
            {
                { Cos(angleInRadian), -Sin(angleInRadian), 0, 0 },
                 { Sin(angleInRadian), Cos(angleInRadian), 0, 0 },
                 { 0, 0, 1, 0},
                 { 0, 0, 0, 1}
            };

            RotationX.matrix = new double[,]
            {
                { 1, 0, 0, 0},
                { 0, 1, 0, 0},
                { 0,0,Cos(angleInRadianX), -Sin(angleInRadianX)},
                { 0,0,Sin(angleInRadianX), Cos(angleInRadianX)}

            };

            return Translation * RotationZ * RotationX;
        }
    }
}
