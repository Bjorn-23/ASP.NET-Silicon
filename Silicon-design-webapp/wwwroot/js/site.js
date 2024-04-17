document.addEventListener("DOMContentLoaded", function () {
     

    ///----- mobile menu ---------------------------------------------
    const mobileBtn = document.querySelector("#btn-mobile")

    mobileBtn.addEventListener('click', () => {
        const menu = document.querySelector('#mobile-menu')

        const isOpen = menu.getAttribute('aria-expanded') === 'true'
        //console.log(isOpen)
        menu.setAttribute('aria-expanded', !isOpen)
    })

    ///----- mobile menu ---------------------------------------------

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

})