using PizzaStoreWebApi.Models;

namespace PizzaStoreWebApi.Services
{
    public interface IPizzaServicesV2
    {
        public Task<List<PizzasDetails>> PizzasListAsync(PizzaQueryParameters queryParameters);
        public Task<PizzasDetails> GetPizzaDetailByIdAsync(string productId);
        public Task<PizzaResponse> AddPizzaAsync(PizzasDetails productDetails);
        public Task UpdatePizzaAsync(string productId, PizzasDetails productDetails);
        public Task DeletePizzasAsync(String productId);
    }
}
