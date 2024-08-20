using Microsoft.EntityFrameworkCore;
using VShopSchool.CouponAPI.Models;

namespace VShopSchool.CouponAPI.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Coupon> Coupons { get; set; }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            base.OnModelCreating(mb);

            mb.Entity<Coupon>().HasData(new Coupon
            {
                CouponId = 1,
                CouponCode = "VSHOP_Promo_10",
                Discount = 10
            });
            mb.Entity<Coupon>().HasData(new Coupon
            {
                CouponId = 2,
                CouponCode = "VSHOP_Promo_20",
                Discount = 20
            });
        }


    }
}
