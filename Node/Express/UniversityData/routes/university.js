var express = require("express");

var router = express.Router();

var path = require("path");

var csv = require("csv-parser");

var fs = require("fs");

router.get("/", function (req, res, next) {
  res.render("university", { title: "University" });
});

router.get("/getuniversities", (req, res) => {
  const results = [];
  const filepath = path.join(__dirname, "..", "Data", "cwurData.csv");

  fs.createReadStream(filepath)
    .pipe(csv())
    .on("data", (data) => results.push(data))
    .on("end", () => {
      res.json(results);
    })
    .on("error", (err) => {
      res.status(500).json({ error: "Failed to read csv file." });
    });
});

//  if i wanted to make a backend filter function, how would I do that?
router.post("/filteredbackend", (req, res) => {
  const thingy = req.body;
  res.json({
    thingyName: thingy,
  });
});

module.exports = router;
