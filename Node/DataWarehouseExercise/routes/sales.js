const express = require("express");
const router = express.Router();
const path = require("path");
const axios = require("axios");
const fs = require("fs");

// Route to products
router.get("/", (req, res) => {
  res.sendFile(path.join(__dirname, "../views/sales.html"));
});

// Simulating a database
let dataWarehouse = [];

router.get("/getsales", async (req, res) => {
  const productsUrl = "http://localhost:3000/products/getproducts";
  const customersUrl = "http://localhost:3000/customers/getcustomers";
  if (dataWarehouse.length > 0) {
    res
      .status(200)
      .json({ statusCode: 200, message: "OK", data: dataWarehouse });
    return;
  }

  try {
    const productsResponse = await axios.get(productsUrl);
    const customersResponse = await axios.get(customersUrl);
    const productsData = productsResponse.data.data;
    const customersData = customersResponse.data.data.results;

    let numTransactions = 1000;
    for (let i = 0; i < numTransactions; i++) {
      let randomProductIndex = Math.floor(Math.random() * productsData.length);
      let randomCustomerIndex = Math.floor(
        Math.random() * customersData.length
      );
      let randomQuantity = Math.floor(Math.random() * 10) + 1;
      let transaction = {
        id: i + 1,
        product: productsData[randomProductIndex].title,
        customer: customersData[randomCustomerIndex].name.first,
        quantity: randomQuantity,
        total: randomQuantity * productsData[randomProductIndex].price,
        category: productsData[randomProductIndex].category,
        customerGender: customersData[randomCustomerIndex].gender,
        customerCity: customersData[randomCustomerIndex].location.city,
        customerState: customersData[randomCustomerIndex].location.state,
        customerCountry: customersData[randomCustomerIndex].location.country,
      };
      dataWarehouse.push(transaction);
    }
    console.log("This is a transaction: " + dataWarehouse[23]);
    res
      .status(200)
      .json({ statusCode: 200, message: "OK", data: dataWarehouse });
  } catch (error) {
    console.error(error);
    res.status(500).json({ statusCode: 500, message: "Internal Server Error" });
  }
});

router.get("/save", async (req, res) => {
  // console.log(dataWarehouse);
  if (dataWarehouse.length === 0) {
    res.status(400).json({ statusCode: 400, message: "No data to save" });
    return;
  }

  const fileName = "salesTest.json";
  const filePath = path.join(__dirname, "../public/data", fileName);
  fs.writeFileSync(filePath, JSON.stringify(dataWarehouse, null, 2));
  res.status(200).json({ statusCode: 200, message: "Data saved" });
});

module.exports = router;
