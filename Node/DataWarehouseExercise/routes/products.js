const express = require("express");
const router = express.Router();
const path = require("path");
const axios = require("axios");

// Route to products
router.get("/", (req, res) => {
  res.sendFile(path.join(__dirname, "../views/products.html"));
  res.send(200).json({ statusCode: 200, message: "OK - products page" });
});

router.get("/getproducts", async (req, res) => {
  const url = "https://fakestoreapi.com/products";
  try {
    const response = await axios.get(url);
    const data = response.data;
    console.log(data);
    res.status(200).json({ statusCode: 200, message: "OK", data: data });
  } catch (error) {
    console.error(error);
    res.status(500).json({ statusCode: 500, message: "Internal Server Error" });
  }
});

module.exports = router;
