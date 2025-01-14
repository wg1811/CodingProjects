document.getElementById('convertFtoCForm').addEventListener('click', async (event) => {
    event.preventDefault();
    const numToConvert = event.target;
    const response = await fetch('http://localhost:5047/tempconverter', {
        method: 'POST',
        headers: { "Content-Type" : "application/json"},
        body: JSON.stringify({
                temp: numToConvert
                })
    })
    .then((response) => {
        if (!response.ok) {
            throw new Error("fetch failed")
        }
        return response.json();
    })
    .then((data) => {
        console.log(data)
    })
    .catch((error) => {
        console.error("Error",error);
    })
    document.getElementById('convertFtoCResult').innerText = response.json();
}); 