using System;
using System.IO;
using System.Drawing;
using System.Reflection;

namespace ImageFilter
{
    public class Program
    {
        // Цвета, которые на фото.
        // Приведём все цвета изображения к этим цветам.
        // Цель: навести резкость изображения, убрать плавные переходы между цветами, размытие.
        public static Color transparent = Color.FromArgb(0, 0, 0);
        public static Color purple = Color.FromArgb(210, 27, 148);
        public static Color black = Color.FromArgb(27, 27, 27);
        public static Color white = Color.FromArgb(255, 207, 198);
        public static Color corn = Color.FromArgb(247, 121, 105);

        /// <summary>
        /// Возвращает «расстояние» между цветами.
        /// </summary>
        /// <returns></returns>
        public static int DistanceBetweenColors(Color color1, Color color2)
        {
            return Convert.ToInt32(Math.Sqrt(
                   Math.Pow((color1.R - color2.R), 2) 
                 + Math.Pow((color1.G - color2.G), 2)
                 + Math.Pow((color1.B - color2.B), 2)));
        }

        /// <summary>
        /// Возвращает минимальное значение пяти аргументов.
        /// </summary>
        public static int Min(int v, int w, int x, int y, int z)
        {
            return Math.Min(v, Math.Min(w, Math.Min(x, Math.Min(y, z))));
        }

        public static void Main(string[] args)
        {
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            Bitmap image = new Bitmap(path + "\\Mighty Final Fight - Guy.png");
            
            // Обходим всё изображение.
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    int distanceToTransparent = DistanceBetweenColors(image.GetPixel(x, y), transparent);
                    int distanceToPurple = DistanceBetweenColors(image.GetPixel(x, y), purple);
                    int distanceToBlack = DistanceBetweenColors(image.GetPixel(x, y), black);
                    int distanceToWhite = DistanceBetweenColors(image.GetPixel(x, y), white);
                    int distanceToCorn = DistanceBetweenColors(image.GetPixel(x, y), corn);

                    int minDistance = Min(distanceToTransparent, distanceToPurple, distanceToBlack, distanceToWhite, distanceToCorn);

                    if (minDistance == distanceToTransparent)
                    {
                        image.SetPixel(x, y, transparent);
                    }
                    else if (minDistance == distanceToPurple)
                    {
                        image.SetPixel(x, y, purple);
                    }
                    else if (minDistance == distanceToBlack)
                    {
                        image.SetPixel(x, y, black);
                    }
                    else if (minDistance == distanceToWhite)
                    {
                        image.SetPixel(x, y, white);
                    }
                    else if (minDistance == distanceToCorn)
                    {
                        image.SetPixel(x, y, corn);
                    }
                }
            }

            image.MakeTransparent();

            image.Save(path + "\\Mighty Final Fight - Guy (filtered).png");
        }
    }
}
