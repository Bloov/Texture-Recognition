using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageProcessing;
using ImageRecognition;

namespace RecognitionSpeedTest
{
    class Program
    {
        /*private static Stopwatch timer;
        private static SimpleImage image, subImage;
        private static GLCMCreator glcm;
        private static LBPCreator lbp;*/

        static void Main(string[] args)
        {
        /*    timer = new Stopwatch();
            long totalTime = 0;
            
            Console.WriteLine("Load test");
            timer.Start();
            for (int i = 1; i <= 16; ++i)
            {
                image = new SimpleImage("D:\\Знания\\Универ\\МРО\\Labs\\textures\\new\\" + i.ToString() + ".jpg");
                image = new SimpleImage("D:\\Знания\\Универ\\МРО\\Labs\\textures\\new\\nt_" + i.ToString() + ".jpg");
                totalTime += timer.ElapsedMilliseconds;
                Console.Write("{0} - {1}    {2}", i, timer.ElapsedMilliseconds, (i % 3 == 0) ? ("\n") : (""));
                timer.Restart();
            }
            timer.Stop();
            Console.WriteLine("\nLoad test total time - {0}\n", totalTime);

            //image = new SimpleImage("D:\\Знания\\Универ\\МРО\\Labs\\textures\\new\\nt_8.jpg");
            /*Console.WriteLine("SubImage test");
            totalTime = 0;
            timer.Start();
            for (int i = 0; i < 16; ++i)
            {
                image = new SimpleImage("D:\\Знания\\Универ\\МРО\\Labs\\textures\\new\\" + (i + 1).ToString() + ".jpg");
                int scountx = image.Width / 40;
                int scounty = image.Height / 40;
                for (int sx = 0; sx < scountx; ++sx)
                {
                    for (int sy = 0; sy < scounty; ++sy)
                    {
                        subImage = image.GetSubImage(sx * 40, sy * 40, sx * 40 + 40, sy * 40 + 40);
                    }
                }
                totalTime += timer.ElapsedMilliseconds;
                Console.Write("{0} - {1}   {2}", i, timer.ElapsedMilliseconds, ((i + 1) % 3 == 0) ? ("\n") : (""));
                timer.Restart();
            }
            timer.Stop();
            Console.WriteLine("\nSubImage test total time - {0}\n", totalTime);
            
            image = new SimpleImage("D:\\Знания\\Универ\\МРО\\Labs\\textures\\new\\nt_8.jpg");
            Console.WriteLine("LBPHistogramm test");
            totalTime = 0;
            timer.Start();
            for (int i = 0; i < 16; ++i)
            {
                //image = new SimpleImage("D:\\Знания\\Универ\\МРО\\Labs\\textures\\new\\" + (i + 1).ToString() + ".jpg");
                lbp = new LBPCreator(image);
                totalTime += timer.ElapsedMilliseconds;
                Console.Write("{0} - {1}   {2}", i, timer.ElapsedMilliseconds, ((i + 1) % 3 == 0) ? ("\n") : (""));
                timer.Restart();
            }
            timer.Stop();
            Console.WriteLine("\nLBPHistogramm test total time - {0}\n", totalTime);
            
            Console.WriteLine("GLCM test");
            totalTime = 0;
            timer.Start();
            for (int i = 0; i < 20; ++i)
            {
                glcm = new GLCMCreator(image);
                totalTime += timer.ElapsedMilliseconds;
                Console.Write("{0} - {1}   {2}", i, timer.ElapsedMilliseconds, ((i + 1) % 3 == 0) ? ("\n") : (""));
                timer.Restart();
            }
            timer.Stop();
            Console.WriteLine("\nGLCM test total time - {0}\n", totalTime);
            */
            Console.ReadLine();
        }
    }
}
