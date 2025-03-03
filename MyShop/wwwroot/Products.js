const arrFilterCategories = [];

const getFilters = () => {
    const filters = {
        name: document.getElementById('nameSearch').value,
        maxPrice: document.getElementById('maxPrice').value,
        minPrice: document.getElementById('minPrice').value
    }
    return filters;
}

const getUrlForGetProducts = async () => {
    const filters = await getFilters();
    const baseUrl = `api/Products?`;
    const queryParams = [];

    if (filters.nameSearch) queryParams.push(`nameSearch=${encodeURIComponent(filters.nameSearch)}`);
    if (filters.minPrice) queryParams.push(`minPrice=${filters.minPrice}`);
    if (filters.maxPrice) queryParams.push(`maxPrice=${filters.maxPrice}`);
    if (arrFilterCategories.length > 0) {
        arrFilterCategories.forEach(categoryId => {
            queryParams.push(`categoryIds=${categoryId}`);
        });
    }

    return baseUrl + queryParams.join("&");
};

const GetProducts = async () => {
    const filters = getFilters();
    let url = await getUrlForGetProducts()  
    try {
        const response = await fetch(url
            , {
                method: 'Get',
                headers: {
                    'content-Type': 'application/json'
                },
                query: { categoryIds: arrFilterCategories }
            }

        );
        const products = await response.json();
        const countProducts = document.getElementById("counter");
        countProducts.innerText = products.length;
        return products;
    }
    catch (e) {
        alert(e);
    }

}

const clearProductsHtml = () => {
    document.getElementById('ProductList').innerHTML = '';
}


const GetCategories = async () => {
    try {
        const response = await fetch(`api/Categorys`);
        const categories = await response.json();
        return categories;
    }
    catch (e) {
        alert(e.message);
    }
}

const ShowProductsCards = async () => {
    clearProductsHtml();
    const products = await GetProducts();  

    if (products) {
        products.forEach(product => {
            ShowOneProduct(product);
        });
    }
}


const ShowCategories = async () => {
    const categories = await GetCategories();
    if (categories) {
        categories.forEach(category => {
            ShowOneCategory(category);
        });
    }
}

const ShowOneProduct = (product) => {
    let tmp = document.getElementById('temp-card');
    let cloneProduct = tmp.content.cloneNode(true);
    cloneProduct.querySelector('img').src = `./images/${product.imgUrl}`;
    cloneProduct.querySelector('h1').textContent = product.productName;
    cloneProduct.querySelector('.price').innerText = product.price;
    cloneProduct.querySelector('.description').innerText = product.description;
    cloneProduct.querySelector('button').addEventListener("click", () => { addToCart(product); });
    document.getElementById('ProductList').appendChild(cloneProduct);
}


const ShowOneCategory = (category) => {
    let tmp = document.getElementById('temp-category');
    let cloneCategory = tmp.content.cloneNode(true);
    cloneCategory.querySelector(".OptionName").innerText = category.categoryName
    cloneCategory.querySelector('.opt').addEventListener("change", (event) => { addFilterCategory(category, event.currentTarget.checked) })
    document.getElementById('categoryList').appendChild(cloneCategory);
}


const loadProducts = () => {

    window.addEventListener('load', () => {
        ShowProductsCards();
    });
}

const loadCategories = () => {

    window.addEventListener('load', () => {
        ShowCategories();
    });
}

const addToCart = (product) => {

    const orderList = JSON.parse( sessionStorage.getItem("orderList"))||[];
    orderList.push(product);
    sessionStorage.setItem("orderList", JSON.stringify(orderList))
    document.getElementById("ItemsCountText").innerText=orderList.length

}

const addFilterCategory = (catregory, event) => {
    if (event)
        arrFilterCategories.push(catregory.categoryId);
    else {
        let index = arrFilterCategories.indexOf(catregory.categoryId)
        arrFilterCategories.splice(index,1)
    }
    ShowProductsCards();
}

const cartCount = () => {
    const orderList = JSON.parse(sessionStorage.getItem("orderList")) || [];
    document.getElementById("ItemsCountText").innerText = orderList.length
}

loadProducts();
loadCategories();
cartCount();