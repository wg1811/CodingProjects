// Making canvas
const size = 700;
const myCanvas = document.getElementById("myCanvas");

// Map image not necessary cuz vectors.
//const worldMap = new Image();
//worldMap.src = "./ShadedWorldMap.png";

const ctx = myCanvas.getContext("2d");
const map = L.map('map').setView([51.505, -0.09], 13); // Example coordinates and zoom level

// Declaring variables needed 'globally'.  
let weatherData = [];
let fileData = [];
let weatherSystem = [];
let dayLength = 50000;
// Want to show individual weather instance data
let currentWeatherId = 0;

// Get all GeoJSON files into an array
const allGeoJSON = loadGeoJSONFiles();

console.log(allGeoJSON);


  // Not using map image.  Need to figure out how to load vector map
// worldMap.onload = function () {
//     const aspectRatio = worldMap.width / worldMap.height;
//     console.log(worldMap.width + " is the width. " + worldMap.height + " is the height.");
//     const canvasWidth = size;
//     const canvasHeight = canvasWidth / aspectRatio;
//     myCanvas.width = canvasWidth;
//     myCanvas.height = canvasHeight;
//     ctx.drawImage(worldMap, 0, 0, myCanvas.width, myCanvas.height);
//     animate();
// }

  

document.getElementById("startButton").addEventListener("click", async () => {
    await fetchWeather().catch(console.error);
    await loadGeoJSONFiles().catch(console.error);
    showGeoJSONFiles(fileData);
    updateWeatherDisplay();
});

setInterval(fetchWeather, dayLength); 




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
    weatherData = await response.json();
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
        data.size,
    ));
    //console.log(JSON.stringify(weatherSystem, null, 2) + " is System. \nThis is the Data: " + JSON.stringify(weatherData, null, 2));
}

async function loadGeoJSONFiles() {
    const response = await fetch("http://localhost:5000/getgeofiles");
    fileData = await response.json();
    console.log("___________________________" + JSON.stringify(fileData, null, 2));
    return fileData;
}

function showGeoJSONFiles(filePaths) {
    filePaths.forEach(path => {
      fetch(path)
        .then(response => response.json())
        .then(data => {
          // Add each GeoJSON file as a layer to the map
          L.geoJSON(data).addTo(map);
        })
        .catch(error => console.error('Error loading GeoJSON:', path, error));
    });
  }

function updateWeatherDisplay() {
    //console.log("weatherData is a " + typeof(weatherData) + "weatherData:", JSON.stringify(weatherData, null, 2));
    const weather = weatherData.weatherList.find(item => item.id === currentWeatherId);
    console.log("currentWeatherId = " + currentWeatherId + "\n..." + JSON.stringify(weather, null, 2));
    document.getElementById("temp").textContent = weather.temp;
    document.getElementById("atmPressure").textContent = weather.atmPressure;
    document.getElementById("humidity").textContent = weather.humidity;
    document.getElementById("windSpeed").textContent = weather.windSpeed;
    document.getElementById("windDirection").textContent = weather.windDirection;
    document.getElementById("precipitation").textContent = weather.precipitation;
    document.getElementById("cloudiness").textContent = weather.cloudiness;
    document.getElementById("position").textContent = `${weather.weatherPosition.lat}, ${weather.weatherPosition.long}`;
    document.getElementById("size").textContent = weather.size;
    hightlightCurrent(weather);
}

    document.getElementById("nextWeather").addEventListener("click", () => {
    currentWeatherId = (currentWeatherId + 1) % weatherSystem.length; 
    updateWeatherDisplay();
});

// I want the current weather displayed to be highlighted
function hightlightCurrent(weather) {
    weather.color = `rgba(43, 9, 194, 0.93)`;
    //console.log(weather.color + " is the highlighted color");
}

function updateCurrentPosition(weather) {
    document.getElementById("position").textContent = `${weather.weatherPosition.lat.toFixed(2)}, ${weather.weatherPosition.long.toFixed(2)}`;
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
        this.color = `rgba(${Math.floor(Math.min(temp + 90, 255))}, ${Math.floor(cloudiness * 2.55)}, ${Math.floor(precipitation / 8)}, ${Math.max((1 - windSpeed / 100), 0.4)})`;
    }

    draw(ctx) {
        ctx.fillStyle = this.id === currentWeatherId ? 'rgba(43, 9, 194, 0.93)' : this.color;
        ctx.beginPath();
        ctx.arc(this.weatherPosition.lat, this.weatherPosition.long, this.size, 0, Math.PI * 2);
        ctx.fill();
        ctx.closePath();
    }

    move(ctx) {
        if (this.weatherPosition && this.weatherPosition.lat !== undefined && this.weatherPosition.long !== undefined) {
            this.weatherPosition.lat += Math.sin(this.windDirection * (Math.PI / 180)) * (this.windSpeed / 100);
            this.weatherPosition.long -= Math.cos(this.windDirection * (Math.PI / 180)) * (this.windSpeed / 100);
            //This is to wrap around the canvas for a global map
            if (this.weatherPosition.lat > ctx.canvas.width) this.weatherPosition.lat = 0;
            if (this.weatherPosition.long > ctx.canvas.height) this.weatherPosition.long = 0;
            if (this.weatherPosition.lat < 0) this.weatherPosition.lat = ctx.canvas.width;
            if (this.weatherPosition.long < 0) this.weatherPosition.long = ctx.canvas.height;
        } else {
            console.error("Position is undefined or missing x/y:", this.position);
        }
        updateCurrentPosition(this);
    }
}