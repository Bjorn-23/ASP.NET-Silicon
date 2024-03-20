let passWordField = document.querySelector("#createSubscription")
let passwordToggle = document.querySelector("#subscriptionToggle")

passwordToggle.addEventListener("click", () => {
    console.log("click")
    if (passWordField.classList.contains("hidden"))
        passWordField.classList.remove("hidden")
    else
        passWordField.classList.add("hidden")
})