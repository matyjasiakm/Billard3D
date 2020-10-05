using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;
using static System.Math;

namespace GkLAB
{
    public partial class Form1 : Form
    {
        Light light;
        Light upLight;
        Sphere p;
        Table t;
        List<Model> models;
        double FOV = Math.PI / 4;
        double f = 100;
        double n = 1;
        double angle = 0;
        double eps = Pow(10, -8);
        double[,] zBuff;
        MyMatrix viewMatrix;
        MyMatrix projMatrix;
        MyMatrix modelMatrix;
        MyMatrix xxx;
        Camera camera;
        Camera allView;
        Lamp lamp;
        bool GourandShadingActive;
        bool PhongShadingActivated;
        double billardStep;
        double billardRotationStep;
        bool CameraOnBallActivated;
        bool CameraAboveActiveBallActice;
        bool LightOnBall;
        double tableWidth;
        double tableLength;

        public Form1()
        {
            InitializeComponent();

            tableWidth = 18;
            tableLength = 9;

            CameraAboveActiveBallActice = false;
            CameraOnBallActivated = false;

            billardStep = 0.1;
            billardRotationStep = PI / 12;

            LightOnBall = false;

            models = new List<Model>();
            GourandShadingActive = false;
            PhongShadingActivated = false;
            upLight = new Light(0, 0, 7, Color.White);
            light = upLight;

            Vector v1 = new Vector(-12, -12, 10);
            Vector v2 = new Vector(0, 0, 1);
            Vector v3 = new Vector(0, 0, 1);

            allView = new Camera(v1, v2, v3);
            camera = new Camera(v1, v2, v3);

            p = new Sphere(0.5, Color.Aqua, 1);
            p.Translate(new Vector(3, 0, 0.5));


            lamp = new Lamp(1, Color.White);
            lamp.Translate(new Vector(0, 0, 7));

            Sphere p1 = new Sphere(0.5, Color.Blue);
            Sphere p2 = new Sphere(0.5, Color.Red);
            Sphere p3 = new Sphere(0.5, Color.Pink);
            Sphere p4 = new Sphere(0.5, Color.Orange);
            Sphere p5 = new Sphere(0.5, Color.Orchid);
            Sphere p6 = new Sphere(0.5, Color.DarkGoldenrod);


            Sphere p8 = new Sphere(0.5, Color.Pink);
            Sphere p9 = new Sphere(0.5, Color.Orange);
            Sphere p10 = new Sphere(0.5, Color.Orchid);
            Sphere p11 = new Sphere(0.5, Color.DarkGoldenrod);

            p1.Translate(new Vector(-4.75, 1, 0.5));
            p2.Translate(new Vector(-4.75, 0, 0.5));
            p3.Translate(new Vector(-4.75, -1, 0.5));
            p4.Translate(new Vector(-3.75, 0.5, 0.5));
            p5.Translate(new Vector(-3.75, -0.5, 0.5));
            p6.Translate(new Vector(-2.75, 0, 0.5));


            p8.Translate(new Vector(-5.75, 1.5, 0.5));
            p9.Translate(new Vector(-5.75, 0.5, 0.5));
            p10.Translate(new Vector(-5.75, -0.5, 0.5));
            p11.Translate(new Vector(-5.75, -1.5, 0.5));

            t = new Table(tableWidth, tableLength, 2, 1);

            tableWidth = 18;
            tableLength = 9;
            models.Add(p);
            models.Add(t);
            models.Add(p1);
            models.Add(p2);
            models.Add(p3);
            models.Add(p4);
            models.Add(p5);
            models.Add(p6);
            models.Add(p8);
            models.Add(p9);
            models.Add(p10);
            models.Add(p11);
            models.Add(lamp);

            projMatrix = new MyMatrix(4, 4);
            zBuff = new double[pictureBox1.Width, pictureBox1.Height];
            for (int i = 0; i < pictureBox1.Width; i++)
            {
                for (int j = 0; j < pictureBox1.Height; j++)
                {
                    zBuff[i, j] = double.MinValue;
                }
            }

            (double dif, double amb, double spec, double shin) = light.getParameters();
            textBox1.Text = dif.ToString();
            textBox2.Text = amb.ToString();
            textBox3.Text = spec.ToString();
            textBox4.Text = shin.ToString();
            textBox5.Text = billardStep.ToString();
            textBox6.Text = (billardRotationStep * 180 / PI).ToString();
        }


        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Invoke(new Action(() => label1.Text = "Rendering..."));

