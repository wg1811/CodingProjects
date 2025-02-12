var express = require('express');
var router = express.Router();

/* GET home page. */
router.get('/', function(req, res, next) {
  res.render('index', { title: 'Express - default page stuff' });
});

router.get('/testpage', (req, res, next) => {
  res.render('homepage', { title: 'Home page stuff' });
});

module.exports = router;
