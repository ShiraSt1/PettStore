newUser = () => {
    const div = document.querySelector("#newUser")
    div.setAttribute("style","visibility:visible")
}

logIn = async () => {
    const login_username = document.querySelector("#login_username").value;
    const login_password = document.querySelector("#login_password").value;
    const newUser = {username: login_username,password: login_password,firstname:"",lastname:""}
    let flag=false;
    try {
        const response = await fetch("https://localhost:7058/api/users/login", {
            method: "POST",
            body: JSON.stringify(newUser),
            headers: { "Content-Type": "application/json" }
        });

        if (!response.ok) {
            throw new Error(`Error: ${response.status} - ${response.statusText}`);
        }
        const user = await response.json();
        sessionStorage.setItem("user", JSON.stringify(user));
        return window.location.href ="https://localhost:7058/site.html"
    } catch (error) {
        alert(error.message);
    }
}

const signUp = async () => {
    const username = document.querySelector("#username").value;
    const lastname = document.querySelector("#lastname").value;
    const firstname = document.querySelector("#firstname").value;
    const password = document.querySelector("#password").value;

    const user = { username, lastname, firstname, password };

    try {
        const response = await fetch("https://localhost:7058/api/users", {
            method: "POST",
            body: JSON.stringify(user),
            headers: { "Content-Type": "application/json" }
        });

        if (!response.ok) {
            throw new Error(`Error: ${response.status} - ${response.statusText}`);
        }

        alert("User added");
    }   catch (error) {
        alert(error.message);
    }
};

checkPassword = async () => {
    const password = document.getElementById("password").value;
    const response = await fetch('https://localhost:7058/api/users/password',
        { method: 'POST', body: JSON.stringify(password), headers: { "Content-Type": 'application/json' } })
    if (!response.ok)
        throw new Error("Http error. status:" + response.status);
    const passStrength = await response.json()
    alert(passStrength > 2 ? "Password is strong" : "Password is weak")
};

const update = async () => {
    const update_username = document.querySelector("#update_username").value;
    const update_lastname = document.querySelector("#update_lastname").value;
    const update_firstname = document.querySelector("#update_firstname").value;
    const update_password = document.querySelector("#update_password").value;

    const userId = JSON.parse(sessionStorage.getItem("user"));

    const user = {
        userId: userId.userId,
        username: update_username,
        lastname: update_lastname,
        firstname: update_firstname,
        password: update_password
    };

    try {
        console.log(user)
        const response = await fetch(`https://localhost:7058/api/users/${user.userId}`, {
            method: "PUT",
            body: JSON.stringify(user),
            headers: { "Content-Type": "application/json" }
        });
        if (!response.ok) {
            throw new Error(`Error: ${response.status} -- ${response.statusText}`);
        }

        alert("User updated");
    } catch (error) {
        alert(error.message);
    }
}; 