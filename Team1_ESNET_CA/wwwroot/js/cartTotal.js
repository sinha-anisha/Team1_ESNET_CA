function initcart() {
    var currentTotal = parseInt(sessionStorage.getItem("cartTotal"));
    return currentTotal;
}

function add() {
    var currentTotal = initcart() + 1;
    var total = currentTotal;
    sessionStorage.setItem("cartTotal", total.toString());

    var dispaydiv = document.getElementById("displaycart");
    dispaydiv.innerHTML = total;
}