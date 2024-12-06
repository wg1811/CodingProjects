using System;
namespace methodPractice
{
    internal class Program
    {
        static void Main(string[] args)
        {
            double givenMoney = 1;
            Console.WriteLine(MillionMore(givenMoney));
        }

        public static double MillionMore(double fakeMoney)
        {
            return (fakeMoney * 1000000);
        }
    }
}