
const FormErrorHandler = (element, validationResult) => {
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
        spanElement.innerHTML = element.dataset.valRequired
    }
}


const EmailValidator = (element) => {
    const regExp = /^\w+([\.-]?w+)*@\w+([\.-]?\w+)*(\.\w{2,})+$/
    FormErrorHandler(element, regExp.test(element.value))
}

const PasswordValidator = (element) => {
    const regExp = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[^a-zA-Z0-9])(?!.*\s).{8,}$/
    FormErrorHandler(element, regExp.test(element.value))
    
}

let forms = document.querySelectorAll('form')
let inputs = forms[0].querySelectorAll('input')

inputs.forEach(input => {
    if (input.dataset.val === 'true') {

        input.addEventListener('keyup', (e) => {
            switch (e.target.type) {

                case 'email':
                    EmailValidator(e.target)
                    break;

                case 'password':
                    PasswordValidator(e.target)
                    break;
            }
        })
        
    }
})
