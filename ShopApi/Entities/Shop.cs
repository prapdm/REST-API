using System;
using System.Collections.Generic;

namespace ShopApi.Entities
{
    public class Shop
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool HasDelivery { get; set; }
        public string ContactEmail { get; set; }
        public string Phone { get; set; }
        public string Website { get; set; }
        public string Category { get; set; }
        public int? CreatedById { get; set; }
        public virtual User CreatedBy { get; set; }
        public int AddressId { get; set; }

        public virtual Address Address { get; set; }
        public virtual List<Product> Product { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
