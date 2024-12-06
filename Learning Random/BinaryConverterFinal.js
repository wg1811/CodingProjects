
function getUserDecNum() {
    // prompt to get number from user
    let newNumber = prompt("Please enter the number you want coverted to binary.");
    return(newNumber);
}

// get the highest power of two for the value
function getGreatestSecondPower(numToCheck) {
    let secondPower = 1; // Need to deal with 1 and 0 as regNumbers
    let counterNum = 0;
    let highestPower = 0
    if (numToCheck == 1) {
        highestPower == 0
    }
    while(counterNum <= numToCheck) {
        counterNum = 2 ** secondPower;
        //console.log(counterNum + " and the exponent is " + secondPower);
        secondPower++;
    }  
    highestPower = secondPower - 2;
    console.log(highestPower + " is the highest power.");
    return(highestPower);  
}


// create the string of the highest power
function createBinary(powerValue) {
    let newBinary = [];
    for (i = 0; i <= powerValue; i++) {
        if (i == 0) {
            newBinary.push(1);
        } else {
            newBinary.push(0);
        }
    }
    return(newBinary);
   // console.log(newBinary);
}


// amend the string that will be the final value (I create this at the very beginning?)
function amendBinaryString (theFinalAnswer, toBeAdded) {
    theFinalAnswer[(theFinalAnswer.length - toBeAdded.length)] = 1;
    console.log(theFinalAnswer);
}

//theNumber = [1,0,0,0,0,0];
//theNumToBeAdded = [1,0,0];
//testingThis = amendBinaryString(theNumber,theNumToBeAdded);

//get the remainder of first value minue current highest power
function getRemainder(numBefore, nextHighestPower) {
        remainder = numBefore - 2 ** nextHighestPower;
        return(remainder); 
    }

function convertToBinary() {

    let userInputNumber = getUserDecNum();
    thePower = getGreatestSecondPower(userInputNumber);
    console.log(thePower + " is the highest power of " + userInputNumber);
    let convertedToBinary = createBinary(thePower);
    console.log(convertedToBinary + " is the first binary string.");
    let theRemainder = getRemainder(userInputNumber,thePower);
    console.log(theRemainder + " is the first remainder.");
    while(theRemainder != 0) {
        thePower = getGreatestSecondPower(theRemainder)
        console.log(thePower + " is the next highest power.")
        let binaryToAdd = createBinary(thePower);
        console.log(binaryToAdd + " is the binary to add.");
        amendBinaryString(convertedToBinary,binaryToAdd);
        console.log(convertedToBinary + " is the amended binary.");
        theRemainder = getRemainder(theRemainder,thePower)
    }
    let theAnswerToBinary = convertedToBinary.join("");
    console.log(theAnswerToBinary);
    alert(theAnswerToBinary + " is the binary for " + userInputNumber);
    return(theAnswerToBinary);
}

// Add event listener to the button
convertToBinaryButton.addEventListener('click', convertToBinary);
/*let userInputNumber = getUserDecNum();
decNumtoBinary = convertToBinary(userInputNumber);
let theAnswerToBinary = decNumtoBinary.join("")
console.log(theAnswerToBinary);
alert(theAnswerToBinary + " is the binary for " + userInputNumber); */


