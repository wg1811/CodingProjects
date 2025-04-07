var express = require("express");
var router = express.Router();

/* GET home page. */
router.get("/", function (req, res, next) {
  res.render("index", { title: "Express" });
});

//get a a page
router.get("/getpage", async (req, res) => {
  //url
  const url = "https://www.bbc.com/";
  //get with axios
  const response = await axios.get(url);
  const html = response.data;
  // use cheerio
  const page = cheerio.load(html);

  //let us return page
  res.send(page.html());
});

router.get("/findtitles", async (req, res, next) => {
  const url = "https://www.bbc.com/";
  try {
    const response = await axios.get(url);
    const html = response.data;
    const $ = cheerio.load(html);

    // grab all h2 elements
    const titles = [];
    $("h2").each((i, elem) => {
      titles.push($(elem).text().trim());
    });
    //see titles
    console.log("h2 titles", titles);

    res.json(titles); // send as JSON
  } catch (err) {
    console.error(err);
    res.status(500).send("Error fetching titles.");
  }
});

//dynamic source
router.get("/getonedynamic", async (req, res) => {
  //url
  const url = "https://www.bbc.com";
  const results = await getPage(url);
  res.send(results);
});

//function get h2 elements
async function getPage(url) {
  const titles = [];
  const response = await axios.get(url);
  const html = response.data;
  const $ = cheerio.load(html);
  //element tag
  $("div h2").each((i, elem) => {
    const text = $(elem).text().trim();
    if (text) titles.push(text);
  });
  return titles.sort();
}

//using multiple sources
router.get("/getmultiple", async (req, res) => {
  const sources = [
    { name: "BBC", url: "https://www.bbc.com" },
    { name: "CNN", url: "https://edition.cnn.com" },
    { name: "CNBC", url: "https://www.cnbc.com" },
  ];

  try {
    const allTitles = [];

    for (const source of sources) {
      const titles = await getPage(source.url);
      titles.forEach((title) => {
        allTitles.push({ source: source.name, title });
      });
    }

    res.json({ headlines: allTitles });
  } catch (err) {
    console.error("Scrape error:", err.message);
    res.status(500).send("Error fetching pages");
  }
});

module.exports = router;
