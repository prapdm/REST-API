using System.Collections.Generic;

namespace ShopApi.Models
{
    public class ShopDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string ContactEmail { get; set; }
        public string Phone { get; set; }
        public string Website { get; set; }
        public bool HasDelivery { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
        public List<ProductDto> Products { get; set; }
    }
}
