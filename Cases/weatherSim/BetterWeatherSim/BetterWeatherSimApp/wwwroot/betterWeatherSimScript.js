// Making canvas
const size = 700;
let canvasHeight = 1;
const myCanvas = document.getElementById("myCanvas");
myCanvas.width = size;
myCanvas.height = size / canvasHeight;

const ctx = myCanvas.getContext("2d");

document.getElementById("startButton").addEventListener("click", fetchWeather);

let weatherSystem = [];

animate();


//Functions
function animate() {
    ctx.clearRect(0, 0, size, size);
    weatherSystem.forEach((shape) => {
        shape.move(ctx);
        shape.draw(ctx);
    });
    requestAnimationFrame(animate);
}

async function fetchWeather() {
    const response = await fetch("http://localhost:5000/getweathersystem");
    const weatherData = await response.json();
    console.log(JSON.stringify(weatherData, null, 2));
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
    console.log("______________________" + JSON.stringify(weatherSystem, null, 2));
}




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
        this.color = `rgba(${Math.floor(Math.min(temp + 90, 255))}, ${Math.floor(cloudiness * 2.55)}, ${Math.floor(precipitation / 8)}, ${Math.floor(Math.max(1 - windSpeed / 100), 0.4)})`;
    }

    draw(ctx) {
        ctx.beginPath();
        ctx.arc(this.weatherPosition.x, this.weatherPosition.y, this.size, 0, Math.PI * 2);
        console.log("Draw color:  " + this.color);
        ctx.fillStyle = this.color;
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