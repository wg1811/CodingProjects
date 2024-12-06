let userNumber = 11;
let theReverseAnswer = []; //the array gets filled based on if the 'last number' is divisible by two.

while (userNumber > 0) {
  if (userNumber % 2 == 0) {
    theReverseAnswer.push(0);
    userNumber = userNumber - userNumber / 2; //iterate by deleting the 'last number' divided by 2.
  } else {
    theReverseAnswer.push(1);
    userNumber = userNumber - (Math.floor(userNumber / 2) + 1); //for odds, you want to round up.
  }
}

console.log(theReverseAnswer);
let theAnswer = [];
for (let i = theReverseAnswer.length; i >= 0; i--) {
  theAnswer.push(theReverseAnswer[i]);
}
theAnswer = theAnswer.join("");
console.log(theAnswer);

// while (userNumber > 1) {
//   if (userNumber % 2 == 0) {
//     theAnswer.push(0);
//   } else {
//     theAnswer.push(1);
//   }
//   console.log(userNumber + " is the number before subracting num/2");
//   userNumber = userNumber - Math.floor(userNumber / 2);
//   console.log(theAnswer);
//   console.log(userNumber);
// }
