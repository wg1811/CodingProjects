"use strict";
function greet(name) {
    return `Hello, ${name}!`;
}
console.log(greet('World'));
// Basic Data Types
// Numbers are all number.  Everything is a float.
const myAge = 44;
const myFloat = 3.234234;
let hex = 0xf00d;
let binary = 0b1010;
let octal = 0o744;
// String
let myName = "Whatevs";
myName = "Still Whatevs";
// Boolean
let isAdmin = true;
// Array
let exArray = [3, 65, 356, 45, 45];
let exStringArray = ["This", "That", "and the other"];
let exMixedArray = ["Huh?", 293848, false, "Why?"];
console.log(exArray + " " + myName);
// Functions
function sumNumbers(num1, num2) {
    return num1 + num2;
}
console.log(sumNumbers(234, 234.23432));
// Enum.  I'm not sure what these are, actually.  Like a class?
var Color;
(function (Color) {
    Color[Color["Red"] = 0] = "Red";
    Color[Color["Green"] = 1] = "Green";
    Color[Color["Blue"] = 2] = "Blue";
})(Color || (Color = {}));
let c = Color.Green; // Assigns the index of the value to c.
console.log(c);
console.log(Color.Blue);
// Any lets the type change based on what's assigned.
let notSure = 4;
notSure = "Now I'm a string";
notSure = false;
// Making Objects, you have to tell what everything thing is first.
let fighter = {
    name: "Grog",
    height: 220,
    weight: 300,
    hitPoints: 250,
    skills: ["Heavy strike", "Quick strike", "Cleave"],
    inventory: ["Great Sword", "Bracers", "Health potion"],
};
