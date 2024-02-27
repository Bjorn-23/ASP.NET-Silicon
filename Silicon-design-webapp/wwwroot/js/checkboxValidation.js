
const CheckboxErrorHandler = (element, validationResult) => {
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

const CheckboxValidator = (element) => {
    if (element.checked) {
        CheckboxErrorHandler(element, true)
    }
    else {
        CheckboxErrorHandler(element, false)
    }
}

let checkboxForms = document.querySelectorAll('form')
let checkboxInputs = checkboxForms[0].querySelectorAll('input')

checkboxInputs.forEach(input => {
    if (input.dataset.val === 'true') {

        if (input.type === 'checkbox') {
            input.addEventListener('change', (e) => {
                CheckboxValidator(e.target)
            })
        }        
    }
})
