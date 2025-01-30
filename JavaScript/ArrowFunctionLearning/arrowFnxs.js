function addNumbers(num1, num2) {
    const result = num1 + num2;
    return result;
}

num1 = 123;
num2 = 345;

console.log(addNumbers(num1, num2));


// Convert to arrow function
const addNumArrow = (num1, num2) => {const result = num1 + num2; return result;}


// basically, its:
// type fnxName = (param1, paramWhatevs) => {do stuff + otherthings.ToString() or whatever; return something if you used curly brackets};


// Exercises. convert to arrowfnxs
function greetings(userName) {
    return `greetings user ${userName}`;
}

const greetingArrow = (userName2) => `Greetings, ${userName2}! How's your day going?`;

console.log(greetings("Pete"));
console.log(greetingArrow("Pete"));

const makeRando = () => {
    let rando = Math.floor(Math.random() * 1000);
    return rando;
}
console.log(makeRando());

const getSquare = (numToSquare) => numToSquare * numToSquare;

const isEven = (isItEven) => isItEven % 2 == 0 ? "It's even." : "It's note even.";

console.log(isEven(2340892));