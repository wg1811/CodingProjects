// Making canvas
const size = 700;
const myCanvas = document.getElementById("myCanvas");
const worldMap = new Image();
worldMap.src = "./ShadedWorldMap.png";

const ctx = myCanvas.getContext("2d");

worldMap.onload = function () {
    const aspectRatio = worldMap.width / worldMap.height;
    console.log(worldMap.width + " is the width. " + worldMap.height + " is the height.");
    const canvasWidth = size;
    const canvasHeight = canvasWidth / aspectRatio;
    myCanvas.width = canvasWidth;
    myCanvas.height = canvasHeight;
    ctx.drawImage(worldMap, 0, 0, myCanvas.width, myCanvas.height);
    animate();
}

  

document.getElementById("startButton").addEventListener("click", fetchWeather);

let weatherSystem = [];

setInterval(fetchWeather, 20000); 



//Functions
function animate() {
    ctx.clearRect(0, 0, myCanvas.width, myCanvas.height);
    ctx.drawImage(worldMap, 0, 0, myCanvas.width, myCanvas.height);

    weatherSystem.forEach((shape) => {
        shape.move(ctx);
        shape.draw(ctx);
    });
    requestAnimationFrame(animate);
}

async function fetchWeather() {
    const response = await fetch("http://localhost:5000/getweathersystem");
    const weatherData = await response.json();
    weatherSystem = weatherData.weatherList.map(data => new WeatherShape(
        data.id,
        data.temp,
        data.atmPressure,
        data.humidity,
        data.windSpeed,
        data.windDirection,
        data.precipitation,
        data.cloudiness,
        data.weatherPosition,
        data.size
    ));
}

// Want to show weather instance data
// let currentWeatherIndex = 0;

// function updateWeatherDisplay() {
//     const weather = weatherData[currentWeatherIndex];
    
//     document.getElementById("temp").textContent = weather.Temp;
//     document.getElementById("atmPressure").textContent = weather.AtmPressure;
//     document.getElementById("humidity").textContent = weather.Humidity;
//     document.getElementById("windSpeed").textContent = weather.WindSpeed;
//     document.getElementById("windDirection").textContent = weather.WindDirection;
//     document.getElementById("precipitation").textContent = weather.Precipitation;
//     document.getElementById("cloudiness").textContent = weather.Cloudiness;
//     document.getElementById("position").textContent = `${weather.WeatherPosition.latitude}, ${weather.WeatherPosition.longitude}`;
// }

// document.getElementById("nextWeather").addEventListener("click", () => {
//     currentWeatherIndex = (currentWeatherIndex + 1) % weatherData.length; // Cycle through the weather instances
//     updateWeatherDisplay();
// });

// // Initialize weather details on page load
// updateWeatherDisplay();


// The weather will be drawn as a circle of a particular radius, color and speed
class WeatherShape {
    constructor(id, temp, atmPressure, humidity, windSpeed, windDirection, precipitation, cloudiness, weatherPosition, size) {
        this.id = id;
        this.temp = temp;
        this.pressure = atmPressure;
        this.humidity = humidity;
        this.windSpeed = windSpeed;
        this.windDirection = windDirection;
        this.precipitation = precipitation;
        this.cloudiness = cloudiness;
        this.weatherPosition = weatherPosition;
        this.size = size;
        this.color = `rgba(${Math.floor(Math.min(temp + 90, 255))}, ${Math.floor(cloudiness * 2.55)}, ${Math.floor(precipitation / 8)}, ${Math.max((1 - windSpeed / 100), 0.4)})`;
    }

    draw(ctx) {
        ctx.fillStyle = this.color;
        ctx.beginPath();
        ctx.arc(this.weatherPosition.x, this.weatherPosition.y, this.size, 0, Math.PI * 2);
        ctx.fill();
        ctx.closePath();
    }

    move(ctx) {
        if (this.weatherPosition && this.weatherPosition.x !== undefined && this.weatherPosition.y !== undefined) {
            this.weatherPosition.x += Math.sin(this.windDirection * (Math.PI / 180)) * (this.windSpeed / 100);
            this.weatherPosition.y -= Math.cos(this.windDirection * (Math.PI / 180)) * (this.windSpeed / 100);
            // This is to wrap around the canvas, but I don't think I want to do a global map yet?
            // if (this.weatherPosition.x > ctx.canvas.width) this.weatherPosition.x = 0;
            // if (this.weatherPosition.y > ctx.canvas.height) this.weatherPosition.y = 0;
            // if (this.weatherPosition.x < 0) this.weatherPosition.x = ctx.canvas.width;
            // if (this.weatherPosition.y < 0) this.weatherPosition.y = ctx.canvas.height;
        } else {
            console.error("Position is undefined or missing x/y:", this.position);
        }
    }
}