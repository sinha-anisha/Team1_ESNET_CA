window.onload = function () {

    let error_elem = document.getElementById("error_msg");
    
    let form = document.getElementById("form");

   // form.document.getElementById("uname").focus;

    form.onsubmit = function ()
    {

        let letters = /^[a-zA-Z0-9@_.]+$/;

        let email = document.getElementById("uname").value;
        let pass = document.getElementById("pwd").value;


        let username = email.trim();
        let password = pass.trim();

        if (username.length === 0 || password.length === 0)
        {
            error_elem.innerHTML = "Please fill up all fields.";
            //alert("Please fill up all fields.");
            return false;
        }
        else if (!username.match(letters))
        {
            error_elem.innerHTML ="Please enter letters,numbers,@ and underscores only.";
            //alert("Please enter letters,numbers,@ and underscores only.");
            return false;
        }
        return true;
    }
        let elems = document.getElementsByClassName("form-control");
    for (let i = 0; i < elems.length; i++)
    {
            // remove our error message as long as any 
            // of the input boxes have focus
            elems[i].onfocus = function ()
            {
                error_elem.innerHTML = "";
            }
    }
}