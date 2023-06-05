using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PizzaStoreWebApi.Models
{
    public class PizzaQueryParameters
    {
        public decimal? MinPrice { get; set; }  
        public decimal? MaxPrice { get; set; } 
        public string Description { get; set; } = String.Empty;
        public string Name { get; set; } = String.Empty;
        public string SearchTerm { get; set; } = String.Empty;

        const int MaxSize = 100;
        private int _pageSize = 50;
        public int? Page { get; set; } 

        public int Size
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = Math.Min(_pageSize, value);
            }
        }
        public string sortBy { get; set; } = "ProductPrice";
        private string sortOrder = "asc";

        public string SortOrder
        {
            get
            {
                return sortOrder;
            }
            set
            {
                if (value == "asc" || value == "desc")
                {
                    sortOrder = value;
                }
            }
        }
    }
}
