
const EmailErrorHandler = (element, validationResult) => {
    let spanElement = document.querySelector(`[data-valmsg-for="${element.name}"]`)

    if (validationResult) {
        element.classList.remove('input-validation-error')
        spanElement.classList.remove('field-validation-error')
        spanElement.classList.add('field-validation-valid')
        spanElement.innerHTML = ''
    }
    else {
        element.classList.add('input-validation-error')
        spanElement.classList.add('field-validation-error')
        spanElement.classList.remove('field-validation-valid')
        if (element.value === "") {
            spanElement.innerHTML = element.dataset.valRequired
        }
        else
            spanElement.innerHTML = element.dataset.valRegex
    }
}


const EmailValidator = (element) => {
    const regExp = /^\w+([\.-]?w+)*@\w+([\.-]?\w+)*(\.\w{2,})+$/
    EmailErrorHandler(element, regExp.test(element.value))
}


let emailForms = document.querySelectorAll('form')
let emailInputs = emailForms[0].querySelectorAll('input')

emailInputs.forEach(input => {
    if (input.dataset.val === 'true') {
        input.addEventListener('keyup', (e) => {
            EmailValidator(e.target)
        })
    }
})
