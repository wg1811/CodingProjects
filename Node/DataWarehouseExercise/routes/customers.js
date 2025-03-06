const express = require("express");
const router = express.Router();
const path = require("path");
const axios = require("axios");

// Route to products
router.get("/", (req, res) => {
  res.sendFile(path.join(__dirname, "../views/customers.html"));
});

// Trying to simulate a database
let customerResponse = [];

// Route to get customers
router.get("/getcustomers", async (req, res) => {
  const numUsers = 500;
  const url = `https://randomuser.me/api/?results=${numUsers}`;
  if (customerResponse.results && customerResponse.results.length > 0) {
    res
      .status(200)
      .json({ statusCode: 200, message: "OK", data: customerResponse });
    return;
  }
  try {
    const response = await axios.get(url);
    const data = response.data;
    customerResponse = data;
    // console.log(
    //   "this is the data.results(customer.js): ::::::::::::",
    //   customerResponse.results
    // );
    console.log("this is the length", customerResponse.results.length);
    res.status(200).json({ statusCode: 200, message: "OK", data: data });
  } catch (error) {
    console.error(error);
    res.status(500).json({ statusCode: 500, message: "Internal Server Error" });
  }
});

module.exports = router;
