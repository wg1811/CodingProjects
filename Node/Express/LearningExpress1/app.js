const express=require("express");
const port = 3000;
const path = require("path");
const fs = require("fs");
const open = require('open');


const app = express();

app.use(express.static(path.join(__dirname, "view"), {index: false}));
app.use(express.static(path.join(__dirname, "public"), {index: false}));

app.get("/", (req,res) => {
res.send("Hello World");
});

app.get("/home", (req,res) => {
    res.sendFile(path.join(__dirname, "view", "index.html"));
})

app.listen(port, async () => {
    console.log(`listening at ${port}.`);
});