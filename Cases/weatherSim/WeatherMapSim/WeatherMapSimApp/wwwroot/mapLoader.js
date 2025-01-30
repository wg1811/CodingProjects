function loadGoogleMaps() {
    return new Promise((resolve, reject) => {
        if (window.google && window.google.maps) {
            resolve(window.google.maps);
            return;
        }
        
        fetch("/api/getMapsApiKey")
            .then(response => response.json())
            .then(data => {
                const apiKey = data.apiKey;
                if (!apiKey) {
                    reject(new Error("Google Maps API Key not found"));
                    return;
                }

                const scriptContent = `
                        (g=>{var h,a,k,p="The Google Maps JavaScript API",c="google",l="importLibrary",q="__ib__",m=document,b=window;b=b[c]||(b[c]={});var d=b.maps||(b.maps={}),r=new Set,e=new URLSearchParams,u=()=>h||(h=new Promise(async(f,n)=>{await (a=m.createElement("script"));e.set("libraries",[...r]+"");for(k in g)e.set(k.replace(/[A-Z]/g,t=>"_"+t[0].toLowerCase()),g[k]);e.set("callback",c+".maps."+q);a.src="https://maps.googleapis.com/maps/api/js?"+e;d[q]=f;a.onerror=()=>h=n(Error(p+" could not load."));a.nonce=m.querySelector("script[nonce]")?.nonce||"";m.head.append(a)}));d[l]?console.warn(p+" only loads once. Ignoring:",g):d[l]=(f,...n)=>r.add(f)&&u().then(()=>d[l](f,...n))})({
                    key: "${apiKey}",
                    v: "weekly"
                });`;

                // Is this injecting properly?
                const script = document.createElement("script");
                script.innerHTML = scriptContent;
                
                // Create a promise that resolves when Google Maps is fully loaded
                window.initGoogleMapsCallback = () => {
                    resolve(window.google.maps);
                };
                
                // Add callback to check when API is loaded
                script.onload = () => {
                    if (window.google && window.google.maps) {
                        console.log("Google Maps API loaded successfully.");
                        resolve(window.google.maps);
                    } else {
                        // If not immediately available, wait for callback
                        window.initGoogleMapsCallback = () => {
                            console.warn("Google Maps not immediately available, waiting for callback.");
                            resolve(window.google.maps);
                        };
                    }
                };
                
                script.onerror = () => {
                    reject(new Error("Failed to load Google Maps API"));
                };
                document.head.appendChild(script);
            })
            .catch(error => reject(new Error("Error fetching API key: " + error)));
    });
}