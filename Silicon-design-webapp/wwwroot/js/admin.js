
let passWordField = document.querySelector("#AdminPasswordField")
let passwordToggle = document.querySelector("#FormChangePasswordButton")

passwordToggle.addEventListener("click", () => {
    console.log("click")
    if (passWordField.classList.contains("hidden"))
        passWordField.classList.remove("hidden")
    else
        passWordField.classList.add("hidden")
})




