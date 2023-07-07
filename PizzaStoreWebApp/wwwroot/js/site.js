{ 
    let url = 'https://localhost:7055/api/v1/pizzas';
    var data = [];
    fetch(url)
        .then(response => response.json())
        .then(data => {
            console.log(data),
            showPizzas(data);
        })
        .catch(ex => {
            alert("error");
            console.error(ex);
        });
    let openOrder = document.querySelector('.shopping');
    let closeOrder = document.querySelector('.closeShopping');
    let body = document.querySelector('body');
    let list = document.querySelector('.list');
    let listCard = document.querySelector('.listCard');
    let total = document.querySelector('.total');
    let quantity = document.querySelector('.quantity');
    openOrder.addEventListener('click', () => {
        body.classList.add('active');
    });
    closeOrder.addEventListener('click', () => {
        body.classList.remove('active');
    })
    let listCards = [];
    
    function showPizzas(data) {
        data.forEach((pizza, key) => {
            console.log(key);
            let baseStr64 = `${pizza.contentImage}`;
            let newPizza = document.createElement('div');
            newPizza.classList.add('item');
            newPizza.innerHTML = `
                <img src="data:image/jpg;base64,${baseStr64}" class="img-fluid img-thumbnail" id="pizzaImg">
                <div class="title" id="pizzaName">${pizza.productName}</div>
                <p class="lead" id="pizza">${pizza.productDescription}</p>
                <div class="" id="pizzaPrice">${pizza.productPrice}</div>
                <label for="crustselect">Select Crust</label>
                        <select class="form-select form-select-lg mb-2" id="crustselect">
                          <option selected>Classic</option>
                          <option value="0">Classic</option>
                          <option value="0">Deep</option>
                          <option value="0">Thin</option>
                          <option value="3.45">Gluten Free</option>
                          <option value="3.45">Cheesy Crust</option>
                        </select>
                <button onclick="addToOrder(${key})">Add To Order</button>`;
            list.appendChild(newPizza);
        })
    }
    showPizzas(data);
function addToOrder(key) {
    if (listCards[key] == null) {
        listCards[key] = JSON.parse(JSON.stringify(data[key]));;
        console.log(listCards);
        listCards.quantity = 1;
    }
    reloadOrder();
}
function reloadOrder() {
    listCard.innerHTML = '';
    let count = 0;
    let totalPrice = 0;
    listCards.forEach((pizza, key) => {
        //let baseStr64 = `${pizza.contentImage}`;
        totalPrice = totalPrice + pizza.productPrice;
        count = count + pizza.quantity;
        total.innerText = totalPrice.toLocaleString();
        quantity.innerText = count;
        if (value != null) {
            let newDiv = document.createElement("li");
            newDiv.innerHTML = `
                //<img src="data:image/jpg;base64,${baseStr64}" class="img-fluid img-thumbnail">
                <div class="title">${pizza.productName}</div>
                <div class="">${pizza.productPrice}</div>
                <div>${pizza.quantity}</div>
                <div>
                    <button onclick="changeQuantity(${key}, ${pizza.quantity - 1})">-</button>
                    <div class="count">${pizza.quantity}</div>
                    <button onclick="changeQuantity(${key}, ${pizza.quantity + 1})">+</button>
                `;
            listCard.appendChild(newDiv);
        }
    })
    total.innerText = totalPrice.toLocaleString();
    quantity.innerText = count;
    }
    function changeQuantity(key, quantity) {
        if (quantity == 0) {
            delete listCards[key];
        } else {
            listCards[key].quantity = quantity;
            listCards[key].productPrice = quantity * pizzas[key].productPrice;
        }
        reloadOrder();
    }
}




