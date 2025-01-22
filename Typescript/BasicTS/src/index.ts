function greet(name:string): string {
    return `Hello, ${name}!`;
    }
    
console.log(greet('World'));

// Basic Data Types
// Numbers are all number.  Everything is a float.
const myAge:number = 44;
const myFloat:number = 3.234234;
let hex: number = 0xf00d;
let binary: number = 0b1010;
let octal: number = 0o744;

// String
let myName: string = "Whatevs";
myName = "Still Whatevs";

// Boolean
let isAdmin:boolean = true;

// Array
let exArray: number[] = [3, 65, 356, 45, 45];
let exStringArray: Array<string> = ["This", "That", "and the other"];
let exMixedArray: [string, number, boolean, string] = ["Huh?", 293848, false, "Why?"];
console.log(exArray + " " + myName);

// Functions
function sumNumbers(num1: number, num2: number):number {
    return num1 + num2;
}
console.log(sumNumbers(234, 234.23432));

// Enum.  I'm not sure what these are, actually.  Like a class?

enum Color {
    Red,
    Green,
    Blue,
}

let c: Color = Color.Green; // Assigns the index of the value to c.
console.log(c);
console.log(Color.Blue);

// Any lets the type change based on what's assigned.
let notSure: any = 4;
notSure = "Now I'm a string";
notSure = false;

// Making Objects, you have to tell what everything thing is first.
let fighter: {
    name: string;
    height: number;
    weight: number;
    hitPoints: number;
    skills: string[];
    inventory: string[];
} = {
    name: "Grog",
    height: 220,
    weight: 300,
    hitPoints: 250,
    skills: ["Heavy strike", "Quick strike", "Cleave"],
    inventory: ["Great Sword", "Bracers", "Health potion"],
}