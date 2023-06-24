using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using PizzaStoreWebApi.Models;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Driver.Linq;
using WebAPIProject.Models;

namespace PizzaStoreWebApi.Services
{
    public class PizzaServices : IPizzaServices
    {
        public readonly IMongoCollection<PizzasDetails> pizzaCollection;

        public PizzaServices(IOptions<PizzaStoreDatabaseSettings> pizzaStoreDatabaseSettings)
        {
            var mongoClient = new MongoClient(pizzaStoreDatabaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(pizzaStoreDatabaseSettings.Value.DatabaseName);
            pizzaCollection = mongoDatabase.GetCollection<PizzasDetails>(pizzaStoreDatabaseSettings.Value.PizzaCollectionName);
        }
        [MapToApiVersion("1.0")]
        //[Route("api/v{version:apiVersion}/[controller]")]
        //[Route("pizzas")]
        public async Task<List<PizzasDetails>> PizzasListAsync(PizzaQueryParameters queryParameters)
        {
            PizzasDetails newpizza = new PizzasDetails();
            IMongoQueryable<PizzasDetails> pizzas = pizzaCollection.AsQueryable().Where(x=>x.IsProductAvailable==true);
            
            if (queryParameters.MinPrice != null)
            {
                pizzas = pizzas.Where(x => x.ProductPrice >= queryParameters.MinPrice.Value);
            }
            if (queryParameters.MaxPrice != null)
            {
                pizzas = pizzas.Where(p => p.ProductPrice <= queryParameters.MaxPrice.Value);
            }
            if (!string.IsNullOrEmpty(queryParameters.SearchTerm))
            {
                pizzas = pizzas.Where(
                    p => p.ProductDescription.ToLower().Contains(queryParameters.SearchTerm.ToLower()) ||
                    p.ProductName.ToLower().Contains(queryParameters.SearchTerm.ToLower()));
            }
            if (!string.IsNullOrEmpty(queryParameters.Name))
            {
                pizzas = pizzas.Where(
                    p => p.ProductName.ToLower().Contains(
                        queryParameters.Name.ToLower()));
            }
            if (!string.IsNullOrEmpty(queryParameters.Description))
            {
                pizzas = pizzas.Where(
                    p => p.ProductDescription.ToLower().Contains(queryParameters.Description.ToLower()));
            }
            if (!string.IsNullOrEmpty(queryParameters.sortBy))
            {
                if (typeof(PizzasDetails).GetProperty(queryParameters.sortBy) != null)
                {
                    pizzas = pizzas.OrderByCustom(queryParameters.sortBy, queryParameters.SortOrder);
                }
            }
            int Page = queryParameters.Page.GetValueOrDefault(1) == 0 ? 1 : queryParameters.Page.GetValueOrDefault(1);
            
            pizzas = pizzas.Skip((Page - 1) * queryParameters.Size).Take(queryParameters.Size);

            return await (await pizzas.ToCursorAsync()).ToListAsync();
        }
        public async Task<PizzasDetails> GetPizzaDetailByIdAsync([FromQuery]string productId)
        {
            return await pizzaCollection.Find(x => x.ProductId == productId).FirstOrDefaultAsync();
        }
        public async Task<PizzaResponse> AddPizzaAsync(PizzasDetails productDetails)
        {
            PizzaResponse response = new PizzaResponse();//get the response of the insert method
            byte[] binaryContent = File.ReadAllBytes(@"E:\Diploma\MVC and Non Relational Databases\Assessment_2\PizzaStoreWebApi\Images\" + productDetails.File.FileName.ToString());
            productDetails.ContentImage = binaryContent;
            response.IsSuccess = true;
            response.Message = "Pizza has been inserted successfully";
            try
            {
                await pizzaCollection.InsertOneAsync(productDetails);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Error Occured " + ex.Message;
            }
            return response;
        }
        public async Task UpdatePizzaAsync(string productId, PizzasDetails productDetails)
        {
            await pizzaCollection.ReplaceOneAsync(x => x.ProductId == productId, productDetails);
        }
        public async Task DeletePizzasAsync(string productId)
        {
            await pizzaCollection.DeleteOneAsync(x => x.ProductId == productId);
        }
        ////Version 2 Services

    }
}
