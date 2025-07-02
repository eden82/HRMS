document.getElementById("loginBtn").addEventListener("click", () => {
    const username = document.getElementById("username").value;
    const password = document.getElementById("password").value;

    console.log("Attempting login with:", username, password);

    fetch(import.meta.env.VITE_API_URL + '/login', {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ username, password })
    })
        .then(res => {
            if (res.ok) {
                return res.json();
            } else {
                throw new Error("Invalid credentials");
            }
        })
        .then(data => {
            console.log("Login Successful:", data);
            alert(data.message);  // You can replace this with page redirect later
        })
        .catch(err => {
            console.error(err);
            alert("Login failed, check your credentials");
        });
});
