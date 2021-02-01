const slider = document.querySelector('.slider');
const buttons = document.querySelectorAll('.btn-slider');
const options = document.querySelectorAll('.option');
const slides = document.querySelectorAll('.slider-img');
const imgBackgrounds = document.querySelectorAll('.sl-img-bg');

let index = 1;
let optIndex = 0;
let size = slides[index].clientWidth;

buttons.forEach(btn => btn.addEventListener('click', btnCheck));
options.forEach(opt => opt.addEventListener('click', optionFunc));

function btnCheck() {
    if (this.id === "prev") {
        index--;

        if (optIndex == 0) {
            optIndex = options.length - 1;
        }
        else {
            optIndex--;
        }
    }
    else if(this.id === "next") {
        index++;

        if (optIndex == options.length - 1) {
            optIndex = 0;
        }
        else {
            optIndex++;
        }
    }

    slide();
}

function slide() {
    slider.style.transition = "transform .5s ease-in-out";
    update();
}

function update() {
    slider.style.transform = "translateX(" + (-size * index) + "px)";

    options.forEach(opt => opt.classList.remove('colored'));
    options[optIndex].classList.add('colored');

    // imgBackgrounds.forEach(bg => bg.classList.remove('show'));
    // imgBackgrounds[optIndex].classList.add('show');
}

slider.addEventListener('transitionend', () => {
    if (slides[index].id === "last") {
        slider.style.transition = "none";
        index = slides.length - 2;
        slider.style.transform = "translateX(" + (-size * index) + "px)"
    } 
    else if (slides[index].id === "first") {
        slider.style.transition = "none";
        index = 1;
        slider.style.transform = "translateX(" + (-size * index) + "px)"
    }
});

function optionFunc() {
    let i = Number(this.getAttribute('option-index'));
    index = i + 1;
    optIndex = i;
    slide();
}

