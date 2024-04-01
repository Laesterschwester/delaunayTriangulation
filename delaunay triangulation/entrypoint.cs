using System.Security.Cryptography.X509Certificates;

namespace delaunay_triangulation
{
    internal class entrypoint
    {
        public static void entry(PictureBox pictureBox, TextBox console)
        {

            console.Visible = true;

            //bool a = Math.PinsideABC(new Point(-10000, -10000), triangulation.triangles[0].A, triangulation.triangles[0].B, triangulation.triangles[0].C);
            //System.Diagnostics.Debug.WriteLine(a);
            
            
            
            Task task = new Task(() => { gameloop.loop(pictureBox); });
            task.Start();
        }
    }
}
