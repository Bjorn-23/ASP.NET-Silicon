
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

const CompareValidator = (element, comparisonValue) => {
    if (element === comparisonValue)
        return true
    else
    return false
}

const TextValidator = (element, minLength = 2) => {    
    if (element.value.length >= minLength) {
        FormErrorHandler(element, true)
    }
    else {
        FormErrorHandler(element, false)
    }
}

const EmailValidator = (element) => {
    const regExp = /^\w+([\.-]?w+)*@\w+([\.-]?\w+)*(\.\w{2,})+$/
    FormErrorHandler(element, regExp.test(element.value))
}

const PasswordValidator = (element) => {
    if (element.dataset.valEqualtoOther !== undefined) {
        let renamedElement = document.getElementsByName(element.dataset.valEqualtoOther.replace('*', 'Form'))
        let result = CompareValidator(element.value, renamedElement[0].value)
        FormErrorHandler(element, result)
    }
    else {
        const regExp = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[^a-zA-Z0-9])(?!.*\s).{8,}$/
        FormErrorHandler(element, regExp.test(element.value))
    }
}

const CheckboxValidator = (element) => {
    if (element.checked) {
        FormErrorHandler(element, true)
    }
    else {
        FormErrorHandler(element, false)
    }
}

let forms = document.querySelectorAll('form')
let inputs = forms[0].querySelectorAll('input')

inputs.forEach(input => {
    if (input.dataset.val === 'true') {

        if (input.type === 'checkbox') {
            input.addEventListener('change', (e) => {
                CheckboxValidator(e.target)
            })
        }
        else {
            input.addEventListener('keyup', (e) => {
                switch (e.target.type) {

                    case 'text':
                        TextValidator(e.target)
                        break;

                    case 'email':
                        EmailValidator(e.target)
                        break;

                    case 'password':
                        PasswordValidator(e.target)
                        break;
                }
            })
        }         
    }
})
