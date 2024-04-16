document.addEventListener("DOMContentLoaded", function () {

    Validate()
})

function Validate() {
    try {
        const FormErrorHandler = (element, validationResult) => {
            let spanElement = document.querySelector(`[data-valmsg-for="${element.name}"]`)

            if (element.type === 'checkbox') {
                spanElement.innerHTML = element.dataset.valRequired;
            }
            else {

                if (validationResult) {
                    element.classList.remove('input-validation-error')
                    spanElement.classList.remove('field-validation-error')
                    spanElement.classList.add('field-validation-valid')
                    spanElement.innerHTML = ""
                }
                else {
                    element.classList.add('input-validation-error')
                    spanElement.classList.add('field-validation-error')
                    spanElement.classList.remove('field-validation-valid')

                    if (element.value === null || element.value === "" || element.type === 'checkbox') {
                        spanElement.innerHTML = element.dataset.valRequired;
                    } else {
                        if (element.hasAttribute("data-val-regex")) {
                            spanElement.innerHTML = element.dataset.valRegex;
                        }
                        else if (element.hasAttribute("data-val-equalto") && element.value !== "") {
                            spanElement.innerHTML = element.dataset.valEqualto;
                        }
                        else if (element.hasAttribute("data-val-minlength")) {
                            spanElement.innerHTML = element.dataset.valMinlength;
                        }
                    }
                }

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
            const regExp = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,})+$/
            FormErrorHandler(element, regExp.test(element.value))
        }

        const PasswordValidator = (element) => {
            if (element.dataset.valEqualtoOther !== undefined) {
                // for signup to change passwords
                if (element.dataset.valEqualtoOther == "*.Password") {
                    console.log("equal")
                    let renamedElement = document.getElementsByName(element.dataset.valEqualtoOther.replace('*', 'Form'))
                    let result = CompareValidator(element.value, renamedElement[0].value)
                    FormErrorHandler(element, result)
                }
                // for account security - change passwords
                else if (element.dataset.valEqualtoOther == "*.NewPassword") {
                    console.log("equal 2")
                    let renamedElement = document.getElementsByName(element.dataset.valEqualtoOther.replace('*.', ''))
                    let result = CompareValidator(element.value, renamedElement[0].value)
                    FormErrorHandler(element, result)
                }
            }
            else {
                const regExp = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[^a-zA-Z0-9])(?!.*\s).{8,}$/
                FormErrorHandler(element, regExp.test(element.value))
            }
        }

        const PostalCodeValidator = (element) => {
            const regExp = /^\d{3}\s\d{2}$/
            FormErrorHandler(element, regExp.test(element.value))
        }

        const CheckboxValidator = (element) => {
            if (element.checked) {
                CheckboxErrorHandler(element, true)
            }
            else {
                CheckboxErrorHandler(element, false)
            }
        }

        const textarea = document.querySelector('textarea[name="Message"]');
        if (textarea != null) {
            textarea.addEventListener('keyup', (e) => {
                TextValidator(e.target);
            });
        }

        let forms = document.querySelectorAll('form')

        forms.forEach(form => {
            let inputs = form.querySelectorAll('input');

            inputs.forEach(input => {
                if (input.dataset.val === 'true') {

                    if (input.type === 'checkbox') {
                        input.addEventListener('change', (e) => {
                            CheckboxValidator(e.target)
                        })
                    }

                    else if (input.name.toLowerCase().includes('postalcode')) {
                        input.addEventListener('keyup', (e) => {
                            PostalCodeValidator(e.target)
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

                                default:
                                    if (e.target.id.includes('postalCode') || e.target.name.includes('postalCode')) {
                                        PostalCodeValidator(e.target);
                                    }
                                    break;
                            }
                        })
                    }
                }
            });
        });

        const deleteButton = document.querySelector('#deleteButton');
        let form = document.querySelector('#deleteAccount')

        if (form != undefined && deleteButton != undefined) {
            /* Add a click event listener to the delete button*/
            deleteButton.addEventListener('click', function (event) {
                // Need To style this, if possible
                let confirmed = confirm("Are you sure you want to delete your account? This action cannot be undone.");

                // If the user confirms the deletion, submit the form
                if (confirmed) {
                    FormErrorHandler(form);
                    form.submit();
                } else {
                    // Prevent the default action (form submission) if the user cancels
                    event.preventDefault();
                }
            });
        }

    }
    catch { }
}

