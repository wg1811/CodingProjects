var express = require('express');
var path = require('path'); // I'm getting path errors...need to install path? or maybe it should be var path = something?
var router = express.Router();
var fs = require("fs");

// /* GET home page. */
// router.get('/', function(req, res, next) {
//   res.render('index', { title: 'Express' });
// });

//  Read people.json and render it
router.get('/', function(req, res, next) {
  // how to add the people file
  console.log("It got this far");
  const filePath = path.join(__dirname, "../public/data/people.json");  // maybe this should be "public", "data", etc.?
  console.log(filePath + " is the file path.");

  fs.readFile(filePath, "utf-8", (err, data) => {
    if (err) {
      console.error("Error reading file:", err);
      if (!res.headersSent) res.status(500).send("Error reading data file"); 
      return;
    }

    let people;
    try {
      people = JSON.parse(data); // Ensure it's parsed properly
    } catch (parseError) {
      console.error("Error parsing JSON:", parseError);
      if (!res.headersSent) res.status(500).send("Error reading data file"); 
      return;
    }

    console.log("Loaded people:", people); // Debugging output
    res.render('index', { title: 'Express', people }); // ✅ Pass people as an array
  });
});

module.exports = router;
