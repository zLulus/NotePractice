using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet6.CodeLibrary.CalculateAngle
{
    public class CalculateAngleDemo
    {
        public static void Run()
        {
            //Test1();
            Test2();
        }

        private static void Test2()
        {
            //Console.WriteLine(CalculateAngle(new Point(0, 0), new Point(1, 0), new Point(1, 1)));
            //Console.WriteLine(CalculateAngle(new Point(0, 0), new Point(0, 1), new Point(1, 1)));
            Console.WriteLine(CalculateAngle(new Point(0, 0), new Point(0, 3), new Point(4, 0)));
        }

        /// <summary>
        /// https://www.calculator.net/triangle-calculator.html
        /// https://www.omnicalculator.com/math/triangle-angle
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        /// <returns></returns>
        private static double CalculateAngle(Point p1, Point p2, Point p3)
        {
            var p1p2 = CalculateDistance(p1, p2);
            var p2p3 = CalculateDistance(p2, p3);
            var p1p3 = CalculateDistance(p1, p3);

            var cosA1 = (Math.Pow(p1p2, 2) + Math.Pow(p2p3, 2) - Math.Pow(p1p3, 2)) / (2 * p1p2 * p2p3);
            //弧度rad  ->计算角度 180/Math.PI=rad
            var A1 = Math.Acos(cosA1);
            var A1_2 = Math.Acosh(cosA1);
            var cosA2 = (Math.Pow(p1p2, 2) + Math.Pow(p1p3, 2) - Math.Pow(p2p3, 2)) / (2 * p1p2 * p1p3);
            var A2 = Math.Acos(cosA2);
            var cosA3 = (Math.Pow(p2p3, 2) + Math.Pow(p1p3, 2) - Math.Pow(p1p2, 2)) / (2 * p2p3 * p1p3);
            var A3 = Math.Acos(cosA3);

            var a1 = (180 / Math.PI) * A1;//(A1 / (A1 + A2 + A3)) * 180;
            var a2 = (180 / Math.PI) * A2;//(A2 / (A1 + A2 + A3)) * 180;
            var a3 = (180 / Math.PI) * A3;//(A3 / (A1 + A2 + A3)) * 180;
            return A1;
        }

        private static double CalculateDistance(Point p1, Point p2)
        {
            return Math.Sqrt(Math.Pow(p2.X - p1.X, 2) + Math.Pow(p2.Y - p1.Y, 2));
        }

        private static void Test1()
        {
            //0 正上
            Console.WriteLine(CalculateAngle(new Point(0, 0), new Point(1, 0)));
            //45 右上
            Console.WriteLine(CalculateAngle(new Point(0, 0), new Point(1, 1)));
            //90 右
            Console.WriteLine(CalculateAngle(new Point(0, 0), new Point(0, 1)));
            //-45 右下 180+（-45）=135
            Console.WriteLine(CalculateAngle(new Point(0, 0), new Point(1, -1)));
            //180 下
            Console.WriteLine(CalculateAngle(new Point(0, 0), new Point(-1, 0)));
            //-135 左下 360+（-135）=225
            Console.WriteLine(CalculateAngle(new Point(0, 0), new Point(-1, -1)));
            //180 左 270
            Console.WriteLine(CalculateAngle(new Point(0, 0), new Point(-1, 0)));
            //135 左上 315
            Console.WriteLine(CalculateAngle(new Point(0, 0), new Point(-1, 1)));
        }

        private static double CalculateAngle(Point p1, Point p2)
        {
            return Math.Atan2((p2.Y - p1.Y), (p2.X - p1.X)) * 180 / Math.PI;
        }
    }
}
