document.addEventListener("DOMContentLoaded", function () {

    pageSizeSelect()
    categorySelect()
    searchQuery()
    submitForm()
})

//const pageSizeSelector = document.querySelector("#pageSizeSelect")
//const selectedPageSize = pageSizeSelector.querySelector("#selectedPageSize")
//const pageSizeOptions = pageSizeSelector.querySelector("#pageSizeOptions")

//const categorySelector = document.querySelector("#selectCategory")
//const selectedCategory = categorySelector.querySelector("#selectedCategory")
//const categoryOptions = categorySelector.querySelector("#categorySelectOptions")

function pageSizeSelect() {
    try {
        const pageSizeSelector = document.querySelector("#pageSizeSelect")
        const selectedPageSize = pageSizeSelector.querySelector("#selectedPageSize")
        const pageSizeOptions = pageSizeSelector.querySelector("#pageSizeOptions")
        //if eventlistener is attached to selectedPageSize instead, svg icon can not be clicked.
        pageSizeSelector.addEventListener("click", function () {
            pageSizeOptions.style.display = (pageSizeOptions.style.display === 'block') ? 'none' : 'block'
            document.querySelector('#categorySelectOptions').style.display = 'none'
        })

        let options = pageSizeOptions.querySelectorAll(".option")

        options.forEach(function (Option) {
            Option.addEventListener('click', function () {
                selectedPageSize.innerHTML = this.textContent
                let pageSize = this.getAttribute('data-value')
                selectedPageSize.setAttribute('data-value', pageSize)
                updateCoursesByFilters()
            })
        })

    }
    catch { }
}

function categorySelect() {
    try {
        const categorySelector = document.querySelector("#selectCategory")
        const selectedCategory = categorySelector.querySelector("#selectedCategory")
        const categoryOptions = categorySelector.querySelector("#categorySelectOptions")
        //if eventlistener is attached to selectedCategory instead, svg icon can not be clicked.
        categorySelector.addEventListener("click", function () {
            categoryOptions.style.display = (categoryOptions.style.display === 'block') ? 'none' : 'block'
            document.querySelector('#pageSizeOptions').style.display = 'none'
        })

        let options = categoryOptions.querySelectorAll(".option")

        options.forEach(function (option) {
            option.addEventListener('click', function () {
                selectedCategory.innerHTML = this.textContent
                let category = this.getAttribute('data-value')
                if (category !== null) {
                    selectedCategory.setAttribute('data-value', category)
                }
                else {
                    selectedCategory.setAttribute('data-value', "all")
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
        const pageSize = document.querySelector("#selectedPageSize").getAttribute('data-value').toString()
        const category = document.querySelector('#selectedCategory').getAttribute('data-value') || "all"
        const searchQuery = document.querySelector('#searchQuery').value
        const url = `/courses?category=${encodeURIComponent(category)}&searchQuery=${encodeURIComponent(searchQuery)}&pageSize=${encodeURIComponent(pageSize)}`
               
        //console.log(category)
        //console.log(pageSize)
        //console.log(searchQuery)
        //console.log(url)

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
    try {
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
    catch { }
}