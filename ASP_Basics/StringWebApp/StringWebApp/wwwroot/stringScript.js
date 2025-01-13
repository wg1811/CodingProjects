document.getElementById('reverseTextForm').addEventListener('submit', async (event) => {
    event.preventDefault();
    const formData = new FormData(event.target);
    const response = await fetch('http://localhost:5159/reversetext', {
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
    const response = await fetch('http://localhost:5159/replacechar', {
      method: 'POST',
      body: formData,
    });
    const result = await response.text();
    document.getElementById('replaceCharResult').innerText = result;
  });

  
  // Function to handle the Replace Character form
  document.getElementById('removeSpacesForm').addEventListener('submit', async (event) => {
    event.preventDefault();
    const formData = new FormData(event.target);
    const response = await fetch('http://localhost:5159/removespaces', {
      method: 'POST',
      body: formData,
    });
    const result = await response.text();
    document.getElementById('removeSpacesResult').innerText = result;
  });
  
  // Function to handle the Replace Character form
  document.getElementById('convertSinTextForm').addEventListener('submit', async (event) => {
    event.preventDefault();
    const formData = new FormData(event.target);
    const response = await fetch('http://localhost:5159/createsinustext', {
      method: 'POST',
      body: formData,
    });
    const result = await response.text();
    document.getElementById('convertSinTextResult').innerText = result;
  });
  
  // Function to handle the Replace Character form
  document.getElementById('replaceCharForm').addEventListener('submit', async (event) => {
    event.preventDefault();
    const formData = new FormData(event.target);
    const response = await fetch('http://localhost:5159/replacechar', {
      method: 'POST',
      body: formData,
    });
    const result = await response.text();
    document.getElementById('replaceCharResult').innerText = result;
  });
  
  // Function to handle the Replace Character form
  document.getElementById('findFirstCharForm').addEventListener('submit', async (event) => {
    event.preventDefault();
    const formData = new FormData(event.target);
    const response = await fetch('http://localhost:5159/findfirstchar', {
      method: 'POST',
      body: formData,
    });
    const result = await response.text();
    document.getElementById('findFirstCharResult').innerText = result;
  });
  
  // Function to handle the Replace Character form
  document.getElementById('findLastCharForm').addEventListener('submit', async (event) => {
    event.preventDefault();
    const formData = new FormData(event.target);
    const response = await fetch('http://localhost:5159/findlastchar', {
      method: 'POST',
      body: formData,
    });
    const result = await response.text();
    document.getElementById('findLastCharResult').innerText = result;
  });

    // Function to handle the Replace Character form
    document.getElementById('findAllCharForm').addEventListener('submit', async (event) => {
    event.preventDefault();
    const formData = new FormData(event.target);
    const response = await fetch('http://localhost:5159/findallchar', {
        method: 'POST',
        body: formData,
    });
    const result = await response.text();
    document.getElementById('findAllCharResult').innerText = result;
    });
  
  // Function to handle the Replace Character form
  document.getElementById('createSubstringForm').addEventListener('submit', async (event) => {
    event.preventDefault();
    const formData = new FormData(event.target);
    const response = await fetch('http://localhost:5159/createsubstring', {
      method: 'POST',
      body: formData,
    });
    const result = await response.text();
    document.getElementById('createSubstringResult').innerText = result;
  });

    // Function to handle the Replace Character form
    document.getElementById('findSubstringForm').addEventListener('submit', async (event) => {
        event.preventDefault();
        const formData = new FormData(event.target);
        const response = await fetch('http://localhost:5159/findsubstring', {
          method: 'POST',
          body: formData,
        });
        const result = await response.text();
        document.getElementById('findSubstringResult').innerText = result;
      });