            zBuff = new double[pictureBox1.Width, pictureBox1.Height];
            for (int q = 0; q < pictureBox1.Width; q++)
            {
                for (int j = 0; j < pictureBox1.Height; j++)
                {
                    zBuff[q, j] = double.MinValue;
                }
            }

            double pe = 1 / Math.Tan(FOV / 2);
            double a = pictureBox1.Height / (double)pictureBox1.Width;

            double angleInRadian = (angle / 180.0) * Math.PI;
            projMatrix.matrix = new double[,]
            {
                { pe, 0, 0, 0 },
                { 0, pe / a, 0, 0 },
                { 0, 0, -(f + n) / (f - n), -2 * f * n / (f - n) },
                { 0, 0, -1, 0 }
            };

            DirectBitmap bitmap = new DirectBitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics g = Graphics.FromImage(bitmap.Bitmap);


            if (CameraAboveActiveBallActice)
            {
                MyMatrix point = p.getModelMatrix() * p.middlePoint.Matrix();
                Vector v1 = new Vector(point[0, 0], point[1, 0], 20);
                Vector v2 = new Vector(point[0, 0], point[1, 0], 0);
                Vector v3 = new Vector(1, 0, 0);
                Camera c = new Camera(v1, v2, v3);
                camera = c;

            }
            else if (CameraOnBallActivated)
            {
                MyMatrix pointF = p.getModelMatrix() * p.backPointOnTop.Matrix();
                MyMatrix pointT = p.getModelMatrix() * p.frontPointOnTop.Matrix();
                Vector v1 = new Vector(pointF[0, 0], pointF[1, 0], pointF[2, 0]);
                Vector v2 = new Vector(pointT[0, 0], pointT[1, 0], pointT[2, 0]);
                Vector v3 = new Vector(0, 0, 1);
                Camera c = new Camera(v1, v2, v3);
                camera = c;

            }

            if (LightOnBall)
            {
                MyMatrix pointF = p.Translation * p.topVertexOnSphere.Matrix();
                Light l = new Light(pointF[0, 0], pointF[1, 0], pointF[2, 0] + 0.5, Color.White);
                l.setParameters(light.Diffuse, light.Ambient, light.Specular, light.Shiness);
                light = l;
            }

