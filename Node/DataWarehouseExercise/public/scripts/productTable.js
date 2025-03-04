const baseUrl = window.location.origin; // "http://localhost:3000/

async function populateProductTable() {
  const url = `${baseUrl}/products/getproducts`;
  try {
    const response = await fetch(url);
    if (!response.ok) {
      throw new Error(`HTTP error! status: ${response.status}`);
    }
    const data = await response.json();
    //console.log(data);
    //console.log(JSON.stringify(data));
    const results = data.data;
    const table = document.getElementById("products-table-body");
    table.innerHTML = "";
    let row = "";
    let id = 1;
    results.forEach((product) => {
      row += `
      <tr>
        <td>${product.id}</td>
        <td>${product.title}</td>
        <td>${product.description}</td>
        <td>${product.price}</td>
        <td>${product.category}</td>
      </tr>
      `;
    });
    table.innerHTML = row;
  } catch (error) {
    console.error(error);
  }
}

document.addEventListener("DOMContentLoaded", populateProductTable);
