document
  .getElementById('darkmodebutton')
  .addEventListener('click', toggleDarkMode);

document.getElementById('ballbutton').addEventListener('click', animate);

// Constants
const numBalls = 1;
const ballRadius = 5;
const ballMinSpeed = 0.01;
const size = 700;
let x = size / 2;
let y = size / 4;
let speedX = 3;
let speedY = 2;
const myCanvas = document.getElementById('myCanvas');
myCanvas.width = size;
myCanvas.height = size / 2;
let canvasHeight = myCanvas.height;
let canvasWidth = myCanvas.width;
const ctx = myCanvas.getContext('2d');

let isDarkMode = false;
function toggleDarkMode() {
  if (!isDarkMode) {
    isDarkMode = true;
    // cancelAnimationFrame(discoAnimationID);
    darkmodebutton.innerText = 'Dark mode on';
  } else {
    isDarkMode = false;
    //goDisco();
    darkmodebutton.innerText = 'Dark mode off';
  }
}

class Ball {
  constructor(x, y, radius, speedX, speedY, hue, canvasWidth, canvasHeight) {
    this.x = x;
    this.y = y;
    this.radius = radius;
    this.speedX = speedX;
    this.speedY = speedY;
    this.hue = hue;
    this.canvasWidth = canvasWidth;
    this.canvasHeight = canvasHeight;
  }

  move() {
    this.x += this.speedX;
    this.y += this.speedY;

    // Bounce off the walls
    if (this.x - this.radius <= 0 || this.x + this.radius >= this.canvasWidth) {
      this.speedX *= -1;
    }
    if (
      this.y - this.radius <= 0 ||
      this.y + this.radius >= this.canvasHeight
    ) {
      this.speedY *= -1;
    }
  }

  draw(ctx) {
    ctx.beginPath();
    ctx.arc(this.x, this.y, this.radius, 0, Math.PI * 2);
    ctx.strokeStyle = 'white';
    ctx.fillStyle = `hsl(${this.hue}, 100%, 50%)`;
    ctx.fill();
    ctx.stroke();
  }
}
const balls = [];
for (let i = 0; i < numBalls; i++) {
  const hue = (i * 360) / numBalls;
  const ball = new Ball(
    x,
    y,
    ballRadius,
    speedX,
    speedY,
    hue,
    canvasWidth,
    canvasHeight
  );
  balls.push(ball);
}

animate();

//Functions
function animate() {
  console.log('It should be animating.');
  ctx.clearRect(0, 0, canvasWidth, canvasHeight);
  balls.forEach((ball) => ball.move());
  balls.forEach((ball) => ball.draw(ctx));
  requestAnimationFrame(animate);
}
