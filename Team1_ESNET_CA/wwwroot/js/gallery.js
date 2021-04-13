window.onload = function () {
    let elemList = document.getElementsByClassName("like_icon");

    for (let i = 0; i < elemList.length; i++)
        elemList[i].addEventListener("click", onLike);
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

