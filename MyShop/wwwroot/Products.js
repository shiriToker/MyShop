
const filterCategories = [];

const getFilters = () => {
    const filters = {
        name: document.getElementById('nameSearch').value,
        maxPrice: document.getElementById('maxPrice').value,
        minPrice: document.getElementById('minPrice').value
    }
    return filters;
}

const GetProducts = async () => {
    const filters = getFilters();
    let url = `api/Products?name=${filters.name}&minPrice=${filters.minPrice}&maxPrice=${filters.maxPrice}`
    if (filterCategories.length > 0) 
        for (let i = 0; i < filterCategories.length; i++) {
            url += `&categoryIds=${filterCategories[i]}`
        }  
    try {
        const response = await fetch(url
            , {
                method: 'Get',
                headers: {
                    'content-Type': 'application/json'
                },
                query: { categoryIds: filterCategories }
            }

        );
        const products = await response.json();
      //  console.log(products)
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
        const response = await fetch(`https://localhost:44351/api/Categorys`);
        const categories = await response.json();
        return categories;
    }
    catch (e) {
        alert(e);
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
    cloneProduct.querySelector('img').src = `./pic/${product.imgUrl}`;
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
    console.log(category.categoryId)
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
        filterCategories.push(catregory.categoryId);
    else {
        let index = filterCategories.indexOf(catregory.categoryId)
        filterCategories.splice(index,1)
    }
    console.log(filterCategories)

    ShowProductsCards();
}




loadProducts();

loadCategories();








