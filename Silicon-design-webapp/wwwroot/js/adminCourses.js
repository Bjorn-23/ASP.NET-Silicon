
const form = document.querySelector("#coursesForm")
const toggle = document.querySelector("#showAddCourseButton")

toggle.addEventListener("click", () => {
    console.log("click")
    if (form.classList.contains("hidden"))
        form.classList.remove("hidden")
    else
        form.classList.add("hidden")
})