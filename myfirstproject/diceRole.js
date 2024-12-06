
//dice images
const diceImages = [
    "https://i.imgur.com/y3Gx5PX.png",
    "https://i.imgur.com/yy1oZUU.png",
    "https://i.imgur.com/HaOsJYz.png",
    "https://i.imgur.com/pNLBUlJ.png",
    "https://i.imgur.com/OmHFbiL.png",
    "https://i.imgur.com/d13UYg4.png"
];


// Select the dice image and button elements
const dice = document.getElementById('diceImage');
const rollButton = document.getElementById('rollDiceButton');
//let howManySeconds = 30;

// Function to roll the dice
function rollDice() {
    let howManySeconds = prompt("How hard do you want to throw it? I.e. how long should it roll for in seconds?");
    while (isNaN(howManySeconds)) { //checking in its a number
      howManySeconds = prompt("That's not a number jackass.  Please just put in a reasonable number of secconds.")
    }
    
    let totalInterval = howManySeconds * 1000; // How long to roll dice in ms
    let interval = 250; //Setting first image changing time interval
    console.log("-----------------" + totalInterval);
  
  // Function to update the dice image
  function updateDice() {
    // Pick a random dice face
    const randomIndex = Math.floor(Math.random() * diceImages.length);
    dice.src = diceImages[randomIndex];
    console.log(`Random Index: ${randomIndex + 1}`); // trying to test if this runs
  }

    // Slow down the dice roll by increasing the interval
    for (let i = interval; i < totalInterval; i = i + i * .07) {// Gradually slow down
      console.log(`Counter is: ${i}`);
      setTimeout(updateDice, i);
    };

    //Tell user what they landed on in an alert.
    //setTimeout(alert(`You rolled a: ${ + 1}`)), totalInterval + 250;
};

setTimeout(alert(`You rolled a: ${ + 1}`)), totalInterval + 250;

// Add event listener to the button
rollButton.addEventListener('click', rollDice);
