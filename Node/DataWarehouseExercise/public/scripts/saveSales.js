async function saveSales() {
  const url = `${baseUrl}/sales/save`;
  console.log(baseUrl, " - this is the url: ", url);
  try {
    const response = await fetch(url);
    if (!response.ok) {
      throw new Error(`HTTP error! status: ${response.status}`);
    }
    // console.log(response);
    const data = await response.json();
    // console.log("this is the data: ::::::::::::", data);
    // const table = document.getElementById("sales-table-body");
    // table.innerHTML = "";
    // let row = "";
    // data.data.forEach((transaction) => {
    //   row += `
    //   <tr>
    //     <td>${transaction.id}</td>
    //     <td>${transaction.product}</td>
    //     <td>${transaction.customer}</td>
    //     <td>${transaction.total}</td>
    //     <td>${transaction.category}</td>
    //   </tr>
    //   `;
    // });
    // table.innerHTML = row;
  } catch (error) {
    console.error(error);
  }
}
