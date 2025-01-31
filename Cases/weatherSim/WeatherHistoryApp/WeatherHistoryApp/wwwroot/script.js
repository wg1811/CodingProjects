// Making map
let myCanvas;
let ctx;

// Map Functions
async function initMap(lat = 40.7128, lng = -74.006) {
  try {
    const { Map } = await google.maps.importLibrary("maps");
    const { AdvancedMarkerElement } = await google.maps.importLibrary("marker");

    const location = {
      lat: Number(lat),
      lng: Number(lng),
    };

    const map = new Map(document.getElementById("map"), {
      zoom: 10,
      center: location,
      mapId: "2a605d50bf27c0dc",
    });

    new AdvancedMarkerElement({
      position: location,
      map: map,
    });
  } catch (error) {
    console.error("Error initializing map:", error);
  }
}

// Fetch Map coordinates and get Weather
// Helper functions to set the default dates to 10 and 3 days ago and format to YYYY-MM-DD
function getFormattedStartDate() {
  const today = new Date();
  const startDate = new Date(today);
  startDate.setDate(today.getDate() - 10); // 10 days ago

  const year = startDate.getFullYear();
  const month = (startDate.getMonth() + 1).toString().padStart(2, "0");
  const day = startDate.getDate().toString().padStart(2, "0");

  console.log(`${year}-${month}-${day} is the start date (10 days ago).`);
  return `${year}-${month}-${day}`;
}

function getFormattedEndDate() {
  const today = new Date();
  const endDate = new Date(today);
  endDate.setDate(today.getDate() - 3); // 3 days ago

  const year = endDate.getFullYear();
  const month = (endDate.getMonth() + 1).toString().padStart(2, "0");
  const day = endDate.getDate().toString().padStart(2, "0");

  console.log(`${year}-${month}-${day} is the end date (3 days ago).`);
  return `${year}-${month}-${day}`;
}
//
async function fetchAndShowMapWeather(
  address,
  start_date = getFormattedStartDate(),
  end_date = getFormattedEndDate()
) {
  try {
    const response = await fetch(
      `http://localhost:5000/api/getCoordinates?address=${encodeURIComponent(
        address
      )}`
    );
    const data = await response.json();
    const lat = data.lat;
    const long = data.long;

    if (lat && long) {
      await initMap(lat, long);
    } else {
      console.error("Coordinates not found");
    }
  } catch (error) {
    console.error("Error fetching coordinates:", error);
  }
  fetchWeather(lat, long, start_date, end_date);
}

// Start of Map stuff
// Event listener for the address button
document
  .getElementById("getAddressButton")
  .addEventListener("click", async () => {
    const address = document.getElementById("address").value;
    await fetchAndShowMapWeather(address);
  });

// Loading map and canvas
window.onload = async function () {
  try {
    await loadGoogleMaps();
    console.log("Google Maps loaded", google);
    console.log("Google Maps loaded", window.google); // Check if google is defined

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
// End of onload

// Start of Weather functions
document
  .getElementById("getWeatherButton")
  .addEventListener("click", async () => {
    try {
      await fetchWeather(); //  I have to create fields for this and change to submit, I think?
      //animate(); Not ready for this step yet.
      //updateWeatherDisplay(); Not updated with new structure
    } catch (error) {
      console.error("Error during initialization", error);
    }
  });

// Calling this to get the weather in fetchAndShowMap
async function fetchWeather(lat, long, start_date, end_date) {
  const response = await fetch(
    `http://localhost:5000/api/getweather?lat=${lat}&lon=${long}&start_date=${start_date}&end_date=${end_date}`
  );
  const data = await response.json();
  console.log("This is the Data: " + JSON.stringify(data, null, 2));
}
//console.log(JSON.stringify(weatherSystem, null, 2) + " is System. \nThis is the Data: " + JSON.stringify(weatherData, null, 2));

function updateWeatherDisplay() {
  //console.log("weatherData is a " + typeof(weatherData) + "weatherData:", JSON.stringify(weatherData, null, 2));
  const weather = weatherData.weatherList.find(
    (item) => item.id === currentWeatherId
  );
  document.getElementById("temp").textContent = weather.temp;
  document.getElementById("atmPressure").textContent = weather.atmPressure;
  document.getElementById("humidity").textContent = weather.humidity;
  document.getElementById("windSpeed").textContent = weather.windSpeed;
  document.getElementById("windDirection").textContent = weather.windDirection;
  document.getElementById("precipitation").textContent = weather.precipitation;
  document.getElementById("cloudiness").textContent = weather.cloudiness;
  document.getElementById(
    "position"
  ).textContent = `${weather.weatherPosition.lat}, ${weather.weatherPosition.long}`;
  document.getElementById("size").textContent = weather.size;
  hightlightCurrent(weather);
}

// The weather will be drawn as a circle of a particular radius, color and speed
class WeatherShape {
  constructor(
    id,
    temp,
    atmPressure,
    humidity,
    windSpeed,
    windDirection,
    precipitation,
    cloudiness,
    weatherPosition,
    size
  ) {
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
    this.color = `rgba(${Math.floor(Math.min(temp + 90, 255))}, ${Math.floor(
      cloudiness * 2.55
    )}, ${Math.floor(precipitation / 8)}, ${Math.max(
      1 - windSpeed / 100,
      0.4
    )})`;
  }
}
