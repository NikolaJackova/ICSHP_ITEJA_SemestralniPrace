using ITEJA_ICSHP_Jačková_Nikola.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ITEJA_ICSHP_Jačková_Nikola
{
    class Turtle
    {
        private static Turtle Instance { get; set; }

        public static Turtle GetInstance()
        {
            if (Instance == null)
            {
                Instance = new Turtle();
            }
            return Instance;
        }
        private Control Control { get; set; }
        private PictureBox TurtleImage { get; set; }
        private Graphics Graphics { get; set; }
        private Point CenterPoint { get; set; }

        public void InitializeTurtle(Control control)
        {
            Control = control;
            CenterPoint = new Point(control.Width/2, control.Height/2);
            TurtleImage = new PictureBox
            {
                Image = Resources.turtle_png,
                Width = Resources.turtle_png.Width,
                Height = Resources.turtle_png.Height,
                SizeMode = PictureBoxSizeMode.Zoom,
                Location = CenterPoint
            };
            Control.Controls.Add(TurtleImage);
        }

        public void Forward(double distance)
        {

        }
    }
}
