using System;
using System.Collections.Generic;
using System.IO;

namespace WeatherMapSimApp
{
    class Weather
    {
        public int Id { get; set; }
        public double Temp { get; set; } // -90 to 60 °C
        public double AtmPressure { get; set; } // 870 to 1085 hPa (hectopascals)
        public double Humidity { get; set; } // 0 to 100%
        public double WindSpeed { get; set; } // 0 to 120m/s (record is 113m/s)
        public double WindDirection { get; set; } // 0 to 360°
        public double Precipitation { get; set; } // 0 to 2000mm
        public double Cloudiness { get; set; } // 0 to 100%
        public Position? WeatherPosition { get; set; }
        public double Size { get; set; }

        public class Position
        {
            public double Lat { get; set; }
            public double Long { get; set; }
        }

        // Empty constructor. Will make GetWeather()
        public Weather() { }

        // Initialize Random
        static Random random = new Random();

        // Creating Weather
        public Weather GetWeather(int i) // Do I want to return weather (return this;) so I can chain methods?
        {
            this.Id = i;
            double minTemp = -90;
            double regMinTemp = -10;
            double maxTemp = 60;
            double regMaxTemp = 30;
            double extremeRandom = random.NextDouble();

            // Weighted temp so usually not insane.
            if (extremeRandom > .8)
            {
                this.Temp = Math.Round(regMinTemp + extremeRandom * (regMaxTemp - regMinTemp), 2);
            }
            else
            {
                this.Temp = Math.Round(minTemp + extremeRandom * (maxTemp - minTemp), 2);
            }
            double minAtm = 870;
            double maxAtm = 1085;
            this.AtmPressure = Math.Round(minAtm + random.NextDouble() * (maxAtm - minAtm), 2);
            this.Humidity = random.Next(101);

            // Weighted windspeed
            this.WindSpeed = (extremeRandom >= .8) ? random.Next(51, 121) : random.Next(51);
            this.WindDirection = random.Next(360);
            this.Cloudiness = random.Next(101);

            //Weighted precipitation connected to cloudiness
            if (this.Cloudiness > 30)
            {
                this.Precipitation =
                    (extremeRandom >= .8) ? random.Next(150, 2001) : random.Next(201);
            }
            this.WeatherPosition = GetPosition();
            this.Size = (extremeRandom >= .8) ? random.Next(80, 300) : random.Next(10, 80);
            return this;
        }

        // Should get canvas size from client side. Make endpoint and send via js?
        public static Position GetPosition()
        {
            Position position = new Position();
            position.Lat = random.Next(701);
            position.Long = random.Next(701);
            return position;
        }

        public override string ToString()
        {
            return $"Id: {this.Id}, Temp: {this.Temp}, AtmPressure: {this.AtmPressure}, Humidity: {this.Humidity}, \n"
                + $"WindSpeed: {this.WindSpeed}, WindDirection: {this.WindDirection}, Precipitation: {this.Precipitation}, \n"
                + $"Cloudiness: {this.Cloudiness}, Position: ({this.WeatherPosition?.Lat}, {this.WeatherPosition?.Long}), Size: {this.Size}";
        }
    }

    class WeatherSystem
    {
        public List<Weather>? WeatherList { get; set; }

        public WeatherSystem()
        {
            WeatherList = new List<Weather>();
        }

        public WeatherSystem CreateWeatherSystem()
        {
            int numWeather = 10;
            for (int i = 0; i < numWeather; i++)
            {
                Weather weather = new Weather();
                weather.GetWeather(i);
                weather.ToString();
                this.WeatherList?.Add(weather);
            }
            return this;
        }

        public void ConsoleWeatherList()
        {
            if (this.WeatherList == null || this.WeatherList.Count == 0)
            {
                Console.WriteLine("No weather data available.");
                return;
            }
            foreach (var weather in WeatherList)
            {
                Console.WriteLine(weather.ToString());
            }
        }
    }
}


// Rainfall categories (double check. from Bhutan):
// No rain Rainfall amount realised in a day is 0.0 mm
// Very Light Rain Rainfall amount realised in a day is between 0.1 to 0.9 mm
// Light Rain Rainfall amount realised in a day is between 1.0 mm to 10 mm
// Moderate Rain Rainfall amount realised in a day is between 11 to 30 mm
// Heavy Rain Rainfall amount realised in a day is between 31.0 to 70.0 mm
// Very Heavy Rain Rainfall amount realised in a day is between 71.0 to 150 mm
// Extremely Heavy Rain Rainfall amount realised in a day is equal or more than 151 mm
