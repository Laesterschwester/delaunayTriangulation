namespace delaunay_triangulation
{
    internal class triangulation
    {
        //public static Triangle hilfsDreieck = new Triangle(new Point(-200, -600), new Point(-200, 2000), new Point(4200, 600));
        //public static List<Triangle> triangles;
        public static List<Triangle> triangles = new List<Triangle> { new Triangle(new Point(-200, -600), new Point(-200, 2000), new Point(4200, 600)) };
        public static List<EdgeTrianglePair> edgeTrianglePairs;
        public static List<Edge> edges = new List<Edge>();
        public struct Triangle
        {
            public Point A, B, C;
            public Math.circumcenter circumcenter;
            public Triangle(Point a, Point b, Point c)
            {
                this.A = a;
                this.B = b;
                this.C = c;
                this.circumcenter = Math.circumcenterOfThreePoints(a, b, c);
            }
            public bool contains(Point point)
            {
                if (this.A == point || this.B == point || this.C == point)
                {
                    return true;
                }
                else return false;
            }
        }
        public struct Edge
        {

            public Point A, B;
            public Triangle t1, t2;
            public Edge(Point A, Point B)
            {
                this.A = A; this.B = B;
            }
        }
        public static bool edgeListContainsEdge(List<Edge> edges, Edge edge)
        {
            foreach (Edge e in edges)
            {
                if ((edge.A == e.A && edge.B == e.B) || (edge.B == e.A && edge.A == e.B))
                {
                    return true;
                }
            }
            return false;
        }
        public struct EdgeTrianglePair
        {
            Triangle t1, t2;
            Edge edge;
            public EdgeTrianglePair(Triangle t1, Triangle t2, Edge edge)
            {
                this.t2 = t2;
                this.t1 = t1;
                this.edge = edge;
            }
        }


        public static void addTriangle(Triangle t)
        {
            lock (triangles)
            {

                triangles.Add(t);
                /////////////////

                lock (edges)
                {
                    Edge edge1 = new Edge(t.A, t.B);
                    Edge edge2 = new Edge(t.B, t.C);
                    Edge edge3 = new Edge(t.C, t.A);

                    if (!edgeListContainsEdge(edges, edge1))
                    {
                        edges.Add(edge1);
                    }
                    if (!edgeListContainsEdge(edges, edge2))
                    {
                        edges.Add(edge2);
                    }
                    if (!edgeListContainsEdge(edges, edge3))
                    {
                        edges.Add(edge3);
                    }
                }
                /////////////////

            }
        }
        public static Triangle[] findTrianglesFromEdge(List<Triangle> triangles, Edge edge)
        {
            Triangle[] neighbors = new Triangle[2];
            int index = 0;
            int count;
            foreach (Triangle t in triangles)
            {
                count = 0;

                if(t.A == edge.A || t.A == edge.B)
                {
                    count++;
                }
                if (t.B == edge.A || t.B == edge.B)
                {
                    count++;
                }
                if (t.C == edge.A || t.C == edge.B)
                {
                    count++;
                }

                if (count == 2)
                {
                    neighbors[index] = t;
                    index++;
                    if (index == 2)
                    {
                        return neighbors;
                    }
                }
            }
            return neighbors;
        }

        public static void drawTriangle(Triangle triangle, Bitmap bitmap, Graphics graphics)
        {
            Pen pen = new Pen(Color.Black);
            graphics.DrawLine(pen, triangle.A, triangle.B);
            graphics.DrawLine(pen, triangle.B, triangle.C);
            graphics.DrawLine(pen, triangle.C, triangle.A);
        }
        public static void drawTriangleList(List<Triangle> triangles, Bitmap bitmap, Graphics graphics)
        {
            lock (triangles)
            {
                foreach (Triangle t in triangles)
                {
                    drawTriangle(t, bitmap, graphics);
                }
            }
        }
        public static bool isNeighbour(Triangle triangle, Triangle triangleNeighbour)//das geht besser, Felix!
        {
            int verticesConnected = 0;
            if (!(triangle.A == triangleNeighbour.A || triangle.A == triangleNeighbour.B || triangle.A == triangleNeighbour.C))
            {
                verticesConnected++;
            }
            if (!(triangle.B == triangleNeighbour.A || triangle.B == triangleNeighbour.B || triangle.B == triangleNeighbour.C))
            {
                verticesConnected++;
                if (verticesConnected > 1)
                {
                    return true;
                }
                if (verticesConnected == 0)
                {
                    return false;
                }
            }
            if (!(triangle.B == triangleNeighbour.A || triangle.B == triangleNeighbour.B || triangle.B == triangleNeighbour.C))
            {
                return true;
            }
            return false;
        }

        public static List<Triangle> findAllNeighbors(List<Triangle> triangles, Triangle t)
        {
            List<Triangle> neighbors = new List<Triangle>();
            foreach (Triangle t2 in triangles)
            {
                if (isNeighbour(t2, t))
                {
                    neighbors.Add(t2);
                }
            }
            return neighbors;
        }

        public static void findTriangleAroundPoint(Point point, List<Triangle> triangles)
        {
            for (int i = 0; i < triangles.Count; i++)
            {
                Triangle t = triangles[i];
                if (Math.PinsideABC(point, t.A, t.B, t.C))
                {
                    lock (triangles)
                    {
                        Triangle[] triangleArr = subdivideTriangle(t, point);
                        triangles.RemoveAt(i);

                        //triangles.Add(triangleArr[0]);
                        //triangles.Add(triangleArr[1]);
                        //triangles.Add(triangleArr[2]);
                        addTriangle(triangleArr[0]);
                        addTriangle(triangleArr[1]);
                        addTriangle(triangleArr[2]);
                        return;
                    }
                }
            }
        }

        public static Triangle[] subdivideTriangle(Triangle triangle, Point point)
        {
            Triangle triangle1 = new Triangle(point, triangle.A, triangle.B);
            Triangle triangle2 = new Triangle(point, triangle.B, triangle.C);
            Triangle triangle3 = new Triangle(point, triangle.C, triangle.A);

            Triangle[] result = new Triangle[3];
            result[0] = triangle1;
            result[1] = triangle2;
            result[2] = triangle3;
            return result;
        }
        public static void flip(Edge edge)
        {
            //hier die sich berührenden Punkte finden und drehen
            Triangle t1 = edge.t1;
            Triangle t2 = edge.t2;

            Triangle newT1, newT2;
            Edge newEdge = new Edge();


            if(!(t1.A == edge.A || t1.A == edge.B))
            {
                newEdge.A = t1.A;
            }
            else if(!(t1.B == edge.A || t1.B == edge.B)) {
                newEdge.A = t1.B;
            }
            else
            {
                newEdge.A = t1.C;
            }

            if (!(t2.A == edge.A || t2.A == edge.B))
            {
                newEdge.A = t1.A;
            }
            else if (!(t2.B == edge.A || t2.B == edge.B))
            {
                newEdge.A = t2.B;
            }
            else
            {
                newEdge.A = t2.C;
            }
            newT1 = new Triangle(newEdge.A, newEdge.B, edge.A);
            newT2 = new Triangle(newEdge.A, newEdge.B, edge.B);


            newEdge.t1 = newT1;
            newEdge.t2 = newT2;

            //alte dreiecke löschen!!! und neue Dreiecke anhängen
        }
        public static bool flipCondition(Triangle triangle, Edge edge, List<Point> pointArray)
        {
            Point circumcenterXY = new Point((int)triangle.circumcenter.X, (int)triangle.circumcenter.Y);
            foreach (Point point in pointArray)
            {
                if (triangle.circumcenter.D < Math.distance(circumcenterXY, point))
                {
                    flip(edge);
                    return true;
                }
            }

            return false;
        }
        /*public static bool flip(Triangle triangle1, Triangle triangle2)
        {
            Math.circumcenter(triangle1.A, triangle1.B, triangle2.C);
            return false;
        }
        */
        public static void triangulate(List<Triangle> triangles, List<Point> points)
        {
            foreach (Point point in points)
            {
                findTriangleAroundPoint(point, triangles);
            }
        }
    }
}