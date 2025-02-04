namespace FlowerConsoleApp
{
    internal partial class Program
    {
        public static void Menu()
        {
            string menuText =
                $@"

            ---------MENU----------
            1- Show Flower
            2- Show Flowers
            3- Add Flower 
            4- Update Flower
            5- Delete Flower
            6- Exit Program 
            ------------------------
            ";
            //while loop
            while (true)
            {
                //show menu
                Console.WriteLine(menuText);
                Console.WriteLine("Enter your Choice ");
                string userInput = Console.ReadLine() ?? "0";

                //switch
                switch (userInput)
                {
                    case "1":
                        flowerController.ShowFlower();
                        //Console.WriteLine("needs debug");
                        break;
                    case "2":
                        flowerController.ShowFlowers();
                        break;
                    case "3":
                        flowerController.AddFlower();
                        break;
                    case "4":
                        flowerController.UpdateFlower();
                        //Console.WriteLine("need debug");
                        break;
                    case "5":
                        flowerController.DeleteFlower();
                        break;
                    case "6":
                        Console.WriteLine("Exiting Program..");
                        Thread.Sleep(3000);
                        Console.Clear();

                        return;
                    default:
                        Console.WriteLine("invalid option try again");
                        break;
                }
            }
        }
    }
}
