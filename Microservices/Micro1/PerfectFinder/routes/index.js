var express = require("express");
var router = express.Router();

/* GET home page. */
router.get("/", function (req, res, next) {
  res.render("index", { title: "Express" });
});

//check for perfect
router.post("/", (req, res) => {
  const { primeNumber } = req.body;
  console.log(primeNumber, typeof primeNumber);
  // Calculate 2^n - 1 (Mersenne number)
  const mersenneNumber = Math.pow(2, primeNumber) - 1;
  let isPerfect = false;

  // Check if the Mersenne number is prime
  if (isPrime(mersenneNumber)) {
    isPerfect = true;
  }

  // Calculate the potential perfect number
  const maybePerfectNumber = Math.pow(2, primeNumber - 1) * mersenneNumber;

  function isPrime(number) {
    // Convert to integer in case of floating point
    const n = Math.floor(number);

    // Check divisibility by odd numbers up to the square root
    const sqrt = Math.floor(Math.sqrt(n));

    for (let i = 3; i <= sqrt; i += 2) {
      if (n % i === 0) return false;
    }

    return true;
  }

  //return this
  return res.json({
    primeNumber: `The prime number taken from ASP CORE is ${primeNumber}`,
    maybePerfectNumber: `The potential perfect number calculated in NODE FRAME was ${maybePerfectNumber}`,
    isPerfect: `It is ${isPerfect} that the number ${maybePerfectNumber} is a perfect number`,
  });
});
module.exports = router;
