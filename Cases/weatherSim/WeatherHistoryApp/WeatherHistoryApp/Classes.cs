using System;
using System.Collections.Generic;
using System.IO;
using OpenMeteo;

namespace WeatherHistoryApp
{
    class WeatherData
    {
        public int Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public Dictionary<string, List<double>> Hourly { get; set; }
        public Dictionary<string, double> Daily { get; set; }

        // Empty constructor. Will make GetWeather()
        public WeatherData() { }
    }
        
    class WeatherService
    {
        private readonly OpenMeteoClient _client = new OpenMeteoClient();

        public async Task<WeatherData?> GetHistoricalWeather(double lat, double long, string date)
        {
            var request = new WeatherForcastRequest
            {
                Latitude = lat,
                Longitute = long,
                StartDate = DateTime.Parse(date),
                EndDate = DateTime.Parse(date),
                Hourly = new[]
                {
                     "temperature_2m", "relative_humidity_2m", "dewpoint_2m", "apparent_temperature",
            "precipitation", "rain", "snowfall", "snow_depth", "weathercode", "cloudcover",
            "cloudcover_low", "cloudcover_mid", "cloudcover_high", "windspeed_10m",
            "windspeed_80m", "windspeed_120m", "windspeed_180m", "winddirection_10m",
            "winddirection_80m", "winddirection_120m", "winddirection_180m", "shortwave_radiation",
            "direct_radiation", "direct_normal_irradiance", "diffuse_radiation", "vapour_pressure_deficit",
            "surface_pressure", "evapotranspiration", "soil_temperature_0cm", "soil_moisture_0_1cm"
                },

        Daily = new[]
        {
            "temperature_2m_max", "temperature_2m_min", "apparent_temperature_max", "apparent_temperature_min",
            "precipitation_sum", "rain_sum", "snowfall_sum", "precipitation_hours", "windspeed_10m_max",
            "windgusts_10m_max", "winddirection_10m_dominant", "shortwave_radiation_sum", "weathercode"
        },
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
