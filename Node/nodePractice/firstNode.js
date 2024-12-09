var http = require("http");
var dt = require("./firstModule");
var fs = require("fs");
var url = require("url");
var formidable = require("formidable");

http
  .createServer(function (req, res) {
    var q = url.parse(req.url, true);
    var filename = "." + q.pathname;
    fs.readFile(filename, function (err, data) {
      if (err) {
        res.writeHead(404, { "Content-Type": "text/html" });
        return res.end("404 File Not Found");
      }
      res.writeHead(200, { "Content-Type": "text/html" });
      res.write("The date and time are currently: " + dt.myDateTime() + "<br>");
      res.write(data);
      return res.end("Hello World!");
    });
  })
  .listen(8080);
