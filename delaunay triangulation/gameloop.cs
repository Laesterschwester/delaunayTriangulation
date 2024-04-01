using System.Diagnostics;
namespace delaunay_triangulation
{
    public class TimeManager
    {
        private Stopwatch sw;
        private double time = 0; //in ms
        private double lastFrame = 0;

        public TimeManager()
        {
            this.sw = new Stopwatch();
            this.sw.Start();
        }
        public void updateTime()
        {
            this.time = sw.Elapsed.TotalMilliseconds;
        }
        public double getTime()
        {
            return this.time;
        }
        public double getDeltaTime()
        {
            return this.time - this.lastFrame;
        }

    }
    internal class gameloop
    {
        public static void loop(PictureBox pictureBox)
        {
            TimeManager timeManager = new TimeManager();

            while (true)
            {
                Bitmap bitmap = new Bitmap(pictureBox.Width, pictureBox.Height);
                Graphics graphics = Graphics.FromImage(bitmap);
                graphics.Clear(Color.White);
                /////////////////////////////////////////////////////

                

                triangulation.drawTriangleList(triangulation.triangles, bitmap, graphics);
                //System.Diagnostics.Debug.WriteLine(triangulation.triangles.Count);
                Points.drawPoints(bitmap, graphics);







                ///////////////////////////////////////////////////////
                lock(pictureBox){
                    lock (bitmap)
                    {
                        try
                        {
                            pictureBox.Image = bitmap;

                        }
                        catch (Exception e)
                        {
                            //error gefangen, und ignoriert, get rekt!
                        }
                    }
                }
            }
        }
    }
}
