var express = require('express');
const { path } = require('../app'); // I'm getting path errors...need to install path? or maybe it should be var path = something?
var router = express.Router();
var fs = require("fs");

/* GET home page. */
router.get('/', function(req, res, next) {
  res.render('index', { title: 'Express' });
});

//  Read people.json and render it
router.get('/people', function(req, res, next) {
  // how to add the people file
  const filePath = path.join(__dirname, "../public/data/people.json");  // maybe this should be "public", "data", etc.?
  fs.readFile, "utf-8", (err,data) => {
    if (err) {
      console.error("error", err)
      return res.status(500).send("Error");
    }
    const people = JSON.parse(data);
    console.log(people);
    res.render('index', { title: 'Express', people:JSON.stringify(people) });
  }
});

module.exports = router;
