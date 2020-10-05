using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GkLAB
{
    /// <summary>
    /// Class for table model
    /// </summary>
    public class Table : Model
    {
        Vertex middlePoint;
        public Table(double width, double length, double height, double bandHeight)
        {
            Color cloth = Color.ForestGreen;
            Color wood = Color.SaddleBrown;
            middlePoint = new Vertex(0, 0, 0);

            List<Triangle> pom = new List<Triangle>();

            int n = 10;
            double step = width / n;
            double step2 = length / n;
            double stepBoundHeight = bandHeight / n;
            double d1 = length / 8;
            double d2 = width / 8;
            double stepUpWidth = d2 / n;
            double stepUpLength = d1 / n;
            double stepLeg = height / 2;
            double m = 50;
            double stepp = width / m;
            double step2p = length / m;

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (i == m / 2)
                    {
                        pom.Add(new Triangle(new Vertex(-width / 2 + stepp * i, length / 2 - step2p * j, 0), new Vertex(-width / 2 + stepp * (i + 1), length / 2 - step2p * (j), 0), new Vertex(-width / 2 + stepp * i, length / 2 - step2p * (j + 1), 0), Color.Red));
                        pom.Add(new Triangle(new Vertex(-width / 2 + stepp * (i), length / 2 - step2p * (j + 1), 0), new Vertex(-width / 2 + stepp * (i + 1), length / 2 - step2p * (j), 0), new Vertex(-width / 2 + stepp * (i + 1), length / 2 - step2p * (j + 1), 0), Color.Red));
                        continue;
                    }
                    pom.Add(new Triangle(new Vertex(-width / 2 + stepp * i, length / 2 - step2p * j, 0), new Vertex(-width / 2 + stepp * (i + 1), length / 2 - step2p * (j), 0), new Vertex(-width / 2 + stepp * i, length / 2 - step2p * (j + 1), 0), cloth));
                    pom.Add(new Triangle(new Vertex(-width / 2 + stepp * (i), length / 2 - step2p * (j + 1), 0), new Vertex(-width / 2 + stepp * (i + 1), length / 2 - step2p * (j), 0), new Vertex(-width / 2 + stepp * (i + 1), length / 2 - step2p * (j + 1), 0), cloth));
                }
            }

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {

                    //Walls in
                    pom.Add(new Triangle(new Vertex(-width / 2 + step * i, length / 2, bandHeight - stepBoundHeight * (j + 1)), new Vertex(-width / 2 + step * i, length / 2, bandHeight - stepBoundHeight * j), new Vertex(-width / 2 + step * (i + 1), length / 2, bandHeight - stepBoundHeight * j), new Vector(0, -1, 0), wood));
                    pom.Add(new Triangle(new Vertex(-width / 2 + step * (i + 1), length / 2, bandHeight - stepBoundHeight * (j)), new Vertex(-width / 2 + step * (i + 1), length / 2, bandHeight - stepBoundHeight * (j + 1)), new Vertex(-width / 2 + step * (i), length / 2, bandHeight - stepBoundHeight * (j + 1)), new Vector(0, -1, 0), wood));

                    pom.Add(new Triangle(new Vertex(-width / 2 + step * i, -length / 2, bandHeight - stepBoundHeight * (j + 1)), new Vertex(-width / 2 + step * i, -length / 2, bandHeight - stepBoundHeight * j), new Vertex(-width / 2 + step * (i + 1), -length / 2, bandHeight - stepBoundHeight * j), new Vector(0, 1, 0), wood));
                    pom.Add(new Triangle(new Vertex(-width / 2 + step * (i + 1), -length / 2, bandHeight - stepBoundHeight * (j)), new Vertex(-width / 2 + step * (i + 1), -length / 2, bandHeight - stepBoundHeight * (j + 1)), new Vertex(-width / 2 + step * (i), -length / 2, bandHeight - stepBoundHeight * (j + 1)), new Vector(0, 1, 0), wood));

                    pom.Add(new Triangle(new Vertex(-width / 2, length / 2 - step2 * i, bandHeight - stepBoundHeight * j), new Vertex(-width / 2, length / 2 - step2 * i, bandHeight - stepBoundHeight * (j + 1)), new Vertex(-width / 2, length / 2 - step2 * (i + 1), bandHeight - stepBoundHeight * j), new Vector(1, 0, 0), wood));
                    pom.Add(new Triangle(new Vertex(-width / 2, length / 2 - step2 * (i + 1), bandHeight - stepBoundHeight * (j)), new Vertex(-width / 2, length / 2 - step2 * (i + 1), bandHeight - stepBoundHeight * (j + 1)), new Vertex(-width / 2, length / 2 - step2 * (i), bandHeight - stepBoundHeight * (j + 1)), new Vector(1, 0, 0), wood));

                    pom.Add(new Triangle(new Vertex(width / 2, length / 2 - step2 * i, bandHeight - stepBoundHeight * (j + 1)), new Vertex(width / 2, length / 2 - step2 * i, bandHeight - stepBoundHeight * j), new Vertex(width / 2, length / 2 - step2 * (i + 1), bandHeight - stepBoundHeight * j), new Vector(-1, 0, 0), wood));
                    pom.Add(new Triangle(new Vertex(width / 2, length / 2 - step2 * (i + 1), bandHeight - stepBoundHeight * (j)), new Vertex(width / 2, length / 2 - step2 * (i + 1), bandHeight - stepBoundHeight * (j + 1)), new Vertex(width / 2, length / 2 - step2 * (i), bandHeight - stepBoundHeight * (j + 1)), new Vector(-1, 0, 0), wood));


                    //Walls out
                    pom.Add(new Triangle(new Vertex(-width / 2 + step * i, length / 2 + d1, bandHeight - stepBoundHeight * (j + 1)), new Vertex(-width / 2 + step * i, length / 2 + d1, bandHeight - stepBoundHeight * j), new Vertex(-width / 2 + step * (i + 1), length / 2 + d1, bandHeight - stepBoundHeight * j), new Vector(0, 1, 0), wood));
                    pom.Add(new Triangle(new Vertex(-width / 2 + step * (i + 1), length / 2 + d1, bandHeight - stepBoundHeight * (j)), new Vertex(-width / 2 + step * (i + 1), length / 2 + d1, bandHeight - stepBoundHeight * (j + 1)), new Vertex(-width / 2 + step * (i), length / 2 + d1, bandHeight - stepBoundHeight * (j + 1)), new Vector(0, 1, 0), wood));

                    pom.Add(new Triangle(new Vertex(-width / 2 + step * i, -length / 2 - d1, bandHeight - stepBoundHeight * (j + 1)), new Vertex(-width / 2 + step * i, -length / 2 - d1, bandHeight - stepBoundHeight * j), new Vertex(-width / 2 + step * (i + 1), -length / 2 - d1, bandHeight - stepBoundHeight * j), new Vector(0, -1, 0), wood));
                    pom.Add(new Triangle(new Vertex(-width / 2 + step * (i + 1), -length / 2 - d1, bandHeight - stepBoundHeight * (j)), new Vertex(-width / 2 + step * (i + 1), -length / 2 - d1, bandHeight - stepBoundHeight * (j + 1)), new Vertex(-width / 2 + step * (i), -length / 2 - d1, bandHeight - stepBoundHeight * (j + 1)), new Vector(0, -1, 0), wood));

                    pom.Add(new Triangle(new Vertex(-width / 2 - d2, length / 2 - step2 * i, bandHeight - stepBoundHeight * j), new Vertex(-width / 2 - d2, length / 2 - step2 * i, bandHeight - stepBoundHeight * (j + 1)), new Vertex(-width / 2 - d2, length / 2 - step2 * (i + 1), bandHeight - stepBoundHeight * j), new Vector(-1, 0, 0), wood));
                    pom.Add(new Triangle(new Vertex(-width / 2 - d2, length / 2 - step2 * (i + 1), bandHeight - stepBoundHeight * (j)), new Vertex(-width / 2 - d2, length / 2 - step2 * (i + 1), bandHeight - stepBoundHeight * (j + 1)), new Vertex(-width / 2 - d2, length / 2 - step2 * (i), bandHeight - stepBoundHeight * (j + 1)), new Vector(-1, 0, 0), wood));

                    pom.Add(new Triangle(new Vertex(width / 2 + d2, length / 2 - step2 * i, bandHeight - stepBoundHeight * j), new Vertex(width / 2 + d2, length / 2 - step2 * i, bandHeight - stepBoundHeight * (j + 1)), new Vertex(width / 2 + d2, length / 2 - step2 * (i + 1), bandHeight - stepBoundHeight * j), new Vector(1, 0, 0), wood));
                    pom.Add(new Triangle(new Vertex(width / 2 + d2, length / 2 - step2 * (i + 1), bandHeight - stepBoundHeight * (j)), new Vertex(width / 2 + d2, length / 2 - step2 * (i + 1), bandHeight - stepBoundHeight * (j + 1)), new Vertex(width / 2 + d2, length / 2 - step2 * (i), bandHeight - stepBoundHeight * (j + 1)), new Vector(1, 0, 0), wood));
                    //Walls up

                    pom.Add(new Triangle(new Vertex(-width / 2 + step * i, length / 2 + d1 - stepUpLength * (j + 1), bandHeight), new Vertex(-width / 2 + step * i, length / 2 + d1 - stepUpLength * j, bandHeight), new Vertex(-width / 2 + step * (i + 1), length / 2 + d1 - stepUpLength * (j), bandHeight), wood));
                    pom.Add(new Triangle(new Vertex(-width / 2 + step * i, length / 2 + d1 - stepUpLength * (j + 1), bandHeight), new Vertex(-width / 2 + step * (i + 1), length / 2 + d1 - stepUpLength * (j), bandHeight), new Vertex(-width / 2 + step * (i + 1), length / 2 + d1 - stepUpLength * (j + 1), bandHeight), wood));

                    pom.Add(new Triangle(new Vertex(-width / 2 + step * i, -length / 2 - stepUpLength * (j + 1), bandHeight), new Vertex(-width / 2 + step * i, -length / 2 - stepUpLength * j, bandHeight), new Vertex(-width / 2 + step * (i + 1), -length / 2 - stepUpLength * (j), bandHeight), wood));
                    pom.Add(new Triangle(new Vertex(-width / 2 + step * i, -length / 2 - stepUpLength * (j + 1), bandHeight), new Vertex(-width / 2 + step * (i + 1), -length / 2 - stepUpLength * (j), bandHeight), new Vertex(-width / 2 + step * (i + 1), -length / 2 - stepUpLength * (j + 1), bandHeight), wood));

                    pom.Add(new Triangle(new Vertex(-width / 2 - d2 + stepUpWidth * j, length / 2 - step2 * i, bandHeight), new Vertex(-width / 2 - d2 + stepUpWidth * (j + 1), length / 2 - step2 * i, bandHeight), new Vertex(-width / 2 - d2 + stepUpWidth * j, length / 2 - step2 * (i + 1), bandHeight), wood));
                    pom.Add(new Triangle(new Vertex(-width / 2 - d2 + stepUpWidth * j, length / 2 - step2 * (i + 1), bandHeight), new Vertex(-width / 2 - d2 + stepUpWidth * (j + 1), length / 2 - step2 * i, bandHeight), new Vertex(-width / 2 - d2 + stepUpWidth * (j + 1), length / 2 - step2 * (i + 1), bandHeight), wood));

                    pom.Add(new Triangle(new Vertex(width / 2 + stepUpWidth * j, length / 2 - step2 * i, bandHeight), new Vertex(width / 2 + stepUpWidth * (j + 1), length / 2 - step2 * i, bandHeight), new Vertex(width / 2 + stepUpWidth * j, length / 2 - step2 * (i + 1), bandHeight), wood));
                    pom.Add(new Triangle(new Vertex(width / 2 + stepUpWidth * j, length / 2 - step2 * (i + 1), bandHeight), new Vertex(width / 2 + stepUpWidth * (j + 1), length / 2 - step2 * i, bandHeight), new Vertex(width / 2 + stepUpWidth * (j + 1), length / 2 - step2 * (i + 1), bandHeight), wood));

                    ////Legs

                    //Leg1
                    pom.Add(new Triangle(new Vertex(width / 2 + stepUpWidth * (i + 1), length / 2 + stepUpLength * j, bandHeight), new Vertex(width / 2 + stepUpWidth * i, length / 2 + stepUpLength * j, bandHeight), new Vertex(width / 2 + stepUpWidth * i, length / 2 + stepUpLength * (j + 1), bandHeight), wood));
                    pom.Add(new Triangle(new Vertex(width / 2 + stepUpWidth * (i), length / 2 + stepUpLength * (j + 1), bandHeight), new Vertex(width / 2 + stepUpWidth * (i + 1), length / 2 + stepUpLength * (j + 1), bandHeight), new Vertex(width / 2 + stepUpWidth * (i + 1), length / 2 + stepUpLength * (j), bandHeight), wood));

                    pom.Add(new Triangle(new Vertex(width / 2 + stepUpWidth * (i), length / 2, bandHeight - stepLeg * j), new Vertex(width / 2 + stepUpWidth * (i + 1), length / 2, bandHeight - stepLeg * (j)), new Vertex(width / 2 + stepUpWidth * (i), length / 2, bandHeight - stepLeg * (j + 1)), new Vector(0, -1, 0), wood));
                    pom.Add(new Triangle(new Vertex(width / 2 + stepUpWidth * (i + 1), length / 2, bandHeight - stepLeg * j), new Vertex(width / 2 + stepUpWidth * (i + 1), length / 2, bandHeight - stepLeg * (j + 1)), new Vertex(width / 2 + stepUpWidth * (i), length / 2, bandHeight - stepLeg * (j + 1)), new Vector(0, -1, 0), wood));

                    pom.Add(new Triangle(new Vertex(width / 2 + stepUpWidth * (i), length / 2 + d1, bandHeight - stepLeg * j), new Vertex(width / 2 + stepUpWidth * (i + 1), length / 2 + d1, bandHeight - stepLeg * (j)), new Vertex(width / 2 + stepUpWidth * (i), length / 2 + d1, bandHeight - stepLeg * (j + 1)), new Vector(0, 1, 0), wood));
                    pom.Add(new Triangle(new Vertex(width / 2 + stepUpWidth * (i + 1), length / 2 + d1, bandHeight - stepLeg * j), new Vertex(width / 2 + stepUpWidth * (i + 1), length / 2 + d1, bandHeight - stepLeg * (j + 1)), new Vertex(width / 2 + stepUpWidth * (i), length / 2 + d1, bandHeight - stepLeg * (j + 1)), new Vector(0, 1, 0), wood));

                    pom.Add(new Triangle(new Vertex(width / 2, length / 2 + stepUpLength * i, bandHeight - stepLeg * j), new Vertex(width / 2, length / 2 + stepUpLength * (i + 1), bandHeight - stepLeg * (j)), new Vertex(width / 2, length / 2 + stepUpLength * i, bandHeight - stepLeg * (j + 1)), new Vector(-1, 0, 0), wood));
                    pom.Add(new Triangle(new Vertex(width / 2, length / 2 + stepUpLength * (i + 1), bandHeight - stepLeg * j), new Vertex(width / 2, length / 2 + stepUpLength * (i + 1), bandHeight - stepLeg * (j + 1)), new Vertex(width / 2, length / 2 + stepUpLength * (i), bandHeight - stepLeg * (j + 1)), new Vector(-1, 0, 0), wood));

                    pom.Add(new Triangle(new Vertex(width / 2 + d2, length / 2 + stepUpLength * i, bandHeight - stepLeg * j), new Vertex(width / 2 + d2, length / 2 + stepUpLength * (i + 1), bandHeight - stepLeg * (j)), new Vertex(width / 2 + d2, length / 2 + stepUpLength * i, bandHeight - stepLeg * (j + 1)), new Vector(1, 0, 0), wood));
                    pom.Add(new Triangle(new Vertex(width / 2 + d2, length / 2 + stepUpLength * (i + 1), bandHeight - stepLeg * j), new Vertex(width / 2 + d2, length / 2 + stepUpLength * (i + 1), bandHeight - stepLeg * (j + 1)), new Vertex(width / 2 + d2, length / 2 + stepUpLength * (i), bandHeight - stepLeg * (j + 1)), new Vector(1, 0, 0), wood));
                    //Leg2
                    pom.Add(new Triangle(new Vertex(width / 2 + stepUpWidth * i, -length / 2 - stepUpLength * j, bandHeight), new Vertex(width / 2 + stepUpWidth * (i + 1), -length / 2 - stepUpLength * j, bandHeight), new Vertex(width / 2 + stepUpWidth * i, -length / 2 - stepUpLength * (j + 1), bandHeight), wood));
                    pom.Add(new Triangle(new Vertex(width / 2 + stepUpWidth * (i + 1), -length / 2 - stepUpLength * (j + 1), bandHeight), new Vertex(width / 2 + stepUpWidth * (i), -length / 2 - stepUpLength * (j + 1), bandHeight), new Vertex(width / 2 + stepUpWidth * (i + 1), -length / 2 - stepUpLength * (j), bandHeight), wood));


                    pom.Add(new Triangle(new Vertex(width / 2 + stepUpWidth * (i), -length / 2, bandHeight - stepLeg * j), new Vertex(width / 2 + stepUpWidth * (i + 1), -length / 2, bandHeight - stepLeg * (j)), new Vertex(width / 2 + stepUpWidth * (i), -length / 2, bandHeight - stepLeg * (j + 1)), new Vector(0, 1, 0), wood));
                    pom.Add(new Triangle(new Vertex(width / 2 + stepUpWidth * (i + 1), -length / 2, bandHeight - stepLeg * j), new Vertex(width / 2 + stepUpWidth * (i + 1), -length / 2, bandHeight - stepLeg * (j + 1)), new Vertex(width / 2 + stepUpWidth * (i), -length / 2, bandHeight - stepLeg * (j + 1)), new Vector(0, 1, 0), wood));

                    pom.Add(new Triangle(new Vertex(width / 2 + stepUpWidth * (i), -length / 2 - d1, bandHeight - stepLeg * j), new Vertex(width / 2 + stepUpWidth * (i + 1), -length / 2 - d1, bandHeight - stepLeg * (j)), new Vertex(width / 2 + stepUpWidth * (i), -length / 2 - d1, bandHeight - stepLeg * (j + 1)), new Vector(0, -1, 0), wood));
                    pom.Add(new Triangle(new Vertex(width / 2 + stepUpWidth * (i + 1), -length / 2 - d1, bandHeight - stepLeg * j), new Vertex(width / 2 + stepUpWidth * (i + 1), -length / 2 - d1, bandHeight - stepLeg * (j + 1)), new Vertex(width / 2 + stepUpWidth * (i), -length / 2 - d1, bandHeight - stepLeg * (j + 1)), new Vector(0, -1, 0), wood));


                    pom.Add(new Triangle(new Vertex(width / 2, -length / 2 - stepUpLength * i, bandHeight - stepLeg * j), new Vertex(width / 2, -length / 2 - stepUpLength * (i + 1), bandHeight - stepLeg * (j)), new Vertex(width / 2, -length / 2 - stepUpLength * i, bandHeight - stepLeg * (j + 1)), new Vector(-1, 0, 0), wood));
                    pom.Add(new Triangle(new Vertex(width / 2, -length / 2 - stepUpLength * (i + 1), bandHeight - stepLeg * j), new Vertex(width / 2, -length / 2 - stepUpLength * (i + 1), bandHeight - stepLeg * (j + 1)), new Vertex(width / 2, -length / 2 - stepUpLength * (i), bandHeight - stepLeg * (j + 1)), new Vector(-1, 0, 0), wood));

                    pom.Add(new Triangle(new Vertex(width / 2 + d2, -length / 2 - stepUpLength * i, bandHeight - stepLeg * j), new Vertex(width / 2 + d2, -length / 2 - stepUpLength * (i + 1), bandHeight - stepLeg * (j)), new Vertex(width / 2 + d2, -length / 2 - stepUpLength * i, bandHeight - stepLeg * (j + 1)), new Vector(1, 0, 0), wood));
                    pom.Add(new Triangle(new Vertex(width / 2 + d2, -length / 2 - stepUpLength * (i + 1), bandHeight - stepLeg * j), new Vertex(width / 2 + d2, -length / 2 - stepUpLength * (i + 1), bandHeight - stepLeg * (j + 1)), new Vertex(width / 2 + d2, -length / 2 - stepUpLength * (i), bandHeight - stepLeg * (j + 1)), new Vector(1, 0, 0), wood));
                    ///Leg3

                    pom.Add(new Triangle(new Vertex(-width / 2 - d2 + stepUpWidth * (i + 1), length / 2 + stepUpLength * j, bandHeight), new Vertex(-width / 2 - d2 + stepUpWidth * i, length / 2 + stepUpLength * j, bandHeight), new Vertex(-width / 2 - d2 + stepUpWidth * i, length / 2 + stepUpLength * (j + 1), bandHeight), wood));
                    pom.Add(new Triangle(new Vertex(-width / 2 - d2 + stepUpWidth * (i), length / 2 + stepUpLength * (j + 1), bandHeight), new Vertex(-width / 2 - d2 + stepUpWidth * (i + 1), length / 2 + stepUpLength * (j + 1), bandHeight), new Vertex(-width / 2 - d2 + stepUpWidth * (i + 1), length / 2 + stepUpLength * (j), bandHeight), wood));

                    pom.Add(new Triangle(new Vertex(-width / 2 - d2 + stepUpWidth * (i), length / 2, bandHeight - stepLeg * j), new Vertex(-width / 2 - d2 + stepUpWidth * (i + 1), length / 2, bandHeight - stepLeg * (j)), new Vertex(-width / 2 - d2 + stepUpWidth * (i), length / 2, bandHeight - stepLeg * (j + 1)), new Vector(0, -1, 0), wood));
                    pom.Add(new Triangle(new Vertex(-width / 2 - d2 + stepUpWidth * (i + 1), length / 2, bandHeight - stepLeg * j), new Vertex(-width / 2 - d2 + stepUpWidth * (i + 1), length / 2, bandHeight - stepLeg * (j + 1)), new Vertex(-width / 2 - d2 + stepUpWidth * (i), length / 2, bandHeight - stepLeg * (j + 1)), new Vector(0, -1, 0), wood));

                    pom.Add(new Triangle(new Vertex(-width / 2 - d2 + stepUpWidth * (i), length / 2 + d1, bandHeight - stepLeg * j), new Vertex(-width / 2 - d2 + stepUpWidth * (i + 1), length / 2 + d1, bandHeight - stepLeg * (j)), new Vertex(-width / 2 - d2 + stepUpWidth * (i), length / 2 + d1, bandHeight - stepLeg * (j + 1)), new Vector(0, 1, 0), wood));
                    pom.Add(new Triangle(new Vertex(-width / 2 - d2 + stepUpWidth * (i + 1), length / 2 + d1, bandHeight - stepLeg * j), new Vertex(-width / 2 - d2 + stepUpWidth * (i + 1), length / 2 + d1, bandHeight - stepLeg * (j + 1)), new Vertex(-width / 2 - d2 + stepUpWidth * (i), length / 2 + d1, bandHeight - stepLeg * (j + 1)), new Vector(0, 1, 0), wood));

                    pom.Add(new Triangle(new Vertex(-width / 2, length / 2 + stepUpLength * i, bandHeight - stepLeg * j), new Vertex(-width / 2, length / 2 + stepUpLength * (i + 1), bandHeight - stepLeg * (j)), new Vertex(-width / 2, length / 2 + stepUpLength * i, bandHeight - stepLeg * (j + 1)), new Vector(1, 0, 0), wood));
                    pom.Add(new Triangle(new Vertex(-width / 2, length / 2 + stepUpLength * (i + 1), bandHeight - stepLeg * j), new Vertex(-width / 2, length / 2 + stepUpLength * (i + 1), bandHeight - stepLeg * (j + 1)), new Vertex(-width / 2, length / 2 + stepUpLength * (i), bandHeight - stepLeg * (j + 1)), new Vector(1, 0, 0), wood));

                    pom.Add(new Triangle(new Vertex(-width / 2 - d2, length / 2 + stepUpLength * i, bandHeight - stepLeg * j), new Vertex(-width / 2 - d2, length / 2 + stepUpLength * (i + 1), bandHeight - stepLeg * (j)), new Vertex(-width / 2 - d2, length / 2 + stepUpLength * i, bandHeight - stepLeg * (j + 1)), new Vector(-1, 0, 0), wood));
                    pom.Add(new Triangle(new Vertex(-width / 2 - d2, length / 2 + stepUpLength * (i + 1), bandHeight - stepLeg * j), new Vertex(-width / 2 - d2, length / 2 + stepUpLength * (i + 1), bandHeight - stepLeg * (j + 1)), new Vertex(-width / 2 - d2, length / 2 + stepUpLength * (i), bandHeight - stepLeg * (j + 1)), new Vector(-1, 0, 0), wood));
                    //Leg4

                    pom.Add(new Triangle(new Vertex(-width / 2 - d2 + stepUpWidth * i, -length / 2 - stepUpLength * j, bandHeight), new Vertex(-width / 2 - d2 + stepUpWidth * (i + 1), -length / 2 - stepUpLength * j, bandHeight), new Vertex(-width / 2 - d2 + stepUpWidth * i, -length / 2 - stepUpLength * (j + 1), bandHeight), wood));
                    pom.Add(new Triangle(new Vertex(-width / 2 - d2 + stepUpWidth * (i + 1), -length / 2 - stepUpLength * (j + 1), bandHeight), new Vertex(-width / 2 - d2 + stepUpWidth * (i), -length / 2 - stepUpLength * (j + 1), bandHeight), new Vertex(-width / 2 - d2 + stepUpWidth * (i + 1), -length / 2 - stepUpLength * (j), bandHeight), wood));

                    pom.Add(new Triangle(new Vertex(-width / 2 - d2 + stepUpWidth * (i), -length / 2, bandHeight - stepLeg * j), new Vertex(-width / 2 - d2 + stepUpWidth * (i + 1), -length / 2, bandHeight - stepLeg * (j)), new Vertex(-width / 2 - d2 + stepUpWidth * (i), -length / 2, bandHeight - stepLeg * (j + 1)), new Vector(0, 1, 0), wood));
                    pom.Add(new Triangle(new Vertex(-width / 2 - d2 + stepUpWidth * (i + 1), -length / 2, bandHeight - stepLeg * j), new Vertex(-width / 2 - d2 + stepUpWidth * (i + 1), -length / 2, bandHeight - stepLeg * (j + 1)), new Vertex(-width / 2 - d2 + stepUpWidth * (i), -length / 2, bandHeight - stepLeg * (j + 1)), new Vector(0, 1, 0), wood));

                    pom.Add(new Triangle(new Vertex(-width / 2 - d2 + stepUpWidth * (i), -length / 2 - d1, bandHeight - stepLeg * j), new Vertex(-width / 2 - d2 + stepUpWidth * (i + 1), -length / 2 - d1, bandHeight - stepLeg * (j)), new Vertex(-width / 2 - d2 + stepUpWidth * (i), -length / 2 - d1, bandHeight - stepLeg * (j + 1)), new Vector(0, -1, 0), wood));
                    pom.Add(new Triangle(new Vertex(-width / 2 - d2 + stepUpWidth * (i + 1), -length / 2 - d1, bandHeight - stepLeg * j), new Vertex(-width / 2 - d2 + stepUpWidth * (i + 1), -length / 2 - d1, bandHeight - stepLeg * (j + 1)), new Vertex(-width / 2 - d2 + stepUpWidth * (i), -length / 2 - d1, bandHeight - stepLeg * (j + 1)), new Vector(0, -1, 0), wood));

                    pom.Add(new Triangle(new Vertex(-width / 2, -length / 2 - stepUpLength * i, bandHeight - stepLeg * j), new Vertex(-width / 2, -length / 2 - stepUpLength * (i + 1), bandHeight - stepLeg * (j)), new Vertex(-width / 2, -length / 2 - stepUpLength * i, bandHeight - stepLeg * (j + 1)), new Vector(1, 0, 0), wood));
                    pom.Add(new Triangle(new Vertex(-width / 2, -length / 2 - stepUpLength * (i + 1), bandHeight - stepLeg * j), new Vertex(-width / 2, -length / 2 - stepUpLength * (i + 1), bandHeight - stepLeg * (j + 1)), new Vertex(-width / 2, -length / 2 - stepUpLength * (i), bandHeight - stepLeg * (j + 1)), new Vector(1, 0, 0), wood));

                    pom.Add(new Triangle(new Vertex(-width / 2 - d2, -length / 2 - stepUpLength * i, bandHeight - stepLeg * j), new Vertex(-width / 2 - d2, -length / 2 - stepUpLength * (i + 1), bandHeight - stepLeg * (j)), new Vertex(-width / 2 - d2, -length / 2 - stepUpLength * i, bandHeight - stepLeg * (j + 1)), new Vector(-1, 0, 0), wood));
                    pom.Add(new Triangle(new Vertex(-width / 2 - d2, -length / 2 - stepUpLength * (i + 1), bandHeight - stepLeg * j), new Vertex(-width / 2 - d2, -length / 2 - stepUpLength * (i + 1), bandHeight - stepLeg * (j + 1)), new Vertex(-width / 2 - d2, -length / 2 - stepUpLength * (i), bandHeight - stepLeg * (j + 1)), new Vector(-1, 0, 0), wood));
                }

            }

            triangle = pom.ToArray();
        }
    }
}
