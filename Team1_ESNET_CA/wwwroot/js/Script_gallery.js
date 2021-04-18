window.onload = function () {


    var xhttp = new XMLHttpRequest();

    xhttp.onreadystatechange = function () {
        console.log(this.responseText);
    };

    xhttp.open("POST", "/Cart/Index", true);
    xhttp.setRequestHeader('Content-type', 'application/x-www-form-urlencoded');

}
