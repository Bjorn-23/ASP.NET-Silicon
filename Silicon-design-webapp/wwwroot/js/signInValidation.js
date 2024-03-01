
const SignInErrorHandler = (element, validationResult) => {
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
        if (element.value === null || element.value === "" || element.type === 'checkbox') {
            spanElement.innerHTML = element.dataset.valRequired;
        } else if (element.hasAttribute("data-val-regex")) {
                spanElement.innerHTML = element.dataset.valRegex;
        }
    }
}


const EmailValidator = (element) => {
    const regExp = /^\w+([\.-]?w+)*@\w+([\.-]?\w+)*(\.\w{2,})+$/
    SignInErrorHandler(element, regExp.test(element.value))
}

const PasswordValidator = (element) => {
    const regExp = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[^a-zA-Z0-9])(?!.*\s).{8,}$/
    SignInErrorHandler(element, regExp.test(element.value))
    
}

let signInForms = document.querySelectorAll('form')
let signInInputs = signInForms[0].querySelectorAll('input')

signInInputs.forEach(input => {
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
