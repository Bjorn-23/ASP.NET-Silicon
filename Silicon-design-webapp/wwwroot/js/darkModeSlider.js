document.addEventListener("DOMContentLoaded", function () {

    // This is an attempt to be able to have a slider funtion on an entire partialview, regardless of its size. Its nor perfect but in most cases it works.

    let windowWidth = window.innerWidth
    let imageSize = 0
    let imageStart = 0
    let leftSide = 0
    let rightSide = 0
    const container = document.querySelector('#sliderContainer')
    const slider = document.querySelector('#slider')
    const gradientText = document.querySelector('.gradient-text')
    windowResizeHandler()

    window.addEventListener("resize", function () {
        windowResizeHandler()
    })

    function windowResizeHandler() {
        console.log('resizing')
        if (window.innerWidth >= 1200) {
            windowWidth = document.documentElement.clientWidth            
        }
 
        imageSize = parseFloat(window.getComputedStyle(document.querySelector('#mbp-light')).width);       
        imageStart = Math.abs((windowWidth - imageSize) / 2) // split in 2 provided image is centered
        leftSide = Math.abs((imageStart / windowWidth) * 100)
        rightSide = Math.abs(((windowWidth - imageStart) / windowWidth) * 100)
    }


    if (container && slider && gradientText) {
        slider.addEventListener('input', (e) => {
            updateSliderPosition(e.target.value)
        });

        slider.addEventListener('touchmove', (e) => {
            const touch = e.touches[0];
            const sliderRect = slider.getBoundingClientRect();
            const percentage = Math.max(0, Math.min(100, (touch.clientX - sliderRect.left) / sliderRect.width * 100));
            updateSliderPosition(percentage);
        });

        function updateSliderPosition(value) {
            if (imageSize == windowWidth) {
                container.style.setProperty('--imagePosition', `${value}%`);
            }
            else {
                if (value >= leftSide && value <= (rightSide)) {
                    let imageWidth = (value - leftSide) / (rightSide - leftSide) * 100;
                    imageWidth = Math.round(imageWidth * 100) / 100;
                    container.style.setProperty('--imagePosition', `${imageWidth}%`);
                }
            }

            container.style.setProperty('--position', `${value}%`);
            gradientText.style.backgroundImage = `linear-gradient(90deg, #fff ${value}%-1px, #000 ${value}%+1px)`;
        }
    }
})
