namespace WeatherSim
{
    // Need to make a weather class with three parameters; temp, conditions and cloudiness
    class Weather
    {
        public double Temp { get; set; } = 0;
        public string Precipitation { get; set;} = "";
        public double Cloudy { get; set; } = 0;
        public string Deadly { get; set; } = "none";

        // Declare the Random instance
        private Random random = new Random();
    
        // Constructor
        public Weather()
        {

        }

        public int getTemp() {
            return random.Next(-40, 50);
        }

        public string getPrecipitation() {
            string[] precipitationTypes = ["rain", "sleet", "snow", "hail", "fog", "drizzle", "heavy rain"];
            int precType = random.Next(0, precipitationTypes.Length - 1);
            this.Precipitation = precipitationTypes[precType];
            int chancePrep = random.Next(0, 101);
            if (this.Temp < -1 && chancePrep > 40) {
                this.Precipitation = "snow";
                return this.Precipitation;
                } else if (chancePrep > 40) {
                    return this.Precipitation;
                    } else return "sunny";
        }

        public string getDeadlyWeather() {
            string[] deadlyWeather = ["tornado", "hurricane", "wild fires", "tsunami"];
            int badWeatherChance = random.Next(0, 101);
            int badWeatherType = random.Next(0,4);
            if (badWeatherChance < 6) {
                this.Deadly = deadlyWeather[badWeatherType];
            }
            return this.Deadly;
        }

        public int getCloudy() {
            int chanceCloudy = random.Next(0, 101);
            if (chanceCloudy > 21) {
                return 0;
            } else return random.Next(21, 101);
        }

        public void generateWeather() {
            this.Temp = getTemp();
            this.Precipitation = getPrecipitation();
            this.Cloudy = getCloudy();
            string reallyBadWeather = getDeadlyWeather();

            // Display weather
            Console.WriteLine($@"
                ____________________________
                Today's Weather:
                Temperature: {this.Temp}
                Conditions: {this.Precipitation}
                Cloudiness: {this.Cloudy}%
                Extreme Weather: {reallyBadWeather}
                ___________________________");
        }
    }

}


