using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Math;

namespace GkLAB
{
    public class Sphere : Model
    {
        public Vertex middlePoint;
        public Vertex backPointOnTop;
        public Vertex frontPointOnTop;
        public Vertex topVertexOnSphere;
        public double r;
        public Sphere(double r = 1, Color color = default(Color), int withStripInTheMiddle = 0)
        {
            backPointOnTop = new Vertex(-2 * r, 0, 2 * r);
            frontPointOnTop = new Vertex(r, 0, 2 * r);
            topVertexOnSphere = new Vertex(0, 0, r);

            List<Triangle> pom = new List<Triangle>();
            List<Vertex> pom2 = new List<Vertex>();
            double step = PI / 10;
            this.r = r;

            for (double fi = -PI; fi < PI; fi += step)
            {
                for (double thetha = 0; thetha < PI; thetha += step)
                {
                    Vertex p1 = new Vertex(r * Sin(thetha) * Cos(fi), r * Sin(thetha) * Sin(fi), r * Cos(thetha));
                    Vertex p2 = new Vertex(r * Sin(thetha) * Cos(fi + step), r * Sin(thetha) * Sin(fi + step), r * Cos(thetha));
                    Vertex p3 = new Vertex(r * Sin(thetha + step) * Cos(fi), r * Sin(thetha + step) * Sin(fi), r * Cos(thetha + step));
                    Vertex p4 = new Vertex(r * Sin(thetha + step) * Cos(fi + step), r * Sin(thetha + step) * Sin(fi + step), r * Cos(thetha + step));
                    
                    pom2.Add(p1);
                    pom2.Add(p2);
                    pom2.Add(p3);
                    pom2.Add(p4);
                   
                    if (withStripInTheMiddle == 1)
                    {
                        if (Abs(r * Sin(thetha) * Cos(fi)) < r / 2)
                        {
                            pom.Add(new Triangle(p1, p2, p3, p1.get3DVector(), p2.get3DVector(), p3.get3DVector(), Color.DarkGray));
                            pom.Add(new Triangle(p3, p2, p4, p3.get3DVector(), p2.get3DVector(), p4.get3DVector(), Color.DarkGray));
                            continue;
                        }
                    }

                    pom.Add(new Triangle(p1, p2, p3, p1.get3DVector(), p2.get3DVector(), p3.get3DVector(), color));
                    pom.Add(new Triangle(p3, p2, p4, p3.get3DVector(), p2.get3DVector(), p4.get3DVector(), color));
                }
            }
            triangle = pom.ToArray();
            vertex = pom2.ToArray();
            middlePoint = new Vertex(0, 0, 0);
        }


        public void SetColor(Color color)
        {
            foreach (var elem in triangle)
            {
                elem.SetColor(color);
            }
        }
        public void Translate(Vector vector)
        {
            Translation[0, 3] += vector.U;
            Translation[1, 3] += vector.V;
            Translation[2, 3] += vector.W;
        }

        public void RotationAxisinMiddleZ(double alfa)
        {
            angleInRadian += alfa;
        }

    }
}
