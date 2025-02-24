using System;
using System.Security.Cryptography.X509Certificates;
namespace arrayLearning
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Program arrayProgram = new Program();

            arrayProgram.GreetingUser();
            arrayProgram.SoBoring();

            double taxRate = .3;
            double monthlyIncome = 3000000;
            double showNetIncome = ShowNetMonthlyIncome(taxRate, monthlyIncome);
            Console.WriteLine(showNetIncome + " is the net monthly income.");

            int[] numberArray = [2, 3234, 45, 546, 7, 76, 435, 342, 345, 768, 5, 8, 9, 54, 32, 43, 6568];
            //loops
            /*  int numArraySize = numberArray.Length;
              for (int i = 0; i < numArraySize; i++)
              {
                  Console.Write(numberArray[i] + ", ");
              } */
            int keepTrack = 0;
            for (int i = 0; i <= 100; i++)
            {
                keepTrack = keepTrack + i;
            }
            Console.WriteLine(keepTrack);

            int test1 = 5;
            int test2 = 10;
            int testResult = SumRange(test1, test2);
            // Console.WriteLine(testResult);

            //string array 
            string[] fruits = {
                "Lemon",
                "Apple",
                "Kiwi",
                "Fig",
                "Date",
                "Banana",
                "Grape",
                "Cherry",
                "Elderberry",
                "Honeydew"
            };

            //showArrayElem(fruits);
            Console.WriteLine("");
            string[] alphaFruits = new string[fruits.Length];
            Array.Copy(fruits, alphaFruits, fruits.Length);
            Array.Sort(alphaFruits);
            ShowArrayElem(fruits);
            Console.WriteLine("");
            ShowArrayElem(alphaFruits);
        }
        public static int SumRange(int lowNum, int highNum)
        {
            int theSum = 0;
            for (int i = lowNum; i < highNum; i++)
            {
                theSum += i;
            }
            Console.WriteLine(theSum);
            return (theSum);
        }

        public static void ShowArrayElem(string[] toShow)
        {
            for (int i = 0; i < toShow.Length; i++)
            {
                Console.Write(toShow[i] + " ");
            }
        }

        public void GreetingUser()
        {
            Console.WriteLine("Yo killer.");
        }
        public void SoBoring()
        {
            Console.WriteLine("Can this class GET any more BORING!?");
        }

        public static double ShowNetMonthlyIncome(double taxRate, double income)
        {
            double theResult = 0;
            theResult = income - income * taxRate;
            return theResult;
        }
    }
}

