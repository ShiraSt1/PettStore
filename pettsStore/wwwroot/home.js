






newUser = () => {
    const div = document.querySelector("#newUser")
    div.setAttribute("style","visibility:visible")
}



let userId; 
logIn = async () => {
    const login_username = document.querySelector("#login_username").value;
    const login_password = document.querySelector("#login_password").value;
    userId = login_username;
    console.log(userId)
    let flag=false;
    try {
        const response = await fetch("https://localhost:7058/api/users")
        if (!response.ok) {
            throw new Error("Error adding new user")
        }
        const data = await response.json()
        data.forEach(user => {
            if ((user.username).toString() === login_username && user.password === login_password) {
                flag = true;
                window.location.href = "https://localhost:7058/site.html";
                return;
            }
        });
        if (!flag) {
            alert("Username or password are not correct");

        }
        
        }
    catch (error) {
        alert(error)
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

const update = async () => {
    const update_username = document.querySelector("#update_username").value;
    const update_lastname = document.querySelector("#update_lastname").value;
    const update_firstname = document.querySelector("#update_firstname").value;
    const update_password = document.querySelector("#update_password").value;

    const user = {
        username: update_username,
        lastname: update_lastname,
        firstname: update_firstname,
        password: update_password
    };

    try {
        //userId = 1;
        console.log(userId)
        const response = await fetch(`https://localhost:7058/api/users/${userId}`, {
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