            foreach (var m in models)
            {
                var pom = m as Lamp;
                if (LightOnBall && pom != null) continue;

                Parallel.ForEach(m.triangle, (elem) =>
                {

                    MyMatrix pM1 = m.getModelMatrix() * elem.vertices[0].Matrix();
                    MyMatrix pM2 = m.getModelMatrix() * elem.vertices[1].Matrix();
                    MyMatrix pM3 = m.getModelMatrix() * elem.vertices[2].Matrix();

                    MyMatrix p1 = projMatrix * camera.viewMatrix * pM1;

                    MyMatrix p2 = projMatrix * camera.viewMatrix * pM2;

                    MyMatrix p3 = projMatrix * camera.viewMatrix * pM3;


                    if (p1.matrix[3, 0] < 10e-5 || p2.matrix[3, 0] < 10e-5 || p3.matrix[3, 0] < 10e-5) return;

                    double x1 = (p1.matrix[0, 0] / Abs(p1.matrix[3, 0]) + 1) * pictureBox1.Width / 2;
                    double x2 = (p2.matrix[0, 0] / Abs(p2.matrix[3, 0]) + 1) * pictureBox1.Width / 2;
                    double x3 = (p3.matrix[0, 0] / Abs(p3.matrix[3, 0]) + 1) * pictureBox1.Width / 2;

                    double y1 = (-p1.matrix[1, 0] / Abs(p1.matrix[3, 0]) + 1) * pictureBox1.Height / 2;
                    double y2 = (-p2.matrix[1, 0] / Abs(p2.matrix[3, 0]) + 1) * pictureBox1.Height / 2;
                    double y3 = (-p3.matrix[1, 0] / Abs(p3.matrix[3, 0]) + 1) * pictureBox1.Height / 2;

                    if ((x1 < 0 && x2 < 0 && x3 < 0) || (x1 > pictureBox1.Width && x2 > pictureBox1.Width && x3 > pictureBox1.Width)) return;// continue;
                    if ((y1 < 0 && y2 < 0 && y3 < 0) || (y1 > pictureBox1.Height && y2 > pictureBox1.Height && y3 > pictureBox1.Height)) return;// continue;

                    double z1 = (p1.matrix[2, 0] / p1.matrix[3, 0] + 1) / 2;
                    double z2 = (p2.matrix[2, 0] / p2.matrix[3, 0] + 1) / 2;
                    double z3 = (p3.matrix[2, 0] / p3.matrix[3, 0] + 1) / 2;

                    Vertex v1 = new Vertex(x1, y1, z1);
                    Vertex v2 = new Vertex(x2, y2, z2);
                    Vertex v3 = new Vertex(x3, y3, z3);

                    Vector bv1 = new Vector(pM1.GetVertex(), camera.getPositionInVertex()).getNormalizedVectorIn3D();
                    Vector bv2 = new Vector(pM2.GetVertex(), camera.getPositionInVertex()).getNormalizedVectorIn3D();
                    Vector bv3 = new Vector(pM3.GetVertex(), camera.getPositionInVertex()).getNormalizedVectorIn3D();
                    Vector n1 = (m.getModelMatrix() * elem.normal1.getMatrix()).get3DVector();
                    Vector n2 = (m.getModelMatrix() * elem.normal2.getMatrix()).get3DVector();
                    Vector n3 = (m.getModelMatrix() * elem.normal3.getMatrix()).get3DVector();
                    Vector t1 = new Vector(pM1.GetVertex(), light.getLightPosition());
                    Vector t2 = new Vector(pM2.GetVertex(), light.getLightPosition());
                    Vector t3 = new Vector(pM3.GetVertex(), light.getLightPosition());

                    Edge[] edges = new Edge[]
                {
                    new Edge(v1,v2),
                    new Edge(v2,v3),
                    new Edge(v3,v1)
                };

                    Color col1 = CalculateColorInPoint(light, camera, pM1.GetVertex(), n1, elem.color);
                    Color col2 = CalculateColorInPoint(light, camera, pM2.GetVertex(), n2, elem.color);
                    Color col3 = CalculateColorInPoint(light, camera, pM3.GetVertex(), n3, elem.color);

                    if (GourandShadingActive || PhongShadingActivated)
                    {
                        edges = new Edge[]
                       {
                            new Edge(v1,v2,col1,col2,bv1,bv2,n1,n2,t1,t2),
                            new Edge(v2,v3,col2,col3,bv2,bv3,n2,n3,t2,t3),
                            new Edge(v3,v1,col3,col1,bv3,bv1,n3,n1,t3,t1)

                         };
                    }



                    var color = CalculateColorAverage(col1, col2, col3);
                    if (PhongShadingActivated)
                    {
                        color = elem.color;
                    }
                    Fill(bitmap, edges, color, p1, p2, p3);
                });
            }
            for (int q = 0; q < pictureBox1.Width; q++)
            {
                for (int j = 0; j < pictureBox1.Height; j++)
                {
                    if (zBuff[q, j] == double.MinValue) bitmap.SetPixel(q, j, Color.Black);
                }
            }

