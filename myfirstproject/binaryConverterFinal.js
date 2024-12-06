
function getUserDecNum() {
    // prompt to get number from user
    let newNumber = prompt("Please enter the number you want coverted to binary.");
    return(newNumber);
}

// get the highest power of two for the value
function getGreatestSecondPower(numToCheck) {
    let secondPower = 1; // Need to deal with 1 and 0 as numToCheck
    let counterNum = 0;
    let highestPower = 0
    if (numToCheck == 1) {
        highestPower == 0
    }
    while(counterNum <= numToCheck) {
        counterNum = 2 ** secondPower;
        secondPower++;
    }  
    highestPower = secondPower - 2;
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
}


// amend the string that will be the final value 
function amendBinaryString (theFinalAnswer, toBeAdded) {
    theFinalAnswer[(theFinalAnswer.length - toBeAdded.length)] = 1;
}

//get the remainder of first value minue current highest power
function getRemainder(numBefore, nextHighestPower) {
        remainder = numBefore - 2 ** nextHighestPower;
        return(remainder); 
    }

function convertToBinary() {
    let userInputNumber = getUserDecNum();
    thePower = getGreatestSecondPower(userInputNumber);             //gets the highest power, which is converted to binary and used to create the first string
    let convertedToBinary = createBinary(thePower);
    let theRemainder = getRemainder(userInputNumber,thePower);
    while(theRemainder != 0) {                                      //getting the highest power of remainders, converting to binary and adding the existing string
        thePower = getGreatestSecondPower(theRemainder)
        let binaryToAdd = createBinary(thePower);
        amendBinaryString(convertedToBinary,binaryToAdd);
        theRemainder = getRemainder(theRemainder,thePower)
    }
    let theAnswerToBinary = convertedToBinary.join("");             //getting rid of all the commas
    alert(theAnswerToBinary + " is the binary for " + userInputNumber);
    return(theAnswerToBinary);
}

// Add event listener to the button
convertToBinaryButton.addEventListener('click', convertToBinary);


