const express = require("express");
const router = express.Router();
const path = require("path");

//  Route to index
router.get("/", (req, res) => {
  res.sendFile(path.join(__dirname, "../views/index.html"));
});

module.exports = router;
