var express = require('express');
var router = express.Router();

/* GET home page. */
router.get('/', (req, res, next) => {

  let user = {
    name:"Devrim",
    password:"1234"
  }
  res.render('auth', { name:user.name, password:user.password });
});

module.exports = router;
