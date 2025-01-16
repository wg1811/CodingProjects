document.addEventListener('DOMContentLoaded', () => {
    document.getElementById('convertFtoCButton').addEventListener('click', async (event) => {
        event.preventDefault();
        const numToConvert = document.getElementById("numToConvert").value;
        
        try {
            const response = await fetch('http://localhost:5261/tempconverter', {
                method: 'POST',
                headers: { "Content-Type" : "application/json"},
                body: JSON.stringify({
                        temp: parseFloat(numToConvert)
                })
            });

            if (!response.ok) {
                throw new Error("fetch failed")
            }

            const data = await response.json();
            document.getElementById('convertFtoCResult').innerText = `${data}°C`;

        } catch(error) {
            console.error("Error",error);
            document.getElementById('convertFtoCResult').innerText = "An error occurred.";
        }
    }); 
});