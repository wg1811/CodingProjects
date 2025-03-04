const express = require("express");
const router = express.Router();
const path = require("path");
const axios = require("axios");

// Route to products
router.get("/", (req, res) => {
  res.sendFile(path.join(__dirname, "../views/sales.html"));
});

router.get("/getsales", async (req, res) => {
  const productsUrl = "http://localhost:3000/products/getproducts";
  const customersUrl = "http://localhost:3000/customers/getcustomers";
  let dataWarehouse = [];
  try {
    const productsResponse = await axios.get(productsUrl);
    const customersResponse = await axios.get(customersUrl);
    const productsData = productsResponse.data.data;
    const customersData = customersResponse.data.data.results;
    console.log("customer data - ", customersData[0].name.first);
    console.log("product data - ", productsData[0].title);

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
    console.log(dataWarehouse[23]);
    res
      .status(200)
      .json({ statusCode: 200, message: "OK", data: dataWarehouse });
  } catch (error) {
    console.error(error);
    res.status(500).json({ statusCode: 500, message: "Internal Server Error" });
  }
});

module.exports = router;
