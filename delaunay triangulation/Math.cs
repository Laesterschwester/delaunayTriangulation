namespace delaunay_triangulation
{
    internal class Math
    {
        public struct circumcenter
        {
            public double X, Y, D;
            public circumcenter(double X, double Y, double D)
            {
                this.X = X;
                this.Y = Y;
                this.D = D;
            }
        }
        public struct doublePoint
        {
            public double X;
            public double Y;
            public doublePoint(double X, double Y)
            {
                this.X = X;
                this.Y = Y;
            }
        }

        public static bool PinsideABC(Point p, Point a, Point b, Point c)
        {
            double areaABC;
            double areaPAB;
            double areaPBC;
            double areaPCA;
            areaABC = triangleArea(a, b, c);
            areaPAB = triangleArea(p, a, b);
            areaPBC = triangleArea(p, b, c);
            areaPCA = triangleArea(p, c, a);
            System.Diagnostics.Debug.WriteLine("areaABC: " + areaABC);
            System.Diagnostics.Debug.WriteLine("areaPAB: " + areaPAB);
            System.Diagnostics.Debug.WriteLine("areaPBC: " + areaPBC);
            System.Diagnostics.Debug.WriteLine("areaPCA: " + areaPCA);

            if (areaPCA < 0 || areaPAB < 0 || areaPBC < 0)
            {
                return false;
            }
            if (areaPCA + areaPAB + areaPBC == areaABC)
            {
                if (areaPCA == 0 || areaPBC == 0 || areaPAB == 0)
                {
                    return false;
                }
                return true;
            }
            if(areaPCA + areaPAB + areaPBC > areaABC)
            {
                return false;
            }
            return true;
        }

        public static circumcenter circumcenterOfThreePoints(Point A, Point B, Point C)
        {
            double t = A.X * A.X + A.Y * A.Y - B.X * B.X - B.Y * B.Y;
            double u = A.X * A.X + A.Y * A.Y - C.X * C.X - C.Y * C.Y;
            double j = (A.X - B.X) * (A.Y - C.Y) - (A.X - C.X) * (A.Y - B.Y);
            double x = (-(A.Y - B.Y) * u + (A.Y - C.Y) * t) / (2 * j);
            double y = ((A.X - B.X) * u - (A.X - C.X) * t) / (2 * j);
            double d = System.Math.Sqrt((A.X - x) * (A.X - x) + (A.Y - y) * (A.Y - y));
            return new circumcenter(x, y, d);
        }
        public static double distance(Point A, Point B)
        {
            return System.Math.Sqrt((B.X-A.X)*(B.X-A.X) + (B.Y-A.Y)*(B.Y-A.Y));
        }
        public static Point getVectorAB(Point a, Point b)
        {
            return new Point(b.X - a.X, b.Y - a.Y);
        }
        public static double triangleArea(Point a, Point b, Point c)
        {
            return 0.5 * (System.Math.Abs(a.X * (b.Y - c.Y) + b.X * (c.Y - a.Y) + c.X * (a.Y - b.Y)));
        }
    }
}
