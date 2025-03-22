async function signup() {
    const username = document.getElementById("username").value;
    const email = document.getElementById("email").value;
    const password = document.getElementById("password").value;

    const response = await fetch("/Auth/Signup", {
        method: "POST",
        headers: { "Content-Type": "application/x-www-form-urlencoded" },
        body: `username=${username}&email=${email}&password=${password}`
    });

    const result = await response.text();
    alert(result);
}

async function login() {
    const email = document.getElementById("email").value;
    const password = document.getElementById("password").value;

    const response = await fetch("/Auth/Login", {
        method: "POST",
        headers: { "Content-Type": "application/x-www-form-urlencoded" },
        body: `email=${email}&password=${password}`
    });

    const result = await response.text();
    alert(result);
}
