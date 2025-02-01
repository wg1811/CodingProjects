// Making map
let myCanvas;
let ctx;

// Map Initialization
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

// Getting Map and Weather Data
async function fetchAndShowMapWeather(
  address,
  start_date = getFormattedStartDate(),
  end_date = getFormattedEndDate()
) {
  let lat;
  let long;
  try {
    const response = await fetch(
      `http://localhost:5127/api/getCoordinates?address=${encodeURIComponent(
        address
      )}`
    );
    const data = await response.json();
    lat = data.lat;
    long = data.lng;
    console.log(`The lat is ${lat} and the long is ${long}`); // Delete once working

    if (lat && long) {
      await initMap(lat, long);
    } else {
      console.error("Coordinates not found");
    }
  } catch (error) {
    console.error("Error fetching coordinates:", error);
  }

  // Getting Weather
  fetchWeather(lat, long, start_date, end_date);
}

// User inputs and submits Explore Weather button
document.getElementById("submitButton").addEventListener("click", async () => {
  const address = document.getElementById("address").value;
  const startDate = document.getElementById("startDate").value;
  const endDate = document.getElementById("endDate").value;

  // Validate inputs
  if (!address || !startDate || !endDate) {
    alert("Please fill in all fields");
    return;
  }

  // Validate date range
  if (new Date(startDate) > new Date(endDate)) {
    alert("Start date must be before or equal to end date");
    return;
  }

  // Disable button during fetch to prevent multiple submissions
  const submitButton = document.getElementById("submitButton");
  submitButton.disabled = true;
  submitButton.textContent = "Loading...";

  try {
    await fetchAndShowMapWeather(address, startDate, endDate);
  } catch (error) {
    console.error("Error fetching weather data:", error);
    alert("Failed to retrieve weather data. Please try again.");
  } finally {
    submitButton.disabled = false;
    submitButton.textContent = "Explore Weather";
  }
});

// Calling this to get the weather in fetchAndShowMap
let currentIndex = 0; // Needed to display first date's data.
let weatherData = null; // Needs to be accessible by prev / next buttons.
async function fetchWeather(lat, lon, start_date, end_date) {
  console.log("About to fetch weather.");
  const response = await fetch(
    `http://localhost:5127/api/getweather?lat=${lat}&lon=${lon}&start_date=${start_date}&end_date=${end_date}`
  );
  weatherData = await response.json();
  console.log("This is the Data: " + JSON.stringify(weatherData, null, 2)); //Confirming the JSON is right.

  // Show the first day's weather.
  if (weatherData && weatherData.daily) {
    console.log("Weather data and .daily are true.");
    updateWeatherDisplay(currentIndex);
  }
}

//  Display weather data under map

function updateWeatherDisplay(index) {
  const dailyData = weatherData.daily;
  console.log("This is the Data: " + JSON.stringify(dailyData, null, 2)); //Confirming the JSON is right.

  if (!dailyData) {
    console.error("Daily weather data is missing");
    return;
  }
  console.log("temperature_2m_max: ", dailyData.temperature_2m_Max);
  console.log(
    "typeof temperature_2m_max: ",
    typeof dailyData.temperature_2m_max
  );
  document.getElementById("maxTemp").textContent =
    weatherData.daily.temperature_2m_Max[index] ?? "N/A";
  document.getElementById("minTemp").textContent =
    dailyData.temperature_2m_Min[index] ?? "N/A";
  document.getElementById("atmPressure").textContent =
    dailyData.pressure_Msl_Mean[index] ?? "N/A";
  document.getElementById("windSpeed").textContent =
    dailyData.wind_Speed_10m_Max[index] ?? "N/A";
  document.getElementById("windDirection").textContent =
    dailyData.wind_Direction_10m_Dominant[index] ?? "N/A";
  document.getElementById("precipitation").textContent =
    dailyData.precipitation_Sum[index] ?? "N/A";
  document.getElementById("weatherCode").textContent =
    dailyData.weather_Code[index] ?? "N/A";
  document.getElementById("position").textContent =
    `${weatherData.latitude}, ${weatherData.longitude}` ?? "N/A";
}

document.getElementById("nextDay").addEventListener("click", function () {
  console.log(weatherData + " is weatherData.");
  console.log(weatherData.daily + " is weatherData.daily.");
  console.log(
    weatherData.daily.time.length + " is weatherData.daily.time.length."
  );

  if (
    weatherData &&
    weatherData.daily &&
    currentIndex < weatherData.daily.time.length - 1
  ) {
    console.log("Next button should work?");
    currentIndex++;
    updateWeatherDisplay(currentIndex);
  }
});

document.getElementById("prevDay").addEventListener("click", function () {
  if (weatherData && weatherData.daily && currentIndex > 0) {
    currentIndex--;
    updateWeatherDisplay(currentIndex);
  }
});
