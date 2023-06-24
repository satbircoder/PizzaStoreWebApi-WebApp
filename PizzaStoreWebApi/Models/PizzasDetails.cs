using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace PizzaStoreWebApi.Models
{
    public class PizzasDetails
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? ProductId { get; set; }

        [BsonElement("ProductName")]
        [Required]
        public string? ProductName { get; set; }

        [BsonElement("ProductDescription")]
        [Required]
        public string? ProductDescription { get; set; }

        [BsonElement("ProductPrice")]
        [BsonRepresentation(BsonType.Decimal128)]
        [Required]
        public decimal ProductPrice { get; set; }

        [BsonElement("Category")]
        [Required]
        public string? Category { get; set; }

        [BsonElement("IsProductAvailable")]
        [Required]
        public bool IsProductAvailable { get; set; }

        [BsonIgnore]
        public IFormFile? File { get; set; }
        [BsonElement("ContentImage")]
        public byte[]? ContentImage { get; set; }



    }

    public class PizzaResponse //Class to get the response of webapi
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
