const canvas = document.getElementById("canvas1");
const ctx = canvas.getContext("2d");
const particlesColor = "#64B5F6";
canvas.width = window.innerWidth;
canvas.height = window.innerHeight;

let particlesArray;

let mouse = {
  x: null,
  y: null,
  radius: (canvas.height / 120) * (canvas.width / 120),
};

window.addEventListener("mousemove", function (event) {
  mouse.x = event.x;
  mouse.y = event.y;
});

class Particle {
  constructor(x, y, directionX, directionY, size, color) {
    this.x = x;
    this.y = y;
    this.directionX = directionX;
    this.directionY = directionY;
    this.size = size;
    this.color = color;
  }

  draw() {
    ctx.beginPath();
    ctx.arc(this.x, this.y, this.size, 0, Math.PI * 2, false);
    ctx.fillStyle = this.color;
    ctx.fill();
  }

  update() {
    if (this.x > canvas.width || this.x < 0) {
      this.directionX = -this.directionX;
    }
    if (this.y > canvas.height || this.y < 0) {
      this.directionY = -this.directionY;
    }

    let dx = mouse.x - this.x;
    let dy = mouse.y - this.y;
    let distance = Math.sqrt(dx * dx + dy * dy);
    if (distance < mouse.radius + this.size) {
      if (mouse.x < this.x && this.x < canvas.width - this.size * 10) {
        this.directionX = -this.directionX;
        this.x += 1;
      }
      if (mouse.x > this.x && this.x > this.size * 10) {
        this.directionX = -this.directionX;
        this.x -= 1;
      }
      if (mouse.y < this.y && this.y < canvas.height - this.size * 10) {
        this.directionY = -this.directionY;
        this.y += 1;
      }
      if (mouse.y > this.y && this.y > this.size * 10) {
        this.directionY = -this.directionY;
        this.y -= 1;
      }
    }

    this.x += this.directionX / 2;
    this.y += this.directionY / 2;
    this.draw();
  }
}

function init() {
  particlesArray = [];
  let numberOfParticles = (canvas.height * canvas.width) / 9000;
  for (let i = 0; i < numberOfParticles; i++) {
    let size = Math.random() * 5 + 1;
    let x = Math.random() * (innerWidth - size * 2 - size * 2) + size / 2;
    let y = Math.random() * (innerHeight - size * 2 - size * 2) + size / 2;
    let directionX = Math.random() * 5 - 2.5;
    let directionY = Math.random() * 5 - 2.5;
    let color = particlesColor;

    particlesArray.push(
      new Particle(x, y, directionX, directionY, size, color)
    );
  }
}

function connect() {
  let opacityValue = 1;
  for (let a = 0; a < particlesArray.length; a++) {
    for (let b = a; b < particlesArray.length; b++) {
      let distance = 
      (particlesArray[a].x - particlesArray[b].x) *
        (particlesArray[a].x - particlesArray[b].x) +
        (particlesArray[a].y - particlesArray[b].y) *
          (particlesArray[a].y - particlesArray[b].y);

      if (distance < (canvas.width / 7) * (canvas.height / 7)) {
        opacityValue = 1 - distance / 20000;
        ctx.strokeStyle = "rgba(96,125,139, " + opacityValue + ")";
        ctx.lineWidth = 1;
        ctx.beginPath();
        ctx.moveTo(particlesArray[a].x, particlesArray[a].y);
        ctx.lineTo(particlesArray[b].x, particlesArray[b].y);
        ctx.stroke();
      }
    }
  }

  // for (let i = 0; i < particlesArray.length; i++) {
  //   // let distance = 100;
  //   let distance =
  //     (mouse.x - particlesArray[i].x) * (mouse.x - particlesArray[i].x) +
  //     (mouse.y - particlesArray[i].y) * (mouse.y - particlesArray[i].y);

  //   if (distance < (canvas.width / 4) * (canvas.height / 7)) {
  //     opacityValue = 1 - distance / 40000;
  //     ctx.strokeStyle = "rgba(106, 27, 154, " + opacityValue + ")";
  //     ctx.lineWidth = 1.5;
  //     ctx.beginPath();
  //     ctx.moveTo(mouse.x, mouse.y);
  //     ctx.lineTo(particlesArray[i].x, particlesArray[i].y);
  //     ctx.stroke();
  //   }
  // }
}

// window.addEventListener("click", function () {
//   spawn();
// });

// function spawn() {
//   for (let i = 0; i < 10; i++) {
//     let size = Math.random() * 5 + 1;
//     let x = mouse.x;
//     let y = mouse.y;
//     let directionX = Math.random() * 5 - 2.5;
//     let directionY = Math.random() * 5 - 2.5;
//     let color = particlesColor;

//     particlesArray.push(
//       new Particle(x, y, directionX, directionY, size, color)
//     );
//   }
// }

function animate() {
  requestAnimationFrame(animate);
  ctx.clearRect(0, 0, innerWidth, innerHeight);

  for (let i = 0; i < particlesArray.length; i++) {
    particlesArray[i].update();
  }
  connect();
}

window.addEventListener("resize", function () {
  canvas.width = innerWidth;
  canvas.height = innerHeight;
  mouse.radius = (canvas.height / 80) * (canvas.height / 80);
  init();
});

window.addEventListener("mouseout", function () {
  mouse.x = undefined;
  mouse.y = undefined;
});

init();
animate();