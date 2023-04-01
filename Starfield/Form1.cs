using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Starfield
{
    public partial class Form1 : Form
    {
        public class Star
        { 
            public float X { get; set; }
            public float Y { get; set; }
            public float Z { get; set; }
        }

        private Star[] all_stars = new Star[15000];

        private Random random_numbers = new Random();

        private Graphics graphics;

        public Form1()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            graphics.Clear(Color.Black);

            foreach (var one_star in all_stars)
            {
                DrawStars(one_star);
                MoveStars(one_star);
            }

            pictureBox1.Refresh();
        }

        private void MoveStars(Star one_star)
        {
            one_star.Z -= 15;

            if (one_star.Z < 1)
            {
                one_star.X = random_numbers.Next(-pictureBox1.Width, pictureBox1.Width);
                one_star.Y = random_numbers.Next(-pictureBox1.Height, pictureBox1.Height);
                one_star.Z = random_numbers.Next(1, pictureBox1.Width);
            }
        }

        private void DrawStars(Star one_star)
        {
            float sizeStar = Map1(one_star.Z, 0, pictureBox1.Width, 5, 0);

            float x = Map1(one_star.X / one_star.Z, 0, 1, 0, pictureBox1.Width) + pictureBox1.Width / 2;
            
            float y = Map1(one_star.Y / one_star.Z, 0, 1, 0, pictureBox1.Height) + pictureBox1.Height / 2;
            
            graphics.FillEllipse(Brushes.Azure, x, y, sizeStar, sizeStar);
        }

        private float Map1(float n, float start_1, float stop_1, float start_2, float stop_2)
        {
            return ((n - start_1)/(stop_1 - start_1))*(stop_2 - start_2) + start_2;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);

            graphics = Graphics.FromImage(pictureBox1.Image);

            for (int i = 0; i < all_stars.Length; i++)
            {
                all_stars[i] = new Star()
                {
                    X = random_numbers.Next(-pictureBox1.Width, pictureBox1.Width),
                    Y = random_numbers.Next(-pictureBox1.Height, pictureBox1.Height),
                    Z = random_numbers.Next(1, pictureBox1.Width)
                };

                timer1.Start();
            }
        }
    }
}
