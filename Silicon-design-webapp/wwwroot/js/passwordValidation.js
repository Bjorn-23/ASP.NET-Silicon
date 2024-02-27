
const PasswordErrorHandler = (element, validationResult) => {
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


const PasswordValidator = (element) => {
    if (element.dataset.valEqualtoOther !== undefined) {
        let renamedElement = document.getElementsByName(element.dataset.valEqualtoOther.replace('*.', ''))
        let result = CompareValidator(element.value, renamedElement[0].value)
        PasswordErrorHandler(element, result)
    }
    else {
        const regExp = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[^a-zA-Z0-9])(?!.*\s).{8,}$/
        PasswordErrorHandler(element, regExp.test(element.value))
    }
}


let forms = document.querySelectorAll('form')
let inputs = forms[0].querySelectorAll('input')

inputs.forEach(input => {
    if (input.dataset.val === 'true') {
        input.addEventListener('keyup', (e) => {
            PasswordValidator(e.target)
        })        
    }
})
