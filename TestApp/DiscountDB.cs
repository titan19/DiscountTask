using System.Data.Entity;
using TestApp.Models;

namespace TestApp
{
    public class DiscountDB : DbContext
    {
        public DbSet<Multiplier> Multipliers { get; set; }
        public DbSet<Discount> Discounts { get; set; }
    }
}
