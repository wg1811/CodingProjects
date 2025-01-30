// Making map
let myCanvas;
let ctx;

async function initMap(lat = 40.7128, lng = -74.0060)
{
    try {
        const { Map } = await google.maps.importLibrary("maps");
        const { AdvancedMarkerElement } = await google.maps.importLibrary("marker");
        
        const location = { 
            lat: Number(lat), 
            lng: Number(lng) 
        };

        const map = new Map(document.getElementById("map"), {
            zoom: 10,
            center: location,
            mapId: "2a605d50bf27c0dc"
        });
        
        new AdvancedMarkerElement({
            position: location,
            map: map,
        });
    } catch (error) {
        console.error("Error initializing map:", error);
    }
}

// Fetch coordinates from the backend and initialize the map
async function fetchAndShowMap(address) {
    try {
        const response = await fetch(`http://localhost:5236/api/getCoordinates?address=${encodeURIComponent(address)}`);
        const data = await response.json();

        if (data.lat && data.lng) {
            await initMap(data.lat, data.lng);
        } else {
            console.error("Coordinates not found");
        }
    } catch (error) {
        console.error("Error fetching coordinates:", error);
    }
}
// Event listener for the address button
document.getElementById("getAddressButton").addEventListener("click", async () => {
    const address = document.getElementById("address").value;
    await fetchAndShowMap(address);
});

// Making canvas
window.onload = async function () {
try{
    await loadGoogleMaps();
    console.log("Google Maps loaded", google); 
    console.log("Google Maps loaded", window.google);  // Check if google is defined

    initMap();


    myCanvas = document.getElementById("myCanvas");

    ctx = myCanvas.getContext("2d");
    if (!ctx) {
        console.error("Could not get 2D context!");
        return;
    }

    resizeCanvas();
    window.addEventListener("resize", resizeCanvas);

    function resizeCanvas() {
        let mapDiv = document.getElementById("map");
        if (!mapDiv) {
            console.error("Map element not found!");
            return;
        }

        myCanvas.width = mapDiv.clientWidth;
        myCanvas.height = mapDiv.clientHeight;
    }

    console.log("Canvas initialized successfully!");
} catch (error) {
    console.error("Error loading Google Maps:", error);
} 
};

// Declaring avariables needed 'globally'.  
let weatherData = [];
let weatherSystem = [];
let dayLength = 50000;
// Want to show individual weather instance data
let currentWeatherId = 0;

document.getElementById("getWeather").addEventListener("click", async () => {
    try{
    await fetchWeather();
    animate();
    updateWeatherDisplay();
    } catch (error) {
        console.error("Error during initialization", error);
    }
});

setInterval(fetchWeather, dayLength); 




//Functions
function animate() {
    ctx.clearRect(0, 0, myCanvas.width, myCanvas.height);

    weatherSystem.forEach((shape) => {
        shape.move(ctx);
        shape.draw(ctx);
    });
    requestAnimationFrame(animate);
}

async function fetchWeather() {
    const response = await fetch("http://localhost:5236/api/getweathersystem");
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
        if(this.id === currentWeatherId) {
            updateCurrentPosition(this);
        }
    }
}