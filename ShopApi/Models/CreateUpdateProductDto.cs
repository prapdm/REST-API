using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ShopApi.Models
{
    public class CreateUpdateProductDto
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public IFormFile file { get; set; }
        [Required]
        public decimal Price { get; set; }
        public int ShopId { get; set; }
        [Required]
        public string Category { get; set; }
    }
}
