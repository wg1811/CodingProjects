using System;
using System.Collections.Generic;
using System.IO;

namespace WeatherHistoryApp
{
    class WeatherData
    {
        public int Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public Dictionary<string, List<double>> Hourly { get; set; } = new();
        public Dictionary<string, double> Daily { get; set; } = new();

        // Empty constructor. Will make GetWeather()
        public WeatherData() { }
    }

    class WeatherService
    {
        public async Task<string?> GetHistoricalWeather(
            double lat,
            double lng,
            string startDate,
            string endDate
        )
        {
            try
            {
                using var client = new HttpClient();
                var WeatherUrl =
                    $"https://archive-api.open-meteo.com/v1/archive?latitude={lat}&longitude={lng}&daily=weather_code,temperature_2m_max,temperature_2m_min&timezone=auto&start_date={startDate}&end_date={endDate}";
                var response = await client.GetAsync(WeatherUrl);

                Console.WriteLine($"Is this not working... Status code: {response.StatusCode}");

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    Console.WriteLine(
                        $"Nope. Didn't work to get weather apis. Status code: {response.StatusCode}"
                    );
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
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
