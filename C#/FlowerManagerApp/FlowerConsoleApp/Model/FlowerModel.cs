namespace FlowerConsoleApp
{
    public class Flower
    {
        public int Id { get; set; } = 0;
        public string FlowerCommonName { get; set; } = "";
        public string FlowerLatinName { get; set; } = "";
        public string FlowerWatering { get; set; } = "";
        public string FlowerSunlight { get; set; } = "";
        public string FlowerImage { get; set; } = ""; // There is probably an image class I should use or make?
    }
}
