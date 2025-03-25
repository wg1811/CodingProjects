// Store our data globally to avoid repeated fetches
let universityData = [];
let rankingChart;
let distributionChart;

// Fetch data when the page loads
document.addEventListener("DOMContentLoaded", function () {
  // Fetch the university data from our Express endpoint
  fetch("/university/getuniversities")
    .then((response) => {
      if (!response.ok) {
        throw new Error("Network response was not ok: " + response.statusText);
      }
      return response.json();
    })
    .then((data) => {
      // Store the data and initialize charts
      universityData = data;

      // Populate year filter with available years
      populateYearFilter(data);

      // Populate country filter with available countries
      populateCountryFilter(data);

      // Create initial charts
      createRankingChart();
      createCountryDistributionChart();

      // Set up event listeners for filters
      document
        .getElementById("yearFilter")
        .addEventListener("change", updateCharts);
      document
        .getElementById("metricSelect")
        .addEventListener("change", updateCharts);
      document
        .getElementById("countLimit")
        .addEventListener("change", updateCharts);
      document
        .getElementById("countryFilter")
        .addEventListener("change", updateCharts);
    })
    .catch((error) => {
      console.error("Error fetching university data:", error);
      alert(
        "Failed to load university data. Please check the console for details."
      );
    });
});

function getFilteredBackend(thingy) {
  // post to backend
  // return response
}

// Helper function to populate the year filter dropdown
function populateYearFilter(data) {
  const yearFilter = document.getElementById("yearFilter");

  // Extract unique years from the data
  const years = [...new Set(data.map((uni) => uni.year))].sort();

  // Add each year as an option
  years.forEach((year) => {
    const option = document.createElement("option");
    option.value = year;
    option.textContent = year;
    yearFilter.appendChild(option);
  });
}

// Helper function to populate country filter
function populateCountryFilter(data) {
  const countryFilter = document.getElementById("countryFilter");
  const countries = [...new Set(data.map((uni) => uni.country))]
    .filter((country) => country)
    .sort();

  countries.forEach((country) => {
    const option = document.createElement("option");
    option.value = country;
    option.textContent = country;
    countryFilter.appendChild(option);
  });
}

// Function to filter data based on selected filters
function getFilteredData() {
  const yearFilter = document.getElementById("yearFilter").value;
  const limit = parseInt(document.getElementById("countLimit").value);
  const countryFilter = document.getElementById("countryFilter").value;

  // Apply year filter if not "all"
  let filtered = universityData;
  if (yearFilter !== "all") {
    filtered = filtered.filter((uni) => uni.year === yearFilter);
  }
  // Apply country fileter if not "all"
  if (countryFilter !== "all") {
    filtered = filtered.filter((uni) => uni.country === countryFilter);
  }

  // Sort by world rank (numeric sort)
  filtered = filtered.sort(
    (a, b) => parseInt(a.world_rank) - parseInt(b.world_rank)
  );

  // Limit to the selected number of universities
  return filtered.slice(0, limit);
}

// Function to create the main ranking chart
function createRankingChart() {
  const filteredData = getFilteredData();
  const metric = document.getElementById("metricSelect").value;
  const metricDisplay =
    document.getElementById("metricSelect").options[
      document.getElementById("metricSelect").selectedIndex
    ].text;

  // Extract data for the chart
  const labels = filteredData.map((uni) => uni.institution);
  const values = filteredData.map((uni) => parseFloat(uni[metric]) || 0);

  // Set up the canvas context
  const ctx = document.getElementById("rankingChart").getContext("2d");

  // Destroy previous chart if it exists
  if (rankingChart) {
    rankingChart.destroy();
  }

  // Create a new chart
  rankingChart = new Chart(ctx, {
    type: "bar",
    data: {
      labels: labels,
      datasets: [
        {
          label: metricDisplay,
          data: values,
          backgroundColor: "rgba(54, 162, 235, 0.7)",
          borderColor: "rgba(54, 162, 235, 1)",
          borderWidth: 1,
        },
      ],
    },
    options: {
      indexAxis: "y", // Horizontal bar chart for better readability with many institutions
      responsive: true,
      plugins: {
        title: {
          display: true,
          text: `University Rankings by ${metricDisplay}`,
          font: {
            size: 16,
          },
        },
        legend: {
          display: false,
        },
        tooltip: {
          callbacks: {
            label: function (context) {
              const uni = filteredData[context.dataIndex];
              return [
                `${metricDisplay}: ${context.parsed.x}`,
                `World Rank: ${uni.world_rank}`,
                `Country: ${uni.country}`,
              ];
            },
          },
        },
      },
      scales: {
        x: {
          beginAtZero: true,
          title: {
            display: true,
            text: metricDisplay,
          },
        },
        y: {
          title: {
            display: true,
            text: "Institution",
          },
        },
      },
    },
  });
}

// Function to create the country distribution chart
function createCountryDistributionChart() {
  const filteredData = getFilteredData();

  // Count universities by country
  const countryCount = {};
  filteredData.forEach((uni) => {
    if (uni.country) {
      countryCount[uni.country] = (countryCount[uni.country] || 0) + 1;
    }
  });

  // Convert to arrays for Chart.js
  const countries = Object.keys(countryCount);
  const counts = Object.values(countryCount);

  // Generate colors for each country
  const backgroundColors = countries.map(
    (_, i) => `hsl(${i * (360 / countries.length)}, 70%, 60%)`
  );

  // Set up the canvas context
  const ctx = document
    .getElementById("countryDistributionChart")
    .getContext("2d");

  // Destroy previous chart if it exists
  if (distributionChart) {
    distributionChart.destroy();
  }

  // Create a new chart
  distributionChart = new Chart(ctx, {
    type: "pie",
    data: {
      labels: countries,
      datasets: [
        {
          data: counts,
          backgroundColor: backgroundColors,
          borderWidth: 1,
        },
      ],
    },
    options: {
      responsive: true,
      plugins: {
        title: {
          display: true,
          text: "Distribution by Country",
          font: {
            size: 16,
          },
        },
        tooltip: {
          callbacks: {
            label: function (context) {
              const percentage = Math.round(
                (context.parsed * 100) / filteredData.length
              );
              return `${context.label}: ${context.parsed} universities (${percentage}%)`;
            },
          },
        },
      },
    },
  });
}

// Function to update both charts when filters change
function updateCharts() {
  createRankingChart();
  createCountryDistributionChart();
}

// Data test
function getAllDataTest() {
  console.log("Button clicked!");
  console.log("Total universities loaded:", universityData.length);
  console.log("Sample university data:", universityData[0]);
}
