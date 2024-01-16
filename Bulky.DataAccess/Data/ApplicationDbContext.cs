using Bulky.Models.Models;
using Microsoft.EntityFrameworkCore;



namespace Bulky.DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Action", DisplayOrder = 1 },
                new Category { Id = 2, Name = "Scifi", DisplayOrder = 2 },
                new Category { Id = 3, Name = "Drama", DisplayOrder = 3 }
                );
            modelBuilder.Entity<Product>().HasData(
                    new Product
					{
						Id = 1,
						Title = "Fortune of Time",
						Author = "Billy Spark",
						Description = "Present Vitar sodales Libero",
						ISBN = "SWD9999001",
                        ListPrice = 99,
                        Price  = 90,
                        Price50 = 85,
                        Price100 = 80
					}
                );

		}
    }
}
