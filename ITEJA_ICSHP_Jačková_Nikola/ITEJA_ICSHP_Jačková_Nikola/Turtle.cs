using ITEJA_ICSHP_Jačková_Nikola.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
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
        public float X { get; private set; }
        public float Y { get; private set; }
        public float Angle { get; private set; }
        public int Delay { get; private set; }
        private Control Control { get; set; }
        private Image TurtleImage { get; set; } = Resources.turtle_colour;
        private Graphics Graphics { get; set; } = null;
        private Pen Pen { get; set; }
        private Point CenterPoint { get; set; }

        public void InitializeTurtle(Control control, PaintEventArgs e)
        {
            Control = control;
            Graphics = e.Graphics;
            CenterPoint = new Point(control.Width / 2 - TurtleImage.Width / 2, control.Height / 2 - TurtleImage.Height / 2);

            X = CenterPoint.X;
            Y = CenterPoint.Y;
            Angle = 0;
            Delay = 500;

            Pen = new Pen(Brushes.Black, 4);
        }

        public void Forward(double distance)
        {
            double angleRadians = Angle * Math.PI / 180;
            float newX = X + (float)(Math.Cos(angleRadians) * distance);
            float newY = Y + (float)(Math.Sin(angleRadians) * distance);

            Graphics.DrawLine(Pen, X, Y, newX, newY);

            X = newX;
            Y = newY;
            DrawTurtle(0);
            Paint();
        }

        public void Backward(double distance)
        {
            Forward(-distance);
        }

        public void Rotate(double angle)
        {
            Angle += (float)angle;
            DrawTurtle((float)angle);
            Paint();
        }

        private void DrawTurtle(float angle)
        {
            Image oldImage = TurtleImage;
            TurtleImage = RotateTurtle(angle);
            oldImage.Dispose();
            Graphics.DrawImage(TurtleImage, new PointF(X - TurtleImage.Width / 2, Y - TurtleImage.Height / 2));
        }

        private Bitmap RotateTurtle(float angle)
        {
            Bitmap rotatedImage = new Bitmap(TurtleImage.Width, TurtleImage.Height);
            rotatedImage.SetResolution(TurtleImage.HorizontalResolution, TurtleImage.VerticalResolution);

            using (Graphics graphics = Graphics.FromImage(rotatedImage))
            {
                graphics.TranslateTransform(TurtleImage.Width / 2, TurtleImage.Height / 2);

                graphics.RotateTransform(angle);

                graphics.TranslateTransform(-TurtleImage.Width / 2, -TurtleImage.Height / 2);

                graphics.DrawImage(TurtleImage, new Point(0, 0));
            }
            return rotatedImage;
        }

        private void Paint()
        {
            Control.Update();
            Thread.Sleep(Delay);
            Application.DoEvents();
        }
    }
}
