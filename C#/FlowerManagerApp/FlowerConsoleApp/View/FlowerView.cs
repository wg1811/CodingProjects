namespace FlowerConsoleApp
{
    public class FlowerView
    {
        public void DisplayFlower(Flower flower)
        {
            Console.WriteLine(new string('_', 30));
            Console.WriteLine(
                $"Flower id: {flower.Id}\nFlower common name: {flower.FlowerCommonName}\nFlower latin name: {flower.FlowerLatinName}\nFlower watering instructions: {flower.FlowerWatering}\nFlower sunlight instructions: {flower.FlowerSunlight}"
            );
            Console.WriteLine(new string('_', 30));
        }

        public void DisplayFlowers(List<Flower> flowers)
        {
            Console.WriteLine(new string('_', 30));
            foreach (var flower in flowers)
            {
                DisplayFlower(flower);
            }
        }
    }
}
