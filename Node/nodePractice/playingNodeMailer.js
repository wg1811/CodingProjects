var nodemailer = require("nodemailer");

transporter = nodemailer.createTransport({
  service: "gmail",
  auth: {
    user: "wg1811.kodehode@gmail.com",
    pass: "SpacemanSpiff80",
  },
});

var mailOptions = {
  from: "wg1811.kodehode@gmail.com",
  to: "wtg107@gmail.com",
  subject: "Emails from Node js?",
  text: "This is super easy. I wonder what nefarious things people use it for.",
};

transporter.sendMail(mailOptions, function (error, info) {
  if (error) {
    console.log(error);
  } else {
    console.log("Email sent: " + info.response);
  }
});
