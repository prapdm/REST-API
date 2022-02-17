using Microsoft.EntityFrameworkCore;

namespace ShopApi.Entities
{
    public class ShopDBContext : DbContext
    {
        public ShopDBContext(DbContextOptions<ShopDBContext> options) : base(options)
        {

        }
 



    }
}