            pictureBox1.Invoke(new Action(() => e.Graphics.DrawImage(bitmap.Bitmap, 0, 0)));

            bitmap.Dispose();
            Invoke(new Action(() => label1.Text = "DONE"));

        }
        public Color CalculateColorAverage(Color c1, Color c2, Color c3)
        {
            double R1 = c1.R;
            double R2 = c2.R;
            double R3 = c3.R;

            double G1 = c1.G;
            double G2 = c2.G;
            double G3 = c3.G;

            double B1 = c1.B;
            double B2 = c2.B;
            double B3 = c3.B;

            return Color.FromArgb(255, (int)((R1 + R2 + R3) / 3), (int)((G1 + G2 + G3) / 3), (int)((B1 + B2 + B3) / 3));
        }


        private void Fill(DirectBitmap bitmap, Edge[] edges, Color color, MyMatrix p1, MyMatrix p2, MyMatrix p3)
        {
            double minY = edges.Min(x => x.To.Y);
            double maxY = edges.Max(x => x.To.Y);

            List<BucketSortEdge>[] ET = new List<BucketSortEdge>[(int)maxY - (int)minY + 2];
            List<BucketSortEdge> AET = new List<BucketSortEdge>();


            foreach (var e in edges)
            {
                if (ET[(int)Math.Min(e.From.Y, e.To.Y) - (int)minY] == null) ET[(int)Math.Min(e.From.Y, e.To.Y) - (int)minY] = new List<BucketSortEdge>();
                ET[(int)Math.Min(e.From.Y, e.To.Y) - (int)minY].Add(new BucketSortEdge(e));
            }

            int y = 0;
            Color C1 = new Color(), C2 = new Color(), C = new Color();
            Vector V1 = new Vector(), V2 = new Vector(), V = new Vector(), O = new Vector(), O1 = new Vector(), O2 = new Vector(), T = new Vector(), T1 = new Vector(), T2 = new Vector();
            while (ET[y] != null || AET.Count != 0)
            {
                if (ET[y] != null)
                    AET.AddRange(ET[y]);
                AET.RemoveAll(x => (int)x.maxY == y + (int)minY);

                AET.Sort((x, z) => x.minX.CompareTo(z.minX));

                int i = 0;

                if (!(y + (int)minY < 0 || y + (int)minY > pictureBox1.Height))
                {

                    double Z1;
                    double Z2;
                    double Z;


                    while (i < AET.Count)
                    {
                        if (i + 1 < AET.Count)
                        {
                            if ((int)AET[i].minX != (int)AET[i + 1].minX)
                            {
                                double x1 = AET[i].minX;
                                double x2 = AET[i + 1].minX;

                                double q = Distance2D(x1, y + (int)minY, AET[i].from.X, AET[i].from.Y) / Distance2D(AET[i].from.X, AET[i].from.Y, AET[i].to.X, AET[i].to.Y);
                                Z1 = (1 / AET[i].from.Z) * (1 - q) + (1 / AET[i].to.Z) * q;

                                if (GourandShadingActive)
                                {
                                    C1 = InterpolateColor(AET[i].fromColor, AET[i].toColor, q);
                                }
                                else if (PhongShadingActivated)
                                {
                                    O1 = InterpolateVectorNormal(AET[i].fromB, AET[i].toB, q);
                                    V1 = InterpolateVectorNormal(AET[i].normalFrom, AET[i].normalTo, q);
                                    T1 = InterpolateVectorNormal(AET[i].toLightfrom, AET[i].toLightto, q);
                                }
                                q = Distance2D(x2, y + (int)minY, AET[i + 1].from.X, AET[i + 1].from.Y) / Distance2D(AET[i + 1].from.X, AET[i + 1].from.Y, AET[i + 1].to.X, AET[i + 1].to.Y);
                                Z2 = (1 / AET[i + 1].from.Z) * (1 - q) + (1 / AET[i + 1].to.Z) * q;

                                if (GourandShadingActive)
                                {
                                    C2 = InterpolateColor(AET[i + 1].fromColor, AET[i + 1].toColor, q);
                                }
                                else if (PhongShadingActivated)
                                {
                                    O2 = InterpolateVectorNormal(AET[i + 1].fromB, AET[i + 1].toB, q);
                                    T2 = InterpolateVectorNormal(AET[i + 1].toLightfrom, AET[i + 1].toLightto, q);
                                    V2 = InterpolateVectorNormal(AET[i + 1].normalFrom, AET[i + 1].normalTo, q);
                                }
                                
                                Parallel.For((int)AET[i].minX + 1, (int)AET[i + 1].minX + 1, (j) =>
                                {
                                    {

                                        q = Distance2D(j, y + (int)minY, x1, y + (int)minY) / Distance2D(x1, y + (int)minY, x2, y + (int)minY);
                                        Z = (Z1) * (1 - q) + (Z2) * q;

                                        if (GourandShadingActive)
                                        {
                                            C = InterpolateColor(C1, C2, q);
                                        }
                                        else if (PhongShadingActivated)
                                        {
                                            O = InterpolateVectorNormal(O1, O2, q);
                                            T = InterpolateVectorNormal(T1, T2, q);
                                            V = InterpolateVectorNormal(V1, V2, q);
                                        }
                                        if (j < bitmap.Width && j >= 0 && y + (int)minY >= 0 && y + (int)minY < bitmap.Height)
                                            if (Z > zBuff[j, y + (int)minY])
                                            {
                                                if (GourandShadingActive)
                                                {
                                                    bitmap.SetPixel(j, y + (int)minY, C);
                                                }
                                                else if (PhongShadingActivated)
                                                {
                                                    Color c = CalculateColorInPointPhong(light, O, T, V, color);
                                                    bitmap.SetPixel(j, y + (int)minY, c);
                                                }
                                                else
                                                {
                                                    bitmap.SetPixel(j, y + (int)minY, color);
                                                }
                                                zBuff[j, y + (int)minY] = Z;

                                            }
                                    }
                                });

                                i += 2;
                            }
                            else
                            {
                                i++;
                            }
                        }
                        else
                            break;
                    }

                }
                y++;
                AET.ForEach(x => x.minX += x.m);
            }

        }

