
using Microsoft.EntityFrameworkCore;
using ProductSupplierMVC.Models;

namespace ProductSupplierMVC.Data
{
    public static class DbInitializer
    {
        public static async Task SeedAsync(
            AppDbContext context)
        {
            if (await context.Suppliers.AnyAsync())
                return;

            context.Suppliers.AddRange(
                new Supplier
                {
                    Name = "Samsung",
                    Email = "contact@samsung.example",
                    Phone = "0123456789",
                    Address = "Hà Nội"
                },
                new Supplier
                {
                    Name = "Apple",
                    Email = "contact@apple.example",
                    Phone = "0987654321",
                    Address = "TP. Hồ Chí Minh"
                },
                new Supplier
                {
                    Name = "Dell",
                    Email = "contact@dell.example",
                    Phone = "0912345678",
                    Address = "Đà Nẵng"
                }
            );

            await context.SaveChangesAsync();
        }
    }
}
