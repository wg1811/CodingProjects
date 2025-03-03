using System;

namespace SpaceshipConApp
{
    public class Menu
    {
        public void Show()
        {
            Console.WriteLine("1. Create a new spaceship");
            Console.WriteLine("2. Show spaceship status");
            Console.WriteLine("3. Refuel spaceship");
            Console.WriteLine("4. Change spaceship destination");
            Console.WriteLine("5. Board a pilot");
            Console.WriteLine("6. Launch spaceship");
            Console.WriteLine("7. Exit");
        }

        public int GetInput()
        {
            Console.Write("What are your orders, Admiral?");
            int choice = Convert.ToInt32(Console.ReadLine());
            return choice;
        }

        public void ShowError()
        {
            Console.WriteLine("Invalid input. Please try again.");
        }
    }
}
