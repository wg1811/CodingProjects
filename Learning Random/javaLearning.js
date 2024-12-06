// Numbers:

//let firstNumber = "10";
//let secondNumber = "20";
//let result = firstNumber + secondNumber;

let radius = 10;
let secondNumber = 20;
let piApprox = 3.141
let result = radius * secondNumber;
// console.log("first time: " + result);
radius = 49.732;
result = radius * secondNumber;
// console.log("second time: " + result);


let circleArea = piApprox * (radius * radius)
let circleCirc = 2 * piApprox * radius

//Math exercises
let weeklyGroceryCost = 2000;
let yearlyGroceryCost = weeklyGroceryCost * 52;
//console.log("Emma's yearly grocery cost is " + yearlyGroceryCost);

//Objects
let canSki = true;
let personName = "Sarah";
let personAge = "12";
let personObject = {
    name : personName,
    age : personAge,
    canSki : canSki,
    tax : 0
}



//else ifs
//if <18 tax = 0, 18 - 30 tax = 10, 31+ tax = 30. all else tax = 50
if (personObject.age < 18) {
    personObject.tax = 0;
} else if (personObject.age >= 18 && personObject.age <= 30) {
    personObject.tax = 10;
    } else if (personObject.age > 30) {
        personObject.tax = 30;
    } else {
        personObject.tax = 50;
    }
console.log("The tax rate for " + personObject.name + " is " + personObject.tax)

//ifs

theUserName = "admin";
thePassword = "12334";

adminCreds = {
    username: theUserName,
    password: thePassword,
}

//if ((adminCreds.username == "admin") && (adminCreds.password == "1234")) {
 //   console.log("Welcome Admin");
//} else {
   // console.log("Incorrect user name or password.")
//}

//Booleans:

let wearsTshirt = true;
let hasHat = false;
let wearsGlasses = false;
let isShort = false;

//arrays
let teamMembers = [
    "Javier", 
    "Bob", 
    "Sigrid", 
    "Timmy", 
    "Haakon", 
    "Nicolas", 
    "Shavan", 
    "Damien", 
    "Lena"
];

teamMembers.push("George");
teamMembers.splice(2,1);
//console.log(teamMembers);

let howManyFruit = [1, 4, 126, 2, 7, 28, 39];

let teamSize = teamMembers.length;
// console.log(teamSize);

let randoMember = Math.floor(Math.random() * teamSize);
// console.log("Team member number " + (randoMember + 1) + " is " + teamMembers[randoMember] + ".");

let addedFruits = howManyFruit[2] + howManyFruit[4];

// Array of pizza types
const pizzaTypes = [
    "Margherita",
    "Pepperoni",
    "BBQ Chicken",
    "Hawaiian",
    "Vegetarian",
    "Meat Lover's",
    "Buffalo Chicken",
    "Supreme",
    "Four Cheese",
    "Mushroom Delight",
  ];
  
  // Array of pizza toppings
  const toppings = [
    "Extra Cheese",
    "Pepperoni",
    "Olives",
    "Mushrooms",
    "Onions",
    "Bell Peppers",
    "Pineapple",
    "Bacon",
    "Chicken",
    "Sausage",
  ];


//random numbers:

let smallRandom = Math.random();
// console.log("Random number between 0 and 1: " + smallRandom);

let bigRandom = Math.floor(Math.random() * 1000000);
// console.log("Random number between 0 and a million: " + bigRandom);

//Objects:

let car = {
    "color" : "red",
    "brand" : "Ford",
    "model" : "Mustang",
    "year" : 2010
}

let dog = {
    "color" : "brown and white",
    "weight" : 30,
    "age" : 3
}

let ship = {
    "tonnage" : 400000000,
    "color" : "teal",
    "length" : 100,
    "width" : 30,
    "idNumber" : "WSS2345346536"
}

let member = {
    "name" : "Wesley",
    "birthday" : "21/10/1980",
    "hobbies" : ["Climbing", "Gaming", "Reading", "Scuba Diving"]
    }

//exponents
let theExponent = 2 ** 4;

//modulo, modus or remainder.  Give how many left over after you divide by something.
let theModusNumber = 245625655
let theRemainder = theModusNumber%7

let isDivfive = theModusNumber%5
let isDivSeven = theModusNumber%7
let isDivTwelve = theModusNumber%12

if (isDivfive == 0) {
    console.log(theModusNumber + " is divisible by 5.");
} else {
    console.log(theModusNumber + " is not divisible by 5.")
}

if (isDivSeven == 0) {
    console.log(theModusNumber + " is divisible by 7.");
} else {
    console.log(theModusNumber + " is not divisible by 7.")
}

if (isDivTwelve == 0) {
    console.log(theModusNumber + " is divisible by 12.");
} else {
    console.log(theModusNumber + " is not divisible by 12.")
}

console.log(theRemainder);

console.log(theExponent);

console.log(member);

console.log(addedFruits)

console.log(teamMembers[2]);

console.log(isShort);

console.log(result + " ; " + circleArea + " ; " + circleCirc);