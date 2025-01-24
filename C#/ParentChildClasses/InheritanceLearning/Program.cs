using System;

namespace InheritanceLearning
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            // Snake Instances
            Snake snake1 = new Snake
            {
                Name = "Cobra",
                Age = 5,
                Habitat = new string[] { "Desert", "Jungle" }, //Ask GTP how to encapsulate this so I can only choose from the options in the init.
                Length = 3.5,
                IsVenomous = true,
            };

            Snake snake2 = new Snake
            {
                Name = "Python",
                Age = 8,
                Habitat = new string[] { "Jungle", "Plains" },
                Length = 4.2,
                IsVenomous = false,
            };

            // Bear Instances
            Bear bear1 = new Bear
            {
                Name = "Grizzly Bear",
                Age = 12,
                Habitat = new string[] { "Forest", "Mountain" },
                IsHibernating = true,
                BearType = new string[] { "Grizzly" },
            };

            Bear bear2 = new Bear
            {
                Name = "Polar Bear",
                Age = 15,
                Habitat = new string[] { "Polar" },
                IsHibernating = true,
                BearType = new string[] { "Polar" },
            };

            // Cat Instances
            Cat cat1 = new Cat
            {
                Name = "Lion",
                Age = 7,
                Habitat = new string[] { "Savannah", "Grassland" },
                CatType = new string[] { "Lion" },
            };

            Cat cat2 = new Cat
            {
                Name = "Housecat",
                Age = 3,
                Habitat = new string[] { "Urban" },
                CatType = new string[] { "Housecat" },
            };

            // Display info for each animal
            snake1.DisplayInfo();
            snake2.DisplayInfo();
            bear1.DisplayInfo();
            bear2.DisplayInfo();
            cat1.DisplayInfo();
            cat2.DisplayInfo();

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