        private double Distance2D(double x1, double y1, double x2, double y2)
        {
            return Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1));
        }

        private Color InterpolateColor(Color C1, Color C2, double q)
        {
            double R = C1.R * (1 - q) + C2.R * (q);
            double G = C1.G * (1 - q) + C2.G * (q);
            double B = C1.B * (1 - q) + C2.B * (q);
            R = Math.Min(Math.Max(0, R), 255);
            G = Math.Min(Math.Max(0, G), 255);
            B = Math.Min(Math.Max(0, B), 255);

            return Color.FromArgb((int)R, (int)G, (int)B);
        }
        private Vector InterpolateVectorNormal(Vector v1, Vector v2, double q)
        {
            Vector v = (v1 * (1 - q) + v2 * q);
            return v;
        }
        private Color CalculateColorInPoint(Light light, Camera camera, Vertex v, Vector normal, Color color)
        {
            Vector n = normal.getMatrix().getNormalize3DVector();
            Vector vectorToLight1 = new Vector(v, light.getLightPosition()).getNormalizedVectorIn3D();
            Vector vectorToCamera1 = new Vector(v, camera.getPositionInVertex()).getNormalizedVectorIn3D();

            double triangleR = color.R / 255.0;
            double triangleG = color.G / 255.0;
            double triangleB = color.B / 255.0;
            double lightR = light.lightColor.R / 255.0;
            double lightG = light.lightColor.G / 255.0;
            double lightB = light.lightColor.B / 255.0;

            double diff = light.Diffuse;
            double sp = light.Specular;
            double shi = light.Shiness;
            double amb = light.Ambient;

            double att1 = CalculateAtt(Vertex.Distance(v, light.getLightPosition()));
            double R1 = amb * triangleR * lightR + (diff * Vector.DotProduct(n, vectorToLight1)) * triangleR * lightR + sp * Math.Pow(Vector.DotProduct(n * 2 * Vector.DotProduct(n, vectorToLight1) - vectorToLight1, vectorToCamera1), shi) * triangleR * lightR * att1;
            double G1 = amb * triangleG * lightG + (diff * Vector.DotProduct(n, vectorToLight1)) * triangleG * lightG + sp * Math.Pow(Vector.DotProduct(n * 2 * Vector.DotProduct(n, vectorToLight1) - vectorToLight1, vectorToCamera1), shi) * triangleG * lightG * att1;
            double B1 = amb * triangleB * lightB + (diff * Vector.DotProduct(n, vectorToLight1)) * triangleB * lightB + sp * Math.Pow(Vector.DotProduct(n * 2 * Vector.DotProduct(n, vectorToLight1) - vectorToLight1, vectorToCamera1), shi) * triangleB * lightB * att1;

            R1 = Math.Min(Math.Max(0, R1), 1) * 255;
            G1 = Math.Min(Math.Max(0, G1), 1) * 255;
            B1 = Math.Min(Math.Max(0, B1), 1) * 255;

            return Color.FromArgb(255, (int)R1, (int)G1, (int)B1);
        }
        private Color CalculateColorInPointPhong(Light light, Vector toObserver, Vector toLight, Vector normal, Color color)
        {
            double d = toLight.Length3D();
            Vector n = normal.getMatrix().getNormalize3DVector();
            Vector vectorToLight1 = toLight.getNormalizedVectorIn3D();
            Vector vectorToCamera1 = toObserver.getNormalizedVectorIn3D();

            double triangleR = color.R / 255.0;
            double triangleG = color.G / 255.0;
            double triangleB = color.B / 255.0;
            double lightR = light.lightColor.R / 255.0;
            double lightG = light.lightColor.G / 255.0;
            double lightB = light.lightColor.B / 255.0;

            double diff = light.Diffuse;
            double sp = light.Specular;
            double shi = light.Shiness;
            double amb = light.Ambient;

            double att1 = CalculateAtt(d);
            double R1 = amb * triangleR * lightR + (diff * Vector.DotProduct(n, vectorToLight1)) * triangleR * lightR + sp * Math.Pow(Vector.DotProduct(n * 2 * Vector.DotProduct(n, vectorToLight1) - vectorToLight1, vectorToCamera1), shi) * triangleR * lightR * att1;
            double G1 = amb * triangleG * lightG + (diff * Vector.DotProduct(n, vectorToLight1)) * triangleG * lightG + sp * Math.Pow(Vector.DotProduct(n * 2 * Vector.DotProduct(n, vectorToLight1) - vectorToLight1, vectorToCamera1), shi) * triangleG * lightG * att1;
            double B1 = amb * triangleB * lightB + (diff * Vector.DotProduct(n, vectorToLight1)) * triangleB * lightB + sp * Math.Pow(Vector.DotProduct(n * 2 * Vector.DotProduct(n, vectorToLight1) - vectorToLight1, vectorToCamera1), shi) * triangleB * lightB * att1;

            R1 = Math.Min(Math.Max(0, R1), 1) * 255;
            G1 = Math.Min(Math.Max(0, G1), 1) * 255;
            B1 = Math.Min(Math.Max(0, B1), 1) * 255;

            return Color.FromArgb(255, (int)R1, (int)G1, (int)B1);
        }

        private double CalculateAtt(double distance)
        {
            return 1 / ((1 + 2 * distance + 1 * distance * distance));
        }

        private bool BallInRectangle(Vector v)
        {
            var pom = p.getModelMatrix();
            pom[0, 3] += v.U;
            pom[1, 3] += v.V;
            pom[2, 3] += v.W;
            var pointMid = (pom * p.middlePoint.Matrix()).GetVertex();
            if (pointMid.X > 0 && pointMid.X < tableWidth / 2 && Abs(pointMid.Y) < tableLength / 2) return true;
            return false;
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W)
            {
                if (!BallInRectangle(new Vector(billardStep, 0, 0))) return;

                p.Translate(new Vector(billardStep, 0, 0));
                pictureBox1.Refresh();
            }
            else if (e.KeyCode == Keys.S)
            {
                if (!BallInRectangle(new Vector(-billardStep, 0, 0))) return;
                p.Translate(new Vector(-billardStep, 0, 0));
                pictureBox1.Refresh();
            }
            else if (e.KeyCode == Keys.Q)
            {
                p.RotationAxisinMiddleZ(-billardRotationStep);
                pictureBox1.Refresh();
            }
            else if (e.KeyCode == Keys.D)
            {
                if (!BallInRectangle(new Vector(0, -billardStep, 0))) return;
                p.Translate(new Vector(0, -billardStep, 0));
                pictureBox1.Refresh();
            }
            else if (e.KeyCode == Keys.A)
            {
                if (!BallInRectangle(new Vector(0, billardStep, 0))) return;
                p.Translate(new Vector(0, billardStep, 0));
                pictureBox1.Refresh();
            }
            else if (e.KeyCode == Keys.E)
            {
                p.RotationAxisinMiddleZ(billardRotationStep);
                pictureBox1.Refresh();
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                CameraAboveActiveBallActice = false;
                CameraOnBallActivated = false;

                camera = allView;
                pictureBox1.Refresh();
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                CameraAboveActiveBallActice = true;
                CameraOnBallActivated = false;

                pictureBox1.Refresh();
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {
                CameraAboveActiveBallActice = false;
                CameraOnBallActivated = true;

                pictureBox1.Refresh();
            }
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton4.Checked)
            {
                upLight.setParameters(light.Diffuse, light.Ambient, light.Specular, light.Shiness);
                light = upLight;
                LightOnBall = false;
                pictureBox1.Refresh();
            }
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton5.Checked)
            {
                LightOnBall = true;
                pictureBox1.Refresh();
            }

        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            GourandShadingActive = false;
            PhongShadingActivated = false;
            pictureBox1.Refresh();
        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            GourandShadingActive = false;
            PhongShadingActivated = true;
            pictureBox1.Refresh();
        }

        private void radioButton8_CheckedChanged(object sender, EventArgs e)
        {
            GourandShadingActive = true;
            PhongShadingActivated = false;
            pictureBox1.Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();

            if (cd.ShowDialog() == DialogResult.OK)
            {
                light.lightColor = cd.Color;
                lamp.ChangeColor(cd.Color);
                pictureBox2.BackColor = cd.Color;
                pictureBox1.Refresh();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double d, a, s, sh;
            if (double.TryParse(textBox1.Text, out d) && double.TryParse(textBox2.Text, out a)
                && double.TryParse(textBox3.Text, out s) && double.TryParse(textBox4.Text, out sh))
            {
                if (d < 0 || a < 0 || s < 0 || sh < 0)
                {
                    MessageBox.Show("Wrong parameters!");
                    return;
                }
                light.setParameters(d, a, s, sh);
                pictureBox1.Refresh();
            }
            else
            {
                MessageBox.Show("Wrong parameters!");
            }
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            pictureBox1.Refresh();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            double m, r;
            if (double.TryParse(textBox5.Text, out m) && double.TryParse(textBox6.Text, out r))
            {
                if (m < 0 || m > tableWidth / 2 - 0.5 || m > tableLength / 2 - 0.5)
                {
                    MessageBox.Show("Too big step!");
                }
                billardRotationStep = Abs(r * PI / 180);
                billardStep = m;
            }
            else
            {
                MessageBox.Show("Wrong steps parameters!");
            }
        }

        private void instructionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
        }

        private void informationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Projekt Billard wykonał Mateusz Matyjasiak", "Inforation");
        }
    }

}

