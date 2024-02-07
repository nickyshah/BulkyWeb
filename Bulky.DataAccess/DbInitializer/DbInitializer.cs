using Bulky.DataAccess.Data;
using Microsoft.AspNetCore.Identity;

namespace Bulky.DataAccess.DbInitializer
{
    internal class DbInitializer : IDbInitializer
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _db;

        public DbInitializer(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext db)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _db = db;
        }

        public void Initialize()
        {



            //Migrations if they are not applied


            // Create roles if they are not created


            // If roles are not created, then we will create admin user as well


        }
    }
}
