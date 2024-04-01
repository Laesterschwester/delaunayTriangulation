using System.Threading.Tasks;

namespace delaunay_triangulation
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow; // Hide window border
            this.WindowState = FormWindowState.Maximized; // Start in full-screen mode
            this.StartPosition = FormStartPosition.CenterScreen;
            pictureBox1.Dock = DockStyle.Fill;

            entrypoint.entry(pictureBox1, textBox1);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            lock (pictureBox1)
            {
                Points.addPointsFromClick(e);
            }
        }
    }
}
