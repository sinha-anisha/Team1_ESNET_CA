window.onload = function () {

    
    
    let form = document.getElementById("form");

   // form.document.getElementById("uname").focus;

    form.onsubmit = function () {

        let letters = /^[a-zA-Z0-9@_]+$/;

        let email = document.getElementById("uname").value;
        let pass = document.getElementById("pwd").value;

       
        let username = email.trim();
        let password = pass.trim();

        if (username.length === 0 || password.length === 0)
        {

            alert("Please fill up all fields.");
            return false;
        }
        else if (!username.match(letters))
        {
                alert("Please enter letters,numbers,@ and underscores only.");
            return false;
        }
        return true;
    }
}