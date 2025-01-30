function loadGoogleMaps() {
  return new Promise((resolve, reject) => {
    if (window.google && window.google.maps) {
      resolve(window.google.maps);
      return;
    }
    console.log("About to fetch.");
    fetch("/api/getMapsApiKey")
      .then((response) => response.json())
      .then((data) => {
        const apiKey = data.apiKey;
        if (!apiKey) {
          reject(new Error("Google Maps API Key not found"));
          return;
        }
        const scriptUrl = `https://maps.googleapis.com/maps/api/js?key=${apiKey}&callback=initGoogleMapsCallback&async=1`;

        const script = document.createElement("script");
        script.src = scriptUrl;
        script.async = true;
        script.defer = true;
        console.log(scriptUrl);

        // Create a promise that resolves when Google Maps is fully loaded
        window.initGoogleMapsCallback = () => {
          resolve(window.google.maps);
          console.log("Resovling Google Maps");
        };

        // Add callback to check when API is loaded
        script.onload = () => {
          if (window.google && window.google.maps) {
            console.log("Google Maps API loaded successfully.");
            resolve(window.google.maps);
          } else {
            // If not immediately available, wait for callback
            window.initGoogleMapsCallback = () => {
              console.warn(
                "Google Maps not immediately available, waiting for callback."
              );
              resolve(window.google.maps);
            };
          }
        };

        script.onerror = () => {
          reject(new Error("Failed to load Google Maps API"));
        };
        document.head.appendChild(script);
      })
      .catch((error) => reject(new Error("Error fetching API key: " + error)));
  });
}
