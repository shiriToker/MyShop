const getInputValue = (selector) => document.querySelector(selector)?.value.trim() || "";

const setUpdatePageValues = () => {
    const currentUser = JSON.parse(sessionStorage.getItem("user"));
    if (!currentUser) return;

    document.querySelector("#UserNameUpdate").value = currentUser.userName.trim();
    document.querySelector("#PasswordUpdate").value = currentUser.password.trim();
    document.querySelector("#FirstName").value = currentUser.firstName.trim();
    document.querySelector("#LastName").value = currentUser.lastName.trim();
};

const getUpdatedUserDetails = () => {
    const newUser = {
        UserName: getInputValue("#UserNameUpdate"),
        Password: getInputValue("#PasswordUpdate"),
        FirstName: getInputValue("#FirstName"),
        LastName: getInputValue("#LastName"),
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
const checkPassword = async () => {
    let password = document.querySelector("#PasswordUpdate").value
    let result = document.querySelector("#CheckPassword")
    try {
        const responseCheckPassword = await fetch(`api/Users/password/?password=${password}`, {
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

const checkIfExistUser = () => {
    const currentUser = JSON.parse(sessionStorage.getItem("user"));
    if (!currentUser) {
        alert("שגיאה: אין משתמש מחובר.");
        window.location.href = "Products.html";
    }
}

const updateUser = async () => {

    const userToUpdate = getUpdatedUserDetails();
    if (!userToUpdate) return;

    try {
      await checkPassword();
        const currentUser = JSON.parse(sessionStorage.getItem("user"))
        const responsePut = await fetch(`api/Users/${currentUser.userId}`, {
            method: 'PUT',
            headers: {
                'content-Type': 'application/json'
            },
            body: JSON.stringify(userToUpdate)
        })
        if (responsePut.status === 400) throw new Error("!כל השדות חובה, בדוק את תקינותם");
        if (responsePut.status === 409) throw new Error("שם משתמש כבר קיים");
        if (!responsePut.ok) throw new Error("משהו השתבש, נסה שוב");
      
        if (responsePut.status == 200) {
            sessionStorage.setItem("user", JSON.stringify(await responsePut.json()));
            alert(`פרטי משתמש ${currentUser.userId} עודכנו בהצלחה!`)
            window.location.href = "Products.html";
        }          
    }
    catch (error) {
        alert(error.message)
    }
}
setUpdatePageValues()
checkIfExistUser()