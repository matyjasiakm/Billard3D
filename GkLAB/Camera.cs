using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GkLAB
{
    public class Camera
    {
        public Vector Position { get; private set; }
        Vector Target;
        Vector UpVector;
        public MyMatrix viewMatrix { get; private set; }

        public Camera(Vector positon, Vector target, Vector upvector)
        {
            Position = positon;
            Target = target;
            UpVector = upvector;


            Vector zaxis = (Position - Target).getNormalizedVectorIn3D();
            Vector xaxis = Vector.CrossProduct(upvector, zaxis).getNormalizedVectorIn3D();
            Vector yaxis = Vector.CrossProduct(zaxis, xaxis).getNormalizedVectorIn3D();

            MyMatrix pom1 = new MyMatrix(4, 4);
            pom1[0, 0] = xaxis.U;
            pom1[0, 1] = xaxis.V;
            pom1[0, 2] = xaxis.W;

            pom1[1, 0] = yaxis.U;
            pom1[1, 1] = yaxis.V;
            pom1[1, 2] = yaxis.W;

            pom1[2, 0] = zaxis.U;
            pom1[2, 1] = zaxis.V;
            pom1[2, 2] = zaxis.W;

            pom1[3, 3] = 1;

            MyMatrix pom2 = new MyMatrix(4, 4);
            pom2[0, 3] = -Position.U;
            pom2[1, 3] = -Position.V;
            pom2[2, 3] = -Position.W;
            pom2[0, 0] = 1;
            pom2[1,1] = 1;
            pom2[2,2] = 1;
            pom2[3,3] = 1;


            viewMatrix = (pom1 * pom2);

        }
        public Vertex getPositionInVertex()
        {
            return new Vertex(Position.U, Position.V, Position.W);
        }

    }
}
