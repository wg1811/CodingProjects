using System;
using System.IO;
using System.Text.Json; // 1. Needed to do json stuff
using System.Text.RegularExpressions;

namespace FlowerConsoleApp
{
    public class FlowerController
    {
        private List<Flower> flowers = new List<Flower>();
        private FlowerView view = new();
        public int nextId = 0;
        private string filePath = "flowers.json"; // 2. Add file path

        public FlowerController()
        {
            LoadFromFile(); // 3. Method to handle the file created way below.
        }

        //  Student CRUD stuff
        public void AddFlower()
        {
            Console.WriteLine("\nPlease enter the flower's common name: ");
            string flowerCommonName = CheckIfString(Console.ReadLine());

            Console.WriteLine("\nPlease enter the flower's latin name: ");
            string flowerLatinName = CheckIfString(Console.ReadLine());

            Console.WriteLine("\nPlease enter the flower's watering instructions: ");
            string flowerWatering = CheckIfString(Console.ReadLine());

            Console.WriteLine("\nPlease enter the flower's sunlight requirements: ");
            string flowerSunlight = CheckIfString(Console.ReadLine());

            Console.WriteLine("\nPlease enter the flower's Image URL: ");
            string flowerImage = CheckIfString(Console.ReadLine());

            Flower flower = new Flower()
            {
                Id = nextId++,
                FlowerCommonName = flowerCommonName,
                FlowerLatinName = flowerLatinName,
                FlowerWatering = flowerWatering,
                FlowerSunlight = flowerSunlight,
                FlowerImage = flowerImage,
            };

            SaveToFile(); // 4. Have to save so it persists (made below)

            Console.WriteLine("Flower entered successfully.");
        }

        public void ShowFlower()
        {
            Console.WriteLine("Please enter the flower id.");
            int flowerId = CheckIfId(Console.ReadLine());
            var existingFlower = flowers.Find(f => f.Id == flowerId);
            if (existingFlower == null)
            {
                Console.WriteLine("Flower id doesn't exist");
            }
            else
            {
                view.DisplayFlower(existingFlower);
            }
        }

        public void ShowFlowers()
        {
            view.DisplayFlowers(flowers);
        }

        public void DeleteFlower()
        {
            Console.WriteLine("Please enter the flower id.");
            int flowerId = CheckIfId(Console.ReadLine());
            var existingFlower = flowers.Find(f => f.Id == flowerId);
            if (existingFlower == null)
            {
                Console.WriteLine("Flower id doesn't exist");
            }
            else
            {
                flowers.Remove(existingFlower);
                SaveToFile();
                Console.WriteLine("Flower successfully deleted.");
            }
        }

        public void UpdateFlower()
        {
            Console.WriteLine("Enter Flower Id to Update");
            int id = CheckIfId(Console.ReadLine());
            var existingFlower = flowers.Find(f => f.Id == id);
            if (existingFlower == null)
            {
                Console.WriteLine("Flower id is not found");
            }
            else
            {
                Console.WriteLine("\nPlease enter the flower's common name: ");
                string flowerCommonName = CheckIfString(Console.ReadLine());

                Console.WriteLine("\nPlease enter the flower's latin name: ");
                string flowerLatinName = CheckIfString(Console.ReadLine());

                Console.WriteLine("\nPlease enter the flower's watering instructions: ");
                string flowerWatering = CheckIfString(Console.ReadLine());

                Console.WriteLine("\nPlease enter the flower's sunlight requirements: ");
                string flowerSunlight = CheckIfString(Console.ReadLine());

                Console.WriteLine("\nPlease enter the flower's Image URL: ");
                string flowerImage = CheckIfString(Console.ReadLine());

                //update existed Student
                existingFlower.FlowerCommonName = flowerCommonName;
                existingFlower.FlowerLatinName = flowerLatinName;
                existingFlower.FlowerWatering = flowerWatering;
                existingFlower.FlowerSunlight = flowerSunlight;
                existingFlower.FlowerImage = flowerImage;

                SaveToFile(); //save changing
                Console.WriteLine("Flower successfully updated.");
            }
        }

        // Function to check if the input is a valid string (only letters)
        public static string CheckIfString(string? input)
        {
            string textInput = input ?? string.Empty;
            while (true)
            {
                if (Regex.IsMatch(textInput, @"^[a-zA-Z]+$")) // I need to amend this so you can write sentences for watering and sunlight and image?
                {
                    return textInput;
                }
                else
                {
                    Console.WriteLine("Invalid input! Please enter only letters.");
                    textInput = Console.ReadLine() ?? string.Empty;
                }
            }
        }

        // Function to check if the input is a valid Id
        public static int CheckIfId(string? input)
        {
            string keyInput = input ?? string.Empty;
            while (true)
            {
                if (int.TryParse(keyInput, out int studentId))
                {
                    if (studentId >= 0)
                    {
                        return studentId;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input! Please enter a valid Id.");
                        keyInput = Console.ReadLine() ?? string.Empty;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input! Please enter a valid Id.");
                    keyInput = Console.ReadLine() ?? string.Empty;
                }
            }
        }

        // Load File stuff
        private void LoadFromFile()
        {
            if (File.Exists(filePath))
            {
                string jsonData = File.ReadAllText(filePath);
                flowers = JsonSerializer.Deserialize<List<Flower>>(jsonData) ?? new List<Flower>();

                if (flowers.Count > 0)
                {
                    nextId = flowers.Max(f => f.Id) + 1;
                }
            }
        }

        private void SaveToFile()
        {
            string jsonData = JsonSerializer.Serialize(
                flowers,
                new JsonSerializerOptions { WriteIndented = true }
            );
            File.WriteAllText(filePath, jsonData);
        }
    }
}
