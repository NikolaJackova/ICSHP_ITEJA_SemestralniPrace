using ITEJA_ICSHP_Jačková_Nikola.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ITEJA_ICSHP_Jačková_Nikola
{
    static class Turtle
    {
        public static PictureBox TurtleImage { get; private set; }
        public static Control Control { get; private set; }

        public static void InitializeTurtle(Control control)
        {
            Control = control;
            TurtleImage = new PictureBox();
            TurtleImage.Image = Resources.turtle_png;
            Control.Controls.Add(TurtleImage);
        }
    }
}
