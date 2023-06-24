{ 
    fetch('https://localhost:7055/api/v1/pizzas')
        .then(response => response.json())
        .then(data => {
            console.log(data);
            showPizzas(data);
        })
        .catch(ex => {
            alert("error");
            console.error(ex);
        });
    let openOrder = document.querySelector('.shopping');
    let closeOrder = document.querySelector('.closeShopping');
    let body = document.querySelector('body');
    openOrder.addEventListener('click', () => {
        body.classList.add('active');
    });
    closeOrder.addEventListener('click', () => {
        body.classList.remove('active');
    })
    function showPizzas(pizzas) {
        var pizzaUI = "";
        pizzas.forEach(pizza => {
            console.log(pizza);
            let baseStr64 = `${pizza.contentImage}`;
            
            pizzaUI += `
                        <div class="card text-center" style="margin-top:0.8rem; width:350px">
                            <img src="data:image/jpg;base64,${baseStr64}" class="img-fluid img-thumbnail">
                            <ul id="pizzas-list">
                                <li><h3>${pizza.productName}</h3></li>
                                <li><p class="card-text">${pizza.productDescription}</p></li>
                                <li><h3>$ ${pizza.productPrice} Only</h3></li>
                            </ul>
                         <div class="mt-auto">
                       <label class="form-select-lg mb-2 text-left" for="crustselect">Select Crust</label>
                        <select class="form-select form-select-lg mb-2" id="crustselect">
                          <option selected>Classic</option>
                          <option value="1">Classic</option>
                          <option value="2">Deep</option>
                          <option value="3">Thin</option>
                          <option value="3">Gluten Free</option>
                        </select>
                            <button class="btn btn-lg btn-block btn-success mt-auto" style="margin:0.3rem;">
                                Add To Order
                            </button>
                        </div>
                        </div>`
            document.getElementById("containerpizza").innerHTML = pizzaUI;
        });
        }
    //}
}




