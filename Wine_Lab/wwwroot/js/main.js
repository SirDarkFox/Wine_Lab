const page = document.querySelector(".page");
const introImg = document.querySelector(".img-bg");
const border = document.querySelector(".border");
const aboutContent = document.querySelector(".about-container");
const opacity = document.querySelectorAll(".opacity");
const introContent = document.querySelector(".intro-container");
const btnToTop = document.querySelector(".btn-to-top");
const flexData = document.querySelectorAll(".flex-data");

let pageHeight = page.offsetHeight;
let borderWidth = 100;

function data() {
    flexData.forEach(elem => {
        animateNumbers(elem, 0, parseInt(elem.textContent), 2000);
    })
}

data();

function animateNumbers(elem, begin, end, duration) {
    let start = null;
    const step = (timestamp) => {
        if (!start) {
            start = timestamp;
        }
        const progress = Math.min((timestamp - start) / duration, 1);
        if (progress < 1) {
            window.requestAnimationFrame(step);
        }
        elem.textContent = Math.floor(progress * (end - begin) + begin);
    };
    window.requestAnimationFrame(step);
}

window.addEventListener("scroll", function() {
    let scroll = window.pageYOffset;
    let speed = introImg.dataset.speed;
    // let sectionY = div.getBoundingClientRect();

    introImg.style.transform = `translateY(${scroll * speed}px)`;

    border.style.width = `${borderWidth - (scroll / 5)}%`;

    aboutContent.style.transform = `translateY(${scroll / pageHeight * 50 - 50}px)`;

    opacity.forEach(elem => {
        elem.style.opacity = scroll / pageHeight * 1.4;
    });

    introContent.style.opacity =- scroll / (pageHeight / 2) + 1;

    if (document.body.scrollTop > 500 || document.documentElement.scrollTop > 500) {
        btnToTop.style.display = "block";
    } else {
        btnToTop.style.display = "none";
    }
})

btnToTop.addEventListener('click', moveToTop);

function moveToTop() {
    window.scrollTo({top: 0, behavior: 'smooth'});
}