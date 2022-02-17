using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public int? CreatedBy { get; set; }
        public virtual User User { get; set; }
        public int AddressId { get; set; }

        public virtual Address Address { get; set; }
        public virtual List<Product> Product { get; set; }
    }
}
