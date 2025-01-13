using WeatherSim;
using System;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

internal class Program
{
    static void Main(string[] args)
    {
        Weather weather = new Weather();

        // Console.WriteLine(weather.getTemp();
        // Console.WriteLine(weather.getPrecipitation());
        // Console.WriteLine(weather.getExtremeWeather());
        // Console.WriteLine(weather.getCloudy());

        weather.generateWeather();

        // Extreme weather warning
        if (weather.Temp < -10 && weather.Precipitation == "snow")
        {
            Console.WriteLine($@"
            ************************
            Extreme Weather Warning!
            Do Not Go Outside
            ************************");
        }

        if (weather.Deadly != "none") 
        {
            Console.WriteLine($@"
            ************************
            Extreme Weather Warning!
            Evacuate Now or
            Seek Appropriate Shelter
            ************************");
        }
    }
}
