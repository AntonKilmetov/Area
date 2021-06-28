using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LakeArea
{
    public partial class Form1 : Form
    {
        private Random random = new Random();
        const double Area = 8.59 * 7.11; // площадь озера км^2
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var bmp = new Bitmap(Image.FromFile(@"Lake3.png")); // загрузка изображения
            var pixelColor = bmp.GetPixel(bmp.Width / 2, bmp.Height / 2); // эталонный пиксель
            textBox8.Text = Test(bmp, pixelColor).ToString(); // запись данных
            double a, b, c, d, k, f, g;
            a = b = c = d = k = f = g = 0;
            for (int i = 0; i < 20; i++)
            {
                textBox1.Text = MonteCarlo(bmp, pixelColor, 500).ToString();
                textBox2.Text = MonteCarlo(bmp, pixelColor, 1000).ToString();
                textBox3.Text = MonteCarlo(bmp, pixelColor, 5000).ToString();
                textBox4.Text = MonteCarlo(bmp, pixelColor, 10000).ToString();
                textBox5.Text = MonteCarlo(bmp, pixelColor, 50000).ToString();
                textBox6.Text = MonteCarlo(bmp, pixelColor, 100000).ToString();
                textBox7.Text = MonteCarlo(bmp, pixelColor, 1000000).ToString();

                textBox9.Text = Error(double.Parse(textBox1.Text), double.Parse(textBox8.Text)).ToString();
                textBox10.Text = Error(double.Parse(textBox2.Text), double.Parse(textBox8.Text)).ToString();
                textBox11.Text = Error(double.Parse(textBox3.Text), double.Parse(textBox8.Text)).ToString();
                textBox12.Text = Error(double.Parse(textBox4.Text), double.Parse(textBox8.Text)).ToString();
                textBox13.Text = Error(double.Parse(textBox5.Text), double.Parse(textBox8.Text)).ToString();
                textBox14.Text = Error(double.Parse(textBox6.Text), double.Parse(textBox8.Text)).ToString();
                textBox15.Text = Error(double.Parse(textBox7.Text), double.Parse(textBox8.Text)).ToString();

                a += double.Parse(textBox9.Text);
                b += double.Parse(textBox10.Text);
                c += double.Parse(textBox11.Text);
                d += double.Parse(textBox12.Text);
                k += double.Parse(textBox13.Text);
                f += double.Parse(textBox14.Text);
                g += double.Parse(textBox15.Text);
            }
            textBox16.Text = (a / 20).ToString();
            textBox17.Text = (b / 20).ToString();
            textBox18.Text = (c / 20).ToString();
            textBox19.Text = (d / 20).ToString();
            textBox20.Text = (k / 20).ToString();
            textBox21.Text = (f / 20).ToString();
            textBox22.Text = (g / 20).ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex">Точный результат</param>
        /// <param name="approx">Приближенный результат</param>
        /// <returns>Относительная погрешность</returns>
        private double Error(double ex, double approx) => Math.Abs(approx - ex) / approx * 100;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bmp">Изображение</param>
        /// <param name="pixelColor">Цвет эталонного пикселя</param>
        /// <param name="iteiterationsCount">Количество итераций</param>
        /// <returns></returns>
        private double MonteCarlo(Bitmap bmp, Color pixelColor, int iteiterationsCount)
        {
            var counter = 0;
            for (int i = 0; i < iteiterationsCount; i++)
            {
                int x = random.Next(0, bmp.Width);
                int y = random.Next(0, bmp.Height);
                if (bmp.GetPixel(x, y) == pixelColor)
                    counter++;
            }
            return ((double)counter / iteiterationsCount) * Area;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bmp">Изображение</param>
        /// <param name="pixelColor">Цвет эталонного пикселя</param>
        /// <returns>Точная площадь</returns>
        private double Test(Bitmap bmp, Color pixelColor)
        {
            int counter = 0;
            for (int i = 0; i < bmp.Height; i++)      
                for (int j = 0; j < bmp.Width; j++)
                    if (bmp.GetPixel(j, i) == pixelColor)
                        counter++;
            return (double)counter / (bmp.Height * bmp.Width) * Area;
        }
    }
}
