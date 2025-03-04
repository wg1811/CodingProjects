const baseUrl = window.location.origin; // "http://localhost:3000/

async function populateSalesTable() {
  const url = `${baseUrl}/sales/getsales`;
  try {
    const response = await fetch(url);
    if (!response.ok) {
      throw new Error(`HTTP error! status: ${response.status}`);
    }
    console.log(response);
    const data = await response.json();
    const table = document.getElementById("sales-table-body");
    table.innerHTML = "";
    let row = "";
    data.forEach((transaction) => {
      row += `
      <tr>
        <td>${transaction.id}</td>
        <td>${transaction.product}</td>
        <td>${transaction.customer}</td>
        <td>${transaction.total}</td>
        <td>${transaction.category}</td>
      </tr>
      `;
    });
    table.innerHTML = row;
  } catch (error) {
    console.error(error);
  }
}

document.addEventListener("DOMContentLoaded", populateSalesTable);
