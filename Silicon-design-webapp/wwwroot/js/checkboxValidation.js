
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
let checkboxInputs = checkboxForms[1].querySelectorAll('input')

checkboxInputs.forEach(input => {
    if (input.dataset.val === 'true') {

        input.addEventListener('change', (e) => {
            CheckboxValidator(e.target)
        })      
    }
})

var deleteButton = document.querySelector('.delete-button');
var form = document.querySelector('.delete-account')

/* Add a click event listener to the delete button*/
deleteButton.addEventListener('click', function (event) {
    // Need To style this, if possible
    var confirmed = confirm("Are you sure you want to delete your account? This action cannot be undone.");

    // If the user confirms the deletion, submit the form
    if (confirmed) {
        form.submit();
    }
    else
        event.preventDefault();
});