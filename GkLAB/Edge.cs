using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Math;
namespace GkLAB
{
   public class Edge
    {
        Vertex from;
        Vertex to;
        public Vector fromB;
        public Vector toB;
        public Color colorFrom;
        public Color colorTo;
        public Vector normalTo;
        public Vector normalFrom;
        public Vector toLightfrom;
        public Vector toLightto;
        public Vertex From
        {        
            get { return from; }
            set { from = value; }
        }
        
        public Vertex To
        {
            get { return to; }
            set { to = value; }
        }
        public Edge(Vertex _from, Vertex _to)
        {
            from = _from;
            to = _to;
        }
        public Edge(Vertex _from, Vertex _to,Color _colorfrom, Color _colorto)
        {
            from = _from;
            to = _to;
            colorFrom = _colorfrom;
            colorTo = _colorto;
        }
        public Edge(Vertex _from, Vertex _to, Color _colorfrom, Color _colorto,Vector beforeFromProjection,Vector beforeToProjection,Vector normalFrom,Vector normalTo,Vector v1,Vector v2)
        {
            from = _from;
            to = _to;
            colorFrom = _colorfrom;
            colorTo = _colorto;
            fromB = beforeFromProjection;
            toB = beforeToProjection;
            this.normalFrom = normalFrom;
            this.normalTo = normalTo;
            toLightfrom = v1;
            toLightto = v2;
        }
        public int SignOfVectorProduct(Point point)
        {
            return Sign((to.X - from.X)*(point.Y - from.Y) - (point.X - from.X)*(to.Y - from.Y));
        }
    }
}
