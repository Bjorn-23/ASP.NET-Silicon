document.addEventListener("DOMContentLoaded", function () {
     

    ///----- mobile menu ---------------------------------------------
    const mobileBtn = document.querySelector("#btn-mobile")

    mobileBtn.addEventListener('click', () => {
        const menu = document.querySelector('#mobile-menu')

        const isOpen = menu.getAttribute('aria-expanded') === 'true'
        //console.log(isOpen)
        menu.setAttribute('aria-expanded', !isOpen)
    })




    ///----- Dark/Light mode switch ----------------------------------

    const sw = document.querySelectorAll('#switchMode')

    sw.forEach(sw => {
        sw.addEventListener("change", function () {
            let theme = this.checked ? "dark" : "light"

            fetch(`/sitesettings/changetheme?theme=${theme}`)
                .then(res => {
                    if (res.ok)
                        window.location.reload()
                    else
                        console.log("Error, please contact site owner if issue persists")
                })
        })
    })



    ///----- Scroll to top button ----------------------------------
    const scrollButton = document.querySelector("#scrollToTop")

    window.addEventListener('scroll', function () {
        //console.log("eventlistener working")
        if (window.scrollY >= 600 && scrollButton.classList.contains('hide-btn')) {
            scrollButton.classList.remove('hide-btn')
        }
        if (window.scrollY < 600 && !scrollButton.classList.contains('hide-btn')) {
 
            scrollButton.classList.add('hide-btn')
        }
    })

    scrollButton.addEventListener("click", function () {
        window.scrollTo({ top: 0, behavior: 'smooth' })
    })


})