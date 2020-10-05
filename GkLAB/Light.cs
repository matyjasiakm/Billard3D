using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GkLAB
{
    public class Light
    {
        public Vector lightPosition;
        public Color lightColor;
        public double Diffuse;
        public double Shiness;
        public double Ambient;
        public double Specular;

        public Light(double x, double y, double z, Color color)
        {
            lightPosition = new Vector(x, y, z);
            lightColor = color;
            Diffuse = 1;
            Ambient = 0.5;
            Specular = 0.25;
            Shiness = 64;
        }
        public Vertex getLightPosition()
        {
            return new Vertex(lightPosition.U, lightPosition.V, lightPosition.W);
        }
        public (double dif, double amb, double spec, double shin) getParameters()
        {
            return (Diffuse, Ambient, Specular, Shiness);
        }
        public void setParameters(double dif, double amb, double spec, double shin)
        {
            Diffuse = dif;
            Ambient = amb;
            Specular = spec;
            Shiness = shin;
        }
    }
}
