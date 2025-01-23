using System;

namespace InheritanceLearning
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("hello world");

            Person firstPerson = new() { Name = "Bob", Age = 78 };

            Staff firstStaff = new() { Name = "Peter", Age = 76 };

            TruckDriver firstDriver = new()
            {
                TruckType = "Mac",
                Name = "Cleatus",
                Age = 26,
            };

            Console.WriteLine(firstPerson.Name + " " + firstStaff.Age);
            Console.WriteLine(firstStaff.Position + " is the position; the ID is " + firstStaff.Id);
            firstPerson.DisplayInfo();
            firstStaff.DisplayInfo();
            firstDriver.DisplayInfo();
        }
    }
}
