using System.ComponentModel.DataAnnotations;

namespace TestRestAPI.Models.Entities
{
    public class Item
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(100)]
        public required string Name { get; set; }
        public double Price { get; set; }
        public string? Notes { get; set; }
        public byte[]  Image { get; set; }
        public int CategoryId { get; set; }
        public Category CategoryName { get; set; }

    }
}
