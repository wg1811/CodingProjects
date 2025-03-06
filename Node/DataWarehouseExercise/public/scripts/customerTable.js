const baseUrl = window.location.origin; // "http://localhost:3000/

async function populateCustomerTable() {
  const url = `${baseUrl}/customers/getcustomers`;
  try {
    const response = await fetch(url);
    if (!response.ok) {
      throw new Error(`HTTP error! status: ${response.status}`);
    }
    const data = await response.json();
    //console.log(data);
    //console.log(JSON.stringify(data));
    const results = data.data.results;
    const table = document.getElementById("customers-table-body");
    table.innerHTML = "";
    let row = "";
    let id = 1;
    results.forEach((customer) => {
      row += `
      <tr>
        <td>${id++}</td>
        <td>${customer.name.first} ${customer.name.last}</td>
        <td>${customer.gender}</td>
        <td>${customer.location.city}</td>
        <td>${customer.location.state}</td>
        <td>${customer.location.country}</td>
      </tr>
      `;
    });
    table.innerHTML = row;
  } catch (error) {
    console.error(error);
  }
}

document.addEventListener("DOMContentLoaded", populateCustomerTable);
