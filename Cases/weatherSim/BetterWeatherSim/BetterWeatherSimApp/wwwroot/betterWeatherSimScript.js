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
    weatherSystem = weatherData.weatherList.map(data => new WeatherShape(data));
    console.log("______________________" + JSON.stringify(weatherSystem, null, 2));
}




// The weather will be drawn as a circle of a particular radius, color and speed
class WeatherShape {
    constructor(id, temp, pressure, humidity, windSpeed, windDirection, preciptation, cloudiness, weatherPosition, size) {
        this.id = id;
        this.temp = temp;
        this.pressure = pressure;
        this.humidity = humidity;
        this.windSpeed = windSpeed;
        this.windDirection = windDirection;
        this.preciptation = preciptation;
        this.cloudiness = cloudiness;
        this.weatherPosition = weatherPosition;
        this.size = size;
        this.color = `rgba(${Math.min(temp + 90, 255)}, ${cloudiness * 2.55}, ${preciptation / 8}, ${Math.max(1 - windSpeed / 100, 0.4)})`;
    }

    draw(ctx) {
        ctx.beginPath();
        ctx.arc(this.weatherPosition.x, this.weatherPosition.y, this.size, 0, Math.PI * 2);
        ctx.fill(this.color);
        ctx.closePath();
    }

    move(ctx) {
        if (this.position && this.position.x !== undefined && this.position.y !== undefined) {
            this.position.x += Math.sin(this.windDirection * (Math.PI / 180)) * (this.windSpeed / 10);
            this.position.y -= Math.cos(this.windDirection * (Math.PI / 180)) * (this.windSpeed / 10);
            if (this.position.x > ctx.canvas.width) this.position.x = 0;
            if (this.position.y > ctx.canvas.height) this.position.y = 0;
            if (this.position.x < 0) this.position.x = ctx.canvas.width;
            if (this.position.y < 0) this.position.y = ctx.canvas.height;
        } else {
            console.error("Position is undefined or missing x/y:", this.position);
        }
    }
}