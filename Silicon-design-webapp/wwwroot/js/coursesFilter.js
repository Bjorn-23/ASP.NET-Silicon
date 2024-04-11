﻿document.addEventListener("DOMContentLoaded", function () {

    select()
    searchQuery()
})

function select() {
    try {
        let select = document.querySelector(".select")
        let selected = select.querySelector(".selected")
        let selectOptions = select.querySelector(".select-options")

        //if eventlistener is attached to selected instead, svg icon can not be clicked. 
        select.addEventListener("click", function () {
            selectOptions.style.display = (selectOptions.style.display === 'block') ? 'none' : 'block'
        })

        let options = selectOptions.querySelectorAll(".option")

        options.forEach(function (option) {
            option.addEventListener('click', function () {
                selected.innerHTML = this.textContent
                //selectOptions.style.display = "none" //If using selected as eventlistener for dropdown menu, enable this line of code.
                let category = this.getAttribute('data-value')
                if (category !== null) {
                    selected.setAttribute('data-value', category)
                }
                else {
                    selected.setAttribute('data-value', "all")
                }
                updateCoursesByFilters()
            })
        })

    }
    catch { }
}

function searchQuery() {
    try {
        document.querySelector('#searchQuery').addEventListener('keyup', function () {
            updateCoursesByFilters()
        })
    }
    catch { }
}

function updateCoursesByFilters() {
    try {
        const category = document.querySelector('.select .selected').getAttribute('data-value') || "all"

        const searchQuery = document.querySelector('#searchQuery').value
        const url = `/courses?category=${encodeURIComponent(category)}&searchQuery=${encodeURIComponent(searchQuery)}`

        fetch(url).then(res => res.text()).then(data => {
            const parser = new DOMParser()
            const dom = parser.parseFromString(data, 'text/html')
            document.querySelector('.items').innerHTML = dom.querySelector('.items').innerHTML

            const pagination = dom.querySelector('.pagination') ? dom.querySelector('.pagination').innerHTML : ""
            document.querySelector('.pagination').innerHTML = pagination
        })
    }
    catch { }
}


function submitForm(event) {
    event.preventDefault();

    var submitButtons = document.querySelectorAll(".bookmark-form");
    submitButtons.forEach(function (button) {
        button.addEventListener('click', function () {
            // Find the parent form of the clicked button
            var form = button.closest('form');

            form.submit();
        });
    });
}