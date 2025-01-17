
const getDetailsSignUp = () => {
    return newUser = {
        UserName: document.querySelector("#UserNameSignUp").value,
        FirstName: document.querySelector("#FirstName").value,
        LastName: document.querySelector("#LastName").value,
        Password: document.querySelector("#PasswordSignUp").value
    }
}

const getDetailsLogIn = () => {
    return newUser = {
        UserName: document.querySelector("#UserNameLogin").value,
        Password: document.querySelector("#PasswordLogin").value

    }
}


const showSignUp = () => {

    const signUp = document.querySelector(".signUpDiv")
    signUp.classList.remove("signUpDiv")


}

const checkPassword = async () => {
    let password = document.querySelector("#PasswordSignUp").value
    let result = document.querySelector("#CheckPassword")
    try {
        const responseCheckPassword = await fetch(`https://localhost:44351/api/Users/password/?password=${password}`, {
            method: 'POST',
            headers: {
                'content-Type': 'application/json'
            }
        })
        if (!responseCheckPassword.ok) {
            const DataPost = await responseCheckPassword.json();
            result.value = DataPost
            throw new Error("סיסמה לא חזקה")
        }
        const DataPost = await responseCheckPassword.json();
        result.value = DataPost

    }
    catch (error) {
        throw (error)
    }

}
const addNewUser = async () => {
    const newUser = getDetailsSignUp();
    
    try {
       await checkPassword();
        const responsePost = await fetch(`https://localhost:44351/api/Users`, {
            method: 'POST',
            headers: {
                'content-Type': 'application/json'
            },
            body: JSON.stringify(newUser)
        })
        if (!responsePost.ok)
            throw new Error(`error status :${responsePost.status}`)
        const dataPost = await responsePost.json();
     alert("משתמש נוסף בהצלחה")
    }
    catch (error) {
        alert(error)
    }
}

const logInUser = async () => {
    const user = getDetailsLogIn();
    try {
        const responsePost = await fetch(`https://localhost:44351/api/Users/login?UserName=${user.UserName}&Password=${user.Password}`, {
            method: 'POST',
            headers: {
                'content-Type': 'application/json'
            },

        })
        if (!responsePost.ok) {
            throw new Error(`error status :${responsePost.status}`)

        }
        try {
            const dataPost = await responsePost.json();
            if (dataPost) {
                console.log(dataPost)
                sessionStorage.setItem("user", JSON.stringify(dataPost))
                window.location.href = "ShoppingBag.html";

            }
        }
        catch (error) {
            alert("משתמש לא רשום")
        }
    }
    catch (error) {
        alert("שגיאה בכניסה למערכת")
    }
}





