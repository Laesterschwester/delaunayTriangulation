using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;

namespace delaunay_triangulation
{
    internal class Points
    {
        static int arrayLen = 0;
        static List<Point> pointArray = new List<Point>();
        //static Point[] pointArray = new Point[0];
        public Points(List<Point> points)
        {
            lock (pointArray)
            {
                pointArray = points;
                arrayLen = pointArray.Count;

            }
        }
        public static void addPoints(Point point)
        {
            lock (pointArray) {
                pointArray.Add(point);
                arrayLen++;
            }
            
        }
        public static void addPointsFromClick(EventArgs e)
        {
            MouseEventArgs mouseEventArgs = e as MouseEventArgs;
            Point point = mouseEventArgs.Location;
            if(!pointArray.Contains(point))
            {
                lock(pointArray) {
                    pointArray.Add(point);
                    arrayLen++;
                }
            }
            triangulation.findTriangleAroundPoint(point, triangulation.triangles);//////////////////7
        }

        public static void drawPoints(Bitmap bitmap, Graphics graphics)
        {
            int radius = 4;
            int diameter = radius * 2;
            List <Point> arrayCopy = new List<Point>(pointArray);
            foreach(Point point in arrayCopy)
            {
                graphics.FillEllipse(new SolidBrush(Color.Black), new Rectangle(point.X - radius, point.Y-radius, diameter, diameter));
            }
        }
    }
}
