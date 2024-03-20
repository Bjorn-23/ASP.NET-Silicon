let passWordField = document.querySelector("#createSubscription")
let passwordToggle = document.querySelector("#subscriptionToggle")

passwordToggle.addEventListener("click", () => {
    console.log("click")
    if (passWordField.classList.contains("hidden"))
        passWordField.classList.remove("hidden")
    else
        passWordField.classList.add("hidden")
})

let forms = document.querySelectorAll('.subscription-delete-wrapper')

forms.forEach(form => {
    let deleteButtons = form.querySelectorAll('.btn-delete-all');

    deleteButtons.forEach(deleteButton => {
        deleteButton.addEventListener('click', function (event) {

            console.log("in eventlistener")
            // Need To style this, if possible
            var confirmed = confirm("Are you sure you want to delete this subscriber? This action cannot be undone.");

            // If the user confirms the deletion, submit the form
            if (confirmed) {
                form.submit();
            }
            else
                event.preventDefault();
        });
    })
})
