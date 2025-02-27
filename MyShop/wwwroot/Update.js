
const editValueOfUpdatePage = () => {
    const userName = document.querySelector("#UserNameUpdate")
    const password = document.querySelector("#PasswordUpdate")
    const firstName = document.querySelector("#FirstName")
    const lastName = document.querySelector("#LastName")
    const currentUser = JSON.parse(sessionStorage.getItem("user"))
    userName.value = currentUser.userName
    password.value = currentUser.password
    firstName.value = currentUser.firstName
    lastName.value = currentUser.lastName
}
editValueOfUpdatePage()

const getDetailsUpdate = () => {
    return newUser = {
        UserName: document.querySelector("#UserNameUpdate").value,
        FirstName: document.querySelector("#FirstName").value,
        LastName: document.querySelector("#LastName").value,
        Password: document.querySelector("#PasswordUpdate").value
    }
}
const checkPassword = async () => {
    let password = document.querySelector("#PasswordUpdate").value
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

const updateUser = async () => {
    const updateUser = getDetailsUpdate();
    try {
      await checkPassword();
        const currentUser = JSON.parse(sessionStorage.getItem("user"))
        const responsePut = await fetch(`https://localhost:44351/api/Users/${currentUser.userId}`, {
            method: 'PUT',
            headers: {
                'content-Type': 'application/json'
            },
            body: JSON.stringify(updateUser)
        })
        if (!responsePut.ok)
            throw new Error(`HTTP error! status ${responsePut.status}`)

        if (responsePut.status == 200) {
            sessionStorage.setItem("user", JSON.stringify(await responsePut.json()));
            alert(`פרטי משתמש ${currentUser.userId} עודכנו בהצלחה!`)
            window.location.href = "Products.html";
        }
           
    }
    catch (error) {
        alert("מצטערים משהו השתשב נסה שוב...\nהשגיאה:" + error)
    }


}