const addButtons = document.querySelectorAll(".add-basket-button");
const box = document.querySelector(".basket-books");

addButtons.forEach(btn => {
    btn.addEventListener("click", function (e) {
        e.preventDefault();

        var endpoint = btn.getAttribute("href");

        fetch(endpoint)
            .then(response => response.text())
            .then(data => {
                box.innerHTML = data;
            })
    })
})


//----------------------------------------------------

const showmodalbtns = document.querySelectorAll(".showmodalbtn");
const modalbox = document.querySelector(".product-details-modal");

showmodalbtns.forEach(btn => {
    btn.addEventListener("click", function (e) {
        e.preventDefault();

        var endpoint = btn.getAttribute("href");

        fetch(endpoint)
            .then(response => response.text())
            .then(data => {
                modalbox.innerHTML = data;
            })
    })
})