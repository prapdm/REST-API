using System.ComponentModel.DataAnnotations;

namespace ShopApi.Models
{
    public class UpdateShopDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public bool HasDelivery { get; set; }
        [EmailAddress]
        [Required]
        public string ContactEmail { get; set; }
        [MaxLength(200)]
        public string Website { get; set; }
        [Phone]
        [Required]
        public string Phone { get; set; }
        [MaxLength(50)]
        public string City { get; set; }
        [Required]
        [MaxLength(50)]
        public string Street { get; set; }
        [Required]
        [MaxLength(10)]
        public string PostalCode { get; set; }
    }
}
