const mobileBtn = document.querySelector("#btn-mobile")

mobileBtn.addEventListener('click', () => {
    const menu = document.querySelector('#mobile-menu')

    const isOpen = menu.getAttribute('aria-expanded') === 'true'
    console.log(isOpen)
    menu.setAttribute('aria-expanded', !isOpen)
})

//need added functionality for when someone presses a link to close the actual menu too


///works great with touch and mouse input.
const container = document.querySelector('#sliderContainer');
const slider = document.querySelector('#slider');
const gradientText = document.querySelector('.gradient-text');

if (container && slider && gradientText) {
    slider.addEventListener('input', (e) => {
        updateSliderPosition(e.target.value);
    });

    slider.addEventListener('touchmove', (e) => {
        const touch = e.touches[0];
        const sliderRect = slider.getBoundingClientRect();
        const percentage = Math.max(0, Math.min(100, (touch.clientX - sliderRect.left) / sliderRect.width * 100));
        updateSliderPosition(percentage);
    });

    function updateSliderPosition(value) {
        container.style.setProperty('--position', `${value}%`);
        gradientText.style.backgroundImage = `linear-gradient(90deg, #fff ${value}%-1px, #000 ${value}%+1px)`;
    }
}