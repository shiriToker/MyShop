const getInputValue = (selector) => document.querySelector(selector)?.value.trim() || "";

const getAllDetailsForLogin = () => ({
    UserName: getInputValue("#UserNameLogin"),
    Password: getInputValue("#PasswordLogin"),
});

const getAllDetailsForSignUp = () => {
    const newUser = {
        UserName: getInputValue("#userName"),
        Password: getInputValue("#password"),
        FirstName: getInputValue("#firstName"),
        LastName: getInputValue("#lastName"),
    };

    if (Object.values(newUser).some(value => !value)) {
        alert("כל השדות הם חובה. נא למלא את כולם.");
        return null;
    }
    
    if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(newUser.UserName)) {
        alert("כתובת האימייל אינה תקינה.");
        return null;
    }

    if (newUser.FirstName.length > 20 || newUser.LastName.length > 20) {
        alert("שם פרטי ושם משפחה עד 20 תווים בלבד.");
        return null;
    }

    return newUser;
};

const showRegister = () => document.querySelector('.signUpDiv').style.display = 'block';;


const checkPassword = async () => {
    let password = getInputValue("#PasswordSignUp")
    let result = document.querySelector("#CheckPassword")
    try {
        const responseCheckPassword = await fetch(`api/Users/password/?password=${password}`, {
            method: 'POST',
            headers: {
                'content-Type': 'application/json'
            }
        })
        const DataPost = await responseCheckPassword.json();
            result.value = DataPost

        if (!responseCheckPassword.ok) {
            throw new Error("סיסמה לא חזקה")
        }

    }
    catch (error) {
        throw (error.message)
    }

}
const addNewUser = async () => {
    const newUser = getAllDetailsForSignUp();
    if (!newUser) return

    try {
        await checkPassword();
        const responsePost = await fetch(`api/Users`, {
            method: 'POST',
            headers: {
                'content-Type': 'application/json'
            },
            body: JSON.stringify(newUser)
        })
        if (responsePost.status == 400)
            throw new Error(`!כל השדות חובה, בדוק את תקינותם`)
        if (!responsePost.ok)
            throw new Error(`משהו השתבש, נסה שוב`)
        alert("משתמש נוסף בהצלחה")
    }

    catch (error) {
        alert(error.message)
    }
}

const isValidUser = (user) => user.UserName && user.Password;

const logInUser = async () => {
    const user = getAllDetailsForLogin();
    if (!isValidUser(user))
      return alert("כל השדות חובה");
    try {
        const responsePost = await fetch(`api/Users/login?UserName=${user.UserName}&Password=${user.Password}`, {
            method: 'POST',
            headers: {
                'content-Type': 'application/json'
            },

        })
        if (responsePost.status == 400)
            throw new Error(`כל השדות חובה`)
        if (responsePost.status == 204)
            throw new Error(`משתמש לא רשום`)
        if (!responsePost.ok)
            throw new Error(`משהו השתבש, נסה שוב`)

        const dataPost = await responsePost.json();
        sessionStorage.setItem("user", JSON.stringify(dataPost))
        window.location.href = "ShoppingBag.html";
    }
    catch (error) {
        alert(error.message)
    }
}