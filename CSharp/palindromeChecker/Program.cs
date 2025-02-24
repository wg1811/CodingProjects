using System;
using System.Linq;
namespace palindromeChecker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string palinOne = "A man a plan a canal Panama";
            string palinTwo = "racecar";
            Console.WriteLine(palinOne);
            string noSpacesPalinOne = RemoveSpaces(palinOne);
            char[] palinOneChars = ConvertToArray(noSpacesPalinOne);
            Console.WriteLine(noSpacesPalinOne + " ------ is the string with no spaces.");
            CheckPalindrome(palinOneChars);
            Console.WriteLine(palinTwo);
            string noSpacesPalinTwo = RemoveSpaces(palinTwo);
            char[] palinTwoChars = ConvertToArray(noSpacesPalinTwo);
            Console.WriteLine(noSpacesPalinTwo + " ------ is the string with no spaces.");
            CheckPalindrome(palinTwoChars);
        }

        static char[] ConvertToArray(string toConvert)
        {
            return toConvert.ToCharArray();
        }

        static string RemoveSpaces(string withSpaces)
        {
            string noSpaces = withSpaces.Replace(" ", string.Empty);
            return noSpaces.ToLower();
        }

        static void CheckPalindrome(char[] toCheck)
        {
            char[] theReverse = new char[toCheck.Length];
            for (int i = 0; i < toCheck.Length; i++)
            {
                theReverse[i] = toCheck[i];
            }
            Array.Reverse(theReverse);
            if (theReverse.SequenceEqual(toCheck))
            {
                Console.WriteLine("That's a palindrome.");
            }
            else Console.WriteLine("That's not a palindrome.");
        }
        static void ShowPalin(string thePalin, char[] toShow)
        {
            foreach (char i in toShow)
            {
                Console.Write(i);
            }
            Console.WriteLine("");
        }
    }
}