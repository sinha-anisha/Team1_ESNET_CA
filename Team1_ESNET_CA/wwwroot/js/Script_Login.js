window.onload = function () {

    //alert("hiiiiiii");
    let form = document.getElementById("form");
    form.onsubmit = function () {

        let email = document.getElementById("uname");
        let pass = document.getElementById("pwd");



        let username = email.value.trim();
        let password = pass.value.trim();

        if (username.length === 0 || password.length === 0) {

            let err_elem = document.getElementById("err_msg");
            err_elem.innerHTML = "Please fill up all fields.";
            alert(err_elem.innerHTML);
            alert("hghjh");
            return false;
        }
        return true;
    }
}