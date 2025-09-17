using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCalculate
{
    public class Calculate
    {
        /// <summary>
        /// 加法
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static double Add(double a, double b)
        {
            return a + b;
        }
        /// <summary>
        /// 减法
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static double Sub(double a, double b)
        {
            return a - b;
        }
        /// <summary>
        /// 乘法
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static double Mul(double a, double b)
        {
            return a * b;
        }
        /// <summary>
        /// 除法
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static double Div(double a, double b)
        {
            if (b == 0)
            {
                //正无穷大
                return double.PositiveInfinity;
            }
            return (a / b);
        }
    }
}
