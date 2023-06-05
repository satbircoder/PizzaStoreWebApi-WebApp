using Microsoft.AspNetCore.Mvc;
using PizzaStoreWebApi.Models;
using PizzaStoreWebApi.Services;

namespace PizzaStoreWebApi.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    //[ApiVersion("2.0")]
    //[Route("api/v{version:apiVersion}/[controller]")]
    [Route("pizzas")]
    
    public class PizzaController : ControllerBase
    {
        private readonly IPizzaServices _pizzaServices;
        public PizzaController(IPizzaServices pizzaServices) 
        { 
            _pizzaServices= pizzaServices;
        }
        [HttpGet]//getting all the pizzas in database
        public async Task<List<PizzasDetails>> GetAllPizzas([FromQuery] PizzaQueryParameters queryParameters)
        {
            
            return await _pizzaServices.PizzasListAsync(queryParameters);
        }
        [HttpGet("{pizzaId:length(24)}")]
        public async Task<ActionResult<PizzasDetails>> GetPizzasById(string pizzaId)
        {
            var productDetails = await _pizzaServices.GetPizzaDetailByIdAsync(pizzaId);
            if (productDetails is null)
            {
                return NotFound();
            }
            return productDetails;
        }
       
        [HttpPost]
        public async Task<IActionResult> PostPizza([FromForm]PizzasDetails pizzasDetails)
        {
            PizzaResponse response = new PizzaResponse();
            
            try
            {
             await _pizzaServices.AddPizzaAsync(pizzasDetails);
            return CreatedAtAction(nameof(GetAllPizzas), new
            {
               id = pizzasDetails.ProductId
            }, pizzasDetails) ;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
                return Ok(response);
            }
            
        }

        [HttpPut("{pizzaId:length(24)}")]
        public async Task<IActionResult> UpdatePizza(string pizzaId, PizzasDetails pizzasDetails)
        {
            var productDetail = await _pizzaServices.GetPizzaDetailByIdAsync(pizzaId);
            if(productDetail is null)
            {
                return NotFound();
            }
            pizzasDetails.ProductId = productDetail.ProductId;
            await _pizzaServices.UpdatePizzaAsync(pizzaId, pizzasDetails);
            return Ok();
        }
        [HttpDelete("{pizzaId:length(24)}")]
        public async Task<IActionResult> DeletePizza(string pizzaId)
        {
            var productDeatils = await _pizzaServices.GetPizzaDetailByIdAsync(pizzaId);
            if(productDeatils is null)
            {
                return NotFound();
            }
            await _pizzaServices.DeletePizzasAsync(pizzaId);
            return Ok();
        }
        
    }

    //[ApiVersion("2.0")]
    //[Route("Pizzas")]
    //[ApiController]
    //public class PizzaV2Controller : ControllerBase
    //{
    //    private readonly IPizzaServices _pizzaServices;
    //    public PizzaV2Controller(IPizzaServices pizzaServices)
    //    {
    //        _pizzaServices = pizzaServices;
    //    }
    //    [HttpGet]//getting all the pizzas in database
    //    public async Task<List<PizzasDetails>> GetAllPizzas([FromQuery] PizzaQueryParameters queryParameters)
    //    {
    //        return await _pizzaServices.PizzasListAsync(queryParameters);
    //    }
    //    [HttpGet("{pizzaId:length(24)}")]
    //    public async Task<ActionResult<PizzasDetails>> GetPizzasById(string pizzaId)
    //    {
    //        var productDetails = await _pizzaServices.GetPizzaDetailByIdAsync(pizzaId);
    //        if (productDetails is null)
    //        {
    //            return NotFound();
    //        }
    //        return productDetails;
    //    }

    //    [HttpPost]
    //    public async Task<IActionResult> PostPizza(PizzasDetails pizzasDetails)
    //    {
    //        PizzaResponse response = new PizzaResponse();

    //        try
    //        {
    //            await _pizzaServices.AddPizzaAsync(pizzasDetails);
    //            return CreatedAtAction(nameof(GetAllPizzas), new
    //            {
    //                id = pizzasDetails.ProductId
    //            }, pizzasDetails);
    //        }
    //        catch (Exception ex)
    //        {
    //            response.IsSuccess = false;
    //            response.Message = ex.Message;
    //            return Ok(response);
    //        }

    //    }

    //    [HttpPut("{pizzaId:length(24)}")]
    //    public async Task<IActionResult> UpdatePizza(string pizzaId, PizzasDetails pizzasDetails)
    //    {
    //        var productDetail = await _pizzaServices.GetPizzaDetailByIdAsync(pizzaId);
    //        if (productDetail is null)
    //        {
    //            return NotFound();
    //        }
    //        pizzasDetails.ProductId = productDetail.ProductId;
    //        await _pizzaServices.UpdatePizzaAsync(pizzaId, pizzasDetails);
    //        return Ok();
    //    }
    //    [HttpDelete("{pizzaId:length(24)}")]
    //    public async Task<IActionResult> DeletePizza(string pizzaId)
    //    {
    //        var productDeatils = await _pizzaServices.GetPizzaDetailByIdAsync(pizzaId);
    //        if (productDeatils is null)
    //        {
    //            return NotFound();
    //        }
    //        await _pizzaServices.DeletePizzasAsync(pizzaId);
    //        return Ok();
    //    }
    //}
}
