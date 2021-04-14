


window.onload = function () {
    var paystatus = "false";
    var datastatus = "false";
    var paycount = "0";
    var datacount = "0";

    var getpaystatus = sessionStorage.getItem("payselected");
    var getdatastatus = sessionStorage.getItem("dataselected");

    sessionStorage.setItem("paycount", paycount);
    sessionStorage.setItem("datacount", datacount)
    let elemList = document.getElementsByClassName("like_icon");

    for (let i = 0; i < elemList.length; i++)
        elemList[i].addEventListener("click", onLike);
}
if (getpaystatus == "true") {
    paystatus = "true";
}
else {
    paystatus = "false";
}
if (getdatastatus == "true") {
    datastatus = "true";
}
else {
    datastatus = "false";
}

loginname = "Guest";

var namediv = document.getElementById("name");
var cartdiv = document.getElementById("cartid");

var getcount = sessionStorage.getItem("count");
var storedcount = getcount;

namediv.innerHTML = loginname;

if ((paystatus == "true") && (datastatus == "false")) {
    storedcount = 1;
}
else if ((paystatus == "false") && (datastatus == "true")) {
    storedcount = 1;
}
else if ((paystatus == "true") && (datastatus === "true")) {
    storedcount = 2;
}
else if ((paystatus == "false") && (datastatus == "false")) {
    storedcount = 0;
}

var updatedcount = storedcount.toString();
sessionStorage.setItem("count", updatedcount);

var cartdiv = document.getElementById("cartid");
cartdiv.innerHTML = updatedcount;

}


function addItem() {
    var storedcount = 0;
    var paystatus = "false";
    var datastatus = "false";

    var getpaystatus = sessionStorage.getItem("payselected");
    var getdatastatus = sessionStorage.getItem("dataselected");

    if (getpaystatus == "true") {
        paystatus = "true";
    }
    else {
        paystatus = "false";
    }
    if (getdatastatus == "true") {
        datastatus = "true";
    }
    else {
        datastatus = "false";
    }

    if ((paystatus == "true") && (datastatus == "false")) {
        storedcount = 1;
    }
    else if ((paystatus == "false") && (datastatus == "true")) {
        storedcount = 1;
    }
    else if ((paystatus == "true") && (datastatus === "true")) {
        storedcount = 2;
    }
    else if ((paystatus == "false") && (datastatus == "false")) {
        storedcount = 0;
    }

    var updatedcount = storedcount.toString();
    sessionStorage.setItem("count", updatedcount);


    var cartdiv = document.getElementById("cartid");
    cartdiv.innerHTML = updatedcount;

}


function addPay() {
    var payinfo = "Payment Module";
    var payquantity = "1";
    var payselected = "true";


    sessionStorage.setItem("payproduct", payinfo);
    sessionStorage.setItem("payquantity", payquantity);
    sessionStorage.setItem("payselected", payselected);

    var getpaycount = sessionStorage.getItem("paycount");

    var paycount = parseInt(getpaycount);

    paycount = paycount + 1;

    sessionStorage.setItem("paycount", paycount.toString());
}


function addData() {
    var datainfo = "Database Module";
    var dataquantity = "1";
    var dataselected = "true";


    sessionStorage.setItem("dataproduct", datainfo);
    sessionStorage.setItem("dataquantity", dataquantity);
    sessionStorage.setItem("dataselected", dataselected);

    var getdatacount = sessionStorage.getItem("datacount");

    var datacount = parseInt(getdatacount);

    datacount = datacount + 1;

    sessionStorage.setItem("datacount", datacount.toString());
}

function add() {

    var getpaycount = sessionStorage.getItem("paycount");
    var getdatacount = sessionStorage.getItem("datacount");
    var convertpaycount = parseInt(getpaycount);
    var convertdatacount = parseInt(getdatacount);
    var totalcount = convertpaycount + convertdatacount;
    sessionStorage.setItem("totalcount", totalcount.toString());

    var totaldiv = document.getElementById("total");
    totaldiv.innerHTML = sessionStorage.getItem("totalcount");

}


function logIn() {
    var payinfo = sessionStorage.getItem("payproduct", payinfo);
    var payquantity = sessionStorage.getItem("payquantity", payquantity);
    var payselected = sessionStorage.getItem("payselected", payselected);

    var datainfo = sessionStorage.getItem("dataproduct", datainfo);
    var dataquantity = sessionStorage.getItem("dataquantity", dataquantity);
    var dataselected = sessionStorage.getItem("dataselected", dataselected);

    localStorage.setItem("payproduct", payinfo);
    localStorage.setItem("payquantity", payquantity);
    localStorage.setItem("payselected", payselected);

    localStorage.setItem("dataproduct", datainfo);
    localStorage.setItem("dataquantity", dataquantity);
    localStorage.setItem("dataselected", dataselected);

    var paystatus = "false";
    var datastatus = "false";

    var getpaystatus = sessionStorage.getItem("payselected");
    var getdatastatus = sessionStorage.getItem("dataselected");

    if (getpaystatus == "true") {
        paystatus = "true";
    }
    else {
        paystatus = "false";
    }
    if (getdatastatus == "true") {
        datastatus = "true";
    }
    else {
        datastatus = "false";
    }

    if ((paystatus == "true") && (datastatus == "false")) {
        storedcount = 1;
    }
    else if ((paystatus == "false") && (datastatus == "true")) {
        storedcount = 1;
    }
    else if ((paystatus == "true") && (datastatus === "true")) {
        storedcount = 2;
    }
    else if ((paystatus == "false") && (datastatus == "false")) {
        storedcount = 0;
    }

    var updatedcount = storedcount.toString();
    localStorage.setItem("count", updatedcount);

    var getpaycount = sessionStorage.getItem("paycount");
    var getdatacount = sessionStorage.getItem("datacount");

    localStorage.setItem("paycount", getpaycount);
    localStorage.setItem("datacount", getdatacount);

    alert("Are you sure you want to login?");
    window.open("Login.html", "_blank");
}

window.onunload = function clearCart() {
    sessionStorage.clear()
}

function onLike(event) {
    let elem = event.currentTarget;
    let elemId = elem.getAttribute("id");

    if (elem.getAttribute("src") === "/img/like.png")
        sendLike(false, elemId);
    else
        sendLike(true, elemId);
}



function sendLike(likeIt, elemId) {
    let xhr = new XMLHttpRequest();

    xhr.open("POST", "/Emo/Like");
    xhr.setRequestHeader("Content-Type", "application/json; charset=utf8");
    xhr.onreadystatechange = function () {
        // done receiving streaming bytes
        if (this.readyState === XMLHttpRequest.DONE) {
            // check if HTTP operation is okay
            if (this.status !== 200)
                return;

            let data = JSON.parse(this.responseText);

            // if some error on server, don't update client's view
            if (!data.success)
                return;

            let elem = document.getElementById(elemId)
            if (!elem)
                return;

            if (likeIt)
                elem.setAttribute("src", "/img/like.png");
            else
                elem.setAttribute("src", "/img/like_gray.png");
        }
    };

    // send like/unlike choice to server
    xhr.send(JSON.stringify({
        PhotoId: elemId,
        LikeIt: likeIt
    }));
}

