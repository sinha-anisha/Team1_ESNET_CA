window.onload = function () {

    let error_elem = document.getElementById("error_msg");

    
    let form = document.getElementById("form");

    // form.document.getElementById("uname").focus;

    form.onsubmit = function () {

        let validmail = /^[a-zA-Z0-9@_.]+$/;
        let letters = /^[a-zA-Z]+$/;
        let validmobile = /^[0-9]+$/;
        let validpwd = /^[a-zA_Z0-9@_]+$/;

        let fname = document.getElementById("fname").value.trim();
        let lname = document.getElementById("lname").value.trim();
        let mail = document.getElementById("email").value.trim();
        let mobile = document.getElementById("mobile").value.trim();
        let pwd = document.getElementById("pwd").value.trim();
        let confirmpwd = document.getElementById("confirmpwd").value.trim();



        if (fname.length === 0 || lname.length === 0 || mail.length === 0 || mobile.length === 0 || pwd.length === 0 || confirmpwd.length === 0 )
        {

            //alert("Please fill up all fields.");

            error_elem.innerHTML = "Please fill up all fields.";
            return false;
        }
        else if (!fname.match(letters))
        {
            error_elem.innerHTML = "Please enter only letters  for First Name.";

            //alert("Please enter only letters  for First Name.");
            return false;
        }
        else if (!lname.match(letters))
        {
            error_elem.innerHTML = "Please enter only letters for Last Name";

            //alert("Please enter only letters for Last Name.");
            return false;
        }
        else if (!mail.match(validmail))
        {
            error_elem.innerHTML = "Please enter letters,digits,@ and underscores only.";

            //alert("Please enter letters,digits,@ and underscores only.");
            return false;
        }
        else if (!mobile.match(validmobile))
        {
            error_elem.innerHTML = "Please enter only digits for mobile no. ";

            //alert("Please enter only digits for mobile no.");
            return false;
        }
        else if (!pwd.match(validpwd))
        {
            error_elem.innerHTML = "Please enter letters,numbers,@ and underscores only. ";

            //alert("Please enter letters,numbers,@ and underscores only.");
            return false;
        }
        else if (!confirmpwd.match(validpwd))
        {
            error_elem.innerHTML = "Please enter letters,numbers,@ and underscores only.";

            //alert("Please enter letters,numbers,@ and underscores only.");
            return false;
        }
        else if (confirmpwd != pwd)
        {
            error_elem.innerHTML = "Password does not match.";

            //alert("Password does not match.");
            return false;
        }
        return true;
    }
}