using System.ComponentModel.DataAnnotations;
using TestRestAPI.Models.Entities;

namespace TestRestAPI.Models
{
    public class mdlItem
    {
     
        [MaxLength(100)]
        public required string Name { get; set; }
        public double Price { get; set; }
        public string? Notes { get; set; }
        public IFormFile Image { get; set; }
        public int CategoryId { get; set; }

    } 
}
