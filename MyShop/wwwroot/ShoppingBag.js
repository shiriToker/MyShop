const getCartFromSessionStorage = () => {
    const orderList = JSON.parse(sessionStorage.getItem("orderList")) || [];
    return orderList;
}

const calculateCountAndAmount = () => {
    let totalAmount = 0
    const orderList = getCartFromSessionStorage()
    orderList.map(item=>totalAmount+=item.price)
    document.getElementById('totalAmount').innerHTML = totalAmount;
    document.getElementById('itemCount').innerHTML = orderList.length ;

}

const ShowProductsCards = () => {
    clearCartHtml()
    const products = getCartFromSessionStorage();

    if (products) {
        products.forEach(product => {
            ShowOneProduct(product);
        });
    }
}

const ShowOneProduct = (product) => {
    let tmp = document.getElementById('temp-row');
    let cloneProduct = tmp.content.cloneNode(true);
    cloneProduct.querySelector('.image').style.backgroundImage = `url(./images/${product.imgUrl})`;
    cloneProduct.querySelector('.itemName').innerText = product.productName
    cloneProduct.querySelector('.price').textContent = product.price;
    cloneProduct.querySelector('.DeleteButton').addEventListener("click", () => { deleteItem(product); });
    document.querySelector('.cistGroup').appendChild(cloneProduct);
}

const clearCartHtml = () => {
    document.querySelector('.cistGroup').innerHTML = '';

}

const deleteItem = (product) => {
    let orderList = getCartFromSessionStorage()
    for (var i = 0; i < orderList.length; i++) {
        if (orderList[i].productId == product.productId) {
            orderList.splice(i, 1);
            break;
        }    
    }
    sessionStorage.setItem("orderList", JSON.stringify(orderList))
    ShowProductsCards()
    calculateCountAndAmount()
}

const payment =async () => {
    let orderList = getCartFromSessionStorage();
   const newOrder= await createOrder(orderList)
    if (newOrder) {
      sessionStorage.removeItem("orderList");
        clearCartHtml()
        const myOrder = await newOrder.json()
        alert(`הזמנה מספר ${myOrder.orderId} התקבלה בהצלחה`);
         window.location.href = "Products.html";
    }
    
}

const createOrder = async (orderList) => {
    if (!sessionStorage.getItem("user")) {
        window.location.href = "Login.html";
        return false
    }
    let url = `api/Orders`
    const orderPost = createOrderPost(orderList)  
    try {
        const response = await fetch(url
            , {
                method: 'Post',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(orderPost)
            }
        );
        if (response.status === 400) {
            throw new Error('נמצאה פריצה, סכום לא תואם לנתונים')
        }
        else
        return response;
    }
        catch (e) {
        alert(e)
        return null
     }
}

const createOrderPost = (orderList) => {
    return orderPost = {
        orderSum: document.getElementById('totalAmount').textContent,
            userId: JSON.parse(sessionStorage.getItem("user")).userId,
            orderItems: orderList.map(item => { return { productId: item.productId, quantity:1 } })

    }
}

ShowProductsCards();

calculateCountAndAmount()