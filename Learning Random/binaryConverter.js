// prompt to get number from user
let regNumber = prompt("Please enter the number you want coverted to binary.");

//creating an array for the values of each binary digit place.
function createBinary() {
    let binaryNum = [];
    binaryNum[0] = 1;
    return(binaryNum);
}

//find greatest power of 2 that is less than regNumber
function greatestSecondPower(numToCheck) {
    let secondPower = 1; // Need to deal with 1 and 0 as regNumbers
    let counterNum = 0;
    while(counterNum < numToCheck) {
        counterNum = 2 ** secondPower;
        console.log(counterNum + " and the exponent is " + secondPower);
        secondPower++;
    }  
    console.log((secondPower - 2) + " is the highest power.");
    return(secondPower - 2);  
}
//console.log(greatestSecondPower(regNumber));

//get the remainder from deleting the highest or next highest power
function getRemainder(numBefore, nextHighestPower) {
    if (numBefore >=  2 ** nextHighestPower) {
        remainder = numBefore - 2 ** nextHighestPower;
        return(remainder); 
    } else {
        return(remainder);
    }
} 

/*  if (numToCheck == 0 && !isFirstZero) {
        isFirstZero = true;
        theBinary = 1;
    } else if (numToCheck < 2 ** powerToCheck && numToCheck != 0) {
        theBinary = 0;
    } */

//figure out which binary for that place
function getBinaryDigit(numToCheck, powerToCheck) {
    let theBinary = 0;
    if (numToCheck >= 2 ** powerToCheck) {
        theBinary = 1;
    }
    return(theBinary);
}

//fill array with correct binary digit
function fillBinaryNum (binaryArray, binaryDigit) {
    console.log(binaryArray);
    binaryArray.push(binaryDigit);
   // return(binaryArray);
 }

//fill up the array
function loopToGetandFillBinaries (theRemainder, thePower, theBinaryNum) {
    thePower = thePower - 1 //because the first place is a 1 and already defined
    while (thePower >= 0) {
        console.log("the remainder is " + theRemainder + " the power is " + thePower);
        let binaryDigitHolder = getBinaryDigit(theRemainder,thePower);
        fillBinaryNum(theBinaryNum, binaryDigitHolder);
        thePower--;
        theRemainder = getRemainder(theRemainder, thePower);
    }
    return(theBinaryNum)
}
theAnswerinBin = createBinary();

let theHighestPower = greatestSecondPower(regNumber);
console.log (theHighestPower + " is the greatest power of two in " + regNumber)

let theFirstRemainder = getRemainder(regNumber, theHighestPower);
console.log (theFirstRemainder + " is the remainder of " + regNumber + " - " + 2 ** theHighestPower);

loopToGetandFillBinaries(theFirstRemainder, theHighestPower, theAnswerinBin)
console.log("The binary of " + regNumber + " is " + theAnswerinBin);


//creating an array for the values of each binary digit place.
/* function createBinary() {
    let binaryNum = [0];
    for (let i = 0; i < greatestSecondPower(regNumber); i++) {
        binaryNum.push(0);
    }  
    binaryNum[0] = 1;
    console.log(binaryNum);
    return(binaryNum);
}*/
//console.log(binaryNum);  

/*
function fillBinaryNum (binaryNum) {
    let theRemainder = regNumber - (2 ** greatestSecondPower(regNumber));
    console.log(theRemainder + " is the remainder.");
    for (let i = 1; i < binaryNum.length; i++) {
        if (theRemainder == 0) {
            binaryNum[i] = 1
        }
    theRemainder = theRemainder - (2 ** greatestSecondPower(theRemainder));
    }  
}  

let thisDigit = 1;
let thisBinary = createBinary();
console.log(thisBinary + " is the array after creation.");
fillBinaryNum(thisBinary, thisDigit);

console.log(thisBinary + " is the array after filling.");
*/