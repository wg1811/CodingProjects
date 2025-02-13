// 1st:  Install Express - npm install express
// 2nd:  Install Path - npm install path
// 3rd:  Install File System - npm install fs
// 4th:  Install Cookie parser - npm install cookie-parser (not sure why)

const express = require("express");
const path = require("path");
const fs = require("fs");
const cookieParser = require("cookie-parser");

const PORT = 5000;

const app = express();

//  Adding json stuff
app.use(express.json());
app.use(express.urlencoded({extended:false}));

//  Connectding folders
app.use(express.static(path.join(__dirname, "view")));
app.use(express.static(path.join(__dirname, "public")));

app.get("/", (req,res) => {
    res.sendFile(path.join(__dirname, "view", "index.html"));
})

app.post("/sumnums", (req, res) => {
    let {num1, num2} = req.body;
    num1 = parseFloat(num1);
    num2 = parseFloat(num2);
    const result = num1 + num2;
    res.json({
        num1:num1,
        num2:num2,
        result:result
    });
})

//  Test endpoint
app.get("/", (req,res) => {
    res.send("Hello World.  I will be your server today.");
});

app.listen(PORT, () => {
    console.log(`Listening on port ${PORT}`);
});