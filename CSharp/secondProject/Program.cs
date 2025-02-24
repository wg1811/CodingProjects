using System;
namespace secondProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Console.WriteLine("What is your name?");
            // string? myName = Console.ReadLine();
            // Console.WriteLine($"Nice to meet you, {myName}.");
            // Console.WriteLine("How old are you?");
            // string? myAgeString = Console.ReadLine();
            // Console.WriteLine($"{myAgeString}! You look great.");

            // // Convert from string to int
            // int myAge = Convert.ToInt32(myAgeString);
            // Console.WriteLine($"{myAge}! You look great.");

            // //Get data types
            // Console.WriteLine($"Do you think you'll be dead ten years from now, when you're " + (myAge + 10) + "?  Houston");
            Random random = new Random();
            int randomNumber = random.Next(1,500);
            Console.WriteLine(randomNumber);

            Console.ReadLine
        }
    }
}
