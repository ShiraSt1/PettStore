newUser = () => {
    const div = document.querySelector("#newUser")
    div.setAttribute("style","visibility:visible")
}

logIn = async () => {
    const login_username = document.querySelector("#login_username").value;
    const login_password = document.querySelector("#login_password").value;
    const newUser = {Username: login_username,Password: login_password,Firstname:"",Lastname:""}
    let flag=false;
    try {
        const response = await fetch("https://localhost:44337/api/users/login", {
            //const response = await fetch("https://localhost:7058/api/users/login", {
            method: "POST",
            body: JSON.stringify(newUser),
            headers: { "Content-Type": "application/json" }
        });

        if (!response.ok) {
            throw new Error(`Error: ${response.status} - ${response.statusText}`);
        }
        const user = await response.json();
        sessionStorage.setItem("user", JSON.stringify(user));
        return window.location.href ="https://localhost:44337/site.html"
    } catch (error) {
        alert(error.message);
    }
}

const signUp = async () => {
    const Username = document.querySelector("#username").value;
    const Lastname = document.querySelector("#lastname").value;
    const Firstname = document.querySelector("#firstname").value;
    const Password = document.querySelector("#password").value;

    const user = { Username, Lastname, Firstname, Password };

    try {
        const response = await fetch("https://localhost:44337/api/users", {
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
    const Password = document.getElementById("password").value;
    const response = await fetch('https://localhost:44337/api/users/password',
        { method: 'POST', body: JSON.stringify(Password), headers: { "Content-Type": 'application/json' } })
    if (!response.ok) {
        alert("Http error. status:" + response.status);
        throw new Error("Http error. status:" + response.status);
    }
    const passStrength = await response.json()
    alert(passStrength ? "Password is strong" : "Password is weak")
};

const update = async () => {
    const update_username = document.querySelector("#update_username").value;
    const update_lastname = document.querySelector("#update_lastname").value;
    const update_firstname = document.querySelector("#update_firstname").value;
    const update_password = document.querySelector("#update_password").value;

    const userId = JSON.parse(sessionStorage.getItem("user"));

    const user = {
        Id: userId.id,
        Username: update_username,
        Lastname: update_lastname,
        Firstname: update_firstname,
        Password: update_password
    };

    try {
        const response = await fetch(`https://localhost:44337/api/users/${user.Id}`, {
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