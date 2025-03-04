const express = require("express");
const PORT = 3000;
const path = require("path");
const cors = require("cors");

const indexRouter = require("./routes/index");
const productsRouter = require("./routes/products");
const customersRouter = require("./routes/customers");
const salesRouter = require("./routes/sales");

const app = express();

app.use(cors());
app.use(express.static(path.join(__dirname, "views")));
app.use(express.static(path.join(__dirname, "public")));

app.use("/", indexRouter);
app.use("/products", productsRouter);
app.use("/customers", customersRouter);
app.use("/sales", salesRouter);

//  Listen on port 3000
app.listen(PORT, () => {
  console.log(`Server is running on port ${PORT}`);
});

module.exports = app;
