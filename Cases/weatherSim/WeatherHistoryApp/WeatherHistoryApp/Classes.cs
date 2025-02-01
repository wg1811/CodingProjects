using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace WeatherHistoryApp
{
    public class WeatherResponse
    {
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string Timezone { get; set; } = string.Empty;
        public double? Elevation { get; set; }
        public double? Utc_Offset_Seconds { get; set; }
        public double? GenerationTime_Ms { get; set; }
        public string StartDate { get; set; } = string.Empty;
        public string EndDate { get; set; } = string.Empty;
        public DailyUnits? DailyUnits { get; set; }
        public HourlyUnits? HourlyUnits { get; set; }
        public DailyData? Daily { get; set; }
        public HourlyData? Hourly { get; set; }
    }

    public class DailyData
    {
        public required string?[] Time { get; set; }
        public required int?[] Weather_Code { get; set; }
        public required double?[] Temperature_2m_Max { get; set; }
        public required double?[] Temperature_2m_Min { get; set; }
        public required double?[] Apparent_Temperature_Max { get; set; }
        public required double?[] Apparent_Temperature_Min { get; set; }
        public required double?[] Precipitation_Sum { get; set; }
        public required double?[] Precipitation_Hours { get; set; }
        public required double?[] Pressure_Msl_Mean { get; set; }
        public required string?[] Sunrise { get; set; }
        public required string?[] Sunset { get; set; }
        public required double?[] Wind_Speed_10m_Max { get; set; }
        public required double?[] Wind_Gusts_10m_Max { get; set; }
        public required int?[] Wind_Direction_10m_Dominant { get; set; }
        public required double?[] Shortwave_Radiation_Sum { get; set; }
        public required double?[] Et0_Fao_Evapotranspiration { get; set; }
    }

    public class HourlyData
    {
        public required string?[] Time { get; set; }
        public required double?[] Temperature_2m { get; set; }
        public required int?[] Weather_Code { get; set; }
        public required double?[] Relative_Humidity_2m { get; set; }
        public required double?[] Dew_Point_2m { get; set; }
        public required double?[] Apparent_Temperature { get; set; }
        public required double?[] Precipitation { get; set; }
        public required double?[] Pressure_Msl { get; set; }
        public required double?[] Cloud_Cover { get; set; }
        public required double?[] Visibility { get; set; }
        public required double?[] Wind_Speed_10m { get; set; }
        public required int?[] Wind_Direction_10m { get; set; }
        public required double?[] Wind_Gusts_10m { get; set; }
        public required double?[] Soil_Temperature_0cm { get; set; }
        public required double?[] Soil_Moisture_0_1cm { get; set; }
    }

    public class DailyUnits
    {
        public string Weather_Code { get; set; } = string.Empty;
        public string Temperature_2m_Max { get; set; } = "°C";
        public string Temperature_2m_Min { get; set; } = "°C";
        public string Apparent_Temperature_Max { get; set; } = "°C";
        public string Apparent_Temperature_Min { get; set; } = "°C";
        public string Precipitation_Sum { get; set; } = "mm";
        public string Precipitation_Hours { get; set; } = "h";
        public string Pressure_Msl_Mean { get; set; } = "hPa";
        public string Sunrise { get; set; } = "";
        public string Sunset { get; set; } = "";
        public string Wind_Speed_10m_Max { get; set; } = "m/s";
        public string Wind_Gusts_10m_Max { get; set; } = "m/s";
        public string Wind_Direction_10m_Dominant { get; set; } = "°";
        public string Shortwave_Radiation_Sum { get; set; } = "MJ/m²";
        public string Et0_Fao_Evapotranspiration { get; set; } = "mm";
    }

    public class HourlyUnits
    {
        public string Time { get; set; } = "";
        public string Temperature_2m { get; set; } = "°C";
        public string Weather_Code { get; set; } = "";
        public string Relative_Humidity_2m { get; set; } = "%";
        public string Dew_Point_2m { get; set; } = "°C";
        public string Apparent_Temperature { get; set; } = "°C";
        public string Precipitation { get; set; } = "mm";
        public string Pressure_Msl { get; set; } = "hPa";
        public string Cloud_Cover { get; set; } = "%";
        public string Visibility { get; set; } = "m";
        public string Wind_Speed_10m { get; set; } = "m/s";
        public string Wind_Direction_10m { get; set; } = "°";
        public string Wind_Gusts_10m { get; set; } = "m/s";
        public string Soil_Temperature_0cm { get; set; } = "°C";
        public string Soil_Moisture_0_1cm { get; set; } = "m³/m³";
    }

    class WeatherService
    {
        public async Task<string?> TestWeather(
            double lat,
            double lng,
            string startDate,
            string endDate
        )
        {
            try
            {
                using var client = new HttpClient();
                var weatherUrl =
                    $"https://archive-api.open-meteo.com/v1/archive"
                    + $"?latitude={lat}&longitude={lng}"
                    + $"&daily=weather_code,temperature_2m_max,temperature_2m_min,precipitation_sum,pressure_msl_mean,apparent_temperature_max,apparent_temperature_min,precipitation_hours,sunrise,sunset,wind_speed_10m_max,wind_gusts_10m_max,wind_direction_10m_dominant,shortwave_radiation_sum,et0_fao_evapotranspiration"
                    + $"&hourly=temperature_2m,relative_humidity_2m,dew_point_2m,apparent_temperature,precipitation,weather_code,pressure_msl,cloud_cover,visibility,wind_speed_10m,wind_direction_10m,wind_gusts_10m,soil_temperature_0cm,soil_moisture_0_1cm"
                    + $"&timezone=auto"
                    + $"&start_date={startDate}"
                    + $"&end_date={endDate}";

                var response = await client.GetAsync(weatherUrl);

                return response.IsSuccessStatusCode
                    ? await response.Content.ReadAsStringAsync()
                    : null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }

        public async Task<WeatherResponse?> GetHistoricalWeather(
            double lat,
            double lng,
            string startDate,
            string endDate
        )
        {
            try
            {
                using var client = new HttpClient();
                var weatherUrl =
                    $"https://archive-api.open-meteo.com/v1/archive"
                    + $"?latitude={lat}&longitude={lng}"
                    + $"&daily=weather_code,temperature_2m_max,temperature_2m_min,precipitation_sum,pressure_msl_mean,apparent_temperature_max,apparent_temperature_min,precipitation_hours,sunrise,sunset,wind_speed_10m_max,wind_gusts_10m_max,wind_direction_10m_dominant,shortwave_radiation_sum,et0_fao_evapotranspiration"
                    + $"&hourly=temperature_2m,relative_humidity_2m,dew_point_2m,apparent_temperature,precipitation,weather_code,pressure_msl,cloud_cover,visibility,wind_speed_10m,wind_direction_10m,wind_gusts_10m,soil_temperature_0cm,soil_moisture_0_1cm"
                    + $"&timezone=auto"
                    + $"&start_date={startDate:yyyy-MM-dd}"
                    + $"&end_date={endDate:yyyy-MM-dd}";

                var response = await client.GetAsync(weatherUrl);

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine(
                        $"API request failed with status code: {response.StatusCode}"
                    );
                    return null;
                }

                var jsonResponse = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Raw API response: {jsonResponse}"); // Is it working?

                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                var weatherResponse = JsonSerializer.Deserialize<WeatherResponse>(
                    jsonResponse,
                    options
                );

                if (weatherResponse == null)
                {
                    Console.WriteLine("Failed to deserialize weather response");
                    return null;
                }

                return weatherResponse;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }
    }
}
// This is the json response.  I have to map it to the classes
// "{\"latitude\":40.738136,\"longitude\":-74.04254,\"generationtime_ms\":0.2688169479370117,\"utc_offset_seconds\":-18000,\"timezone\":\"America/New_York\",\"timezone_abbreviation\":\"GMT-5\",\"elevation\":32.0,\"daily_units\":{\"time\":\"iso8601\",\"weather_code\":\"wmo code\",\"temperature_2m_max\":\"°C\",\"temperature_2m_min\":\"°C\",\"precipitation_sum\":\"mm\",\"pressure_msl_mean\":\"hPa\"},\"daily\":{\"time\":[\"2010-10-10\",\"2010-10-11\",\"2010-10-12\",\"2010-10-13\",\"2010-10-14\",\"2010-10-15\",\"2010-10-16\",\"2010-10-17\"],\"weather_code\":[0,53,63,0,63,51,51,1],\"temperature_2m_max\":[19.9,23.4,19.0,16.7,16.1,14.0,14.1,19.6],\"temperature_2m_min\":[6.7,11.6,12.7,8.4,10.0,9.3,8.9,7.0],\"precipitation_sum\":[0.00,1.20,12.90,0.00,11.20,1.10,0.10,0.00],\"pressure_msl_mean\":[1012.1,1008.6,1009.2,1016.3,1011.2,999.6,1008.0,1011.7]}}"
