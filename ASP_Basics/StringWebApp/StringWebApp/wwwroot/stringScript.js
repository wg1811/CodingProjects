document.getElementById('reverseTextForm').addEventListener('submit', async (event) => {
    event.preventDefault();
    const formData = new FormData(event.target);
    const response = await fetch('http://localhost:5000/reversetext', {
      method: 'POST',
      body: formData,
    });
    const result = await response.text();
    document.getElementById('reverseTextResult').innerText = result;
  });

  // Function to handle the Replace Character form
  document.getElementById('replaceCharForm').addEventListener('submit', async (event) => {
    event.preventDefault();
    const formData = new FormData(event.target);
    const response = await fetch('http://localhost:5000/replacechar', {
      method: 'POST',
      body: formData,
    });
    const result = await response.text();
    document.getElementById('replaceCharResult').innerText = result;
  });

  